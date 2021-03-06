﻿using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mellis.Core.Entities;
using Mellis.Core.Exceptions;
using Mellis.Lang.Python3.Grammar;
using Mellis.Lang.Python3.Syntax;
using Mellis.Lang.Python3.Syntax.Statements;

namespace Mellis.Lang.Python3.Tests.SyntaxConstructor
{
    public abstract class BaseVisitClass<TContextType> : BaseVisitClass where TContextType : ParserRuleContext
    {
        // ReSharper disable InconsistentNaming
        protected Mock<TContextType> contextMock;
        // ReSharper restore InconsistentNaming

        [TestInitialize]
        public override void TestInitialize()
        {
            contextMock = GetMockRule<TContextType>();

            base.TestInitialize();
        }

        public abstract SyntaxNode VisitContext();

        [TestMethod]
        public virtual void Visit_InvalidToken_Test()
        {
            // Arrange
            var unexpectedNode = GetTerminal(Python3Parser.ASYNC);
            contextMock.SetupChildren(
                unexpectedNode
            );

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>(VisitContext,
                message: $"expected throw for context `{Python3Parser.ruleNames[contextMock.Object.RuleIndex]}`");

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, contextMock, unexpectedNode);
            contextMock.VerifyLoopedChildren(1);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public virtual void Visit_InvalidRule_Test()
        {
            // Arrange
            var unexpectedRule = GetMockRule<Python3Parser.File_inputContext>();

            unexpectedRule.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                unexpectedRule.Object
            );

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>(VisitContext,
                message: $"expected throw for context `{Python3Parser.ruleNames[contextMock.Object.RuleIndex]}`");

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, startTokenMock, stopTokenMock, contextMock,
                unexpectedRule.Object);
            contextMock.VerifyLoopedChildren(1);

            unexpectedRule.Verify();
            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public virtual void Visit_NoChildren_Test()
        {
            // Arrange
            contextMock.SetupForSourceReference(startTokenMock, stopTokenMock);
            contextMock.SetupChildren();

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>(VisitContext,
                message: $"expected throw for context `{Python3Parser.ruleNames[contextMock.Object.RuleIndex]}`");

            Assert.That.ErrorExpectedChildFormatArgs(ex, startTokenMock, stopTokenMock, contextMock);

            contextMock.Verify();
            ctorMock.Verify();
        }
    }

    public class BaseVisitClass
    {
        // ReSharper disable InconsistentNaming
        protected Mock<Grammar.SyntaxConstructor> ctorMock;
        protected Grammar.SyntaxConstructor ctor;
        protected Mock<IToken> startTokenMock;

        protected Mock<IToken> stopTokenMock;
        // ReSharper restore InconsistentNaming

        public static Mock<T> GetMockRule<T>() where T : ParserRuleContext
        {
            return new Mock<T>(ParserRuleContext.EmptyContext, 0) {CallBase = true};
        }

        public static ITerminalNode GetTerminal(int symbol)
        {
            var mock = new Mock<ITerminalNode>();
            mock.Setup(o => o.Symbol).Returns(GetSymbol(symbol));
            return mock.Object;
        }

        public static ITerminalNode GetTerminal(int symbol, string text)
        {
            var mock = new Mock<ITerminalNode>();
            mock.Setup(o => o.Symbol).Returns(GetSymbol(symbol, text));
            return mock.Object;
        }

        public static ITerminalNode GetMissingTerminal(int symbol)
        {
            var mock = new Mock<ITerminalNode>();
            mock.Setup(o => o.Symbol).Returns(GetMissingSymbol(symbol));
            return mock.Object;
        }

        public static IToken GetSymbol(int symbol)
        {
            return GetSymbol(symbol, Python3Parser.DefaultVocabulary
                .GetLiteralName(symbol)?.Trim('\''));
        }

        public static IToken GetSymbol(int symbol, string text)
        {
            var mock = new Mock<IToken>(MockBehavior.Strict);
            mock.SetupGet(o => o.Type).Returns(symbol);
            mock.SetupGet(o => o.Text).Returns(text);
            mock.SetupGet(o => o.Line).Returns(5);
            mock.SetupGet(o => o.Column).Returns(6);
            mock.SetupGet(o => o.StartIndex).Returns(10);
            // 9 if null, because stopindex is inclusive it's
            // 1 less than start if it's zero-width
            mock.SetupGet(o => o.StopIndex).Returns(10 + text?.Length - 1 ?? 9);
            return mock.Object;
        }

        public static IToken GetMissingSymbol(int symbol)
        {
            string name = Python3Parser.DefaultVocabulary.GetLiteralName(symbol)
                          ?? Python3Parser.DefaultVocabulary.GetSymbolicName(symbol)
                              .ToLowerInvariant();

            var mock = new Mock<IToken>(MockBehavior.Strict);
            mock.SetupGet(o => o.Type).Returns(symbol);
            mock.SetupGet(o => o.Text).Returns($"<missing {name}>");
            mock.SetupGet(o => o.Line).Returns(7);
            mock.SetupGet(o => o.Column).Returns(8);
            mock.SetupGet(o => o.StartIndex).Returns(-1); // !important
            mock.SetupGet(o => o.StopIndex).Returns(-1);
            return mock.Object;
        }

        public static Statement GetStatementMock()
        {
            return new Mock<Statement>(MockBehavior.Strict, SourceReference.ClrSource).Object;
        }

        public static Statement GetAssignmentMock()
        {
            return new Mock<Assignment>(MockBehavior.Strict, SourceReference.ClrSource,
                GetExpressionMock(), GetExpressionMock()).Object;
        }

        public static StatementList GetStatementList(int count)
        {
            return new StatementList(SourceReference.ClrSource,
                new byte[count].Select(_ => GetStatementMock()).ToArray());
        }

        public static ExpressionNode GetExpressionMock(SourceReference source)
        {
            return new Mock<ExpressionNode>(MockBehavior.Strict, source).Object;
        }

        public static ExpressionNode GetExpressionMock()
        {
            return GetExpressionMock(SourceReference.ClrSource);
        }

        public static Identifier GetIdentifierMock(string name, SourceReference source)
        {
            return new Mock<Identifier>(MockBehavior.Strict, source, name).Object;
        }

        public static Identifier GetIdentifierMock(string name)
        {
            return GetIdentifierMock(name, SourceReference.ClrSource);
        }

        public static ArgumentsList GetArgumentsListMock(params ExpressionNode[] arguments)
        {
            return new Mock<ArgumentsList>(
                    MockBehavior.Strict,
                    SourceReference.ClrSource,
                    arguments)
                .Object;
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            ctorMock = new Mock<Grammar.SyntaxConstructor>
            {
                CallBase = true
            };
            ctor = ctorMock.Object;

            startTokenMock = new Mock<IToken>();
            startTokenMock.SetupGet(o => o.Line).Returns(1);
            startTokenMock.SetupGet(o => o.Column).Returns(2);
            stopTokenMock = new Mock<IToken>();
            stopTokenMock.SetupGet(o => o.Line).Returns(3);
            stopTokenMock.SetupGet(o => o.Column).Returns(4);
        }
    }
}