using Default_Block;
using Default_Level;
using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Input;
using Game_development_project.Classes.Level_Design.TypeBlocks;
using Game_development_project.Classes.Miscellaneous;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player.PlayerBehaviors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Player
{

    internal class Hero : Character, IJumpBehavior
    {

        #region Private variables

        //There can only be one hero: singelton applied
        private static Hero uniqueHero;

        //The hero is the only character who has a jump function and has a sprite for it
        private Texture2D jumpSprite;
       
        private Animation attackAnimation;
        private Animation damageAnimation;
        //private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation moveAnimation;

        //Needs to be included otherwise the collision stops working
        private Rectangle boundingBox;

        private Level currentLevel;

        //Bool used to check if the hero is on an tile
        bool isOnObject = false;
        //Used to trigger the level complete state
        private bool isOnTrigger = false;

        //Used to determine the direction of where the hero is going
        private IInputReader inputReader;

        #endregion

        #region Get/setters

       //Comes from the IJumpBehvaior
        public Texture2D JumpSprite 
        { 
            get { return jumpSprite; } 
            set { jumpSprite = value; } 
        }

        public Level CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        //It uses the variable in the hero class, if removed the collision stops working
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        //Checks if the hero has jumped, comes from the IJumpBehavior
        public bool HasJumped { get; set; }
        //The fall speed of the hero if he's in the air
        public float FallVelocity { get; set; } = 0;

        public bool IsOnTrigger
        {
            get { return isOnTrigger; }
            set { isOnTrigger = value; }
        }

       


        #endregion

        #region Initialize

        //Some sprites are assigned in the base constructor
        private Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D boundingBox, Level level) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {

            JumpSprite = jumpSprite;
           
            //Creating animations for the sprites
            this.attackAnimation = CreateAnimation(this.AttackSprite, 4, 4, 1);
            this.damageAnimation = CreateAnimation(this.DamageSprite, 1, 1, 1);
            //this.deathAnimation = CreateAnimation(this.DeathSprite, 10, 10, 1);
            this.idleAnimation = CreateAnimation(this.IdleSprite, 10, 10, 1);
            this.jumpAnimation = CreateAnimation(this.jumpSprite, 3, 3, 1);
            this.moveAnimation = CreateAnimation(this.MoveSprite, 10, 10, 1);

            this.inputReader = new KeyboardReader();

            //Start position of the hero
            //Maybe should be adjustable in the constructor?
            Position = new Vector2(-1, 0);
            
            //Default horizontal velocity of the hero
            HorizontalVelocity = 6;

            //Used for the bouding box, will later be removed or set to color white
            BoundingBoxTexture = boundingBox;

            //Adjusting the bounding box on the sprite
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

            //Needed for the collision with level method
            CurrentLevel = level;
        }
        #endregion

        #region Singleton
       
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
        //Resets the hero when changing game-state
        public void ResetHero()
        {
            Position = new Vector2(-1, 0);
            FallVelocity = 0;
            Health = 100;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //KeyboardReader has a static variables which are needed to determine the direction and state of the hero
            Direction = KeyboardReader.characterDirection;
            CharacterState = KeyboardReader.characterState;

            if (HasDied)
            {
                CharacterState = new DeathState();
            }
            else
            {
                if (IsHit)
                {
                    CharacterState = new DamagedState();

                }
            }
            if (CharacterState is IdleState)
            {
                CharacterState.Draw(spriteBatch, IdleSprite, idleAnimation, Direction, Position, this);
            }
            else if (CharacterState is MoveState)
            {
                CharacterState.Draw(spriteBatch, MoveSprite, moveAnimation, Direction, Position, this);
            }
            else if (CharacterState is JumpState)
            {
                CharacterState.Draw(spriteBatch, jumpSprite, jumpAnimation, Direction, Position, this);
            }
            else if (CharacterState is AttackState)
            {
                CharacterState.Draw(spriteBatch, AttackSprite, attackAnimation, Direction, Position, this);

            }
            else if (CharacterState is DamagedState)
            {
                CharacterState.Draw(spriteBatch, DamageSprite, damageAnimation, Direction, Position, this);
                //Resets the sprite 
                IsHit = false;
            }
            else if (CharacterState is DeathState)
            {
                CharacterState.Draw(spriteBatch, IdleSprite, idleAnimation, Direction, Position, this);

                //CharacterState.Draw(spriteBatch, deathSprite, deathAnimation, Direction, Position, this);
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            //Used to reset the position of the attackbox, otherwise it remains the same place triggering weird collision of which the enemy can die
            attackBox = new Rectangle();

            if (!HasDied)
            {
                Move();
                CheckCollisionWithLevel(CurrentLevel);
                Jump(-6f, 0.15f);
             
                //If places before the methods or after it will cause error's
                CharacterState = KeyboardReader.characterState;

                foreach (var sprite in sprites)
                {
                    CheckHitCollision(sprite);

                }
                //Updates the animation the current state
                UpdateAnimation(CharacterState, gameTime);

            }    

        }

        public override void CheckHitCollision(Sprite enemy)
        {
            if (enemy.AttackBox.Intersects(boundingBox))
            {
                if (Health > 0)
                {
                    //Debug.WriteLine("player hit");

                    IsHit = true;
                    CharacterState = new DamagedState();
                    if (enemy.AttackBox.TouchRightOf(boundingBox))
                    {
                        //Debug.WriteLine("Enemy attacks right");
                        Direction = new RightDirection();
                    }
                    else
                    {
                        //Debug.WriteLine("Enemy attacks left");
                        Direction = new LeftDirection();
                    }

                    Health--;
                }
                else
                {
                    //Debug.WriteLine("player dead");
                    HasDied = true;
                    CharacterState = new DeathState();

                }
            }
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

        public override void CheckCollision(Rectangle newRectangle)
        {
            if (boundingBox.TouchTopOf(newRectangle))
            {
                isOnObject = true;
                HasJumped = false;
                //The last value depends on the size of the tilemap and sprite
                //If the last value is bigger than 43 the player will start jumping on the tile for a while
                Position.Y = newRectangle.Top - boundingBox.Height - 43;
                //Debug.WriteLine("Touching top");



            }
            else
            {
                isOnObject = false;
            }
            if (boundingBox.TouchBottomOf(newRectangle))
            {
                Position.Y = newRectangle.Bottom -  28;
            }
            if (boundingBox.TouchLeftOf(newRectangle))
            {
                Position.X = newRectangle.X - boundingBox.Width - 60;
                //Debug.WriteLine("Touching left");

            }
            if (boundingBox.TouchRightOf(newRectangle))
            {
                Position.X = newRectangle.Right - boundingBox.Width - 20;
                //Debug.WriteLine("Touching right");

            }
            if (Position.X < 0)
            {
                Position.X = 0;
                //Debug.WriteLine("Touching left border");

            }
            //If the buttom of the screen is touched by the boudingbox of the hero the GameOverScreen will be triggered
            if (boundingBox.Bottom >= 600)
            {
                //Debug.WriteLine("Player touches the buttom of the screen.");
                HasDied = true;
                CharacterState = new DeathState();
            }

        }

        #endregion



        #region Private methods

        private void UpdateAnimation(State characterState, GameTime gameTime)
        {
            if (characterState is IdleState)
            {
                idleAnimation.Update(gameTime);

            }
            else if (characterState is DeathState)
            {
                idleAnimation.Update(gameTime);
               //deathAnimation.Update(gameTime);
            }
            else if (characterState is DamagedState)
            {

                damageAnimation.Update(gameTime);

            }
            else if (characterState is MoveState)
            {
                moveAnimation.Update(gameTime);
            }
            else if (characterState is JumpState)
            {
                jumpAnimation.Update(gameTime);
            }
            else if (characterState is AttackState)
            {
                attackAnimation.Update(gameTime);
            }
        }


        private void Move()
        {
            //Reads input
            var direction = inputReader.ReadInput();

            //Adjusts x-direction of the hero 
            direction.X *= HorizontalVelocity;
            //Updates the position with the new direction
            Position += direction;

            //Moves the bounding box of the hero
            MoveBoundingBox(Position);

            if (CharacterState is AttackState)
            {
                MoveAttackBox();
            }         

        }

        private void CheckCollisionWithLevel(Level level)
        {
            foreach (Block block in level.TileList)
            {
                //The collision between the hero and the level is checked in this method
                CheckCollision(block.Rectangle);

                //Checks if the block is a trigger, if it is the LevelCompleteState game-state will be triggered
                if (block is TriggerBlock)
                {
                    if (boundingBox.TouchTopOf(block.Rectangle))
                    {
                        //Debug.WriteLine("player touches trigger");
                        IsOnTrigger = true;

                    }

                }

            }
        }

        public void Jump(float jumpHeight, float fallSpeed)
        {

            Position.Y += FallVelocity;

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
           
            if (isOnObject == false)
            {
                HasJumped = true;
            }
        }
        #endregion


    }
}
