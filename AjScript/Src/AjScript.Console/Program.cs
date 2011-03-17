﻿namespace AjScript.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjScript.Compiler;
    using AjScript.Commands;
    using AjScript.Primitives;

    class Program
    {
        static void Main(string[] args)
        {
            IContext context = new Context(0);
            Parser parser = new Parser(System.Console.In, context);
            context.DefineVariable("write");
            context.SetValue("write", new WriteFunction());
            context.DefineVariable("writeln");
            context.SetValue("writeln", new WriteLineFunction());
            context.DefineVariable("Object");
            context.SetValue("Object", new ObjectFunction());

            Console.WriteLine("AjScript 0.1 (JavaScript-like Interpreter in C#)");

            for (ICommand cmd = parser.ParseCommand(); cmd != null; cmd = parser.ParseCommand())
                cmd.Execute(context);
        }
    }
}
