using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Default_Block;

namespace Game_development_project.Classes.Level_Design.Level1
{
    internal class GrassBlock: Block
    {
        public GrassBlock(Rectangle newrectangle) : base(newrectangle)
        {
            this.isTrigger = false;
            texture = Content.Load<Texture2D>("Textures/TileAssets/GrassTile");
            this.Rectangle = newrectangle;
           
         
        }
    }
}
