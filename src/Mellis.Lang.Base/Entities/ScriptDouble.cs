﻿using System;
using System.Globalization;
using Mellis.Core.Exceptions;
using Mellis.Core.Interfaces;
using Mellis.Lang.Base.Resources;
using Mellis.Tools.Extensions;

namespace Mellis.Lang.Base.Entities
{
    /// <summary>
    /// Basic functionality of a double value.
    /// </summary>
    public abstract class ScriptDouble : ScriptBaseType
    {
        public double Value { get; }

        protected ScriptDouble(IProcessor processor, double value, string name = null)
            : base(processor, name)
        {
            Value = value;
        }

        public override string GetTypeName()
        {
            return Localized_Base_Entities.Type_Double_Name;
        }

        public override bool TryCoerce(Type type, out object value)
        {
            switch (Type.GetTypeCode(type))
            {
            case TypeCode.Boolean:
                value = !Value.Equals(0d);
                return true;

            case TypeCode.Byte:
                value = (byte)Value;
                return true;

            case TypeCode.Int16:
                value = (short)Value;
                return true;

            case TypeCode.Int32:
                value = (int)Value;
                return true;

            case TypeCode.Int64:
                value = (long)Value;
                return true;

            case TypeCode.SByte:
                value = (sbyte)Value;
                return true;

            case TypeCode.UInt16:
                value = (ushort)Value;
                return true;

            case TypeCode.UInt32:
                value = (uint)Value;
                return true;

            case TypeCode.UInt64:
                value = (ulong)Value;
                return true;

            case TypeCode.Single:
                value = (float)Value;
                return true;

            case TypeCode.Double:
                value = Value;
                return true;

            case TypeCode.Decimal:
                value = (decimal)Value;
                return true;

            case TypeCode.Char:
                value = Value.Equals(0) ? '\x0' : '\x1';
                return true;

            case TypeCode.Object when typeof(ScriptDouble).IsAssignableFrom(type):
                value = this;
                return true;

            case TypeCode.String:
                value = ToString();
                return true;

            default:
                value = default;
                return false;
            }
        }

        public override bool IsTruthy()
        {
            return !Value.Equals(0d);
        }

        public override IScriptType ArithmeticUnaryPositive()
        {
            return this;
        }

        public override IScriptType ArithmeticUnaryNegative()
        {
            return Processor.Factory.Create(-Value);
        }

        public override IScriptType ArithmeticAdd(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptDouble rhsDouble:
                return Processor.Factory.CreateAppropriate(Value + rhsDouble.Value);

            case ScriptInteger rhsInt:
                return Processor.Factory.CreateAppropriate(Value + rhsInt.Value);

            default:
                return null;
            }
        }

        public override IScriptType ArithmeticSubtract(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptDouble rhsDouble:
                return Processor.Factory.CreateAppropriate(Value - rhsDouble.Value);

            case ScriptInteger rhsInt:
                return Processor.Factory.CreateAppropriate(Value - rhsInt.Value);

            default:
                return null;
            }
        }

        public override IScriptType ArithmeticMultiply(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInt:
                return Processor.Factory.CreateAppropriate(Value * rhsInt.Value);

            case ScriptDouble rhsDouble:
                return Processor.Factory.CreateAppropriate(Value * rhsDouble.Value);

            default:
                return null;
            }
        }

        public override IScriptType ArithmeticDivide(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger when rhsInteger.Value.Equals(0):
            case ScriptDouble rhsDouble when rhsDouble.Value.Equals(0d):
                throw new RuntimeException(nameof(Localized_Base_Entities.Ex_Math_DivideByZero),
                    Localized_Base_Entities.Ex_Math_DivideByZero);

            case ScriptDouble rhsDouble:
                return Processor.Factory.CreateAppropriate(Value / rhsDouble.Value);

            case ScriptInteger rhsInt:
                return Processor.Factory.CreateAppropriate(Value / rhsInt.Value);

            default:
                return null;
            }
        }

        public override IScriptType ArithmeticModulus(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value % rhsInteger.Value);
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value % rhsDouble.Value);
            default:
                return null;
            }
        }

        public override IScriptType ArithmeticExponent(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Math.Pow(Value, rhsInteger.Value));
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Math.Pow(Value, rhsDouble.Value));
            default:
                return null;
            }
        }

        public override IScriptType ArithmeticFloorDivide(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create((int)Math.Floor(Value / rhsInteger.Value));
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create((int)Math.Floor(Value / rhsDouble.Value));
            default:
                return null;
            }
        }

        public override IScriptType CompareEqual(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value.Equals(rhsInteger.Value));
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value.Equals(rhsDouble.Value));
            default:
                return Processor.Factory.False;
            }
        }

        public override IScriptType CompareNotEqual(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(!Value.Equals(rhsInteger.Value));
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(!Value.Equals(rhsDouble.Value));
            default:
                return Processor.Factory.True;
            }
        }

        public override IScriptType CompareGreaterThan(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value > rhsInteger.Value);
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value > rhsDouble.Value);
            default:
                return null;
            }
        }

        public override IScriptType CompareGreaterThanOrEqual(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value >= rhsInteger.Value);
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value >= rhsDouble.Value);
            default:
                return null;
            }
        }

        public override IScriptType CompareLessThan(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value < rhsInteger.Value);
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value < rhsDouble.Value);
            default:
                return null;
            }
        }

        public override IScriptType CompareLessThanOrEqual(IScriptType rhs)
        {
            switch (rhs)
            {
            case ScriptInteger rhsInteger:
                return Processor.Factory.Create(Value <= rhsInteger.Value);
            case ScriptDouble rhsDouble:
                return Processor.Factory.Create(Value <= rhsDouble.Value);
            default:
                return null;
            }
        }

        public override string ToString()
        {
            switch (Value)
            {
            case double.PositiveInfinity:
                return Localized_Base_Entities.Type_Double_PosInfinity;
            case double.NegativeInfinity:
                return Localized_Base_Entities.Type_Double_NegInfinity;
            case double.NaN:
                return Localized_Base_Entities.Type_Double_NaN;

            default:
                return Value.ToString(CultureInfo.InvariantCulture).ToLower();
            }
        }
    }
}