﻿using System;
using Antlr4.Runtime.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mellis.Core.Exceptions;
using Mellis.Lang.Python3.Exceptions;
using Mellis.Lang.Python3.Grammar;
using Mellis.Lang.Python3.Syntax;
using Mellis.Lang.Python3.Syntax.Statements;

// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_Elsewhere

namespace Mellis.Lang.Python3.Tests.SyntaxConstructor
{
    [TestClass]
    public class VisitExprStmtTests : BaseVisitClass
    {
        [TestInitialize]
        public void TestInitializeExprStmt()
        {
            ctorMock.Setup(o => o.VisitTestlist_star_expr(It.IsAny<Python3Parser.Testlist_star_exprContext>()))
                .Returns(GetExpressionMock());
        }

        [TestMethod]
        public void Visit_BasicAssignment_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();
            contextMock.SetupForSourceReference(startTokenMock, stopTokenMock);

            var lhsMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            var rhsMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();

            contextMock.SetupChildren(
                lhsMock.Object,
                GetTerminal(Python3Parser.ASSIGN),
                rhsMock.Object
            );

            // Act
            SyntaxNode result = ctor.VisitExpr_stmt(contextMock.Object);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Assignment));
            contextMock.VerifyLoopedChildren(3);

            ctorMock.Verify(o => o.VisitTestlist_star_expr(lhsMock.Object), Times.Once);
            ctorMock.Verify(o => o.VisitTestlist_star_expr(rhsMock.Object), Times.Once);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_MultiAssignment_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var testListMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();

            ITerminalNode secondEqual = GetTerminal(Python3Parser.ASSIGN);

            contextMock.SetupChildren(
                testListMock.Object,
                GetTerminal(Python3Parser.ASSIGN),
                testListMock.Object,
                secondEqual,
                testListMock.Object
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>((Action) Action);

            Assert.That.ErrorNotYetImplFormatArgs(ex, secondEqual, "=");
            // Should throw at 4th
            contextMock.VerifyLoopedChildren(4);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_Empty_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();
            contextMock.SetupForSourceReference(startTokenMock, stopTokenMock);
            contextMock.SetupChildren();

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>((Action) Action);

            Assert.That.ErrorExpectedChildFormatArgs(ex, startTokenMock, stopTokenMock, contextMock);
            contextMock.VerifyLoopedChildren(0);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_InvalidOrdering_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var testListMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            var secondTestListMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            secondTestListMock.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                testListMock.Object,
                secondTestListMock.Object,
                GetTerminal(Python3Parser.ASSIGN)
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>((Action) Action);

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, startTokenMock, stopTokenMock, contextMock,
                secondTestListMock.Object);
            // Should throw at 2nd
            contextMock.VerifyLoopedChildren(2);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_AugmentedAssignment_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var testListStarMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            var augAssignMock = GetMockRule<Python3Parser.AugassignContext>();
            var testListMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();

            var addAssign = GetTerminal(Python3Parser.ADD_ASSIGN);
            augAssignMock.SetupChildren(addAssign);
            augAssignMock.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                testListStarMock.Object,
                augAssignMock.Object,
                testListMock.Object
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>((Action) Action);

            Assert.That.ErrorNotYetImplFormatArgs(ex, startTokenMock, stopTokenMock, "+=");
            // Should throw at 2nd
            contextMock.VerifyLoopedChildren(2);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_AnnotatedAssignment_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var testListStarMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            var annAssignMock = GetMockRule<Python3Parser.AnnassignContext>();
            annAssignMock.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                testListStarMock.Object,
                annAssignMock.Object
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>((Action) Action);

            Assert.That.ErrorNotYetImplFormatArgs(ex, startTokenMock, stopTokenMock, ":");
            contextMock.VerifyLoopedChildren(2);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_YieldExpression_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var testListStarMock = GetMockRule<Python3Parser.Testlist_star_exprContext>();
            var yieldExprMock = GetMockRule<Python3Parser.Yield_exprContext>();
            yieldExprMock.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                testListStarMock.Object,
                yieldExprMock.Object
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxNotYetImplementedExceptionKeyword>((Action) Action);

            Assert.That.ErrorNotYetImplFormatArgs(ex, startTokenMock, stopTokenMock, "yield");
            contextMock.VerifyLoopedChildren(2);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_OnlyAssignmentToken_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            ITerminalNode assignNode = GetTerminal(Python3Parser.ASSIGN);

            contextMock.SetupChildren(
                assignNode
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>((Action) Action);

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, contextMock, assignNode);
            contextMock.VerifyLoopedChildren(1);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_InvalidToken_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            ITerminalNode assignNode = GetTerminal(Python3Parser.ASYNC);

            contextMock.SetupChildren(
                assignNode
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>((Action) Action);

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, contextMock, assignNode);
            contextMock.VerifyLoopedChildren(1);

            contextMock.Verify();
            ctorMock.Verify();
        }

        [TestMethod]
        public void Visit_InvalidContext_Test()
        {
            // Arrange
            var contextMock = GetMockRule<Python3Parser.Expr_stmtContext>();

            var innerMocker = GetMockRule<Python3Parser.File_inputContext>();
            innerMocker.SetupForSourceReference(startTokenMock, stopTokenMock);

            contextMock.SetupChildren(
                innerMocker.Object
            );

            void Action() { ctor.VisitExpr_stmt(contextMock.Object); }

            // Act + Assert
            var ex = Assert.ThrowsException<SyntaxException>((Action) Action);

            Assert.That.ErrorUnexpectedChildTypeFormatArgs(ex, startTokenMock, stopTokenMock, contextMock, innerMocker.Object);
            contextMock.VerifyLoopedChildren(1);

            contextMock.Verify();
            ctorMock.Verify();
        }
    }
}