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
Public Class CutPrimitive
    Inherits Primitive

    Private Shared mInstance As New CutPrimitive()

    Public Const Value As String = "!"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As CutPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If Not pars Is Nothing AndAlso pars.Length <> 0 Then
            Throw New ArgumentException("Cut no recibe parámetros")
        End If
        Return False
    End Function

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New CutNode(pm, Me)
    End Function
End Class
