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

<TestFixture()> Public Class TestObjects
    <Test()> Public Sub TestObjects1()
        Dim po1 As PrologObject
        Dim po2 As PrologObject
        Dim po3 As PrologObject

        po1 = New IntegerObject(1)
        po2 = New IntegerObject(1)
        po3 = New IntegerObject(2)

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))
        Assert.IsFalse(po1.GetHashCode = po3.GetHashCode)

        po1 = New StringObject("a")
        po2 = New StringObject("a")
        po3 = New StringObject("b")

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))
        Assert.IsFalse(po1.GetHashCode = po3.GetHashCode)

        po1 = New StringObject("X")
        po2 = New StringObject("X")
        po3 = New StringObject("Y")

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))
        Assert.IsFalse(po1.GetHashCode = po3.GetHashCode)
    End Sub

    <Test()> Public Sub TestObjects2()
        Dim po1 As PrologObject
        Dim po2 As PrologObject
        Dim po3 As PrologObject

        po1 = Utilities.ToPrologObject(1)
        po2 = Utilities.ToPrologObject(1)
        po3 = Utilities.ToPrologObject(2)

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))

        Assert.AreEqual(po1.ToString, "1")
        Assert.AreEqual(po2.ToString, "1")
        Assert.AreEqual(po3.ToString, "2")

        po1 = Utilities.ToPrologObject("a")
        po2 = Utilities.ToPrologObject("a")
        po3 = Utilities.ToPrologObject("b")

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))
        Assert.IsFalse(po1.GetHashCode = po3.GetHashCode)

        Assert.AreEqual(po1.ToString, "a")
        Assert.AreEqual(po2.ToString, "a")
        Assert.AreEqual(po3.ToString, "b")

        po1 = Utilities.ToPrologObject("X")
        po2 = Utilities.ToPrologObject("X")
        po3 = Utilities.ToPrologObject("Y")

        Assert.AreEqual(po1, po2)
        Assert.AreEqual(po1.GetHashCode, po2.GetHashCode)
        Assert.IsFalse(po1.Equals(po3))

        Assert.AreEqual(po1.ToString, "X")
        Assert.AreEqual(po2.ToString, "X")
        Assert.AreEqual(po3.ToString, "Y")
    End Sub

    <Test()> Public Sub TestVariables1()
        Dim v1 As Variable
        Dim v2 As Variable
        Dim v3 As Variable
        Dim pm As New PrologMachine()

        v1 = pm.Variable(0)
        v2 = pm.Variable(0)
        v3 = pm.Variable(1)

        Assert.AreEqual(v1, v2)
        Assert.AreEqual(v1.GetHashCode, v2.GetHashCode)

        Assert.IsFalse(v1.Equals(v3))

        Assert.AreEqual(v1.ToString, "_0")
        Assert.AreEqual(v2.ToString, "_0")
        Assert.AreEqual(v3.ToString, "_1")
    End Sub
End Class
