﻿using System;
using CedMod.Addons.QuerySystem;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace CedMod.Addons.Events.Commands
{
    public class Queue : ICommand
    {
        public string Command { get; } = "queue";

        public string[] Aliases { get; } = new string[]
        {
        };

        public string Description { get; } = "Gets the current event queue";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            if (sender.IsPanelUser() ? !sender.CheckPermission(PlayerPermissions.FacilityManagement) : !sender.CheckPermission("cedmod.events.queue"))
            {
                response = "No permission";
                return false;
            }

            response = "";
            response += $"Current event: [0] {(EventManager.currentEvent == null ? "None" : $"{EventManager.currentEvent.EventName} - ({EventManager.currentEvent.EventPrefix})")}\n\n\nQueue:\n";
            foreach (var evnt in EventManager.nextEvent)
            {
                response += $"[{EventManager.nextEvent.IndexOf(evnt)}] {evnt.EventName} - ({evnt.EventPrefix})\n";
            }
            ThreadDispatcher.SendHeartbeatMessage(true);
            return true;
        }
    }
}