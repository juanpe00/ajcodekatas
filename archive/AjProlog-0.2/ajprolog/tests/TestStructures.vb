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
Imports AjLopez.AjProlog
Imports NUnit.Framework

<TestFixture()> Public Class TestStructures
    <Test()> Public Sub TestStructures1()
        Dim st1 As New StructureObject("f")
        Dim st2 As New StructureObject("f")
        Dim st3 As New StructureObject("g")

        Assert.AreEqual(st1, st1)
        Assert.AreEqual(st1, st2)
        Assert.AreEqual(st2, st1)

        Assert.IsTrue(st1.Arity = 0)
        Assert.IsTrue(st2.Arity = 0)
        Assert.IsTrue(st3.Arity = 0)
        Assert.AreEqual(st1.GetHashCode, st1.GetHashCode)
        Assert.AreEqual(st1.GetHashCode, st2.GetHashCode)
        Assert.IsFalse(st1.Equals(st3))
        Assert.IsFalse(st2.Equals(st3))
        Assert.IsFalse(st3.Equals(st1))
        Assert.IsFalse(st3.Equals(st2))
    End Sub

    <Test()> Public Sub TestStructures2()
        Dim st1 As New StructureObject("f", "a", "b")
        Dim st2 As New StructureObject("f", "a", "b")
        Dim st3 As New StructureObject("g", "c", "d")

        Assert.AreEqual(st1, st1)
        Assert.AreEqual(st1, st2)
        Assert.AreEqual(st2, st1)
        Assert.IsTrue(st1.Arity = 2)
        Assert.IsTrue(st2.Arity = 2)
        Assert.IsTrue(st3.Arity = 2)
        Assert.AreEqual(st1.GetHashCode, st1.GetHashCode)
        Assert.AreEqual(st1.GetHashCode, st2.GetHashCode)
        Assert.IsFalse(st1.Equals(st3))
        Assert.IsFalse(st2.Equals(st3))
        Assert.IsFalse(st3.Equals(st1))
        Assert.IsFalse(st3.Equals(st2))
    End Sub

    <Test()> Public Sub TestStructures3()
        Dim st1 As New StructureObject("f", 1, 2)
        Dim st2 As New StructureObject("f", 1, 2)
        Dim st3 As New StructureObject("g", 3, 4)

        Assert.AreEqual(st1, st1)
        Assert.AreEqual(st1, st2)
        Assert.AreEqual(st2, st1)
        Assert.IsTrue(st1.Arity = 2)
        Assert.IsTrue(st2.Arity = 2)
        Assert.IsTrue(st3.Arity = 2)
        Assert.AreEqual(st1.GetHashCode, st1.GetHashCode)
        Assert.AreEqual(st1.GetHashCode, st2.GetHashCode)
        Assert.IsFalse(st1.Equals(st3))
        Assert.IsFalse(st2.Equals(st3))
        Assert.IsFalse(st3.Equals(st1))
        Assert.IsFalse(st3.Equals(st2))
    End Sub
End Class
