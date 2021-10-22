﻿using BedrockService.Shared.Classes;
using BedrockService.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace BedrockService.Service.Server.Management
{
    public class PlayerManager : IPlayerManager
    {
        readonly IServerConfiguration serverConfiguration;
        readonly ILogger logger;

        public PlayerManager(IServerConfiguration serverConfiguration, ILogger logger)
        {
            this.serverConfiguration = serverConfiguration;
            this.logger = logger;
        }

        public void ProcessConfiguration(string[] entries)
        {


        }

        public void PlayerConnected(string username, string xuid)
        {
            IPlayer playerFound = serverConfiguration.GetPlayerByXuid(xuid);
            if (playerFound == null)
            {
                serverConfiguration.AddUpdatePlayer(new Player(xuid, username, DateTime.Now.Ticks.ToString(), "0", "0", false, serverConfiguration.GetProp("default-player-permission-level").ToString(), false, false));
                return;
            }
            serverConfiguration.AddUpdatePlayer(playerFound);
        }

        public void PlayerDisconnected(string xuid)
        {
            IPlayer playerFound = serverConfiguration.GetPlayerByXuid(xuid);
            string[] oldTimes = playerFound.GetTimes();
            playerFound.UpdateTimes(oldTimes[1], DateTime.Now.Ticks.ToString());
        }

        public void UpdatePlayerFromCfg(string xuid, string username, string permission, string whitelisted, string ignoreMaxPlayerLimit)
        {
            IPlayer playerFound = serverConfiguration.GetPlayerByXuid(xuid);
            if (playerFound == null)
            {
                playerFound = new Player(serverConfiguration.GetProp("default-player-permission-level").ToString());
                playerFound.Initialize(xuid, username);
            }
            playerFound.UpdateRegistration(permission, whitelisted, ignoreMaxPlayerLimit);
        }

        public IPlayer GetPlayerByXUID(string xuid)
        {
            if (GetPlayers().Count > 0)
                return serverConfiguration.GetPlayerByXuid(xuid);
            return null;
        }

        public void SetPlayer(IPlayer player)
        {
            try
            {
                serverConfiguration.GetPlayerList()[serverConfiguration.GetPlayerList().IndexOf(player)] = player;
            }
            catch
            {
                serverConfiguration.GetPlayerList().Add(player);
            }
        }

        public List<IPlayer> GetPlayers() => serverConfiguration.GetPlayerList();
    }
}
