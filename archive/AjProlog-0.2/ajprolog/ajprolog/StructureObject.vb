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
Imports System.Text

Public Class StructureObject
    Inherits PrologObject

    Private mFunctor As PrologObject
    Private mParameters As PrologObject()

    Public Sub New(ByVal functor As Object, ByVal ParamArray parameters() As Object)
        Me.New(ToPrologObject(functor), parameters)
    End Sub

    Sub New(ByVal st As StructureObject, ByVal pm As PrologMachine, ByVal vars As ArrayList, ByVal offset As Integer)
        mFunctor = pm.AdjustVariables(st.Functor, vars, offset)

        If st.Arity = 0 Then
            mParameters = Nothing
        Else
            mParameters = Array.CreateInstance(GetType(PrologObject), st.Arity)
        End If

        Dim np As Integer

        For np = 0 To st.Arity - 1
            mParameters(np) = pm.AdjustVariables(st.Parameters(np), vars, offset)
        Next
    End Sub

    Public Sub New(ByVal functor As PrologObject, ByVal ParamArray parameters() As Object)
        If functor Is Nothing Then
            Throw New ArgumentNullException("functor")
        End If

        mFunctor = functor

        If parameters Is Nothing OrElse parameters.Length = 0 Then
            mParameters = Nothing
            Return
        Else
            mParameters = Array.CreateInstance(GetType(PrologObject), parameters.Length)
        End If

        Dim np As Integer

        For np = 0 To parameters.Length - 1
            mParameters(np) = ToPrologObject(parameters(np))
        Next
    End Sub

    Function Normalize() As PrologObject
        If Arity = 0 Then
            Return Functor
        End If

        Return Me
    End Function

    Public ReadOnly Property Functor() As PrologObject
        Get
            Return mFunctor
        End Get
    End Property

    Public ReadOnly Property Arity() As Integer
        Get
            If mParameters Is Nothing Then
                Return 0
            End If
            Return mParameters.Length
        End Get
    End Property

    Public ReadOnly Property Parameters() As PrologObject()
        Get
            Return mParameters
        End Get
    End Property

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim objMe As PrologObject

        objMe = Normalize()

        If Not objMe Is Me Then
            Return objMe.Equals(obj)
        End If

        If TypeOf obj Is StructureObject Then
            obj = DirectCast(obj, StructureObject).Normalize
        End If

        If obj Is Nothing OrElse Not obj.GetType() Is Me.GetType Then
            Return False
        End If

        Dim st As StructureObject = DirectCast(obj, StructureObject)

        If Not Functor.Equals(st.Functor) Then
            Return False
        End If

        If Not Arity = st.Arity Then
            Return False
        End If

        Dim k

        For k = 0 To Arity - 1
            If Not Parameters(k).Equals(st.Parameters(k)) Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim objMe As PrologObject

        objMe = Normalize()

        If Not objMe Is Me Then
            Return objMe.GetHashCode
        End If

        Dim hc As Integer = Functor.GetHashCode Xor Arity

        Dim p As PrologObject

        For Each p In mParameters
            hc = hc Xor p.GetHashCode
        Next

        Return hc
    End Function

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        If TypeOf Functor Is Primitive Then
            Return New PrimitiveNode(pm, Me)
        End If
        Return New FactNode(Me, pm)
    End Function

    Public Overrides Function Evaluate(ByVal pm As PrologMachine) As PrologObject
        If TypeOf Functor Is Primitive Then
            Return DirectCast(Functor, Primitive).Evaluate(pm, Me)
        End If
        Return Me
    End Function

    Public Overrides Function ToString() As String
        If TypeOf Functor Is Primitive Then
            Return DirectCast(Functor, Primitive).ToString(Parameters)
        End If

        If Arity = 0 Then
            Return Functor.ToString
        End If

        Dim sb As New StringBuilder(Parameters.Length * 20)

        sb.Append(Functor.ToString)

        sb.Append("(")

        Dim np As Integer

        For np = 0 To Parameters.Length - 1
            If Parameters(np) Is Nothing Then
                sb.Append("nil")
            Else
                sb.Append(Parameters(np).ToString)
            End If
            If np < Parameters.Length - 1 Then
                sb.Append(",")
            End If
        Next

        sb.Append(")")

        Return sb.ToString
    End Function

    Public Overrides Function ToDisplayString() As String
        Return MyClass.ToString()
    End Function
End Class
