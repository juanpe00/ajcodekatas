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
Public Class IntegerPrimitive
    Inherits Primitive

    Private Shared mInstance As New IntegerPrimitive()

    Public Const Value As String = "integer"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As IntegerPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If pars Is Nothing OrElse pars.Length <> 1 Then
            Throw New ArgumentException("Integer espera un parámetro")
        End If

        Return TypeOf pars(0) Is IntegerObject
    End Function
End Class
