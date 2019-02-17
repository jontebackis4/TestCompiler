﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mellis.Core.Entities;
using Mellis.Core.Exceptions;
using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Instructions;
using Mellis.Lang.Python3.Resources;
using Mellis.Lang.Python3.Tests.TestingOps;

namespace Mellis.Lang.Python3.Tests.Processor
{
    [TestClass]
    public class VarGetEvaluateTests
    {
        [TestMethod]
        public void EvaluateVarGetTest()
        {
            // Arrange
            const string identifier = "foo";
            var processor = new PyProcessor(
                new VarGet(SourceReference.ClrSource, identifier)
            );

            var value = Mock.Of<IScriptType>();

            var globalScope = (PyScope) processor.GlobalScope;
            globalScope.SetVariable(identifier, value);

            // Act
            processor.WalkLine();
            int numOfValues = processor.ValueStackCount;

            // Assert
            Assert.AreEqual(1, numOfValues, "Did not push value.");

            var result = processor.PopValue();
            Assert.AreSame(value, result);
        }

        [TestMethod]
        public void EvaluateVarGetGlobalScopeTest()
        {
            // Arrange
            const string identifier = "foo";
            var processor = new PyProcessor(
                new ScopePush(SourceReference.ClrSource),
                new VarGet(SourceReference.ClrSource, identifier),
                new ScopePop(SourceReference.ClrSource)
            );

            var value = Mock.Of<IScriptType>();

            var globalScope = (PyScope)processor.GlobalScope;
            globalScope.SetVariable(identifier, value);

            // Act
            processor.WalkLine();
            int numOfValues = processor.ValueStackCount;

            // Assert
            Assert.AreEqual(1, numOfValues, "Did not push value.");

            var result = processor.PopValue();
            Assert.AreSame(value, result);
        }
        
        [TestMethod]
        public void EvaluateVarGetLocalScopeTest()
        {
            // Arrange
            const string identifier = "foo";
            var processor = new PyProcessor(
                new ScopePush(SourceReference.ClrSource),
                new VarGet(SourceReference.ClrSource, identifier),
                new ScopePop(SourceReference.ClrSource)
            );

            var value = Mock.Of<IScriptType>();

            processor.WalkInstruction();

            var localScope = (PyScope)processor.CurrentScope;
            localScope.SetVariable(identifier, value);

            // Act
            processor.WalkLine();
            int numOfValues = processor.ValueStackCount;

            // Assert
            Assert.AreEqual(1, numOfValues, "Did not push value.");

            var result = processor.PopValue();
            Assert.AreSame(value, result);
        }

        [TestMethod]
        public void EvaluateVarGetNonExistingScopeTest()
        {
            // Arrange
            const string identifier = "foo";
            var processor = new PyProcessor(
                new ScopePush(SourceReference.ClrSource),
                new VarGet(SourceReference.ClrSource, identifier),
                new ScopePop(SourceReference.ClrSource)
            );

            // Act
            var ex = Assert.ThrowsException<RuntimeException>((Action) processor.WalkLine);

            // Assert
            Assert.That.ErrorFormatArgsEqual(ex,
                nameof(Localized_Python3_Runtime.Ex_Variable_NotDefined),
                identifier);
        }
    }
}