﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using CedMod.Commands;
using CedMod.Commands.Dialog;
using CedMod.Commands.Stuiter;
using RemoteAdmin;
using UnityEngine;

namespace CedMod
{
    using Exiled.API.Enums;
    using Exiled.API.Features;

    /// <summary>
    /// The example plugin.
    /// </summary>
    public class CedModMain : Plugin<Config>
    {
        private Handlers.Server server;
        private Handlers.Player player;
        /// <inheritdoc/>
        public override PluginPriority Priority { get; } = PluginPriority.First;

        /// <inheritdoc/>
        public static Config config;
        
        public override string Author { get; } = "ced777ric#0001";

        public override string Name { get; } = "CedMod";

        public override string Prefix { get; } = "cm";

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;

            if (!File.Exists(Application.dataPath + "/Managed/Newtonsoft.Json.dll"))
            {
                WebClient wc = new WebClient();
                Log.Error("Dependency missing, downloading...");
                ServicePointManager.ServerCertificateValidationCallback += API.ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                wc.DownloadFile("https://cdn.cedmod.nl/files/Newtonsoft.Json.dll", Application.dataPath + "/Managed/Newtonsoft.Json.dll");
                Application.Quit();
            }
            
            if (!File.Exists(Exiled.API.Features.Paths.Dependencies + "/websocket-sharp.dll"))
            {
                WebClient wc = new WebClient();
                Log.Error("Dependency missing, downloading...");
                ServicePointManager.ServerCertificateValidationCallback += API.ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                wc.DownloadFile("https://cdn.cedmod.nl/files/websocket-sharp.dll", Paths.Dependencies + "/websocket-sharp.dll");
                Application.Quit();
            }
            config = Config;
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new StuiterParentCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new LightsoutCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new FFADisableCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new PlayersCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new TotalBansCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new PriorBansCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new RedirectCommand());
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new DiaglogParentCommand());
            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(new StuiterParentCommand());
            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(new LightsoutCommand());
            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(new RedirectCommand());
            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(new DiaglogParentCommand());
            QueryProcessor.DotCommandHandler.RegisterCommand(new TotalBansCommand());
            QueryProcessor.DotCommandHandler.RegisterCommand(new PriorBansCommand());
            RegisterEvents();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            UnregisterEvents();
        }

        /// <summary>
        /// Registers the plugin events.
        /// </summary>
        private void RegisterEvents()
        {
            server = new Handlers.Server();
            player = new Handlers.Player();
            Exiled.Events.Handlers.Server.RestartingRound += server.OnRoundRestart;
            Exiled.Events.Handlers.Server.LocalReporting += server.OnReport;
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += server.OnSendingRemoteAdmin;
            
            Exiled.Events.Handlers.Player.Verified += player.OnJoin;
            Exiled.Events.Handlers.Player.Dying += player.OnDying;
        }

        /// <summary>
        /// Unregisters the plugin events.
        /// </summary>
        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= server.OnRoundRestart;
            Exiled.Events.Handlers.Server.LocalReporting -= server.OnReport;
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= server.OnSendingRemoteAdmin;
            
            Exiled.Events.Handlers.Player.Verified -= player.OnJoin;
            Exiled.Events.Handlers.Player.Dying -= player.OnDying;

            server = null;
            player = null;
        }
    }
}