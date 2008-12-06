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

Public Class QueryPrimitive
    Inherits Primitive

    Private Shared mInstance As New QueryPrimitive()

    Public Const Value As String = "?-"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As QueryPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If pars Is Nothing OrElse pars.Length <> 1 Then
            Throw New ArgumentException("Query espera un parámetro")
        End If
        pm.PushPending(pars(0))
        Return True
    End Function

    Public Overridable Overloads Function ToString(ByVal parameters As PrologObject()) As String
        If parameters Is Nothing OrElse parameters.Length <> 1 Then
            Return MyBase.ToString(parameters)
        End If

        Dim sb As New StringBuilder(50)

        sb.Append(Value)
        sb.Append(parameters(0).ToString)

        Return sb.ToString
    End Function
End Class
