﻿using System;
using System.ComponentModel;
using Zifro.Compiler.Core.Entities;
using Zifro.Compiler.Core.Exceptions;
using Zifro.Compiler.Core.Interfaces;
using Zifro.Compiler.Lang.Python3.Resources;

namespace Zifro.Compiler.Lang.Python3
{
    public partial class PyProcessor
    {
        public void ContinueYieldedValue(IScriptType value)
        {
            throw new System.NotImplementedException();
        }

        public void WalkLine()
        {
            WalkInstruction();

            // Because counter starts at -1
            int? initialRow = GetRow(ProgramCounter);

            if (initialRow.HasValue)
            {
                // Initial is row => walk until next is other row
                while (!(GetRow(ProgramCounter + 1) > initialRow.Value) &&
                       State == ProcessState.Running)
                    WalkInstruction();
            }
            else
            {
                // Initial is clr => walk until next is line
                while (GetRow(ProgramCounter + 1) == null &&
                       State == ProcessState.Running)
                    WalkInstruction();
            }

            int? GetRow(int i)
            {
                var source = GetSourceReference(i);
                return source.IsFromClr
                    ? (int?) null
                    : source.FromRow;
            }
        }

        public void WalkInstruction()
        {
            switch (State)
            {
                case ProcessState.Ended:
                case ProcessState.Error:
                    throw new InternalException(
                        nameof(Localized_Python3_Interpreter.Ex_Process_Ended),
                        Localized_Python3_Interpreter.Ex_Process_Ended);

                case ProcessState.Yielded:
                    throw new InternalException(
                        nameof(Localized_Python3_Interpreter.Ex_Process_Yielded),
                        Localized_Python3_Interpreter.Ex_Process_Yielded);

                case ProcessState.NotStarted when _opCodes.Length == 0:
                    State = ProcessState.Ended;
                    OnProcessEnded(State);
                    break;

                case ProcessState.NotStarted:
                case ProcessState.Running:
                    try
                    {
                        ProgramCounter++;
                        _opCodes[ProgramCounter].Execute(this);

                        if (ProgramCounter + 1 < _opCodes.Length)
                            State = ProcessState.Running;
                        else
                        {
                            State = ProcessState.Ended;
                            OnProcessEnded(State);
                        }
                    }
                    catch (InterpreterException ex)
                    {
                        State = ProcessState.Error;
                        LastError = ex;

                        OnProcessEnded(State);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        State = ProcessState.Error;

                        LastError = new InterpreterLocalizedException(
                            nameof(Localized_Python3_Interpreter.Ex_Unknown_Error),
                            Localized_Python3_Interpreter.Ex_Unknown_Error,
                            ex, ex.Message);

                        OnProcessEnded(State);
                        throw LastError;
                    }

                    break;

                default:
                    throw new InvalidEnumArgumentException(nameof(State), (int)State, typeof(ProcessState));
            }
        }

        private SourceReference GetSourceReference(int opCodeIndex)
        {
            if (opCodeIndex >= 0 && opCodeIndex < _opCodes.Length)
                return _opCodes[opCodeIndex].Source;

            return SourceReference.ClrSource;
        }
    }
}