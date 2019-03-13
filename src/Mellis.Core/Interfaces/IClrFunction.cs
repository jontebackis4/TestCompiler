﻿using Mellis.Core.Entities;

namespace Mellis.Core.Interfaces
{
    public interface IClrFunction : IEmbeddedType
    {
        /// <summary>
        /// Executed by the processor when the function is invoked in the script environment.
        /// </summary>
        IScriptType Invoke(IScriptType[] arguments);
    }
}