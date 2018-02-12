using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace AppriseMobile
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = new Dictionary<string, CommandHandler>();
            commands["users"] = new CommandHandler(PrintUsageUsers, HandleUsers);
            commands["directory"] = new CommandHandler(PrintUsageDirectory, HandleDirectory);
            commands["groups"] = new CommandHandler(PrintUsageGroups, HandleGroups);
            commands["folders"] = new CommandHandler(PrintUsageFolders, HandleFolders);
            commands["content"] = new CommandHandler(PrintUsageContents, HandleContents);

            ApiEnvironment env;
            string grantCode = null;
            string command = null;
            string action = null;
            string[] options = null;

            if (args.GetValueOrDefault(0) == "help")
            {
                var handler = commands.GetValueOrDefault(args.GetValueOrDefault(1));
                if (handler == null) PrintUsageGeneric();
                handler.PrintHelp(args.GetValueOrDefault(2));
            }
            else if (Enum.TryParse(args.GetValueOrDefault(0), true, out env))
            {
                grantCode = args.GetValueOrDefault(1);
                command = args.GetValueOrDefault(2);

                if (grantCode == null || command == null)
                {
                    PrintUsageGeneric();
                    return;
                }

                action = args.GetValueOrDefault(3);
                if (args.Length > 4) options = new ArraySegment<string>(args, 4, args.Length - 4).ToArray();

                using (var client = new ApiClient(env, grantCode))
                {
                    var handler = commands.GetValueOrDefault(command);
                    if (handler == null) PrintUsageGeneric();
                    else handler.Handle(client, action, options);
                }
            }
            else PrintUsageGeneric();
        }

        private static void PrintUsageGeneric()
        {
            Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> <command> <action> [options]");
            Console.WriteLine("Environments: beta or prod");
            Console.WriteLine("Commands: content, folders, groups, users, directory");
            Console.WriteLine("For more info: help <command> <action> (ex: help users, help users upload)");
        }

        #region Users
        private static void PrintUsageUsers(string action = null)
        {
            switch (action)
            {
                case "upload":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> users upload <csv file>");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 users upload test.csv");
                    break;

                default:
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> users <action>");
                    Console.WriteLine("Actions: upload");
                    break;
            }
        }


        private static void HandleUsers(ApiClient client, string action, string[] options)
        {
            switch (action)
            {
                case "upload":
                    Console.WriteLine("Starting User CSV Upload...");
                    var csv = options?.GetValueOrDefault(0);
                    if (!File.Exists(csv)) Console.WriteLine("Error: csv file not found");
                    else client.UploadUserCsv(csv);
                    break;

                default:
                    PrintUsageUsers();
                    break;
            }
        }
        #endregion

        #region Directory
        private static void PrintUsageDirectory(string action = null)
        {
            switch (action)
            {
                case "upload":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> directory upload <csv file> [directory name]");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 directory upload account.csv");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 directory upload single.csv \"Test Directory\"");
                    break;

                default:
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> directory <action>");
                    Console.WriteLine("Actions: upload");
                    break;
            }
        }

        private static void HandleDirectory(ApiClient client, string action, string[] options)
        {
            switch (action)
            {
                case "upload":
                    Console.WriteLine("Starting Directory CSV Upload...");
                    var csv = options?.GetValueOrDefault(0);
                    var directory = options?.GetValueOrDefault(1);
                    if (!File.Exists(csv)) Console.WriteLine("Error: csv file not found");
                    else client.UploadDirectoryCsv(csv, directory);
                    break;

                default:
                    PrintUsageDirectory();
                    break;
            }
        }
        #endregion

        #region Groups
        private static void PrintUsageGroups(string action = null)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> groups get <group id>");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 groups get 123456");
                    break;

                case "list":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> groups list");
                    break;

                default:
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> groups <action> [options]");
                    Console.WriteLine("Actions: list (show all groups)");
                    Console.WriteLine("Actions: get (show one group)");
                    break;
            }
        }

        private static void HandleGroups(ApiClient client, string action, string[] options)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Getting Group...");
                    var id = options?.GetValueOrDefault(0);
                    if (id != null)
                    {
                        var group = client.GetGroup(new ObjectId(id));
                        Console.WriteLine(JsonConvert.SerializeObject(group, Formatting.Indented));
                    }
                    break;

                case "list":
                    Console.WriteLine("Getting All Groups...");
                    var groups = client.GetGroups();
                    Console.WriteLine("id : name");
                    foreach (var group in groups) Console.WriteLine(group.Id + " : " + group.Name);
                    break;

                default:
                    PrintUsageGroups();
                    break;
            }
        }
        #endregion

        #region Folders
        private static void PrintUsageFolders(string action = null)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> folders get <folder id>");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 folder get 123456");
                    break;

                case "list":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> folders list");
                    break;

                default:
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> folders <action> [options]");
                    Console.WriteLine("Actions: list (show all folders)");
                    Console.WriteLine("Actions: get (show one folder)");
                    break;
            }
        }

        private static void HandleFolders(ApiClient client, string action, string[] options)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Getting Folder...");
                    var id = options?.GetValueOrDefault(0);
                    if (id != null)
                    {
                        var folder = client.GetContentFolder(new ObjectId(id));
                        Console.WriteLine(JsonConvert.SerializeObject(folder, Formatting.Indented));
                    }
                    break;

                case "list":
                    Console.WriteLine("Getting All Folders...");
                    var folders = client.GetContentFolders();
                    Console.WriteLine("id : title");
                    foreach (var folder in folders) Console.WriteLine(folder.Id + " : " + folder.Title);
                    break;

                default:
                    PrintUsageFolders();
                    break;
            }
        }
        #endregion

        #region Contents
        private static void PrintUsageContents(string action = null)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> contents get <content id>");
                    Console.WriteLine("Ex: AppriseMobile.exe beta 0123456789 content get 123456");
                    break;

                case "list":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> contents list");
                    break;

                case "upload":
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> contents upload <title> <file> <folder id> [caption]");
                    break;

                default:
                    Console.WriteLine("Usage: AppriseMobile.exe <environment> <grant code> contents <action> [options]");
                    Console.WriteLine("Actions: list (show all contents)");
                    Console.WriteLine("Actions: get (show one content)");
                    Console.WriteLine("Actions: upload (upload new content)");
                    break;
            }
        }

        private static void HandleContents(ApiClient client, string action, string[] options)
        {
            switch (action)
            {
                case "get":
                    Console.WriteLine("Getting Content...");
                    var id = options?.GetValueOrDefault(0);
                    if (id != null)
                    {
                        var content = client.GetContent(new ObjectId(id));
                        Console.WriteLine(JsonConvert.SerializeObject(content, Formatting.Indented));
                    }
                    break;

                case "list":
                    Console.WriteLine("Getting All Contents...");
                    var contents = client.GetContents();
                    Console.WriteLine("id : title");
                    foreach (var content in contents) Console.WriteLine(content.Id + " : " + content.Title);
                    break;

                case "upload":
                    Console.WriteLine("Uploading Content...");
                    var title = options?.GetValueOrDefault(0);
                    var file = options?.GetValueOrDefault(1);
                    var contentFolder = options?.GetValueOrDefault(2);
                    if (title != null && file != null && contentFolder != null)
                    {
                        var content = Content.FromFile(title, file, new ObjectId(contentFolder));

                        var caption = options?.GetValueOrDefault(3);
                        if (caption != null) content.Caption = caption;

                        client.UploadContent(content);
                        Console.WriteLine("Content Uploaded");
                    }
                    else Console.WriteLine("Missing Argument");
                    break;

                default:
                    PrintUsageFolders();
                    break;
            }
        }
        #endregion
    }
}
