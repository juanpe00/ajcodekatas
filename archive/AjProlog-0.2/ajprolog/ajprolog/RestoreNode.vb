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
Public Class RestoreNode
    Inherits Node

    Public Sub New(ByVal pm As PrologMachine, ByVal obj As PrologObject)
        MyBase.New(pm, obj)
    End Sub

    Public Overrides Function ExecuteCall() As Boolean
        Throw New NotSupportedException("No se espera Call en RestoreNode")
    End Function

    Public Overrides Function ExecuteRedo() As Boolean
        Machine.PushPending(Me.Object)
        Return False
    End Function
End Class
