﻿using System;
using Mellis.Core.Interfaces;
using Mellis.Lang.Base.Entities;
using Mellis.Lang.Python3.Resources;

namespace Mellis.Lang.Python3.Entities.Classes
{
    public abstract class PyType<T> : ScriptClrFunction
        where T : IScriptType
    {
        public string ClassName { get; }

        public PyType(
            IProcessor processor,
            string className)
            : base(processor, className)
        {
            ClassName = className;
        }

        public override IScriptType GetTypeDef()
        {
            return new PyType(Processor);
        }

        public override string GetTypeName()
        {
            return Localized_Python3_Entities.Type_Type_Name;
        }

        public override string ToString()
        {
            return string.Format(Localized_Python3_Entities.Type_Type_ToString,
                /* {0} */ ClassName
            );
        }

        #region Comparison implementations
        
        public override IScriptType CompareEqual(IScriptType rhs)
        {
            return Processor.Factory.Create(rhs is PyType<T>);
        }

        public override IScriptType CompareNotEqual(IScriptType rhs)
        {
            return Processor.Factory.Create(!(rhs is PyType<T>));
        }

        #endregion

    }
}