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
Public Class Primitives
    Private Shared mPrimitives As IDictionary = New Hashtable()

    Shared Sub New()
        Register(AndPrimitive.GetInstance)
        Register(QueryPrimitive.GetInstance)
        Register(IfPrimitive.GetInstance)
        Register(TruePrimitive.GetInstance)
        Register(FailPrimitive.GetInstance)
        Register(VarPrimitive.GetInstance)
        Register(NonVarPrimitive.GetInstance)
        Register(AtomPrimitive.GetInstance)
        Register(AtomicPrimitive.GetInstance)
        Register(IntegerPrimitive.GetInstance)
        Register(ListingPrimitive.GetInstance)
        Register(WritePrimitive.GetInstance)
        Register(NlPrimitive.GetInstance)
        Register(TabPrimitive.GetInstance)
        Register(DisplayPrimitive.GetInstance)
        Register(CutPrimitive.GetInstance)
        Register(EqualPrimitive.GetInstance)
        Register(NotEqualPrimitive.GetInstance)
        Register(LessPrimitive.GetInstance)
        Register(PlusPrimitive.GetInstance)
        Register(MinusPrimitive.GetInstance)
        Register(IsPrimitive.GetInstance)
    End Sub

    Public Shared Sub Register(ByVal primitive As Primitive)
        Register(primitive.ToString, primitive)
    End Sub

    Public Shared Sub Register(ByVal name As String, ByVal primitive As Primitive)
        mPrimitives(name) = primitive
    End Sub

    Public Shared Function GetPrimitive(ByVal name As String) As Primitive
        Return mPrimitives(name)
    End Function
End Class
