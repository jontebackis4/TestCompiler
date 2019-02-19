﻿using System;
using Mellis.Core.Entities;
using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Interfaces;

namespace Mellis.Lang.Python3.Instructions
{
    public class JumpIfFalse : Jump
    {
        public JumpIfFalse(SourceReference source, Label label)
            : base(source, label)
        {
        }

        public override void Execute(PyProcessor processor)
        {
            if (Label.OpCodeIndex == -1)
                throw new InvalidOperationException("Label was not assigned an index. Are you sure it was added to the processor?");

            var value = processor.PopValue();

            if (!value.IsTruthy())
            {
                processor.JumpToInstruction(Label.OpCodeIndex);
            }
        }

        public override string ToString()
        {
            return Label?.OpCodeIndex >= 0
                ? $"jmpifn->@{Label.OpCodeIndex}"
                : "jmpifn->{undefined}";
        }
    }
}