using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SteamAccountManager.Logic
{
    public class MultipleInstancesException : Exception
    {
        public readonly Process[] Processes;

        public MultipleInstancesException(Process[] processes) => this.Processes = processes;
    }
    public class SteamNotInstalledException : Exception { }
    public class SteamWontExitException : Exception { }
}
