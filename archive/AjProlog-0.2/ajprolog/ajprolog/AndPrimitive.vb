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
Public Class AndPrimitive
    Inherits Primitive

    Private Shared mInstance As New AndPrimitive()

    Public Const Value As String = ","

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As AndPrimitive
        Return mInstance
    End Function

    Private Sub Add(ByVal pm As PrologMachine, ByVal po As PrologObject)
        If TypeOf po Is StructureObject AndAlso DirectCast(po, StructureObject).Functor Is Me Then
            Dim ast As StructureObject = DirectCast(po, StructureObject)
            Add(pm, ast.Parameters(1))
            Add(pm, ast.Parameters(0))
        Else
            pm.PushPending(po)
        End If
    End Sub

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If pars Is Nothing OrElse pars.Length <> 2 Then
            Throw New ArgumentException("And debe recibir dos parámetros")
        End If
        Add(pm, pars(1))
        Add(pm, pars(0))
        Return True
    End Function

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New AndNode(pm, Me)
    End Function
End Class
