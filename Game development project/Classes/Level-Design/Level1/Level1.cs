using Game_development_project.Classes.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Default_Block;
using Default_Level;
using Game_development_project.Classes.Level_Design.Level;

namespace Game_development_project.Classes.Level_Design.Level1
{
    internal class Level1 : Default_Level.Level
    {
        //Maybe make a abstract level class and for every level an subclass
        public Level1(BlockFactory blockFactory) : base(blockFactory)
        {
            
        }
    }
}

