' +---------------------------------------------------------------------+
' | ajprolog - A Prolog Interpreter                                     |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2004 Angel J. Lopez. All rights reserved.        |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajprolog Software License,       |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://www.ajlopez.net/ajprolog/license.txt.                     |
' +---------------------------------------------------------------------+
'
'
Public Class Compiler
    Private mParser As Parser

    Sub New(ByVal parser As Parser)
        mParser = parser
    End Sub

    Sub New(ByVal expr As String)
        Me.New(New Parser(expr))
    End Sub

    Function CompileArguments() As PrologObject()
        Dim la As New ArrayList()
        Dim arg As PrologObject
        Dim token As token

        arg = CompileArgumentExpression()

        While Not arg Is Nothing
            la.Add(arg)

            token = mParser.NextToken

            If token Is Nothing OrElse token.Type <> TokenType.TokPuntuation Then
                Throw New CompilerException("Se esperaba argumento")
            End If

            If token.Value = ")" Then
                mParser.PushToken(token)
                Exit While
            End If

            If token.Value <> "," Then
                Throw New CompilerException("Se esperaba argumento")
            End If

            arg = CompileArgumentExpression()
        End While

        token = mParser.NextToken

        If token Is Nothing OrElse token.Type <> TokenType.TokPuntuation OrElse token.Value <> ")" Then
            Throw New CompilerException("Se esperaba )")
        End If

        Return la.ToArray(GetType(PrologObject))
    End Function

    Function CompileList() As PrologObject
        Dim expr As PrologObject
        Dim token As token

        expr = CompileArgumentExpression()

        If Not expr Is Nothing Then
            token = mParser.NextToken

            If token.Type = TokenType.TokPuntuation AndAlso token.Value = "]" Then
                Return New ListObject(expr, NilList.GetInstance)
            End If

            If token.Value = "," Then
                Return New ListObject(expr, CompileList())
            End If

            If token.Value <> "|" Then
                Throw New CompilerException("Se esperaba elemento de lista")
            End If

            expr = New ListObject(expr, CompileExpression())
        Else
            expr = NilList.GetInstance
        End If

        token = mParser.NextToken

        If token.Value <> "]" Then
            Throw New CompilerException("Se esperaba elemento de lista")
        End If

        Return expr
    End Function

    ' Compila Termino

    Function CompileTerm() As PrologObject
        Dim token As token
        Dim node As PrologObject

        token = mParser.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type = TokenType.TokInteger Then
            Return New IntegerObject(CInt(token.Value))
        End If

        If token.Type = TokenType.TokPuntuation AndAlso token.Value = "[" Then
            Return CompileList()
        End If

        If token.Type = TokenType.TokPuntuation AndAlso token.Value = "(" Then
            node = CompileExpression()
            token = mParser.NextToken

            If token Is Nothing OrElse token.Value <> ")" Then
                Throw New Exception("Se esperaba )")
            End If

            Return node
        End If

        If token.Type = TokenType.TokAtom Then
            node = Primitives.GetPrimitive(token.Value)
            If node Is Nothing Then
                node = New StringObject(token.Value)
            End If
        Else
            mParser.PushToken(token)
            Return Nothing
        End If

        token = mParser.NextToken

        If token Is Nothing Then
            Return node
        End If

        If token.Type <> TokenType.TokPuntuation Then
            mParser.PushToken(token)
            Return node
        End If

        If token.Value = "(" Then
            Return New StructureObject(node, CompileArguments())
        End If

        mParser.PushToken(token)

        Return node
    End Function

    Function CompileBinaryExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        node = CompileTerm()

        If node Is Nothing Then
            Return Nothing
        End If

        token = mParser.NextToken()

        If Not token Is Nothing AndAlso token.Value = "=" Then
            Return New StructureObject(EqualPrimitive.GetInstance, node, CompileBinaryExpression())
        End If

        If Not token Is Nothing AndAlso token.Value = "\=" Then
            Return New StructureObject(NotEqualPrimitive.GetInstance, node, CompileBinaryExpression())
        End If

        If Not token Is Nothing AndAlso token.Value = "<" Then
            Return New StructureObject(LessPrimitive.GetInstance, node, CompileBinaryExpression())
        End If

        If Not token Is Nothing AndAlso token.Value = "+" Then
            Return New StructureObject(PlusPrimitive.GetInstance, node, CompileBinaryExpression())
        End If

        If Not token Is Nothing AndAlso token.Value = "-" Then
            Return New StructureObject(MinusPrimitive.GetInstance, node, CompileTerm())
        End If

        If Not token Is Nothing AndAlso token.Value = "is" Then
            Return New StructureObject(IsPrimitive.GetInstance, node, CompileBinaryExpression())
        End If

        mParser.PushToken(token)

        Return node
    End Function

    Function CompileAndExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        node = CompileBinaryExpression()

        If node Is Nothing Then
            Return Nothing
        End If

        token = mParser.NextToken()

        If token Is Nothing OrElse token.Value <> AndPrimitive.Value Then
            mParser.PushToken(token)
            Return node
        End If

        Dim node2 As PrologObject

        node2 = CompileAndExpression()

        Return New StructureObject(AndPrimitive.GetInstance, node, node2)
    End Function

    Function CompileTopBinaryExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        node = CompileBinaryExpression()

        If node Is Nothing Then
            Return Nothing
        End If

        token = mParser.NextToken()

        If token Is Nothing OrElse (token.Value <> ":-" And token.Value <> ",") Then
            mParser.PushToken(token)
            Return node
        End If

        Dim op As String = token.Value

        Dim node2 As PrologObject

        node2 = CompileAndExpression()

        If op = "," Then
            Return New StructureObject(AndPrimitive.GetInstance, node, node2)
        Else
            Return New StructureObject(IfPrimitive.GetInstance, node, node2)
        End If
    End Function

    Function CompileArgumentExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        node = CompileBinaryExpression()

        If node Is Nothing Then
            Return Nothing
        End If

        token = mParser.NextToken()

        If token Is Nothing OrElse token.Type <> TokenType.TokAtom OrElse token.Value <> ":-" Then
            mParser.PushToken(token)
            Return node
        End If

        Dim op As String = token.Value

        Dim node2 As PrologObject

        node2 = CompileBinaryExpression()

        Return New StructureObject(IfPrimitive.GetInstance, node, node2)
    End Function

    Function CompileUnaryExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        token = mParser.NextToken()

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type <> TokenType.TokAtom OrElse token.Value <> QueryPrimitive.Value Then
            mParser.PushToken(token)
            Return CompileTopBinaryExpression()
        End If

        node = CompileTopBinaryExpression()

        If node Is Nothing Then
            Throw New CompilerException("Se experaba expresión")
        End If

        Return New StructureObject(QueryPrimitive.GetInstance, node)
    End Function

    Function CompileExpression() As PrologObject
        Dim token As token
        Dim node As PrologObject

        node = CompileUnaryExpression()

        If node Is Nothing Then
            Return Nothing
        End If

        Return node

        'Do
        '    token = mParser.NextToken

        '    If token Is Nothing Then
        '        Return node
        '    End If

        '    If token.Type <> TokenType.TokOperator Then
        '        mParser.PushToken(token)
        '        Return node
        '    End If

        '    Select Case token.Value
        '        Case "=", "<", ">", "<=", ">=", "<>"
        '            node = New BinaryOperatorNode(token.Value, node, CompileTerm())
        '        Case Else
        '            mParser.PushToken(token)
        '            Return node
        '    End Select
        'Loop
    End Function

    Function Compile() As PrologObject
        Dim node As PrologObject

        node = CompileExpression()

        If Not mParser.NextToken() Is Nothing Then
            Throw New CompilerException("Se esperaba fin de expresión")
        End If

        Return node
    End Function

    Function CompileCommand() As PrologObject
        Dim node As PrologObject

        node = CompileExpression()

        If node Is Nothing Then
            Return Nothing
        End If

        Dim tok As Token

        tok = mParser.NextToken

        If tok Is Nothing OrElse tok.Type <> TokenType.TokPuntuation OrElse tok.Value <> "." Then
            Throw New CompilerException("Se esperaba .")
        End If

        If TypeOf node Is StringObject Then
            If DirectCast(node, StringObject).ToString = "exit" Then
                Return Nothing
            End If
        End If
        Return node
    End Function

    Sub CompileProgram(ByVal pm As PrologMachine)
        Dim node As PrologObject

        While True
            node = CompileCommand()

            If node Is Nothing Then
                Return
            End If

            If TypeOf node Is StructureObject AndAlso DirectCast(node, StructureObject).Functor Is QueryPrimitive.GetInstance Then
                pm.Resolve(DirectCast(node, StructureObject).Parameters(0))
                If pm.Interactive And mParser.Reader Is Console.In Then
                    mParser.Flush()
                End If
            Else
                pm.Assertz(node)
            End If
        End While
    End Sub

    Function CompileIdentifier() As String
        Dim token As token = mParser.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If Not token.Type = TokenType.TokAtom Then
            Throw New CompilerException("Se esperaba identificador")
        End If

        Return token.Value
    End Function
End Class

Public Class CompilerException
    Inherits Exception

    Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class

