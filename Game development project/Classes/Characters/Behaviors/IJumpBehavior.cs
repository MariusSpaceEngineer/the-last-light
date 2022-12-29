﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Behaviors
{
    internal interface IJumpBehavior
    {
        public bool HasJumped { get; set; }
        public float FallVelocity { get; set; }
        void Jump(float jumpHeight, float fallSpeed);
    }
}
