﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PoshCode.Properties;

namespace PoshCode.PowerShell
{

    internal class PoshConsolePipeline
    {
        internal readonly Collection<Command> Commands;
        internal readonly IEnumerable Input;
        internal readonly bool IsScript;
        internal ConsoleOutput Output;
        internal readonly TaskCompletionSource<PoshConsolePipelineResults> TaskSource;
        internal Task<PoshConsolePipelineResults> Task => TaskSource.Task;

        internal PoshConsolePipeline(IList<Command> commands, IEnumerable input = null, ConsoleOutput output = ConsoleOutput.Default)
        {
            TaskSource = new TaskCompletionSource<PoshConsolePipelineResults>();
            Commands = new Collection<Command>(commands);
            Input = input;
            IsScript = Commands.First().IsScript;
            Output = output;
        }

        internal PoshConsolePipeline(string script, IEnumerable input = null, ConsoleOutput output = ConsoleOutput.Default) :
            this(new[] { new Command(script, true, true) }, input, output)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            if (Output != ConsoleOutput.Default)
            {
                output.Append("${Results} = ");
            }

            // Show artificial input ...
            if (Input != null)
            {
                output.Append("${Input} | ");
            }

            var enumerator = Commands.GetEnumerator();
            var more = enumerator.MoveNext();
            while (more)
            {
                var cmd = enumerator.Current;
                more = enumerator.MoveNext();
                var script = cmd.IsScript && more;

                if (script)
                {
                    output.Append("&{ ");
                }

                output.Append(cmd.CommandText);
                foreach (var param in cmd.Parameters)
                {
                    if (param.Value == null)
                    {
                        output.AppendFormat(" -{0}", param.Name);
                    }
                    else
                    {
                        output.AppendFormat(" -{0} {1}", param.Name, GetParameterValue(param.Value));
                    }
                }

                output.Append(script ? " } | " : " | ");
            }


            return output.ToString().TrimEnd(' ', '|');
        }

        private static string GetParameterValue(object value)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                return $"\"{value}\"";
            }

            var items = value as IEnumerable;
            if (items != null)
            {
                stringValue = $"@( {string.Join(", ", from object item in items select GetParameterValue(item))})";
            }
            
            if (value is int || value is short || value is long || value is uint || value is ushort || value is ulong)
            {
                stringValue = $"{value:D}";
            }

            if (value is double)
            {
                stringValue = $"{value:F}";
            }

            if (value is decimal)
            {
                stringValue = $"{value:F}D";
            }

            return stringValue ?? $"${{{value}}}";
        }

    }
    

    
}