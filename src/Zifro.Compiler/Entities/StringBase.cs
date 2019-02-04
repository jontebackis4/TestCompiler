﻿using System;
using System.Linq;
using Zifro.Compiler.Core.Exceptions;
using Zifro.Compiler.Core.Interfaces;
using Zifro.Compiler.Resources;

namespace Zifro.Compiler.Entities
{
    public abstract class StringBase : IScriptType
    {
        public abstract IProcessor Processor { get; set; }

        public abstract IScriptType GetTypeDef();
        public abstract string GetTypeName();

        public string Value { get; set; }

        protected object[] GetErrorArgs(params object[] additional)
        {
            return GetErrorArgs().Concat(additional).ToArray();
        }

        protected object[] GetErrorArgs()
        {
            return new object[] {Value, Value.Length};
        }

        public virtual IScriptType Invoke(IScriptType[] arguments)
        {
            throw new RuntimeException(
                nameof(Localized_Base_Entities.Ex_String_Invoke),
                Localized_Base_Entities.Ex_String_Invoke,
                values: GetErrorArgs());
        }

        public virtual IScriptType GetIndex(IScriptType index)
        {
            if (!index.TryConvert(out int intIndex))
            {
                throw new RuntimeException(
                    nameof(Localized_Base_Entities.Ex_String_IndexGet_InvalidType),
                    Localized_Base_Entities.Ex_String_IndexGet_InvalidType,
                    values: GetErrorArgs(index.GetTypeName()));
            }

            if (intIndex < 0 || intIndex >= Value.Length)
            {
                throw new RuntimeException(
                    nameof(Localized_Base_Entities.Ex_String_IndexGet_OutOfRange),
                    Localized_Base_Entities.Ex_String_IndexGet_OutOfRange,
                    values: GetErrorArgs(intIndex));
            }

            return Processor.Factory.Create(Value[intIndex]);
        }

        public virtual IScriptType SetIndex(IScriptType index, IScriptType value)
        {
            throw new RuntimeException(
                nameof(Localized_Base_Entities.Ex_String_IndexSet),
                Localized_Base_Entities.Ex_String_IndexSet,
                values: GetErrorArgs());
        }

        public virtual IScriptType GetProperty(string property)
        {
            throw new RuntimeException(
                nameof(Localized_Base_Entities.Ex_String_PropertyGet),
                Localized_Base_Entities.Ex_String_PropertyGet,
                values: GetErrorArgs(property));
        }

        public virtual IScriptType SetProperty(string property, IScriptType value)
        {
            throw new RuntimeException(
                nameof(Localized_Base_Entities.Ex_String_PropertySet),
                Localized_Base_Entities.Ex_String_PropertySet,
                values: GetErrorArgs(property));
        }

        public bool TryConvert<T>(out T value)
        {
            if (TryConvert(typeof(T), out object boxed))
            {
                value = (T) boxed;
                return true;
            }

            value = default;
            return false;
        }

        public virtual bool TryConvert(Type type, out object value)
        {
            if (type == typeof(string))
            {
                value = Value;
                return true;
            }

            if (type == typeof(char) && Value?.Length >= 1)
            {
                value = Value[0];
                return true;
            }

            value = default;
            return false;
        }

        public IScriptType ArithmeticUnaryPositive()
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticUnaryNegative()
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticAdd(IScriptType rhs)
        {
            if (rhs is StringBase rhsString)
            {
                return Processor.Factory.Create(Value + rhsString.Value);
            }
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticSubtract(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticMultiply(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticDivide(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticModulus(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticExponent(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType ArithmeticFloorDivide(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareEqual(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareNotEqual(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareGreaterThan(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareGreaterThanOrEqual(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareLessThan(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType CompareLessThanOrEqual(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryNot()
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryAnd(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryOr(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryXor(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryLeftShift(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType BinaryRightShift(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType LogicalNot()
        {
            throw new NotImplementedException();
        }

        public IScriptType LogicalAnd(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType LogicalOr(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType MemberIn(IScriptType lhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType MemberNotIn(IScriptType lhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType IdentityIs(IScriptType rhs)
        {
            throw new NotImplementedException();
        }

        public IScriptType IdentityIsNot(IScriptType rhs)
        {
            throw new NotImplementedException();
        }
    }
}