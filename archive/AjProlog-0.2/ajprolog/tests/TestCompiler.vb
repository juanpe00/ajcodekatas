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

<TestFixture()> Public Class TestCompiler
    <Test()> Public Sub TestCompiler1()
        Dim p As New Parser("a")
        Dim c As New Compiler(p)
        Dim po As PrologObject

        po = c.Compile()
        Assert.IsTrue(TypeOf po Is StringObject)
        Assert.AreEqual(New StringObject("a"), po)
    End Sub

    <Test()> Public Sub TestCompiler2()
        Dim p As New Parser("f(a)")
        Dim c As New Compiler(p)
        Dim po As PrologObject

        po = c.Compile()
        Assert.IsTrue(TypeOf po Is StructureObject)

        Dim st As StructureObject

        st = DirectCast(po, StructureObject)

        Assert.AreEqual(New StringObject("f"), st.Functor)
        Assert.AreEqual(New StringObject("a"), st.Parameters(0))
    End Sub

    <Test()> Public Sub TestCompiler3()
        Dim p As New Parser("f(a),g(b)")
        Dim c As New Compiler(p)
        Dim po As PrologObject

        po = c.Compile()
        Assert.IsTrue(TypeOf po Is StructureObject)
        Assert.IsTrue(DirectCast(po, StructureObject).Functor Is AndPrimitive.GetInstance)
    End Sub

    <Test()> Public Sub TestCompiler4()
        Dim p As New Parser("f(a):-g(b)")
        Dim c As New Compiler(p)
        Dim po As PrologObject

        po = c.Compile()
        Assert.IsTrue(TypeOf po Is StructureObject)
        Assert.IsTrue(DirectCast(po, StructureObject).Functor Is IfPrimitive.GetInstance)
    End Sub

    <Test()> Public Sub TestCompiler5()
        Dim p As New Parser("?- f(a),g(b)")
        Dim c As New Compiler(p)
        Dim po As PrologObject

        po = c.Compile()
        Assert.IsTrue(TypeOf po Is StructureObject)
        Assert.AreEqual(QueryPrimitive.GetInstance(), DirectCast(po, StructureObject).Functor)
    End Sub

    <Test()> Public Sub TestCompiler6()
        Dim p As New Parser("hombre(socrates)")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()

        Dim po As PrologObject

        po = c.Compile
        pm.Assertz(po)

        p = New Parser("hombre(X)")
        c = New Compiler(p)
        po = c.Compile

        Assert.IsTrue(pm.Resolve(po))
        Assert.AreEqual(New StringObject("socrates"), pm.Variable(0).Dereference)
    End Sub

    <Test()> Public Sub TestCompiler7()
        Dim p As New Parser("hombre(socrates). ?- hombre(X).")
        Dim c As New Compiler(p)
        Dim pm As New PrologMachine()

        Dim po As PrologObject

        c.CompileProgram(pm)
        Assert.AreEqual(New StringObject("socrates"), pm.Variable(0).Dereference)
    End Sub
End Class
