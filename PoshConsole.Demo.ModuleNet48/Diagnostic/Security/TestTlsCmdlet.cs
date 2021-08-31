using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshConsole.Demo.ModuleNet48.Diagnostic.Security
{
    // based on https://docs.microsoft.com/en-us/powershell/scripting/developer/cmdlet/how-to-write-a-simple-cmdlet?view=powershell-7.1
    [Cmdlet(VerbsDiagnostic.Test, "Tls")]
    public class TestTlsCmdlet : PSCmdlet
    {
        private const string UrlKeyName = "Url";

        [Parameter(Mandatory = true)]
        public string Url
        {
            get;
            set;
        }
        // Override the ProcessRecord method to process
        // the supplied user name and write out a
        // greeting to the user by calling the WriteObject
        // method.
        protected override void ProcessRecord()
        {
            WriteObject("Hello " + Url + "!");
        }
    }
}
