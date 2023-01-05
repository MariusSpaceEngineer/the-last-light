using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Player.PlayerBehaviors
{
    internal interface IJumpBehavior
    {
        public Texture2D JumpSprite { get; set; }
        public bool HasJumped { get; set; }
        public float FallVelocity { get; set; }
        void Jump(float jumpHeight, float fallSpeed);
    }
}
