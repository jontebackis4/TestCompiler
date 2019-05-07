﻿using System.Linq;
using Mellis.Core.Interfaces;
using Mellis.Lang.Base.Entities;
using Mellis.Lang.Python3.Entities.Classes;

namespace Mellis.Lang.Python3.Entities
{
    public class PyDouble : ScriptDouble
    {
        public PyDouble(IProcessor processor, double value, string name = null)
            : base(processor, value, name)
        {
        }

        /// <inheritdoc />
        public override IScriptType Copy(string newName)
        {
            return new PyDouble(Processor, Value, newName);
        }

        /// <inheritdoc />
        public override IScriptType GetTypeDef()
        {
            return new PyDoubleType(Processor);
        }

        public override string ToString()
        {
            string s = base.ToString();
            
            return s.All(char.IsDigit)
                ? s + ".0"
                : s;
        }
    }
}