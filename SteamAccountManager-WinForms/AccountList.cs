using Microsoft.Win32;
using SteamAccountManager.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAccountManager_WinForms
{
    public partial class AccountList : Form
    {
        public AccountList()
        {
            InitializeComponent();
        }

        private void AccountList_Load(object sender, EventArgs e)
        {
            this.lbAccountList.Items.AddRange(Data.GetAccounts());
            foreach (ToolStripMenuItem item in Data.GetAccounts().Select(a => new ToolStripMenuItem(a, null, this.notifyIconMenuItem_Click)).ToArray())
                this.notifyIconMenu.Items.Insert(this.notifyIconMenu.Items.Count - 1, item);
        }

        private void notifyIconMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem source = sender as ToolStripMenuItem;
            if (source == null)
            {
                EventLog.WriteEntry("SteamAccountManager", $"Sender of notifyIconMenuItem_Click is not of type ToolStripMenuItem");
                MessageBox.Show("A weird error occured. Huh.", "What just happened?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.loginAs(source.Text);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.doLogin();
        }

        private void lbAccountList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.doLogin();
        }

        private void doLogin()
        {
            if (this.lbAccountList.SelectedItem as string == null)
                return;

            this.loginAs(this.lbAccountList.SelectedItem as string);
        }

        private void loginAs(string accountName)
        {
            try
            {
                Steam.Kill();
            }
            catch (MultipleInstancesException mie)
            {
                EventLog.WriteEntry("SteamAccountManager", $"Multiple processes of Steam have been found: {String.Join(",", mie.Processes.Select(p => p.Id.ToString()))}");
                MessageBox.Show("Multiple processes of \"steam.exe\" have been found. Please exit steam manually and retry.", "Unexpected amount of Steam", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (SteamWontExitException)
            {
                EventLog.WriteEntry("SteamAccountManager", "Unable to kill Steam.");
                MessageBox.Show("Unable to kill Steam. Is a game still running?", "Very persistent Steam", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Steam.LoginAs(accountName);
            }
            catch (SteamNotInstalledException)
            {
                EventLog.WriteEntry("SteamAccountManager", "Unable to find a Steam installation in the registry.");
                MessageBox.Show("Unable to find a Steam installation in the registry. Please verify that Steam is correctly installed.", "Steam not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Steam.Start();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.Show();
                this.BringToFront();
                this.Focus();
            }));
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon_MouseDoubleClick(sender, e as MouseEventArgs);
        }

        private void AccountList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            this.Hide();
            e.Cancel = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool firstShow = true;
        private void AccountList_Shown(object sender, EventArgs e)
        {
            if (!this.firstShow)
                return;

            this.firstShow = false;
            this.Hide();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AccountPrompt accountPrompt = new AccountPrompt();
            if (accountPrompt.ShowDialog() != DialogResult.OK)
                return;

            Data.AddAccount(accountPrompt.AccountName);

            this.lbAccountList.Items.Add(accountPrompt.AccountName);
            this.notifyIconMenu.Items.Insert(this.notifyIconMenu.Items.Count - 1, new ToolStripMenuItem(accountPrompt.AccountName, null, this.notifyIconMenuItem_Click));
        }

        private void btnRemoveAccount_Click(object sender, EventArgs e)
        {
            if (this.lbAccountList.SelectedItem as string == null)
                return;

            string accountName = this.lbAccountList.SelectedItem as string;
            Data.RemoveAccount(accountName);
            foreach (object item in this.lbAccountList.Items)
            {
                if (item as string == null)
                    continue;

                if (item as string != accountName)
                    continue;

                this.lbAccountList.Items.Remove(item);
                break;
            }
            foreach (object item in this.notifyIconMenu.Items)
            {
                if (item as ToolStripMenuItem == null)
                    continue;

                if ((item as ToolStripMenuItem).Text != accountName)
                    continue;

                if ((item as ToolStripMenuItem).Font.Bold)
                    continue;

                this.notifyIconMenu.Items.Remove(item as ToolStripItem);
                break;
            }
        }
    }
}
