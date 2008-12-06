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
Public Class IntegerObject
    Inherits SimpleObject

    Private mValue As Integer

    Public Sub New(ByVal v As Integer)
        mValue = v
    End Sub

    Public Overrides Function GetHashCode() As Integer
        Return mValue.GetHashCode
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse Not obj.GetType() Is Me.GetType Then
            Return False
        End If

        Return mValue = DirectCast(obj, IntegerObject).mValue
    End Function

    Public Overrides Function ToString() As String
        Return mValue.ToString
    End Function

    Public Function GetValue() As Integer
        Return mValue
    End Function

    Public Overrides Function ToObject() As Object
        Return GetValue()
    End Function
End Class
