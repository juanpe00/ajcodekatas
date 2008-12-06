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
Public Class TabPrimitive
    Inherits Primitive

    Private Shared mInstance As New TabPrimitive()

    Public Const Value As String = "tab"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As TabPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If pars Is Nothing OrElse pars.Length <> 1 Then
            Throw New ArgumentException("Tab debe recibir un parámetro")
        End If

        If Not TypeOf pars(0) Is IntegerObject Then
            Throw New ArgumentException("Tab debe recibir un entero")
        End If

        Dim nt As Integer = DirectCast(pars(0), IntegerObject).GetValue

        While nt > 0
            pm.Output.Write(" ")
            nt -= 1
        End While

        Return True
    End Function
End Class
