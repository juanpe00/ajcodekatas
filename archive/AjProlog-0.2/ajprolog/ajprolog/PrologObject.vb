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
Public MustInherit Class PrologObject
    Public Overridable Function Dereference() As PrologObject
        Return Me
    End Function

    Public Overridable Function Unify(ByVal po As PrologObject) As Boolean
        Dim po0 As PrologObject

        po0 = Me.Dereference

        If Not po0 Is Me Then
            Return po0.Unify(po)
        End If

        po = po.Dereference

        If TypeOf po Is Variable Then
            Return po.Unify(Me)
        End If

        Return Equals(po)
    End Function

    Public Overridable Function MakeNode(ByVal pm As PrologMachine) As Node
        Return New FactNode(Me, pm)
    End Function

    Public Overridable Function ToDisplayString() As String
        Return ToString()
    End Function

    Public Overridable Function ToObject() As Object
        Return ToString()
    End Function

    Public Overridable Function Evaluate(ByVal pm As PrologMachine) As PrologObject
        Return Me
    End Function
End Class
