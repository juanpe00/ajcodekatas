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
Public Class PrimitiveStatusNode
    Inherits PrimitiveNode

    Private mStatus As PrologMachineStatus

    Public Sub New(ByVal pm As PrologMachine, ByVal st As StructureObject)
        MyBase.New(pm, st)
    End Sub

    Public Sub New(ByVal pm As PrologMachine, ByVal obj As Primitive)
        MyBase.New(pm, obj)
    End Sub

    Public Overrides Function ExecuteCall() As Boolean
        SaveStatus()
        If MyBase.ExecuteCall Then
            Return True
        End If
        RestoreStatus()
        Return False
    End Function

    Public Overrides Function ExecuteRedo() As Boolean
        RestoreStatus()
        Machine.PushPending(Me)
        Return False
    End Function

    Sub SaveStatus()
        mStatus = Machine.Status
    End Sub

    Sub RestoreStatus()
        Machine.Status = mStatus
    End Sub
End Class
