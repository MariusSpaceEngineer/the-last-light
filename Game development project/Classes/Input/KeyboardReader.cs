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
        //public static SpriteStates SpriteState { get; set; }
        //public static Direction SpriteDirection { get; set; }

        public static Direction herodirection;

        public static State characterState;

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


                    characterState = new MoveState();
                    herodirection = new LeftDirection();

                    //SpriteState = SpriteStates.Left;
                    //SpriteDirection = Direction.Left;


                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    direction.X += 1;

                    characterState = new MoveState();
                    herodirection = new RightDirection();

                    //SpriteState = SpriteStates.Right;
                    //SpriteDirection = Direction.Right;

                }
                else if (state.IsKeyDown(Keys.Up))
                {
                    characterState = new JumpState();


                    //SpriteState = SpriteStates.Up;
                }
                else if (state.IsKeyDown(Keys.Down))
                {

                    //SpriteState = SpriteStates.Down;

                }

                else if (state.IsKeyDown(Keys.F))
                {
                    characterState = new AttackState();

                    //SpriteState = SpriteStates.Attack;
                    attacking = true;
                }

                else
                {
                    characterState = new IdleState();
                    // SpriteState = SpriteStates.Idle;
                }
            }

            return direction;
        }

    }
}

