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

<TestFixture()> Public Class TestPrimitives
    <Test()> Public Sub TestAtom()
        Dim st1 As New StructureObject("atom", "a")
        Dim st2 As New StructureObject("atom", 1)
        Dim st3 As New StructureObject("atom", New StructureObject("f", "X"))

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsFalse(pm.Resolve(st2))
        Assert.IsFalse(pm.Resolve(st3))
    End Sub

    <Test()> Public Sub TestAtomic()
        Dim st1 As New StructureObject("atomic", "a")
        Dim st2 As New StructureObject("atomic", 1)
        Dim st3 As New StructureObject("atomic", New StructureObject("f", "X"))

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
        Assert.IsFalse(pm.Resolve(st3))
    End Sub

    <Test()> Public Sub TestInteger()
        Dim st1 As New StructureObject("integer", "a")
        Dim st2 As New StructureObject("integer", 1)
        Dim st3 As New StructureObject("integer", New StructureObject("f", "X"))

        Dim pm As New PrologMachine()

        Assert.IsFalse(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
        Assert.IsFalse(pm.Resolve(st3))
    End Sub

    <Test()> Public Sub TestVar()
        Dim st1 As New StructureObject("var", "a")
        Dim st2 As New StructureObject("var", 1)
        Dim st3 As New StructureObject("var", New StructureObject("f", "X"))
        Dim st4 As New StructureObject("var", "X")

        Dim pm As New PrologMachine()

        Assert.IsFalse(pm.Resolve(st1))
        Assert.IsFalse(pm.Resolve(st2))
        Assert.IsFalse(pm.Resolve(st3))
        Assert.IsTrue(pm.Resolve(st4))
    End Sub

    <Test()> Public Sub TestNonVar()
        Dim st1 As New StructureObject("nonvar", "a")
        Dim st2 As New StructureObject("nonvar", 1)
        Dim st3 As New StructureObject("nonvar", New StructureObject("f", "X"))
        Dim st4 As New StructureObject("nonvar", "X")

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
        Assert.IsTrue(pm.Resolve(st3))
        Assert.IsFalse(pm.Resolve(st4))
    End Sub

    <Test()> Public Sub TestEqual1()
        Dim st1 As New StructureObject(EqualPrimitive.GetInstance, "a", "X")

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Resolve(st1))
        Assert.AreEqual(st1.Parameters(0), pm.Variable(0).Dereference)
    End Sub

    <Test()> Public Sub TestEqual2()
        Dim p As New Parser("f(X)=f(a)")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual("a", pm.Variable(0).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestNotEqual1()
        Dim st1 As New StructureObject(NotEqualPrimitive.GetInstance, "a", "X")

        Dim pm As New PrologMachine()

        Assert.IsFalse(pm.Resolve(st1))
        Assert.AreEqual(pm.Variable(0), pm.Variable(0).Dereference)
    End Sub

    <Test()> Public Sub TestNotEqual2()
        Dim p As New Parser("f(X)\=g(a)")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestLess1()
        Dim st1 As New StructureObject(LessPrimitive.GetInstance, 1, 2)

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Resolve(st1))
    End Sub

    <Test()> Public Sub TestLess2()
        Dim p As New Parser("1<2")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestLess3()
        Dim p As New Parser("X=2,1<X")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(2, pm.Variable(0).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestPlus1()
        Dim p As New Parser("1+2")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestPlus2()
        Dim p As New Parser("1+1+2")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestMinus1()
        Dim p As New Parser("2-1")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestMinus2()
        Dim p As New Parser("2+1-2")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
    End Sub

    <Test()> Public Sub TestIs1()
        Dim p As New Parser("X is 1")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(1, pm.Variable(0).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestIs2()
        Dim p As New Parser("X is 1+2")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(3, pm.Variable(0).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestIs3()
        Dim p As New Parser("X is 1+2+3")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(6, pm.Variable(0).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestIs4()
        Dim p As New Parser("N=1, X is 1+N")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(1, pm.Variable(0).Dereference.ToObject)
        Assert.AreEqual(2, pm.Variable(1).Dereference.ToObject)
    End Sub

    <Test()> Public Sub TestIs5()
        Dim p As New Parser("N=1, X is 1+N+N")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()
        Dim po As PrologObject

        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(1, pm.Variable(0).Dereference.ToObject)
        Assert.AreEqual(3, pm.Variable(1).Dereference.ToObject)
    End Sub
End Class
