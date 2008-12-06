Public Class DotObject
    Inherits StringObject

    Private Shared mInstance As New DotObject()

    Private Sub New()
        MyBase.New(".")
    End Sub

    Public Shared Function GetInstance() As DotObject
        Return mInstance
    End Function
End Class
