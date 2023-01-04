using Default_Block;
using Default_Level;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Level_Design.Level1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Game_development_project.Classes.Animations;
using System.Reflection.PortableExecutable;
using Game_development_project.Classes.Input;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using Game_development_project.Classes.Level_Design.TypeBlocks;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player.PlayerBehaviors;
using Game_development_project.Classes.Miscellaneous;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Player
{

    internal class Hero : Character, IJumpBehavior
    {

        #region Private variables
        //There can only be one hero: singelton applied
        private static Hero uniqueHero;

        //Maybe add in the IJumpBehavior properties for those variables?
        //IJumpBehavior is a interface, can't add the Texture to it, maybe as a property?
        private Texture2D jumpSprite;
        //It's not used, might as well remove it

        //Needed to keep the animations
        //Maybe add the right animation to the right state?
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation moveAnimation;



        //It is assigned in the constructor and it's used in the Move() method
        //The sprite class has a LinearVelocity variabel

        //Used to determine the direction of the player
        private IInputReader inputReader;

        //In ICanJump interface?
        //Used to determine if the hero is in the air, needs some work though
        public bool HasJumped { get; set; }
        public float FallVelocity { get; set; } = 0;


        //The Sprite class has a bouding box, can this one be removed?
        private Rectangle boundingBox;

        //The hero states and the different directions that the hero can be (left, right)
        //Could be added in character but will leave it here for now
        //State can be made an abstract class and can have his own update and draw method
        //public State state;

        #endregion

        #region Get/setters

        //Exists in the sprite class, can it be removed?
        //Later needed for checking the collision between enemies and hero
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }


        #endregion

        //In the sprite class, can it be removed?


        //Used by the gamemanager to change the level of the hero for the correct collision
        public Level CurrentLevel;

        //If the bool is true it triggers the gameover screen
        //Is equal to 2 hits of a melee enemy, maybe a beter way to do it than this?
        //Used to stop the animation

        //Used by the jump method, should be put on top
        bool isOnObject = false;

        public bool isOnTrigger = false;



        #region Initialize

        //Used in to Jump method en initialized in the constructor, maybe it can be added to the IJumpBehavior?


        //Some sprites are assigned in the base constructor
        private Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D boundingBox, Level level) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            //Maybe find a way to remove this also
            this.jumpSprite = jumpSprite;



            //Maybe create those animations in the character state?
            attackAnimation = CreateAnimation(this.attackSprite, 4, 4, 1);
            damageAnimation = CreateAnimation(this.damageSprite, 1, 1, 1);
            deathAnimation = CreateAnimation(this.deathSprite, 10, 10, 1);
            idleAnimation = CreateAnimation(this.idleSprite, 10, 10, 1);
            jumpAnimation = CreateAnimation(this.jumpSprite, 3, 3, 1);
            moveAnimation = CreateAnimation(this.moveSprite, 10, 10, 1);

            inputReader = new KeyboardReader();

            //Start position of the hero
            //Maybe should be adjustable in the constructor?
            Position = new Vector2(-1, 0);
            //The default moving speed of the hero
            //Maybe rename it to something else
            HorizontalVelocity = 6;

            //Used for the bouding box, will later be removed or set to color white
            boundingBoxTexture = boundingBox;
            //This is set in the sprite class

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

            //Needed for the CheckCollision() method
            CurrentLevel = level;
        }

        public static Hero GetHero()
        {
            return uniqueHero;
        }
        public static Hero GetHero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D boundingBox, Level level)
        {
            if (uniqueHero == null)
            {
                uniqueHero = new Hero(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, jumpSprite, boundingBox, level);
            }
            return uniqueHero;
        }
        #endregion

        #region Public methods
        //Resets the position and life of the hero because he's unique he doesn't get initialized again so he has to be moved manually
        //Maybe if we use the method of GetHero with parameters will do the same?
        public void ResetHero()
        {
            Position = new Vector2(-1, 0);
            FallVelocity = 0;
            Health = 100;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //Maybe it's better if we use a for loop with breaks that only triggers when the hero state and direction are the same as the one in the loop
            Direction = KeyboardReader.herodirection;
            //In the KeyboardReader there are also states for the hero
            //Maybe find a way to keep his states in one class instead of different ones?
            CharacterState = KeyboardReader.characterState;

            if (HasDied)
            {
                CharacterState = new DeathState();

            }
            else
            {
                //This conditional is needed otherwise he is in DamagedState all the time 
                if (IsHit)
                {
                    CharacterState = new DamagedState();

                }
            }
            if (CharacterState is IdleState)
            {
                CharacterState.Draw(spriteBatch, idleSprite, idleAnimation, Direction, Position, this);
            }
            else if (CharacterState is MoveState)
            {
                CharacterState.Draw(spriteBatch, moveSprite, moveAnimation, Direction, Position, this);
            }
            else if (CharacterState is JumpState)
            {
                CharacterState.Draw(spriteBatch, jumpSprite, jumpAnimation, Direction, Position, this);
            }
            else if (CharacterState is AttackState)
            {
                CharacterState.Draw(spriteBatch, attackSprite, attackAnimation, Direction, Position, this);

            }
            else if (CharacterState is DamagedState)
            {
                CharacterState.Draw(spriteBatch, damageSprite, damageAnimation, Direction, Position, this);
                IsHit = false;
            }
            else if (CharacterState is DeathState)
            {
                CharacterState.Draw(spriteBatch, idleSprite, idleAnimation, Direction, Position, this);

                //CharacterState.Draw(spriteBatch, deathSprite, deathAnimation, Direction, Position, this);
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Used to reset the position of the attackbox, otherwise it remains the same place triggering weird collision of which the hero or enemy can die
            attackBox = new Rectangle();


            //If the player is still alive he can move and jump
            if (!HasDied)
            {
                //Maybe remove the parameter level from Move method and instead make a different method
                Move(CurrentLevel);
                Jump(-6f, 0.15f);
                CharacterState = KeyboardReader.characterState;
                foreach (var sprite in sprites)
                {
                    Attack(sprite);

                }
            }
            else
            {
                //Stops the player from moving when dead
                //Could remain but it's not necessary if we change the gamestate
                HorizontalVelocity = 0;

            }

            //This loop can be added to a method: checkAttackBoxCollision()?


            //Also here, maybe added to the state class and use a foreach loop 
            //Needs to use the reader else the animation will give an error when drawing on screen

            if (CharacterState is IdleState)
            {
                idleAnimation.Update(gameTime);

            }
            else if (CharacterState is DeathState)
            {

                deathAnimation.Update(gameTime);
            }
            else if (CharacterState is DamagedState)
            {

                damageAnimation.Update(gameTime);

            }
            else if (CharacterState is MoveState)
            {
                moveAnimation.Update(gameTime);
            }
            else if (CharacterState is JumpState)
            {
                jumpAnimation.Update(gameTime);
            }
            else if (CharacterState is AttackState)
            {
                attackAnimation.Update(gameTime);
            }


        }

        #endregion

        #region Private methods

        public override void Attack(Sprite sprite)
        {

            if (sprite.AttackBox.Intersects(boundingBox))
            {
                if (Health > 0)
                {
                    Debug.WriteLine("player hit");

                    IsHit = true;
                    CharacterState = new DamagedState();
                    if (sprite.AttackBox.TouchRightOf(boundingBox))
                    {
                        Debug.WriteLine("Enemy attacks right");
                        Direction = new RightDirection();
                    }
                    else
                    {
                        Debug.WriteLine("Enemy attacks left");
                        Direction = new LeftDirection();
                    }

                    Health--;
                }
                else
                {
                    Debug.WriteLine("player dead");
                    HasDied = true;
                    CharacterState = new DeathState();

                }
            }
        }
        public void Move(Level level)
        {
            //Reads input
            var direction = inputReader.ReadInput();

            //Adjusts x-direction of character 

            direction.X *= HorizontalVelocity;
            //Updates the position with the new direction
            Position += direction;

            //This can remain as they are dependent of one another
            MoveBoundingBox(Position);
            if (CharacterState is AttackState)
            {
                MoveAttackBox();
            }

            //This can be his own method in the update
            CheckCollisionWithLevel(level);

        }

        public override void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 52;
            boundingBox.Y = (int)position.Y + 40;
        }

        public override void MoveAttackBox()
        {
            if (Direction is LeftDirection)
            {
                attackBox = new Rectangle(BoundingBox.Left - 50, BoundingBox.Y, 30, 40);


            }
            else
            {
                attackBox = new Rectangle(BoundingBox.Right + 10, BoundingBox.Y, 30, 40);

            }
        }


        private void CheckCollisionWithLevel(Level level)
        {
            //Checks if the bounding box intersects with ther other rectangles from the tileset
            foreach (Block block in level.TileList)
            {

                CheckCollision(block.Rectangle);
                //Maybe put in the collision method
                if (block is TriggerBlock)
                {
                    if (boundingBox.TouchTopOf(block.Rectangle))
                    {
                        Debug.WriteLine("player touches trigger");
                        isOnTrigger = true;

                    }

                }

            }
        }

        public void Jump(float jumpHeight, float fallSpeed)
        {
            //Maybe use the collision helper to stop the player from falling, it may fix the little jumping when on a new block


            Position.Y += FallVelocity;

            //Use the inputreader here maybe?
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && HasJumped == false)
            {
                FallVelocity = jumpHeight;
                HasJumped = true;
                isOnObject = false;
            }

            if (HasJumped && isOnObject == false)
            {
                float i = 1;
                FallVelocity += fallSpeed * i;


            }
            else if (HasJumped == false && isOnObject == true )
            {
                FallVelocity = 0f;
            }
            //Stops with jumping when hits the buttom of the screen
            //Not used can be removed

            if (isOnObject == false)
            {
                HasJumped = false;
            }
            else if (isOnObject == false)
            {
                HasJumped = true;
            }


            //if (HasJumped == false && isOnObject == true)
            //{
            //    FallVelocity = 0f;

            //}

        }

        public override void CheckCollision(Rectangle newRectangle)
        {
            if (boundingBox.TouchTopOf(newRectangle))
            {
                isOnObject = true;
                HasJumped = false;
                //Last int value depends on the size of the tilemap and sprite
                //If the last value is bigger than 43 the player will start jumping on the tile for a while
                Position.Y = newRectangle.Top - boundingBox.Height - 43;
                Debug.WriteLine("Touching top");
               
                

            }
            else
            {
                isOnObject = false;
            }
            if (boundingBox.TouchBottomOf(newRectangle))
            {
                Position.Y = newRectangle.Bottom;
            }
            if (boundingBox.TouchLeftOf(newRectangle))
            {
                //Last int value depends on the size of the tilemap and sprite
                Position.X = newRectangle.X - boundingBox.Width - 60;
                Debug.WriteLine("Touching left");

            }
            if (boundingBox.TouchRightOf(newRectangle))
            {
                Position.X = newRectangle.Right - boundingBox.Width - 20;
                Debug.WriteLine("Touching right");

            }
            if (Position.X < 0)
            {
                Position.X = 0;
                Debug.WriteLine("Touching left border");

            }
            //If the buttom of the screen is touched by the boudingbox of the hero the GameOverScreen will be triggered
            if (boundingBox.Bottom >= 600)
            {
                Debug.WriteLine("Player touches the buttom of the screen.");
                HasDied = true;
                CharacterState = new DeathState();
            }

            #endregion


        }
    }
}
