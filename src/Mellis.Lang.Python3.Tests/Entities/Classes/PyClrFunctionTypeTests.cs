﻿using System;
using Mellis.Core.Exceptions;
using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Entities.Classes;
using Mellis.Lang.Python3.Resources;
using Mellis.Lang.Python3.VM;
using Mellis.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mellis.Lang.Python3.Tests.Entities.Classes
{
    [TestClass]
    public class PyClrFunctionTypeTests : BaseEntityTypeTester<PyClrFunctionType, ScriptClrFunction>
    {
        protected override string ExpectedClassName => Localized_Base_Entities.Type_ClrFunction_Name;

        protected override PyClrFunctionType CreateEntity(PyProcessor processor)
        {
            return new PyClrFunctionType(processor);
        }

        [TestMethod]
        public void CtorCantUseIt()
        {
            // Arrange
            var entity = CreateEntity();

            void Action()
            {
                entity.Invoke();
            }

            // Act
            var ex = Assert.ThrowsException<RuntimeException>((Action) Action);

            // Assert
            Assert.That.ErrorFormatArgsEqual(ex,
                nameof(Localized_Python3_Runtime.Ex_Type_CannotInstantiate),
                /* type name */ ExpectedClassName);
        }
    }
}