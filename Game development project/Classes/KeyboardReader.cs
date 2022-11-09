using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes
{
    
    internal class KeyboardReader : IInputReader
    {
        public static SpriteStates SpriteState { get; set; }
        public static Direction SpriteDirection { get; set; }
        

        public Vector2 ReadInput()
        {
            bool attacking = false;
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (!attacking)
            {
                if (state.IsKeyDown(Keys.Left))
                {
                    direction.X -= 1;

                    SpriteState = SpriteStates.Left;
                    SpriteDirection = Direction.Left;


                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    direction.X += 1;
                    SpriteState = SpriteStates.Right;
                    SpriteDirection = Direction.Right;

                }
                else if (state.IsKeyDown(Keys.Up))
                {
                    direction.Y -= 1;
                    SpriteState = SpriteStates.Up;
                   

                }
                else if (state.IsKeyDown(Keys.Down))
                {
                    direction.Y += 1;
                    SpriteState = SpriteStates.Down;

                }

               else if (state.IsKeyDown(Keys.F))
                {
                    SpriteState = SpriteStates.Attack;
                    attacking = true;
                }

                else
                {
                    SpriteState = SpriteStates.Idle;
                }
            }
            
            return direction;
        }

    }
}

