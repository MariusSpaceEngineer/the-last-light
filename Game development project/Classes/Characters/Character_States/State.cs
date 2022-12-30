using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Character_States
{
    internal abstract class State
    {
        public void Draw(SpriteBatch spriteBatch, Texture2D spriteTexture, Animation animation, Direction direction, Vector2 spritePosition, Sprite sprite)
        {
            SpriteEffects flipHorizontallyEffect = SpriteEffects.FlipHorizontally;

            if (sprite is not Bandit)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(spriteTexture, spritePosition, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipHorizontallyEffect, 0);
                    
                }
                else
                {
                    spriteBatch.Draw(spriteTexture, spritePosition, animation.CurrentFrame.SourceRectangle, Color.White);

                }
                

            }
            else
            {
                if (direction is LeftDirection)
                {
              
                   spriteBatch.Draw(spriteTexture, spritePosition, animation.CurrentFrame.SourceRectangle, Color.White);

                    
                }
                else
                {
                    spriteBatch.Draw(spriteTexture, spritePosition, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipHorizontallyEffect, 0);
                }
                

            }
        }   
    }
}
