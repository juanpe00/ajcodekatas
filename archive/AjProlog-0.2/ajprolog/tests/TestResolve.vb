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
Imports NUnit.Framework
Imports AjLopez.AjProlog

<TestFixture()> Public Class TestResolve
    <Test()> Public Sub TestResolve1()
        Dim pm As New PrologMachine()
        Dim st1 As New StructureObject("a")
        Dim st2 As New StructureObject("a")
        Dim st3 As New StructureObject("c")
        Dim st4 As New StructureObject("X")

        pm.Assertz(st1)

        Assert.IsTrue(pm.Resolve(st2))
        Assert.IsFalse(pm.Resolve(st3))
        Assert.IsTrue(pm.Resolve(st4))

        pm.Assertz(st3)
        Assert.IsTrue(pm.Resolve(st3))
        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
        Assert.IsTrue(pm.Resolve(st4))
    End Sub

    <Test()> Public Sub TestAnd1()
        Dim pm As New PrologMachine()
        Dim st1 As New StructureObject("a")
        Dim st2 As New StructureObject("b")
        Dim st3 As New StructureObject("c")

        pm.Assertz(st1)
        pm.Assertz(st2)

        Assert.IsTrue(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st1, st2)))
        Assert.IsTrue(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st2, st1)))
        Assert.IsTrue(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st1, st1)))
        Assert.IsFalse(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st1, st3)))
        Assert.IsFalse(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st3, st1)))
    End Sub

    <Test()> Public Sub TestAnd2()
        Dim pm As New PrologMachine()
        Dim st1 As New StructureObject("a")
        Dim st2 As New StructureObject("b")
        Dim st3 As New StructureObject("X")

        pm.Assertz(st1)
        pm.Assertz(st2)

        Assert.IsTrue(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st1, st3)))
        Assert.IsTrue(pm.Resolve(New StructureObject(AndPrimitive.GetInstance(), st3, st1)))
    End Sub

    <Test()> Public Sub TestIf1()
        Dim pm As New PrologMachine()
        Dim st1 As New StructureObject("a")
        Dim st2 As New StructureObject("b")
        Dim st3 As New StructureObject("X")

        pm.Assertz(st1)
        pm.Assertz(New StructureObject(IfPrimitive.GetInstance, New StringObject("b"), New StringObject("a")))

        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
    End Sub

    <Test()> Public Sub TestIf2()
        Dim pm As New PrologMachine()
        Dim st1 As New StructureObject("mortal", "socrates")
        Dim st2 As New StructureObject("hombre", "X")
        Dim st3 As New StructureObject("mortal", "X")
        Dim st4 As New StructureObject("hombre", "socrates")

        pm.Assertz(st4)
        pm.Assertz(New StructureObject(IfPrimitive.GetInstance, st3, st2))

        Assert.IsTrue(pm.Resolve(st1))
        Assert.IsTrue(pm.Resolve(st2))
        Assert.AreEqual(New StringObject("socrates"), pm.Variable(0).Dereference)
    End Sub
End Class
