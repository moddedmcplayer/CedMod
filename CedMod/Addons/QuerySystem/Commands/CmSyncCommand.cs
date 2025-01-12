﻿using System;
using CommandSystem;
using Exiled.API.Features;

namespace CedMod.Addons.QuerySystem.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CmSyncCommandCommand : ICommand
    {
        public string Command { get; } = "cmsync";

        public string[] Aliases { get; } = new string[]
        {
        };

        public string Description { get; } = "syncs roles for cedmod panel";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            if (sender.IsPanelUser())
            {
                if (!sender.CheckPermission(PlayerPermissions.SetGroup))
                {
                    response = "No permission";
                    return false;
                }
                if (ServerStatic.PermissionsHandler._members.ContainsKey(Player.Get(int.Parse(arguments.At(0))).UserId))
                {
                    response = "User already has a role";
                    return false;
                }

                if (Player.Get(int.Parse(arguments.At(0))).UserId != arguments.At(2))
                {
                    response = "UserId mismatch";
                    return false;
                }
                ServerStatic.GetPermissionsHandler()._members[Player.Get(int.Parse(arguments.At(0))).UserId] = arguments.At(1);
                Player.Get(int.Parse(arguments.At(0))).ReferenceHub.serverRoles.SetGroup(ServerStatic.GetPermissionsHandler()._groups[arguments.At(1)], false);
                CommandHandler.Synced.Add(Player.Get(int.Parse(arguments.At(0))).UserId);
                response = "Done.";
                return true;
            }

            response = "This command may only be run by the panel";
            return true;
        }
    }
}