using Microsoft.Win32;
using System;
using System.Linq;

namespace SteamAccountManager.Logic
{
    public static class Data
    {
        public static string[] GetAccounts()
        {
            RegistryKey rkSteamAccountManager = GetSteamAccountManagerRegistryKey();
            string[] accountNames = rkSteamAccountManager.GetValueNames();
            rkSteamAccountManager.Close();
            rkSteamAccountManager.Dispose();
            return accountNames;
        }

        public static void AddAccount(string accountName)
        {
            RegistryKey rkSteamAccountManager = GetSteamAccountManagerRegistryKey();
            rkSteamAccountManager.SetValue(accountName, 0, RegistryValueKind.DWord);
            rkSteamAccountManager.Close();
            rkSteamAccountManager.Dispose();
        }

        public static void RemoveAccount(string accountName)
        {
            RegistryKey rkSteamAccountManager = GetSteamAccountManagerRegistryKey();
            rkSteamAccountManager.DeleteValue(accountName);
            rkSteamAccountManager.Close();
            rkSteamAccountManager.Dispose();
        }

        internal static RegistryKey GetSteamRegistryKey()
        {
            using (RegistryKey rkSoftware = Registry.CurrentUser.OpenSubKey("SOFTWARE"))
            {
                if (!rkSoftware.GetSubKeyNames().Contains("Valve"))
                    throw new SteamNotInstalledException();

                using (RegistryKey rkValve = rkSoftware.OpenSubKey("Valve"))
                {
                    if (!rkValve.GetSubKeyNames().Contains("Steam"))
                        throw new SteamNotInstalledException();

                    return rkValve.OpenSubKey("Steam", true);
                }
            }
        }

        internal static RegistryKey GetSteamAccountManagerRegistryKey()
        {
            using (RegistryKey rkSoftware = Registry.CurrentUser.OpenSubKey("SOFTWARE", true))
            {
                if (!rkSoftware.GetSubKeyNames().Contains("CodeFreak"))
                    rkSoftware.CreateSubKey("CodeFreak", true);


                using (RegistryKey rkCodeFreak = rkSoftware.OpenSubKey("CodeFreak", true))
                {
                    if (!rkCodeFreak.GetSubKeyNames().Contains("SteamAccountManager"))
                        rkCodeFreak.CreateSubKey("SteamAccountManager", true);

                    return rkCodeFreak.OpenSubKey("SteamAccountManager", true);
                }
            }
        }
    }
}
