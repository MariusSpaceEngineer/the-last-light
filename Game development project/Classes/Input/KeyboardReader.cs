using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project.Classes.Input
{

    internal class KeyboardReader : IInputReader
    {
       
        //Used in by characters to determine the direction and their state if they use this reader
        public static IDirection characterDirection;
        public static State characterState;

        public Vector2 ReadInput()
        {
            //Used to stop the character from moving when attacking; when true the character can't move
            bool attacking = false;

            //Gets the inputState
            KeyboardState state = (KeyboardState)GetInputState();

            Vector2 direction = Vector2.Zero;

            if (!attacking)
            {
                if (state.IsKeyDown(Keys.Left))
                {
                    direction.X -= 1;

                    characterState = new MoveState();
                    characterDirection = new LeftDirection();
                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    direction.X += 1;

                    characterState = new MoveState();
                    characterDirection = new RightDirection();
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

