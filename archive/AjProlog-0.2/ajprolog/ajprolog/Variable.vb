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
Public Class Variable
    Inherits SimpleObject

    Private mId As Integer
    Private mValue As PrologObject
    Private mMachine As PrologMachine

    Sub New(ByVal id As Integer, ByVal mach As PrologMachine)
        mId = id
        mMachine = mach
    End Sub

    Public ReadOnly Property Id() As Integer
        Get
            Return mId
        End Get
    End Property

    Public Sub Bind(ByVal obj As PrologObject)
        If TypeOf obj Is Variable Then
            Dim v As Variable = DirectCast(obj, Variable)
            If v.Id > mId Then
                v.Bind(Me)
                Return
            End If
        End If

        mValue = obj
        mMachine.Bindings.Add(Me)
    End Sub

    Public Sub Unbind()
        mValue = Nothing
    End Sub

    Public Overrides Function Dereference() As PrologObject
        If mValue Is Nothing Or mValue Is Me Then
            Return Me
        End If

        Return mValue.Dereference
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return mId.GetHashCode + mMachine.GetHashCode
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not obj.GetType() Is Me.GetType Then
            Return False
        End If

        Return mId = DirectCast(obj, Variable).mId AndAlso mMachine.Equals(DirectCast(obj, Variable).mMachine)
    End Function

    Public Overrides Function ToString() As String
        Dim obj As PrologObject

        obj = Dereference()

        If Not obj Is Me Then
            Return obj.ToString
        End If

        Return "_" & mId
    End Function

    Public Overrides Function Evaluate(ByVal pm As PrologMachine) As PrologObject
        Return Dereference()
    End Function
End Class
