Imports System.Text

Public Class DotPrimitive
    Inherits Primitive

    Private Shared mInstance As New DotPrimitive()

    Private Sub New()
        MyBase.New(".")
    End Sub

    Public Shared Function GetInstance() As DotPrimitive
        Return mInstance
    End Function

    Public Overloads Overrides Function ToString(ByVal parameters As PrologObject()) As String
        Dim head As PrologObject
        Dim tail As PrologObject

        head = parameters(0).Dereference
        tail = parameters(1).Dereference

        Dim sb As New StringBuilder(20)

        sb.Append("[")

        sb.Append(head.ToString)

        While TypeOf tail Is StructureObject AndAlso DirectCast(tail, StructureObject).Functor Is DotPrimitive.GetInstance
            Dim list As StructureObject = DirectCast(tail, StructureObject)
            sb.Append(",")
            sb.Append(list.Parameters(0).Dereference.ToString)
            tail = list.Parameters(1).Dereference
        End While

        If TypeOf tail Is NilList Then
            sb.Append("]")
        Else
            sb.Append("|")
            sb.Append(tail.ToString)
            sb.Append("]")
        End If

        Return sb.ToString
    End Function
End Class
