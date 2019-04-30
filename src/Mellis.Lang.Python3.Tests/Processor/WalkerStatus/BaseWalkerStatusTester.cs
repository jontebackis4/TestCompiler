﻿using Mellis.Core.Entities;
using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Instructions;
using Mellis.Lang.Python3.Interfaces;
using Mellis.Lang.Python3.Tests.TestingOps;
using Mellis.Lang.Python3.VM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Mellis.Lang.Python3.Tests.Processor.WalkerStatus
{
    public abstract class BaseWalkerStatusTester
    {
        protected abstract WalkStatus WalkProcessor(PyProcessor processor);

        private class YieldingTestOp : IOpCode
        {
            public SourceReference Source { get; }
            public void Execute(PyProcessor processor)
            {
                processor.Yield(new YieldData(new IScriptType[0], Mock.Of<IClrYieldingFunction>()));
            }
        }

        [TestMethod]
        public void EndedStatusWalkOneOpTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new NopOp()
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Ended, status);
        }

        [TestMethod]
        public void YieldedStatusWhenYieldedTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new YieldingTestOp()
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Yielded, status);
        }

        [TestMethod]
        public void BreakStatusWhenBreakpointOpTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new Breakpoint(BreakCause.LoopEnter)
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Break, status);
        }

        [TestMethod]
        public virtual void BreakStatusOnJumpLimitReachedTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new CompilerSettings {
                    BreakOn = BreakCause.JumpLimitReached,
                    JumpLimit = 2
                },
                new Jump(SourceReference.ClrSource, 1),
                new Jump(SourceReference.ClrSource, 2),
                new Jump(SourceReference.ClrSource, 3)
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Break, status);
        }

        [TestMethod]
        public virtual void EndedStatusOnJumpLimitNotReachedTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new CompilerSettings {
                    BreakOn = BreakCause.JumpLimitReached,
                    JumpLimit = 4
                },
                new Jump(SourceReference.ClrSource, 1),
                new Jump(SourceReference.ClrSource, 2),
                new Jump(SourceReference.ClrSource, 3)
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Ended, status);
        }

        [TestMethod]
        public virtual void BreakStatusOnInstructionLimitReachedTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new CompilerSettings {
                    BreakOn = BreakCause.InstructionLimitReached,
                    InstructionLimit = 2
                },
                new NopOp(),
                new NopOp(),
                new NopOp()
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Break, status);
        }

        [TestMethod]
        public virtual void EndedStatusOnInstructionLimitNotReachedTest()
        {
            // Arrange
            var processor = new PyProcessor(
                new CompilerSettings {
                    BreakOn = BreakCause.InstructionLimitReached,
                    InstructionLimit = 4
                },
                new NopOp(),
                new NopOp(),
                new NopOp()
            );

            // Act
            processor.WalkInstruction(); // warmup
            var status = WalkProcessor(processor);

            // Assert
            Assert.AreEqual(WalkStatus.Ended, status);
        }
    }
}