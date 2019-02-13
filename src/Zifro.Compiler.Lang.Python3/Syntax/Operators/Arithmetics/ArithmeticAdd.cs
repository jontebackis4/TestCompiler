﻿using Zifro.Compiler.Lang.Python3.Instructions;

namespace Zifro.Compiler.Lang.Python3.Syntax.Operators.Arithmetics
{
    public class ArithmeticAdd : BinaryOperator
    {
        public override OperatorCode OpCode => OperatorCode.AAdd;

        public ArithmeticAdd(
            ExpressionNode leftOperand,
            ExpressionNode rightOperand)
            : base(leftOperand, rightOperand)
        {
        }
    }
}