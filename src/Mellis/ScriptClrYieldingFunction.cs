﻿using Mellis.Core.Interfaces;
using Mellis.Resources;

namespace Mellis
{
    public abstract class ScriptClrYieldingFunction : ScriptType, IClrYieldingFunction
    {
        public ScriptClrYieldingFunction(
            IProcessor processor, string functionName)
            : base(processor)
        {
            FunctionName = functionName;
        }

        public override string GetTypeName()
        {
            return Localized_Base_Entities.Type_ClrFunction_Name;
        }

        IProcessor IEmbeddedType.Processor
        {
            set => Processor = value;
        }

        public string FunctionName { get; }

        public abstract void InvokeEnter(params IScriptType[] arguments);

        public abstract IScriptType InvokeExit(IScriptType[] arguments, IScriptType returnValue);

        public override IScriptType CompareEqual(IScriptType rhs)
        {
            return Processor.Factory.Create(rhs == this);
        }

        public override IScriptType CompareNotEqual(IScriptType rhs)
        {
            return Processor.Factory.Create(rhs != this);
        }
    }
}