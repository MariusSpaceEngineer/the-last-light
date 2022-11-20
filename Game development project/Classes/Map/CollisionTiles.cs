using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Map
{
    internal class CollisionTiles: Tiles
    {
        public CollisionTiles(int i, Rectangle newrectangle)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newrectangle;
        }
    }
}
