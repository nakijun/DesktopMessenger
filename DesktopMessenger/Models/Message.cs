﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Models
{
    internal class Message
    {
        public Contact Contact { get; set; }
        public string Content { get; set; }
    }
}
