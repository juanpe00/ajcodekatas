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
Public Class NotEqualPrimitive
    Inherits Primitive

    Private Shared mInstance As New NotEqualPrimitive()

    Public Const Value As String = "="

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As NotEqualPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        Dim status As PrologMachineStatus
        Dim result As Boolean

        status = pm.Status

        result = Not pm.Unify(pars(0), pars(1))

        pm.Status = status

        Return result
    End Function
End Class
