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
Public Class FactNode
    Inherits Node

    Private mStatus As PrologMachineStatus
    Private mFactsEnum As IEnumerator

    Public Sub New(ByVal obj As PrologObject, ByVal mach As PrologMachine)
        MyBase.New(mach, obj)
    End Sub

    Public Overrides Function ExecuteCall() As Boolean
        mFactsEnum = Machine.GetFacts(Me.Object).GetEnumerator
        Return NextFact()
    End Function

    Function NextFact() As Boolean
        If mFactsEnum Is Nothing Then
            Return False
        End If

        While mFactsEnum.MoveNext
            Dim fact As PrologObject = mFactsEnum.Current
            SaveStatus()
            fact = Machine.AdjustVariables(fact)
            If TypeOf fact Is StructureObject AndAlso DirectCast(fact, StructureObject).Functor Is IfPrimitive.GetInstance Then
                Dim ifpo As StructureObject
                ifpo = DirectCast(fact, StructureObject)
                Dim thenpo As PrologObject = ifpo.Parameters(1)
                fact = ifpo.Parameters(0)
                If Machine.Unify(Me.Object, fact) Then
                    Machine.Level += 1
                    Machine.PushPending(thenpo)
                    Machine.PushNode(Me)
                    Return True
                End If
            Else
                If Machine.Unify(Me.Object, fact) Then
                    Machine.PushNode(Me)
                    Return True
                End If
            End If
            RestoreStatus()
        End While

        Machine.PushPending(Me)
        Return False
    End Function

    Protected Sub SaveStatus()
        mStatus = Machine.Status
    End Sub

    Protected Sub RestoreStatus()
        Machine.Status = mStatus
    End Sub

    Public Overrides Function ExecuteRedo() As Boolean
        RestoreStatus()
        Return NextFact()
    End Function

    Public Sub DoCut()
        mFactsEnum = Nothing
    End Sub
End Class
