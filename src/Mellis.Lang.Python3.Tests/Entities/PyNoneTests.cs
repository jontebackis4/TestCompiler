﻿using Mellis.Lang.Base.Resources;
using Mellis.Lang.Python3.Entities;
using Mellis.Lang.Python3.Resources;
using Mellis.Lang.Python3.VM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mellis.Lang.Python3.Tests.Entities
{
    [TestClass]
    public class PyNoneTests : BaseEntityTester<PyNone, object>
    {
        protected override string ExpectedTypeName => Localized_Base_Entities.Type_Null_Name;

        protected override PyNone CreateEntity(PyProcessor processor, object value)
        {
            return new PyNone(processor, nameof(PyNoneTests));
        }

        [DataTestMethod]
        [DataRow(null, "None")]
        public override void ToStringDataTest(object value, string expected)
        {
            base.ToStringDataTest(value, expected);
        }
    }
}