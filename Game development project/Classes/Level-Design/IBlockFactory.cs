using Default_Block;
using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Level_Design.Level
{
    public interface IBlockFactory
    {
        public Block CreateBlock(int number, Rectangle rectangle);
        
    }
}
