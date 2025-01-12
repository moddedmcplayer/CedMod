﻿using System;
using CedMod.Addons.QuerySystem;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;

namespace CedMod.Addons.Events.Commands
{
    public class DisableEvent : ICommand, IUsageProvider
    {
        public string Command { get; } = "disable";

        public string[] Aliases { get; } = new string[]
        {
        };

        public string Description { get; } = "Disables the current event will restart the round immediately";

        public string[] Usage { get; } = new string[]
        {
            "%queuepos%",
        };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            if (arguments.Count < 1)
            {
                response = "To execute this command provide at least 1 arguments!\nUsage: " + this.DisplayCommandUsage();
                return false;
            }
            int queuepos = Convert.ToInt16(arguments.At(0));
            if (queuepos >= 1 && EventManager.nextEvent.Count <= 0)
            {
                if (EventManager.nextEvent == null)
                {
                    response = "There is no event pending for the next round";
                    return false;
                }
            }
            else
            {
                if (EventManager.currentEvent == null)
                {
                    response = "There is no event in progress";
                    return false;
                }
            }

            if (sender.IsPanelUser() ? !sender.CheckPermission(PlayerPermissions.FacilityManagement) : !sender.CheckPermission("cedmod.events.disable"))
            {
                response = "No permission";
                return false;
            }
            
            if (queuepos >= 1)
            {
                var toRemove = EventManager.nextEvent[queuepos - 1];
                EventManager.nextEvent.Remove(toRemove);
                Map.Broadcast(5, $"EventManager: {toRemove.EventName} has been removed from the queue");
            }
            else
            {
                Map.Broadcast(10, $"EventManager: {EventManager.currentEvent.EventName} is being now disabled, round will restart in 3 seconds");
                Timing.CallDelayed(3, () =>
                {
                    Round.Restart(false, false);
                });
            }
            ThreadDispatcher.SendHeartbeatMessage(true);
            response = "Success";
            return true;
        }
    }
}