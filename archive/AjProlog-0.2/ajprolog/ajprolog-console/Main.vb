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

Imports AjLopez.AjProlog

Module Main
    Public Sub Main(ByVal args() As String)
        Dim arg As String
        Dim parser As parser
        Dim compiler As compiler
        Dim machine As New PrologMachine()

        For Each arg In args
            parser = New parser(New StreamReader(arg))
            compiler = New compiler(parser)
            compiler.CompileProgram(machine)
        Next

        parser = New parser(Console.In)
        compiler = New compiler(parser)

        machine.Interactive = True
        '        pm.Trace = True

        While True
            Try
                compiler.CompileProgram(machine)
                Return
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End While
    End Sub
End Module
