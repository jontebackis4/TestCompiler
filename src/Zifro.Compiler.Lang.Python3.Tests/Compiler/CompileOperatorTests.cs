﻿using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zifro.Compiler.Core.Entities;
using Zifro.Compiler.Core.Interfaces;
using Zifro.Compiler.Lang.Python3.Entities;
using Zifro.Compiler.Lang.Python3.Instructions;
using Zifro.Compiler.Lang.Python3.Syntax;
using Zifro.Compiler.Lang.Python3.Syntax.Literals;
using Zifro.Compiler.Lang.Python3.Syntax.Operators;
using Zifro.Compiler.Lang.Python3.Syntax.Operators.Arithmetics;
using Zifro.Compiler.Lang.Python3.Syntax.Operators.Binaries;
using Zifro.Compiler.Lang.Python3.Syntax.Operators.Comparisons;
using Zifro.Compiler.Lang.Python3.Syntax.Operators.Logicals;

namespace Zifro.Compiler.Lang.Python3.Tests.Compiler
{
    [TestClass]
    public class CompileOperatorTests
    {
        [DataTestMethod]
        [DataRow(typeof(ArithmeticAdd), OperatorCode.AAdd, DisplayName = "comp op +")]
        [DataRow(typeof(ArithmeticSubtract), OperatorCode.ASub, DisplayName = "comp op -")]
        [DataRow(typeof(ArithmeticMultiply), OperatorCode.AMul, DisplayName = "comp op *")]
        [DataRow(typeof(ArithmeticDivide), OperatorCode.ADiv, DisplayName = "comp op /")]
        [DataRow(typeof(ArithmeticFloor), OperatorCode.AFlr, DisplayName = "comp op //")]
        [DataRow(typeof(ArithmeticModulus), OperatorCode.AMod, DisplayName = "comp op %")]
        [DataRow(typeof(ArithmeticPower), OperatorCode.APow, DisplayName = "comp op **")]

        [DataRow(typeof(BinaryAnd), OperatorCode.BAnd, DisplayName = "comp op a&b")]
        [DataRow(typeof(BinaryLeftShift), OperatorCode.BLsh, DisplayName = "comp op a<<b")]
        [DataRow(typeof(BinaryRightShift), OperatorCode.BRsh, DisplayName = "comp op a>>b")]
        [DataRow(typeof(BinaryOr), OperatorCode.BOr, DisplayName = "comp op a|b")]
        [DataRow(typeof(BinaryXor), OperatorCode.BXor, DisplayName = "comp op a^b")]

        [DataRow(typeof(CompareEquals), OperatorCode.CEq, DisplayName = "comp op a==b")]
        //[DataRow(typeof(CNEq), OperatorCode.CNEq, DisplayName = "comp op a!=b")]
        //[DataRow(typeof(CGt), OperatorCode.CGt, DisplayName = "comp op a>b")]
        //[DataRow(typeof(CGtEq), OperatorCode.CGtEq, DisplayName = "comp op a>=b")]
        //[DataRow(typeof(CLt), OperatorCode.CLt, DisplayName = "comp op a<b")]
        //[DataRow(typeof(CLtEq), OperatorCode.CLtEq, DisplayName = "comp op a<=b")]

        [DataRow(typeof(LogicalAnd), OperatorCode.LAnd, DisplayName = "comp op a&&b")]
        [DataRow(typeof(LogicalOr), OperatorCode.LOr, DisplayName = "comp op a||b")]
        public void CompileBinaryTests(Type operatorType, OperatorCode expectedCode)
        {
            // Arrange
            var compiler = new PyCompiler();

            var exprMock = new Mock<ExpressionNode>(SourceReference.ClrSource);
            exprMock.Setup(o => o.Compile(compiler))
                .Verifiable();

            var opNode = (BinaryOperator)Activator.CreateInstance(operatorType,
                exprMock.Object, exprMock.Object);

            // Act
            opNode.Compile(compiler);

            // Assert
            Assert.That.IsBinaryOpCode(expectedCode, compiler, index: 0);
            Assert.AreEqual(1, compiler.Count);

            exprMock.Verify(o => o.Compile(compiler), Times.Exactly(2));
        }

        [DataTestMethod]
        [DataRow(typeof(ArithmeticNegative), OperatorCode.ANeg, DisplayName = "comp op +b")]
        [DataRow(typeof(ArithmeticPositive), OperatorCode.APos, DisplayName = "comp op -b")]
        [DataRow(typeof(BinaryNot), OperatorCode.BNot, DisplayName = "comp op ~b")]
        [DataRow(typeof(LogicalNot), OperatorCode.LNot, DisplayName = "comp op !b")]
        public void CompileUnaryTests(Type operatorType, OperatorCode expectedCode)
        {
            // Arrange
            var compiler = new PyCompiler();

            var exprMock = new Mock<ExpressionNode>(SourceReference.ClrSource);
            exprMock.Setup(o => o.Compile(compiler))
                .Verifiable();

            var opNode = (UnaryOperator)Activator.CreateInstance(operatorType,
                SourceReference.ClrSource,
                exprMock.Object);

            // Act
            opNode.Compile(compiler);

            // Assert
            Assert.That.IsBinaryOpCode(expectedCode, compiler, index: 0);
            Assert.AreEqual(1, compiler.Count);

            exprMock.Verify(o => o.Compile(compiler), Times.Once);
        }

    }
}