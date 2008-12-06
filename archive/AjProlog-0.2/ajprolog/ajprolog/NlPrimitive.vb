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
Public Class NlPrimitive
    Inherits Primitive

    Private Shared mInstance As New NlPrimitive()

    Public Const Value As String = "nl"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As NlPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If Not pars Is Nothing AndAlso pars.Length <> 0 Then
            Throw New ArgumentException("Nl no debe recibir parámetros")
        End If

        pm.Output.WriteLine()

        Return True
    End Function
End Class
