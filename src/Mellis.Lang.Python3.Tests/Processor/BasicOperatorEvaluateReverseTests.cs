﻿using System;
using System.Linq.Expressions;
using Mellis.Core.Entities;
using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Exceptions;
using Mellis.Lang.Python3.Extensions;
using Mellis.Lang.Python3.Instructions;
using Mellis.Lang.Python3.VM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Mellis.Lang.Python3.Tests.Processor
{
    [TestClass]
    public class BasicOperatorEvaluateReverseTests
    {
        #region Binary operators

        [TestMethod]
        public void EvaluateBinary_Add_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.AAdd,
                o => o.ArithmeticAdd(It.IsAny<IScriptType>()),
                o => o.ArithmeticAddReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Sub_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.ASub,
                o => o.ArithmeticSubtract(It.IsAny<IScriptType>()),
                o => o.ArithmeticSubtractReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Mul_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.AMul,
                o => o.ArithmeticMultiply(It.IsAny<IScriptType>()),
                o => o.ArithmeticMultiplyReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Div_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.ADiv,
                o => o.ArithmeticDivide(It.IsAny<IScriptType>()),
                o => o.ArithmeticDivideReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Flr_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.AFlr,
                o => o.ArithmeticFloorDivide(It.IsAny<IScriptType>()),
                o => o.ArithmeticFloorDivideReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Mod_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.AMod,
                o => o.ArithmeticModulus(It.IsAny<IScriptType>()),
                o => o.ArithmeticModulusReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_Pow_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.APow,
                o => o.ArithmeticExponent(It.IsAny<IScriptType>()),
                o => o.ArithmeticExponentReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_BAnd_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.BAnd,
                o => o.BinaryAnd(It.IsAny<IScriptType>()),
                o => o.BinaryAndReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_BLsh_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.BLsh,
                o => o.BinaryLeftShift(It.IsAny<IScriptType>()),
                o => o.BinaryLeftShiftReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_BRsh_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.BRsh,
                o => o.BinaryRightShift(It.IsAny<IScriptType>()),
                o => o.BinaryRightShiftReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_BOr_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.BOr,
                o => o.BinaryOr(It.IsAny<IScriptType>()),
                o => o.BinaryOrReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_BXor_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.BXor,
                o => o.BinaryXor(It.IsAny<IScriptType>()),
                o => o.BinaryXorReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CEq_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CEq,
                o => o.CompareEqual(It.IsAny<IScriptType>()),
                o => o.CompareEqual(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CNEq_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CNEq,
                o => o.CompareNotEqual(It.IsAny<IScriptType>()),
                o => o.CompareNotEqual(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CGt_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CGt,
                o => o.CompareGreaterThan(It.IsAny<IScriptType>()),
                o => o.CompareLessThan(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CGtEq_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CGtEq,
                o => o.CompareGreaterThanOrEqual(It.IsAny<IScriptType>()),
                o => o.CompareLessThanOrEqual(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CLt_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CLt,
                o => o.CompareLessThan(It.IsAny<IScriptType>()),
                o => o.CompareGreaterThan(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_CLtEq_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.CLtEq,
                o => o.CompareLessThanOrEqual(It.IsAny<IScriptType>()),
                o => o.CompareGreaterThanOrEqual(It.IsAny<IScriptType>()));
        }

        #endregion

        #region Binary in-place operators

        // As we don't have custom in-place operators (yet)
        // just use the default operators

        [TestMethod]
        public void EvaluateBinary_IAdd_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IAAdd,
                o => o.ArithmeticAdd(It.IsAny<IScriptType>()),
                o => o.ArithmeticAddReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_ISub_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IASub,
                o => o.ArithmeticSubtract(It.IsAny<IScriptType>()),
                o => o.ArithmeticSubtractReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IMul_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IAMul,
                o => o.ArithmeticMultiply(It.IsAny<IScriptType>()),
                o => o.ArithmeticMultiplyReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IDiv_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IADiv,
                o => o.ArithmeticDivide(It.IsAny<IScriptType>()),
                o => o.ArithmeticDivideReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IFlr_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IAFlr,
                o => o.ArithmeticFloorDivide(It.IsAny<IScriptType>()),
                o => o.ArithmeticFloorDivideReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IMod_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IAMod,
                o => o.ArithmeticModulus(It.IsAny<IScriptType>()),
                o => o.ArithmeticModulusReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IPow_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IAPow,
                o => o.ArithmeticExponent(It.IsAny<IScriptType>()),
                o => o.ArithmeticExponentReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IBAnd_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IBAnd,
                o => o.BinaryAnd(It.IsAny<IScriptType>()),
                o => o.BinaryAndReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IBLsh_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IBLsh,
                o => o.BinaryLeftShift(It.IsAny<IScriptType>()),
                o => o.BinaryLeftShiftReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IBRsh_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IBRsh,
                o => o.BinaryRightShift(It.IsAny<IScriptType>()),
                o => o.BinaryRightShiftReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IBOr_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IBOr,
                o => o.BinaryOr(It.IsAny<IScriptType>()),
                o => o.BinaryOrReverse(It.IsAny<IScriptType>()));
        }

        [TestMethod]
        public void EvaluateBinary_IBXor_Test()
        {
            EvaluateBinaryReversedTestTemplate(BasicOperatorCode.IBXor,
                o => o.BinaryXor(It.IsAny<IScriptType>()),
                o => o.BinaryXorReverse(It.IsAny<IScriptType>()));
        }

        #endregion

        private static void EvaluateBinaryReversedTestTemplate(
            BasicOperatorCode opCode,
            Expression<Func<IScriptType, IScriptType>> method,
            Expression<Func<IScriptType, IScriptType>> reversedMethod)
        {
            // Arrange
            var basicOperator = new BasicOperator(SourceReference.ClrSource, opCode);

            var lhsMock = new Mock<IScriptType>(MockBehavior.Strict);
            var rhsMock = new Mock<IScriptType>(MockBehavior.Strict);
            var resultMock = new Mock<IScriptType>();

            lhsMock.Setup(method).Returns((IScriptType)null);
            rhsMock.Setup(reversedMethod).Returns(resultMock.Object);

            var processor = new PyProcessor();
            processor.PushValue(lhsMock.Object);
            processor.PushValue(rhsMock.Object);

            // Act
            basicOperator.Execute(processor);

            int numOfValues = processor.ValueStackCount;
            var result = processor.PopValue();

            // Assert
            Assert.AreEqual(1, numOfValues, "Did not absorb values.");
            Assert.AreSame(resultMock.Object, result, "Did not produce result.");
            rhsMock.Verify(reversedMethod, Times.Once);
            lhsMock.Verify(method, Times.Once);
        }

        [DataTestMethod]
        // Binary operators (lhs op rhs)
        [DataRow(BasicOperatorCode.CIn, "in", DisplayName = "nyi in")]
        [DataRow(BasicOperatorCode.CNIn, "not in", DisplayName = "nyi not in")]
        [DataRow(BasicOperatorCode.CIs, "is", DisplayName = "nyi is")]
        [DataRow(BasicOperatorCode.CIsN, "is not", DisplayName = "nyi is not")]
        [DataRow(BasicOperatorCode.AMat, "@", DisplayName = "nyi @")]
        [DataRow(BasicOperatorCode.IAMat, "@=", DisplayName = "nyi @=")]
        public void EvaluateBinaryReversed_NotYetImplemented_Tests(BasicOperatorCode opCode, string expectedKeyword)
        {
            // Arrange
            var source = new SourceReference(1, 2, 3, 4);
            var basicOperator = new BasicOperator(source, opCode);

            var lhsMock = new Mock<IScriptType>(MockBehavior.Loose); // important Loose
            var rhsMock = new Mock<IScriptType>(MockBehavior.Strict);

            var processor = new PyProcessor();
            processor.PushValue(lhsMock.Object);
            processor.PushValue(rhsMock.Object);

            // Act
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>(delegate
            {
                basicOperator.Execute(processor);
            });

            // Assert
            Assert.That.ErrorNotYetImplFormatArgs(ex, source, expectedKeyword);
        }
    }
}