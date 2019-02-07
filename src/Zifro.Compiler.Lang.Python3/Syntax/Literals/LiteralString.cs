﻿using Zifro.Compiler.Core.Entities;

namespace Zifro.Compiler.Lang.Python3.Syntax.Literals
{
    public class LiteralString : Literal<string>
    {
        public LiteralString(SourceReference source, string value)
            : base(source, value)
        {
        }
    }
}