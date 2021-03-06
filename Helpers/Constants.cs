﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaDiscordBot.Helpers
{
    public static class Constants
    {
        public const string LeaderboardWinnerRoleName = "Strava Leaderboard";
        public class LeaderboardRideType
        {
            public const string RealRide = "Ride";
            public const string VirtualRide = "VirtualRide";
        }

        public static class ChallengeType
        {
            public const string Distance = "Distance";
            public const string Elevation = "Elevation";
            public const string DistanceRide = "Longest Ride";
            public const string Power = "Weighted Power Ride";
        }
    }
}
