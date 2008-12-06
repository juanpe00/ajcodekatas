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
Public Class LessPrimitive
    Inherits Primitive

    Private Shared mInstance As New LessPrimitive()

    Public Const Value As String = "<"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As LessPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        Dim leftparameter As PrologObject
        Dim rightparameter As PrologObject

        leftparameter = pars(0).Evaluate(pm)
        rightparameter = pars(1).Evaluate(pm)

        If IsVariable(leftparameter) OrElse IsVariable(rightparameter) Then
            Throw New Exception("Parámetro libre")
        End If

        If Not IsInteger(leftparameter) OrElse Not IsInteger(leftparameter) Then
            Throw New Exception("Se esperaba Entero")
        End If

        Return leftparameter.ToObject < rightparameter.ToObject
    End Function

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New PrimitiveStatusNode(pm, Me)
    End Function
End Class
