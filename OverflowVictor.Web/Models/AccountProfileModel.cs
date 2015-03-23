﻿using System;

namespace OverflowVictor.Web.Models
{
    public class AccountProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int Reputation { get; set; }
        public int Questions { get; set; }
        public int Answers { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Views { get; set; }
        public DateTime LastSeen { get; set; }

    }
}