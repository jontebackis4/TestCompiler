﻿using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zifro.Compiler.Core.Entities;
using Zifro.Compiler.Core.Exceptions;
using Zifro.Compiler.Core.Interfaces;
using Zifro.Compiler.Lang.Python3.Exceptions;
using Zifro.Compiler.Lang.Python3.Extensions;
using Zifro.Compiler.Lang.Python3.Instructions;

namespace Zifro.Compiler.Lang.Python3.Tests.Processor
{
    [TestClass]
    public class BasicOperatorEvaluateTests
    {
        [TestMethod]
        public void EvaluateBinary_Add_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.AAdd,
                o => o.ArithmeticAdd(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Sub_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.ASub,
                o => o.ArithmeticSubtract(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Mul_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.AMul,
                o => o.ArithmeticMultiply(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Div_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.ADiv,
                o => o.ArithmeticDivide(It.IsAny<IScriptType>()));
        }


        [TestMethod]
        public void EvaluateBinary_Flr_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.AFlr,
                o => o.ArithmeticFloorDivide(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Mod_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.AMod,
                o => o.ArithmeticModulus(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Pow_Test()
        {
            EvaluateBinaryTestTemplate(OperatorCode.APow,
                o => o.ArithmeticExponent(It.IsAny<IScriptType>()));
        }

        [DataTestMethod]
        // Binary operators (lhs op rhs)
        [DataRow(OperatorCode.BAnd, "&", DisplayName = "nyi &")]
        [DataRow(OperatorCode.BLsh, "<<", DisplayName = "nyi <<")]
        [DataRow(OperatorCode.BRsh, ">>", DisplayName = "nyi >>")]
        [DataRow(OperatorCode.BOr, "|", DisplayName = "nyi |")]
        [DataRow(OperatorCode.BXor, "^", DisplayName = "nyi ^")]

        [DataRow(OperatorCode.CEq, "==", DisplayName = "nyi ==")]
        [DataRow(OperatorCode.CNEq, "!=", DisplayName = "nyi !=")]
        [DataRow(OperatorCode.CGt, ">", DisplayName = "nyi >")]
        [DataRow(OperatorCode.CGtEq, ">=", DisplayName = "nyi >=")]
        [DataRow(OperatorCode.CLt, "<", DisplayName = "nyi <")]
        [DataRow(OperatorCode.CLtEq, "<=", DisplayName = "nyi <=")]

        [DataRow(OperatorCode.LAnd, "&&", DisplayName = "nyi &&")]
        [DataRow(OperatorCode.LOr, "||", DisplayName = "nyi ||")]

        // Unary operators (op rhs)
        [DataRow(OperatorCode.ANeg, "+", DisplayName = "nyi +")]
        [DataRow(OperatorCode.APos, "-", DisplayName = "nyi -")]
        [DataRow(OperatorCode.BNot, "~", DisplayName = "nyi ~")]
        [DataRow(OperatorCode.LNot, "!", DisplayName = "nyi !")]
        public void EvaluateBinary_NotYetImplemented_Tests(OperatorCode opCode, string expectedKeyword)
        {
            // Arrange
            var source = new SourceReference(1,2,3,4);
            var processor = new PyProcessor(
                new BasicOperator(source, opCode)
            );

            var lhsMock = new Mock<IScriptType>(MockBehavior.Strict);
            var rhsMock = new Mock<IScriptType>(MockBehavior.Strict);

            processor.PushValue(lhsMock.Object);
            processor.PushValue(rhsMock.Object);

            // Act
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>((Action) processor.WalkLine);

            // Assert
            Assert.IsNotNull(processor.LastError);
            Assert.AreEqual(ProcessState.Error, processor.State);

            Assert.That.ErrorNotYetImplFormatArgs(ex, source, expectedKeyword);
        }

        protected void EvaluateBinaryTestTemplate(OperatorCode opCode, Expression<Func<IScriptType, IScriptType>> method)
        {
            // Arrange
            var processor = new PyProcessor(
                new BasicOperator(SourceReference.ClrSource, opCode)
            );

            var lhsMock = new Mock<IScriptType>(MockBehavior.Strict);
            var rhsMock = new Mock<IScriptType>(MockBehavior.Strict);
            var resultMock = new Mock<IScriptType>();

            lhsMock.Setup(method).Returns(resultMock.Object);

            processor.PushValue(lhsMock.Object);
            processor.PushValue(rhsMock.Object);

            // Act
            processor.WalkLine();
            int numOfValues = processor.ValueStackCount;
            var result = processor.PopValue<IScriptType>();

            // Assert
            Assert.IsNull(processor.LastError, "Last error <{0}>:{1}", processor.LastError?.GetType().Name, processor.LastError?.Message);

            Assert.AreEqual(1, numOfValues, "Did not absorb values.");
            Assert.AreSame(resultMock.Object, result, "Did not produce result.");
            lhsMock.Verify(method);
        }

        [DataTestMethod]
        [DataRow(OperatorCode.AAdd, DisplayName = "is bin op a+b")]
        [DataRow(OperatorCode.ASub, DisplayName = "is bin op a-b")]
        [DataRow(OperatorCode.AMul, DisplayName = "is bin op a*b")]
        [DataRow(OperatorCode.ADiv, DisplayName = "is bin op a/b")]
        [DataRow(OperatorCode.AFlr, DisplayName = "is bin op a//b")]
        [DataRow(OperatorCode.AMod, DisplayName = "is bin op a%b")]
        [DataRow(OperatorCode.APow, DisplayName = "is bin op a**b")]

        [DataRow(OperatorCode.BAnd, DisplayName = "is bin op a&b")]
        [DataRow(OperatorCode.BLsh, DisplayName = "is bin op a<<b")]
        [DataRow(OperatorCode.BRsh, DisplayName = "is bin op a>>b")]
        [DataRow(OperatorCode.BOr, DisplayName = "is bin op a|b")]
        [DataRow(OperatorCode.BXor, DisplayName = "is bin op a^b")]

        [DataRow(OperatorCode.CEq, DisplayName = "is bin op a==b")]
        [DataRow(OperatorCode.CNEq, DisplayName = "is bin op a!=b")]
        [DataRow(OperatorCode.CGt, DisplayName = "is bin op a>b")]
        [DataRow(OperatorCode.CGtEq, DisplayName = "is bin op a>=b")]
        [DataRow(OperatorCode.CLt, DisplayName = "is bin op a<b")]
        [DataRow(OperatorCode.CLtEq, DisplayName = "is bin op a<=b")]

        [DataRow(OperatorCode.LAnd, DisplayName = "is bin op a&&b")]
        [DataRow(OperatorCode.LOr, DisplayName = "is bin op a||b")]
        public void IsBinaryTests(OperatorCode code)
        {
            // Act
            bool isBinary = code.IsBinary();
            bool isUnary = code.IsUnary();

            // Assert
            Assert.IsTrue(isBinary, $"OperatorCode.{code}.IsBinary() was false");
            Assert.IsFalse(isUnary, $"OperatorCode.{code}.IsUnary() was true");
        }

        [DataTestMethod]
        [DataRow(OperatorCode.ANeg, DisplayName = "is un op +a")]
        [DataRow(OperatorCode.APos, DisplayName = "is un op -a")]
        [DataRow(OperatorCode.BNot, DisplayName = "is un op ~a")]
        [DataRow(OperatorCode.LNot, DisplayName = "is un op !a")]
        public void IsUnaryTests(OperatorCode code)
        {
            // Act
            bool isBinary = code.IsBinary();
            bool isUnary = code.IsUnary();

            // Assert
            Assert.IsFalse(isBinary, $"OperatorCode.{code}.IsBinary() was true");
            Assert.IsTrue(isUnary, $"OperatorCode.{code}.IsUnary() was false");
        }
    }
}