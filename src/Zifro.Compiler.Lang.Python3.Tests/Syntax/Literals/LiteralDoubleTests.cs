﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zifro.Compiler.Core.Entities;
using Zifro.Compiler.Lang.Python3.Syntax.Literals;

namespace Zifro.Compiler.Lang.Python3.Tests.Syntax.Literals
{
    [TestClass]
    public class LiteralDoubleTests : BaseLiteralTests<LiteralDouble, double>
    {
        protected override LiteralDouble Parse(SourceReference source, string text)
        {
            return LiteralDouble.Parse(source, text);
        }

        [DataTestMethod]
        [DataRow("0", 0d)]
        [DataRow("10", 10d)]
        [DataRow("0x10", 0x10d)]
        [DataRow("0.1", 0.1d)]
        [DataRow("1e5", 1e5d)]
        public override void ParseValidTest(string input, double expectedValue)
        {
            base.ParseValidTest(input, expectedValue);
        }

        [DataTestMethod]
        [DataRow("0x-1")]
        [DataRow("1_0")]
        [DataRow("01.0")]
        [DataRow("1e0.1")]
        public override void ParseInvalidTest(string input)
        {
            base.ParseInvalidTest(input);
        }

        [DataRow("0b1")]
        [DataRow("0o1")]
        public override void ParseNotYetImplementedTest(string input)
        {
            base.ParseNotYetImplementedTest(input);
        }
    }
}