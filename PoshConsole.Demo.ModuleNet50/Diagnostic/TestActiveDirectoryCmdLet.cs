using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Management.Automation;

namespace PoshConsole.Demo.ModuleNet50
{
    [Cmdlet("Test", "ActiveDirectory")]
    public class TestActiveDirectoryCmdLet : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject("Testing Forest!");
            TestForest();
        }

        private static void TestForest()
        {
            Log.Verbose("Testing AD Forest...");
            var forest = Forest.GetCurrentForest();
        }

        private static void WalkDomain(System.DirectoryServices.ActiveDirectory.Domain domain)
        {
            if (Platform.IsWindows)
            {
                Log.Info("Crawling {0}...", domain.Name);
                DetectNetbiosName(domain);
                try
                {
                    var subDomains = domain.Children;
                }
                catch (Exception e)
                {
                    Log.Error(string.Format("Failed to retrieve children of {0}", domain.Name), e);
                }
            }
        }


        private static void DetectNetbiosName(System.DirectoryServices.ActiveDirectory.Domain domain)
        {
            Log.Verbose("Looking up NETBIOS name for {0}", domain.Name);
        }
    }
}
