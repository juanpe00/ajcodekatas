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
Public Class CutNode
    Inherits PrimitiveNode

    Private mNode As FactNode

    Public Sub New(ByVal pm As PrologMachine, ByVal st As StructureObject)
        MyBase.New(pm, st)
        mNode = pm.GetCutNode

        'If mNode Is Nothing Then
        '    Throw New Exception("Cut no tiene FactNode")
        'End If
    End Sub

    Public Sub New(ByVal pm As PrologMachine, ByVal obj As Primitive)
        MyBase.New(pm, obj)
        mNode = pm.GetCutNode

        'If mNode Is Nothing Then
        '    Throw New Exception("Cut no tiene FactNode")
        'End If
    End Sub

    Public Overrides Function ExecuteCall() As Boolean
        Machine.CutToNode(mNode)
        Return True
    End Function
End Class
