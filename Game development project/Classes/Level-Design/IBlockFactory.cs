using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Default_Block;
using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Level_Design.Level
{
    public interface IBlockFactory
    {
        public Block CreateBlock(int number, Rectangle rectangle);
        
    }
}
