﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zifro.Compiler.Lang.Python3.Tests.TestingOps;

namespace Zifro.Compiler.Lang.Python3.Tests.Compiler
{
    [TestClass]
    public class CompilerTests
    {
        [TestMethod]
        public void PushNullTest()
        {
            // Arrange
            var compiler = new PyCompiler();

            void Action()
            {
                compiler.Push(null); 
            }

            // Act
            Assert.ThrowsException<ArgumentNullException>((Action) Action);
        }

        [TestMethod]
        public void PushOneTest()
        {
            // Arrange
            var compiler = new PyCompiler();

            // Act
            compiler.Push(new NopOp());

            // Assert
            Assert.AreEqual(1, compiler.Count);
        }

        [TestMethod]
        public void PushManyTest()
        {
            // Arrange
            var compiler = new PyCompiler();
            var op1 = new NopOp();
            var op2 = new NopOp();
            var op3 = new NopOp();

            // Act
            compiler.Push(op1);
            compiler.Push(op2);
            compiler.Push(op3);

            // Assert
            Assert.AreEqual(3, compiler.Count);
            Assert.AreSame(op1, compiler[0]);
            Assert.AreSame(op2, compiler[1]);
            Assert.AreSame(op3, compiler[2]);
        }

        [TestMethod]
        public void PushRangeTest()
        {
            // Arrange
            var compiler = new PyCompiler();
            var op1 = new NopOp();
            var op2 = new NopOp();
            var op3 = new NopOp();

            // Act
            compiler.PushRange(new []
            {
                op1,
                op2,
                op3
            });

            // Assert
            Assert.AreEqual(3, compiler.Count);
            Assert.AreSame(op1, compiler[0]);
            Assert.AreSame(op2, compiler[1]);
            Assert.AreSame(op3, compiler[2]);
        }

        [TestMethod]
        public void PushNullRangeTest()
        {
            // Arrange
            var compiler = new PyCompiler();

            // Act
            void Action()
            {
                compiler.PushRange(null);
            }

            // Act
            Assert.ThrowsException<ArgumentNullException>((Action)Action);
        }

        [TestMethod]
        public void PushRangeOneNullTest()
        {
            // Arrange
            var compiler = new PyCompiler();

            // Act
            void Action()
            {
                compiler.PushRange(new []
                {
                    new NopOp(),
                    null,
                    new NopOp(), 
                });
            }

            // Act
            Assert.ThrowsException<ArgumentNullException>((Action)Action);
        }
    }
}