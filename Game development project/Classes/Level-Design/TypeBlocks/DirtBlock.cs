using Default_Block;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design.TypeBlocks
{
    internal class DirtBlock : Block
    {
        public DirtBlock(Rectangle newrectangle) : base(newrectangle)
        {
            this.Rectangle = newrectangle;
            
        }
    }
}
