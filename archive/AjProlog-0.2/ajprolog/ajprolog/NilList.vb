Public Class NilList
    Inherits StringObject

    Private Shared mInstance As New NilList()

    Private Sub New()
        MyBase.New("[]")
    End Sub

    Public Shared Function GetInstance() As NilList
        Return mInstance
    End Function
End Class
