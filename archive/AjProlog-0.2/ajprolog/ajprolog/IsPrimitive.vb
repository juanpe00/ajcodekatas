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
Public Class IsPrimitive
    Inherits Primitive

    Private Shared mInstance As New IsPrimitive()

    Public Const Value As String = "is"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As IsPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        Return pm.Unify(pars(0), pars(1).Evaluate(pm))
    End Function

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New PrimitiveStatusNode(pm, Me)
    End Function
End Class
