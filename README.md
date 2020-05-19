# Steam Account Manager
Handle logins of multiple Steam accounts on the same machine with ease!

## What does this do?

If you have multiple Steam accounts you probably know the hassle of having to re-type the username, password and even get the two factor authentification every time you want to switch accounts.

This application allows you to quickly switch between your Steam accounts with two clicks and without having to re-authenticate.

## Setup

There are no prerequisites other than Windows with .Net Framework 7.2 installed.

To install, simply extract the files into a folder (like `%appdata%\SteamAccountManager`).

1. Start the Steam Account Manager
2. Open the Steam Account Manager Window (double click the icon in the notification area)
3. Add all your accounts to the Steam Account Manager
4. For each account in the Steam Account Manager:
   1. Select the account name
   2. Click on Login
   3. Do the login (password, authenticator) and check [X] Remember my password in Steam
   4. Repeat 4.i. - 4.iv. for all your accounts

## Usage

After setting up your accounts you can right-click the icon in the notification area to select an account or double-click the icon to open the main window to manage accounts.

Be aware that selecting an account will instantly quit Steam, which will in turn quit any open Game without prompt or saving, so make sure that you saved everything before switching!

## Is this safe? How is the account data stored?

This application only stores your Steam username (in the Windows registry) that is used to login, everything else is handled by Steam and is therefore as secure as using the [X] Remember my password function of Steam. You never give this program your password or email.

It is also open-source so you can see how it works (check the [Logic](https://github.com/Longoon12000/SteamAccountManager/tree/master/SteamAccountManager.Logic) project, the other one is only UI).

## Keep it simple!

This application is as simple as it gets. No bloat, simple UI, two clicks from anywhere to change the account, no parsing of any third-party files. That also means it's solid and unlikely to break if Steam ever changes their account files.

### Autostart with Windows

If you want this tool to automatically start with Windows simply put a shortcut to it into `%appdata%\Microsoft\Windows\Start Menu\Programs\Startup`.

## Alternatives

Here are some alternatives that I have found but have not been to my liking. You may like them because they offer more features.

- https://github.com/TcNobo/TcNo-Acc-Switcher
- https://github.com/lemasato/Steam-Account-Switcher
- https://github.com/danielchalmers/SteamAccountSwitcher (WARNING - Requires to enter password in application)
- https://github.com/W3D3/SteamAccountSwitcher (WARNING - Requires to enter password in application)
