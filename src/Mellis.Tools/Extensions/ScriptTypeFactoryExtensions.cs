﻿using System;
using System.Collections.Generic;
using Mellis.Core.Interfaces;

namespace Mellis.Tools.Extensions
{
    public static class ScriptTypeFactoryExtensions
    {
        public static bool TryCreate<T>(this IScriptTypeFactory factory, T clrValue, out IScriptType scriptTypeValue)
        {
            if (clrValue == null)
            {
                scriptTypeValue = factory.Null;
                return true;
            }

            switch (clrValue)
            {
                case bool v:
                    scriptTypeValue = v ? factory.True : factory.False;
                    return true;

                case int v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case short v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case byte v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case long v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case char v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case string v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case IList<IScriptType> v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case IDictionary<IScriptType, IScriptType> v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                case IClrFunction v:
                    scriptTypeValue = factory.Create(v);
                    return true;

                default:
                    scriptTypeValue = default;
                    return false;
            }
        }

        public static IScriptType CreateAppropriate(this IScriptTypeFactory factory, double value)
        {
            if (Math.Abs(value) % 1 <= 1e-10)
            {
                return factory.Create((int)Math.Round(value));
            }

            return factory.Create(value);
        }
    }
}
