using Default_Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design.TypeBlocks
{
    internal class CastleGroundBlock : Block
    {
        public CastleGroundBlock(Rectangle newrectangle) : base(newrectangle)
        {
            texture = Content.Load<Texture2D>("Castle_Ground");
            this.Rectangle = newrectangle;

        }
    }
}
