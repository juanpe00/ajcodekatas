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
Public Module Utilities
    Function Evaluate(ByVal pars As PrologObject()) As PrologObject()
        If pars Is Nothing Then
            Return Nothing
        End If

        If pars.Length = 0 Then
            Return Nothing
        End If

        Dim result() As PrologObject

        result = Array.CreateInstance(GetType(PrologObject), pars.Length)

        Dim np As Integer

        For np = 0 To pars.Length - 1
            result(np) = pars(np).Dereference
        Next

        Return result
    End Function

    Function IsVariableName(ByVal name As String) As Boolean
        If name Is Nothing OrElse name.Length = 0 Then
            Return False
        End If

        If Char.IsUpper(name.Chars(0)) Then
            Return True
        End If

        If name.Chars(0) = "_"c Then
            Return True
        End If

        Return False
    End Function

    Function ToPrologObject(ByVal obj As Object) As PrologObject
        If TypeOf obj Is String Then
            Dim txt As String = DirectCast(obj, String)

            Dim po As PrologObject

            po = Primitives.GetPrimitive(obj)

            If po Is Nothing Then
                Return New StringObject(obj)
            End If

            Return po
        End If

        If TypeOf obj Is Integer Then
            Return New IntegerObject(DirectCast(obj, Integer))
        End If

        If TypeOf obj Is PrologObject Then
            Return DirectCast(obj, PrologObject)
        End If

        If obj Is Nothing Then
            Return Nothing
        End If

        Throw New ArgumentException("Objecto ilegal " & obj.ToString)
    End Function

    Function IsAtom(ByVal po As PrologObject) As Boolean
        If TypeOf po Is StringObject Then
            Return True
        End If

        Return False
    End Function

    Function IsInteger(ByVal po As PrologObject) As Boolean
        If TypeOf po Is IntegerObject Then
            Return True
        End If

        Return False
    End Function

    Function IsAtomic(ByVal po As PrologObject) As Boolean
        If IsAtom(po) OrElse IsInteger(po) Then
            Return True
        End If

        Return False
    End Function

    Function IsVariable(ByVal po As PrologObject) As Boolean
        If TypeOf po Is Variable Then
            Return True
        End If

        Return False
    End Function
End Module
