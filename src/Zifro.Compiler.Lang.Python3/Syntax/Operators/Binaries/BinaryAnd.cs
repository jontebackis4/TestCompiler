﻿namespace Zifro.Compiler.Lang.Python3.Syntax.Operators.Binaries
{
    public class BinaryAnd : BinaryOperator
    {
        public BinaryAnd(
            ExpressionNode leftOperand,
            ExpressionNode rightOperand)
            : base(leftOperand, rightOperand)
        {
        }
    }
}