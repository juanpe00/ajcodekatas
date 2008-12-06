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
Imports System.IO

Public Class Parser
    Private input As TextReader
    Private tokenValue As String
    Private lastChar As Char
    Private hasChar As Boolean
    Private lastToken As Token
    Private hasToken As Boolean

    Private Const separators As String = ",.()[]"

    Sub New(ByVal input As TextReader)
        Me.input = input
    End Sub

    Sub New(ByVal input As String)
        Me.New(New StringReader(input))
    End Sub

    ReadOnly Property Reader() As TextReader
        Get
            Return input
        End Get
    End Property

    Sub Flush()
        While input.Peek > 0
            input.Read()
        End While
    End Sub

    Sub PushChar(ByVal ch As Char)
        lastChar = ch
        hasChar = True
    End Sub

    Function NextChar() As Char
        If hasChar Then
            hasChar = False
            Return lastChar
        End If

        Dim ch As Integer

        If input Is Console.In And input.Peek < 0 Then
            Console.Out.Write("> ")
            Console.Out.Flush()
        End If
        ch = input.Read()

        If ch < 0 Then
            Throw New EndOfInputException()
        End If

        Return ChrW(ch)
    End Function

    Function NextCharSkipBlanks() As Char
        Dim ch As Char

        ch = NextChar()

        While Char.IsWhiteSpace(ch)
            ch = NextChar()
        End While

        Return ch
    End Function

    Function NextName(ByVal firstChar As Char) As Token
        Dim name As String

        name = firstChar

        Dim ch As Char

        Try
            ch = NextChar()

            While Char.IsLetterOrDigit(ch)
                name += ch
                ch = NextChar()
            End While

            PushChar(ch)
        Catch ex As EndOfInputException

        End Try

        Dim token As New token()

        token.Type = TokenType.TokAtom
        token.Value = name

        Return token
    End Function

    Function NextString() As Token
        Dim value As String = ""

        Dim ch As Char

        ch = NextChar()

        While ch <> """"c
            value += ch
            ch = NextChar()
        End While

        Dim token As New token()

        token.Type = TokenType.TokAtom
        token.Value = value

        Return token
    End Function

    Function NextQuote() As Token
        Dim value As String = ""

        Dim ch As Char

        ch = NextChar()

        While ch <> "'"c
            value += ch
            ch = NextChar()
        End While

        Dim token As New token()

        token.Type = TokenType.TokAtom
        token.Value = value

        Return token
    End Function

    Function NextInteger(ByVal firstDigit As Char) As Token
        Dim value As String

        value = firstDigit

        Dim ch As Char

        Try
            ch = NextChar()

            While Char.IsDigit(ch)
                value += ch
                ch = NextChar()
            End While

            PushChar(ch)
        Catch ex As EndOfInputException
        End Try

        Dim token As New token()

        token.Type = TokenType.TokInteger
        token.Value = value

        Return token
    End Function

    Function NextPunctuation(ByVal ch As Char) As Token
        Dim token As New token()

        token.Type = TokenType.TokPuntuation
        token.Value = ch

        'If ch = ","c Then
        '    token.Type = TokenType.TokAtom
        'End If

        Return token
    End Function

    Function NextOperator(ByVal ch As Char) As Token
        Dim value As String

        value = ch

        Try
            ch = NextChar()

            While Not Char.IsLetterOrDigit(ch) And Not Char.IsWhiteSpace(ch) And Not separators.IndexOf(ch) >= 0
                value += ch
                ch = NextChar()
            End While

            PushChar(ch)
        Catch ex As EndOfInputException

        End Try

        Dim token As New token()

        token.Value = value
        token.Type = TokenType.TokAtom

        Return token
    End Function

    Function NextToken() As Token
        If hasToken Then
            hasToken = False
            Return lastToken
        End If

        Dim ch As Char

        Try
            ch = NextCharSkipBlanks()

            If Char.IsLetter(ch) Or ch = "_"c Then
                Return NextName(ch)
            End If

            If ch = """"c Then
                Return NextString()
            End If

            If ch = "'"c Then
                Return NextQuote()
            End If

            If Char.IsDigit(ch) Then
                Return NextInteger(ch)
            End If

            If separators.IndexOf(ch) >= 0 Then
                Return NextPunctuation(ch)
            End If

            Return NextOperator(ch)
        Catch ex As EndOfInputException
            Return Nothing
        End Try
    End Function

    Sub PushToken(ByVal token As Token)
        lastToken = token
        hasToken = True
    End Sub
End Class

Public Enum TokenType
    TokInteger = 1
    TokAtom = 2
    TokPuntuation = 3
End Enum

Public Class Token
    Public Type As TokenType
    Public Value As String
End Class

Public Class EndOfInputException
    Inherits Exception

    Sub New()
        MyBase.New("Fin de Entrada")
    End Sub
End Class

Public Class ParserException
    Inherits Exception

    Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class
