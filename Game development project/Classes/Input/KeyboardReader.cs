using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Input
{

    internal class KeyboardReader : IInputReader
    {
       
        public static Direction herodirection;

        public static State characterState;

        public Vector2 ReadInput()
        {
            bool attacking = false;
            KeyboardState state = (KeyboardState)GetInputState();
            Vector2 direction = Vector2.Zero;

            if (!attacking)
            {
                if (state.IsKeyDown(Keys.Left))
                {

                    direction.X -= 1;


                    characterState = new MoveState();
                    herodirection = new LeftDirection();
                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    direction.X += 1;

                    characterState = new MoveState();
                    herodirection = new RightDirection();
                }
                else if (state.IsKeyDown(Keys.Up))
                {
                    characterState = new JumpState();
                }

                else if (state.IsKeyDown(Keys.F))
                {
                    characterState = new AttackState();
                    attacking = true;
                }

                else
                {
                    characterState = new IdleState();     
                }
            }

            return direction;
        }

        public object GetInputState()
        {
            return Keyboard.GetState();
        }

    }
}

