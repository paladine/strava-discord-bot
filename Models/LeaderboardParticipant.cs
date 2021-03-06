﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using StravaDiscordBot.Models.Strava;

namespace StravaDiscordBot.Models
{
    public class LeaderboardParticipant
    {
        public string DiscordUserId { get; set; }
        public string ServerId { get; set; }
        public string StravaId { get; set; }

        public string GetDiscordMention(bool silent = false) => silent ? $"`<@{DiscordUserId}>`" : $"<@{DiscordUserId}>";
        public LeaderboardParticipant() {}
        public LeaderboardParticipant(string serverId, string userId, string stravaId)
        {
            DiscordUserId = userId;
            ServerId = serverId;
            StravaId = stravaId;
        }


    }
}
