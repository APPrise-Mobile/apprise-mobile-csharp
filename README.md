# apprise-mobile-csharp
A C# client to the APPrise Mobile API

# Directions

View source files if you would like inspiration on how to code your own tool to talk to our API.
Otherwise, Navigate to the releases tab at the top of this project, and download the latest .zip.  The latest release will include a powershell tool that can perform basic communication with our API.

&nbsp;
# Powershell tool Basic Usage

`AppriseMobile.exe <environment> <grant code> <command> <action> [options]`  
Environments: beta or prod  
Commands: content, folders, groups, users, directory

For example,

To upload a CSV of your user base: `AppriseMobile.exe prod xyzgrantcodeherexyz users upload ./user-list.csv`

To upload a CSV of your directory: `AppriseMobile.exe prod xyzgrantcodeherexyz directory upload ./directory-list.csv`

For more info: `AppriseMobile.exe help <command> <action> ` (ex: help users, help users upload)
