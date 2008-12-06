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

<TestFixture()> Public Class TestUnify
    <Test()> Public Sub TestUnify1()
        Dim po1 As New StructureObject("a")
        Dim po2 As New StructureObject("a")
        Dim po3 As New StringObject("a")

        Dim pm As New PrologMachine()

        Assert.IsTrue(pm.Unify(po1, po1))
        Assert.IsTrue(pm.Unify(po1, po2))
        Assert.IsTrue(pm.Unify(po2, po1))
        Assert.IsTrue(pm.Unify(po1, po3))
        Assert.IsTrue(pm.Unify(po3, po1))
        Assert.IsTrue(pm.Unify(po2, po3))
        Assert.IsTrue(pm.Unify(po3, po2))
    End Sub

    <Test()> Public Sub TestUnify2()
        Dim po1 As New StructureObject("a")
        Dim po2 As New StructureObject("b")
        Dim po3 As New StringObject("c")

        Dim pm As New PrologMachine()

        Assert.IsFalse(pm.Unify(po1, po2))
        Assert.IsFalse(pm.Unify(po2, po1))
        Assert.IsFalse(pm.Unify(po1, po3))
        Assert.IsFalse(pm.Unify(po3, po1))
        Assert.IsFalse(pm.Unify(po2, po3))
        Assert.IsFalse(pm.Unify(po3, po2))
    End Sub

    <Test()> Public Sub TestUnify3()
        Dim po1 As New StructureObject("a", "b", "c")
        Dim po2 As New StructureObject("a", "X", "Y")

        Dim pm As New PrologMachine()

        po2 = pm.AdjustVariables(po2)

        Assert.AreEqual("_0", po2.Parameters(0).ToString)
        Assert.AreEqual("_1", po2.Parameters(1).ToString)

        Assert.IsTrue(pm.Unify(po1, po2))
        Assert.IsTrue(pm.Unify(po2, po1))

        Assert.AreEqual(po1.Parameters(0), pm.Variable(0).Dereference)
        Assert.AreEqual(po1.Parameters(1), pm.Variable(1).Dereference)
    End Sub
End Class
