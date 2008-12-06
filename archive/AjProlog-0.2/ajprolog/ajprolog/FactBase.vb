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
Public Class FactBase
    Private mFacts As IList = New ArrayList()

    Public Sub Add(ByVal fact As PrologObject)
        If Not mFacts.Contains(fact) Then
            mFacts.Add(fact)
        End If
    End Sub

    Public Function GetFacts(ByVal po As PrologObject) As IList
        Return mFacts
    End Function

    Private Function IsPredicate(ByVal fact As PrologObject, ByVal atom As StringObject) As Boolean
        If fact.Equals(atom) Then
            Return True
        End If

        If Not TypeOf fact Is StructureObject Then
            Return False
        End If

        Dim st As StructureObject = DirectCast(fact, StructureObject)

        If st.Functor.Equals(atom) Then
            Return True
        End If

        If Not st.Functor Is IfPrimitive.GetInstance Then
            Return False
        End If

        fact = st.Parameters(0)

        If fact.Equals(atom) Then
            Return True
        End If

        If Not TypeOf fact Is StructureObject Then
            Return False
        End If

        st = DirectCast(fact, StructureObject)

        If st.Functor.Equals(atom) Then
            Return True
        End If

        Return False
    End Function

    Public Function GetPredicates(ByVal atom As StringObject) As IList
        Dim result As New ArrayList()

        Dim fact As PrologObject

        For Each fact In mFacts
            If IsPredicate(fact, atom) Then
                result.Add(fact)
            End If
        Next

        Return result
    End Function
End Class
