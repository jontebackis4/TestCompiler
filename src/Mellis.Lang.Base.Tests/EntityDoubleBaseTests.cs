﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mellis.Core.Interfaces;
using Mellis.Lang.Base.Entities;
using Mellis.Lang.Base.Resources;

namespace Mellis.Lang.Base.Tests
{
    [TestClass]
    public class EntityDoubleBaseTests : BaseTestClass
    {
        [TestMethod]
        public void DoubleAdditionWholeTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetDouble(10);

            // Act
            var result = a.ArithmeticAdd(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 15);
        }

        [TestMethod]
        public void DoubleAdditionFractionTest()
        {
            // Arrange
            var a = GetDouble(0.5);
            var b = GetDouble(1);

            // Act
            var result = a.ArithmeticAdd(b);

            // Assert
            AssertArithmeticResult<ScriptDouble>(result, a, b, 1.5);
        }

        [TestMethod]
        public void DoubleAdditionIntegerTest()
        {
            // Arrange
            var a = GetDouble(0.5);
            var b = GetInteger(1);

            // Act
            var result = a.ArithmeticAdd(b);

            // Assert
            AssertArithmeticResult<ScriptDouble>(result, a, b, 1.5);
        }

        [TestMethod]
        public void DoubleAdditionInvalidTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetString("foo");
            object[] expectedFormatArgs = {5d, b.GetTypeName()};
            void Action() { a.ArithmeticAdd(b); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_AddInvalidType), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleSubtractionWholeTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetDouble(10);

            // Act
            var result = a.ArithmeticSubtract(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, -5);
        }

        [TestMethod]
        public void DoubleSubtractionFractionTest()
        {
            // Arrange
            var a = GetDouble(.5);
            var b = GetDouble(.1);

            // Act
            var result = a.ArithmeticSubtract(b);

            // Assert
            AssertArithmeticResult<ScriptDouble>(result, a, b, .4);
        }

        [TestMethod]
        public void DoubleSubtractionIntegerTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetInteger(1);

            // Act
            var result = a.ArithmeticSubtract(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 4);
        }

        [TestMethod]
        public void DoubleSubtractionInvalidTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetString("foo");
            object[] expectedFormatArgs = {5d, b.GetTypeName()};
            void Action() { a.ArithmeticSubtract(b); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_SubtractInvalidType), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleMultiplicationTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetDouble(10);

            // Act
            var result = a.ArithmeticMultiply(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 50);
        }

        [TestMethod]
        public void DoubleMultiplicationIntegerTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetInteger(10);

            // Act
            var result = a.ArithmeticMultiply(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 50);
        }

        [TestMethod]
        public void DoubleMultiplicationInvalidTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetString("foo");
            object[] expectedFormatArgs = {5d, b.GetTypeName()};
            void Action() { a.ArithmeticMultiply(b); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_MultiplyInvalidType), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleDivideWholeTest()
        {
            // Arrange
            var a = GetDouble(50);
            var b = GetDouble(10);

            // Act
            var result = a.ArithmeticDivide(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 5);
        }

        [TestMethod]
        public void DoubleDivideFractionTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetDouble(10);

            // Act
            var result = a.ArithmeticDivide(b);

            // Assert
            AssertArithmeticResult<ScriptDouble>(result, a, b, .5);
        }
        
        [TestMethod]
        public void DoubleDivideIntegerTest()
        {
            // Arrange
            var a = GetDouble(50);
            var b = GetInteger(10);

            // Act
            var result = a.ArithmeticDivide(b);

            // Assert
            AssertArithmeticResult<ScriptInteger>(result, a, b, 5);
        }

        [TestMethod]
        public void DoubleDivideInvalidReturnsNullTest()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetString("foo");
            
            // Act
            var result = a.ArithmeticDivide(b);

            // Act + Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DoubleDivideByZero()
        {
            // Arrange
            var a = GetDouble(5);
            var b = GetDouble(0);
            object[] expectedFormatArgs = { };
            void Action() { a.ArithmeticDivide(b); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Math_DivideByZero), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleIndexGet()
        {
            // Arrange
            var a = GetDouble(5);
            object[] expectedFormatArgs = {5d};

            void Action() { a.GetIndex(null); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_IndexGet), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleIndexSet()
        {
            // Arrange
            var a = GetDouble(5);
            object[] expectedFormatArgs = {5d};

            void Action() { a.SetIndex(null, null); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_IndexSet), expectedFormatArgs);
        }

        [TestMethod]
        public void DoublePropertyGet()
        {
            // Arrange
            var a = GetDouble(5);
            const string property = "prop";
            object[] expectedFormatArgs = {5d, property};

            void Action() { a.GetProperty(property); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_PropertyGet), expectedFormatArgs);
        }

        [TestMethod]
        public void DoublePropertySet()
        {
            // Arrange
            var a = GetDouble(5);
            const string property = "prop";
            object[] expectedFormatArgs = {5d, property};

            void Action() { a.SetProperty(property, null); }

            // Act + Assert
            AssertThrow(Action, nameof(Localized_Base_Entities.Ex_Double_PropertySet), expectedFormatArgs);
        }

        [TestMethod]
        public void DoubleToStringSpecial()
        {
            // Arrange
            var posInf = GetDouble(double.PositiveInfinity);
            var negInf = GetDouble(double.NegativeInfinity);
            var nan = GetDouble(double.NaN);

            // Act
            string posInfStr = posInf.ToString();
            string negInfStr = negInf.ToString();
            string nanStr = nan.ToString();

            // Assert
            Assert.AreEqual(Localized_Base_Entities.Type_Double_PosInfinity, posInfStr);
            Assert.AreEqual(Localized_Base_Entities.Type_Double_NegInfinity, negInfStr);
            Assert.AreEqual(Localized_Base_Entities.Type_Double_NaN, nanStr);
        }
    }
}