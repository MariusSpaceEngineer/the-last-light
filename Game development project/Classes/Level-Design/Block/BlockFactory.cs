using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design.Block
{
    internal class BlockFactory
    {
        public Block CreateBlock(int number)
        {

            return new Block();
        }
    }
}
