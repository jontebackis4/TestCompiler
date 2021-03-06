﻿using Mellis.Core.Interfaces;
using Mellis.Lang.Python3.Exceptions;
using Mellis.Lang.Python3.Resources;

namespace Mellis.Lang.Python3.Entities.Classes
{
    public class PyType : PyType<IScriptType>
    {
        public PyType(
            IProcessor processor)
            : base(
                processor: processor,
                className: Localized_Python3_Entities.Type_Type_Name)
        {
        }

        public override IScriptType Invoke(params IScriptType[] arguments)
        {
            if (arguments.Length > 1)
            {
                throw new RuntimeTooManyArgumentsException(
                    Localized_Python3_Entities.Type_Type_Name,
                    1, arguments.Length);
            }

            if (arguments.Length < 1)
            {
                throw new RuntimeTooFewArgumentsException(
                    Localized_Python3_Entities.Type_Type_Name,
                    1, arguments.Length);
            }

            return arguments[0].GetTypeDef();
        }
    }
}