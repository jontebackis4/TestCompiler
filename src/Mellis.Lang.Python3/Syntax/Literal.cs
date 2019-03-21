﻿using Mellis.Core.Entities;
using Mellis.Core.Interfaces;

namespace Mellis.Lang.Python3.Syntax
{
    public abstract class Literal<T> : ExpressionNode
    {
        protected Literal(SourceReference source, T value)
            : base(source)
        {
            Value = value;
        }

        public T Value { get; }

        public abstract string GetTypeName();

        public abstract IScriptType ToScriptType(VM.PyProcessor processor);
    }
}