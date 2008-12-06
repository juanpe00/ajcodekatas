Public Class NilObject
    Inherits StringObject

    Private Shared mInstance As New NilObject()

    Private Sub New()
        MyBase.New("nil")
    End Sub

    Public Shared Function GetInstance() As NilObject
        Return mInstance
    End Function
End Class
