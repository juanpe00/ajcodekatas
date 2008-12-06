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
Public Class ListingPrimitive
    Inherits Primitive

    Private Shared mInstance As New ListingPrimitive()

    Public Const Value As String = "listing"

    Private Sub New()
        MyBase.New(Value)
    End Sub

    Public Shared Function GetInstance() As ListingPrimitive
        Return mInstance
    End Function

    Public Overrides Function Execute(ByVal pm As PrologMachine, ByVal pars As PrologObject()) As Boolean
        If pars Is Nothing OrElse pars.Length <> 1 Then
            Throw New ArgumentException("Listing debe recibir un parámetro")
        End If

        If Not TypeOf pars(0) Is StringObject Then
            Throw New ArgumentException("Listing deber recibir un Atomo como parámetro")
        End If

        Dim facts As IList

        facts = pm.GetPredicates(DirectCast(pars(0), StringObject))

        Dim fact As PrologObject

        For Each fact In facts
            pm.Output.WriteLine(fact.ToString())
        Next

        Return True
    End Function
End Class
