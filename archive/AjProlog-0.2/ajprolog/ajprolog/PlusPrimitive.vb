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
Public Class PlusPrimitive
    Inherits Primitive

    Private Shared mInstance As New PlusPrimitive()

    Public Const Value As String = "+"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As PlusPrimitive
        Return mInstance
    End Function

    Public Overloads Overrides Function Evaluate(ByVal pm As PrologMachine, ByVal st As StructureObject) As PrologObject
        Dim leftparameter As PrologObject
        Dim rightparameter As PrologObject

        leftparameter = st.Parameters(0).Evaluate(pm)
        rightparameter = st.Parameters(1).Evaluate(pm)

        If IsVariable(leftparameter) OrElse IsVariable(rightparameter) Then
            Throw New Exception("Parámetro libre")
        End If

        If Not IsInteger(leftparameter) OrElse Not IsInteger(leftparameter) Then
            Throw New Exception("Se esperaba Entero")
        End If

        Return ToPrologObject(leftparameter.ToObject + rightparameter.ToObject)
    End Function
End Class
