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
Public Class StringObject
    Inherits SimpleObject

    Private mValue As String

    Public Sub New(ByVal v As String)
        mValue = v
    End Sub

    Public Overrides Function GetHashCode() As Integer
        Return mValue.GetHashCode
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is StructureObject Then
            obj = DirectCast(obj, StructureObject).Normalize
        End If

        If obj Is Nothing OrElse Not obj.GetType() Is Me.GetType Then
            Return False
        End If

        Return mValue.Equals(DirectCast(obj, StringObject).mValue)
    End Function

    Public Overrides Function ToString() As String
        Return mValue
    End Function

    Public Function GetValue() As String
        Return mValue
    End Function

    Public Function IsVariableName() As Boolean
        Return Utilities.IsVariableName(mValue)
    End Function
End Class
