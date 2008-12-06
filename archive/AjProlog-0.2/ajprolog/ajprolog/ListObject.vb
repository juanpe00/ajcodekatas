Public Class ListObject
    Inherits StructureObject

    Public Sub New(ByVal head As PrologObject, ByVal tail As PrologObject)
        MyBase.New(DotPrimitive.GetInstance, head, tail)
    End Sub
End Class
