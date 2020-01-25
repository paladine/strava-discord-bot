﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaDiscordBot.Exceptions
{
    public class StravaException : Exception
    {
        public StravaException(string message) : base(message) 
        {
        }

        public StravaException()
        {
        }

        public StravaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
