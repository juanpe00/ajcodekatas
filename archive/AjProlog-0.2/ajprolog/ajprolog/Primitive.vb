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
Imports System.Text

Public MustInherit Class Primitive
    Inherits StringObject

    Public Sub New(ByVal n As String)
        MyBase.New(n)
    End Sub

    Public Overrides Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New PrimitiveNode(pm, Me)
    End Function

    Public Overridable Function Execute(ByVal pm As PrologMachine, ByVal parameters As PrologObject()) As Boolean
        Return True
    End Function

    Public Overridable Overloads Function Evaluate(ByVal pm As PrologMachine, ByVal st As StructureObject) As PrologObject
        Return st
    End Function

    Public Overridable Overloads Function ToString(ByVal parameters As PrologObject()) As String
        If parameters Is Nothing Then
            Return Me.ToString()
        End If
        If parameters.Length = 0 Then
            Return ToString
        End If

        Dim sb As New StringBuilder(parameters.Length * 20)

        sb.Append(ToString()).Append("(")

        Dim np As Integer

        For np = 0 To parameters.Length - 1
            sb.Append(parameters(np).ToString)
            If np < parameters.Length - 1 Then
                sb.Append(",")
            End If
        Next

        sb.Append(")")

        Return sb.ToString
    End Function
End Class
