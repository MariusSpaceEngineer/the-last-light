using Default_Block;
using Game_development_project.Classes.Level_Design.Level;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.TypeBlocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design.Level2
{
    internal class Level2_BlockFactory : BlockFactory
    {
        public Block CreateBlock(int number, Rectangle rectangle)
        {
            Block block = null;

            if (number == 1)
            {
                block = new CastleGroundBlock(rectangle);
            }
            else if (number == 2)
            {
                block = new DirtBlock(rectangle);
            }
            return block;
        }
    }
}
