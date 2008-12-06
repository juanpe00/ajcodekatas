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

<TestFixture()> Public Class TestParser
    <Test()> Public Sub TestParser1()
        Dim p As New Parser("f")
        Dim t As Token

        t = p.NextToken()

        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser1b()
        Dim p As New Parser(" f ")
        Dim t As Token

        t = p.NextToken()

        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser2()
        Dim p As New Parser("f(a)")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser2b()
        Dim p As New Parser("f ( a ) ")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser3()
        Dim p As New Parser("f(a),g(X)")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        t = p.NextToken()
        Assert.AreEqual("g", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("X", t.Value)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser4()
        Dim p As New Parser("a(Y) :- f(a),g(X)")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("Y", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual(":-", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("g", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("X", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

    <Test()> Public Sub TestParser5()
        Dim p As New Parser("f(a),!,g(X,b)")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        t = p.NextToken()
        Assert.AreEqual("!", t.Value)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        t = p.NextToken()
        Assert.AreEqual("g", t.Value)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        t = p.NextToken()
        Assert.AreEqual("X", t.Value)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("b", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub


    <Test()> Public Sub TestParser6()
        Dim p As New Parser("a(_Y) :- f(_),g(X)")
        Dim t As Token

        t = p.NextToken()
        Assert.AreEqual("a", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("_Y", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual(":-", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("f", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("_", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual(",", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("g", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual("(", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.AreEqual("X", t.Value)
        Assert.AreEqual(TokenType.TokAtom, t.Type)
        t = p.NextToken()
        Assert.AreEqual(")", t.Value)
        Assert.AreEqual(TokenType.TokPuntuation, t.Type)
        t = p.NextToken()
        Assert.IsNull(t)
    End Sub

End Class
