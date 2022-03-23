﻿using System.Collections.Generic;

namespace CedMod.ApiModals
{
    public class PlayerObject
    {
        public string Name { get; set; }
        public bool Staff { get; set; }
        public bool DoNotTrack { get; set; }
        public string UserId { get; set; }
        public int PlayerId { get; set; }
        public RoleType RoleType { get; set; }
    }
}