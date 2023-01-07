using Default_Block;
using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Level_Design.TypeBlocks
{
    internal class TriggerBlock : Block
    {
        public TriggerBlock(Rectangle newrectangle) : base(newrectangle)
        {
            this.isTrigger = true;
            this.texture = Game1.TriggerBlokTexture;
            this.Rectangle = newrectangle;
          
            
        }
    }
}
