using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SteamAccountManager.Logic
{
    public static class Steam
    {
        public static void Kill()
        {
            Process[] steamInstances = Process.GetProcessesByName("steam");
            if (steamInstances.Length == 0)
                return;

            if (steamInstances.Length > 1)
                throw new MultipleInstancesException(steamInstances);

            Process steam = steamInstances[0];
            steam.Kill();

            if (!steam.WaitForExit(10000))
                throw new SteamWontExitException();
        }

        public static void Start()
        {
            Process.Start("steam://open/main");
        }

        public static void LoginAs(string accountName)
        {
            RegistryKey rkSteam = Data.GetSteamRegistryKey();
            rkSteam.SetValue("AutoLoginUser", accountName, RegistryValueKind.String);
            rkSteam.SetValue("RememberPassword", 1, RegistryValueKind.DWord);
            rkSteam.Close();
            rkSteam.Dispose();
        }
    }
}
