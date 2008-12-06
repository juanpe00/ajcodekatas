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
Public MustInherit Class Node
    Private mMachine As PrologMachine
    Private mObject As PrologObject

    Public Sub New(ByVal mach As PrologMachine, ByVal obj As PrologObject)
        mMachine = mach
        mObject = obj
    End Sub

    Public ReadOnly Property Machine()
        Get
            Return mMachine
        End Get
    End Property

    Public ReadOnly Property [Object]() As PrologObject
        Get
            Return mObject
        End Get
    End Property

    Public Overridable Function IsPushable() As Boolean
        Return True
    End Function

    Public Overridable Sub ExecutePush()

    End Sub

    Public MustOverride Function ExecuteCall() As Boolean
    Public MustOverride Function ExecuteRedo() As Boolean
End Class
