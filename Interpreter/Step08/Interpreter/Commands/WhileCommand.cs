﻿namespace Interpreter.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interpreter.Expressions;
    using Interpreter.Predicates;

    public class WhileCommand : ICommand
    {
        private IExpression condition;
        private ICommand command;

        public WhileCommand(IExpression condition, ICommand command)
        {
            this.condition = condition;
            this.command = command;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand Command { get { return this.command; } }

        public void Execute(BindingEnvironment environment)
        {
            while (BooleanPredicates.IsTrue(this.condition.Evaluate(environment)))
                this.command.Execute(environment);
        }
    }
}
