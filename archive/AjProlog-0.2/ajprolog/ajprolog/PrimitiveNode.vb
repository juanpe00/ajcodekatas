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
Public Class PrimitiveNode
    Inherits Node

    Private mPrimitive As Primitive
    Private mParameters As PrologObject()

    Public Sub New(ByVal pm As PrologMachine, ByVal st As StructureObject)
        MyBase.New(pm, st)
        mPrimitive = DirectCast(st.Functor, Primitive)
        mParameters = st.Parameters
    End Sub

    Public Sub New(ByVal pm As PrologMachine, ByVal obj As Primitive)
        MyBase.New(pm, obj)
        mPrimitive = obj
        mParameters = Nothing
    End Sub

    Public Overrides Function ExecuteCall() As Boolean
        If mPrimitive.Execute(Machine, Evaluate(mParameters)) Then
            Machine.Pushnode(Me)
            Return True
        End If
        Machine.PushPending(Me)
        Return False
    End Function

    Public Overrides Function ExecuteRedo() As Boolean
        Machine.PushPending(Me)
        Return False
    End Function
End Class
