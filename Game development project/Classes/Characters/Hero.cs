using Default_Block;
using Default_Level;
using Game_development_project.Classes.Characters.Behaviors;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Level_Design.Level1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

using System.Reflection.PortableExecutable;

namespace Game_development_project.Classes.Characters
{

    internal class Hero : Character, IGameObject, IJumpBehavior
    {

        #region Private variables
        //There can only be one hero: singelton applied
        private static Hero uniqueHero;

        //Maybe add in the IJumpBehavior properties for those variables?
        private Texture2D jumpSprite;
        private Texture2D fallSprite;

        //Needed to keep the animations
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation fallAnimation;
        private Animation moveAnimation;

        //Used by enemies to determine hero location
        private static Vector2 position;

        //It is assigned in the constructor and it's used in the Move() method
        private Vector2 speed;

        //Assigned in the constructor, not really needed but in case new input is added later
        private IInputReader inputReader;

        //In ICanJump interface?
        //Used to determine if the hero is in the air, needs some work though
        private bool hasJumped = false;

        private Rectangle boundingBox;

        //Texture for the bounding box
        private Texture2D blokTexture;

        //The levels
        private Level1 level1;

        //The hero states and the different directions that the hero can be (left, right)
        private State state;
        private Direction direction;

        #endregion

        #region Get/setters

        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }

        }

        //Later needed for checking the collision between enemies and hero
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }
        #endregion

        #region Initialize

        //Some sprites are assigned in the base constructor
        private Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D boundingBox) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            //Maybe find a way to remove this also
            this.jumpSprite = jumpSprite;
            this.fallSprite = jumpFallInBetween;

            this.attackAnimation = CreateAnimation(this.attackSprite, 4, 4, 1);
            this.damageAnimation = CreateAnimation(this.damageSprite, 1, 1, 1);
            this.deathAnimation = CreateAnimation(this.deathSprite, 10, 10, 1);
            this.idleAnimation = CreateAnimation(this.idleSprite, 10, 10, 1);
            this.jumpAnimation = CreateAnimation(this.jumpSprite, 3, 3, 1);
            this.fallAnimation = CreateAnimation(this.fallSprite, 2, 2, 1);
            this.moveAnimation = CreateAnimation(this.moveSprite, 10, 10, 1);

            this.inputReader = new KeyboardReader();

            //Start position of the hero
            Position = new Vector2(-1, 0);
            //The default moving speed of the hero
            speed = new Vector2(2, 0);

            //Used for the bouding box, will later be removed or set to color white
            this.blokTexture = boundingBox;
            this.blokTexture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);
        }

        public static Hero GetHero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D fallSprite, Texture2D boundingBox)
        {
            if (uniqueHero == null)
            {
                uniqueHero = new Hero(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, jumpSprite, fallSprite, boundingBox);
            }
            return uniqueHero;
        }
        #endregion

        #region Public methods

        public void Draw(SpriteBatch spriteBatch)
        {
            //flips the sprite; facing left
            SpriteEffects flipEffect = SpriteEffects.FlipHorizontally;

            this.direction = KeyboardReader.herodirection;
            this.state = KeyboardReader.characterState;

            if (state is IdleState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(idleSprite, position, idleAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else
                {
                    spriteBatch.Draw(idleSprite, position, idleAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                }
            }
            else if (state is MoveState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
            }
            else if (state is JumpState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(jumpSprite, position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(jumpSprite, position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
            }
            else if (state is AttackState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(attackSprite, position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
            }
        }

        public void Update(GameTime gameTime, Level level)
        {
            Move(level);
            Jump(10f, -5f, 0.15f);

            //Needs to use the reader else the animation will give an error when drawing on screen
            if (KeyboardReader.characterState is IdleState)
            {
                idleAnimation.Update(gameTime);
            }
            else if (KeyboardReader.characterState is MoveState)
            {
                moveAnimation.Update(gameTime);
            }
            else if (KeyboardReader.characterState is JumpState)
            {
                jumpAnimation.Update(gameTime);
            }
            else if (KeyboardReader.characterState is AttackState)
            {
                attackAnimation.Update(gameTime);
            }
        }
        #endregion

        #region Private methods

        private void Move(Level level)
        {
            //Reads input
            var direction = inputReader.ReadInput();

            //Adjusts x-direction of character 
            direction.X *= speed.X;
            //Updates the position with the new direction
            position += direction;

            MoveBoundingBox(position);
            CheckCollisionWithLevel(level);

        }

        private void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 52;
            boundingBox.Y = (int)position.Y + 40;
        }
        bool isOnObject = false;
        private void CheckCollisionWithLevel(Level level)
        {
            //Checks if the bounding box intersects with ther other rectangles from the tileset
            foreach (Block block in level.TileList)
            {
                Collision(block.Rectangle, level.Width, level.Height);
            }

            //foreach (Block block in level.TileList)
            //{

            //    //if (IsColliding(block.Rectangle))
            //    //{
            //    //    if (direction.X < 0)
            //    //    {
            //    //        position.X = block.Rectangle.Right - BoundingBox.Width - 22;

            //    //    }
            //    //    else if (direction.X > 0)
            //    //    {
            //    //        position.X = block.Rectangle.Left - BoundingBox.Width - 55;

            //    //    }
            //    //    else if (direction.Y < boundingBox.Y)
            //    //    {

            //    //        position.Y = block.Rectangle.Top - boundingBox.Height - 42;
            //    //        hasJumped = false;

            //    //    }
            //    //    //else
            //    //    //{
            //    //    //    isOnObject = true;
            //    //    //}

            //    //}

            //    if (IsColliding(block.Rectangle))
            //    {

            //        //Right side
            //        //position of the hero is on the left side of the sprite
            //        //if (position.X < block.Rectangle.Location.X + block.Rectangle.Width)
            //        //{
            //        //    position.X = block.Rectangle.Right;

            //        //}
            //        //else if (position.X > block.Rectangle.Left)
            //        //{
            //        //    position.X = block.Rectangle.Left - BoundingBox.Width - 55;

            //        //}
            //        //Right side
            //        //postion of the hero < position of the rectangle right side
            //        //position of hero = position of rectangle right side

            //        if (position.X < block.Rectangle.Right)
            //        {
            //            Debug.WriteLine("it touches");

            //        }
            //        if (position.Y < boundingBox.Y)
            //        {

            //            position.Y = block.Rectangle.Top - boundingBox.Height - 42;
            //            hasJumped = false;

            //        }
            //        else
            //        {
            //            isOnObject = true;
            //        }

            //    }





            //Checks if the hero bounding box intersects with a rectangle

            //if (BoundingBox.Intersects(rectangle))
            //{
            //    if (direction.X < 0)
            //    {
            //        position.X =rectangle.Right - BoundingBox.Width - 22;

            //    }
            //    else if (direction.X > 0)
            //    {
            //        position.X = rectangle.Left - BoundingBox.Width - 55;

            //    }
            //    else if (direction.Y < boundingBox.Y )
            //    {

            //        position.Y = rectangle.Top - boundingBox.Height - 42 ;
            //        //hasJumped = true;
            //    }

            //}


        }

        public void Jump(float jumpHeight, float jumpHeightSpeed, float fallSpeed)
        {
            position.Y += speed.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= jumpHeight;
                speed.Y = jumpHeightSpeed;
                hasJumped = true;
            }

            if (hasJumped)
            {
                float i = 1;
                speed.Y += fallSpeed * i;
                
            }

            if (position.Y + jumpSprite.Height >= 450)
            {
                hasJumped = false;
            }
            else if (isOnObject == false)
            {
                hasJumped = true;
            }

            //else if (!CheckCollsion(rectangle))
            //{
            //    hasJumped = true;
            //}


            if (hasJumped == false)
            {
                speed.Y = 0f;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (boundingBox.TouchTopOf(newRectangle))
            {
                //position.Y = newRectangle.Y - boundingBox.Height;
                //Last int value depends on the size of the tilemap and sprite
                position.Y = newRectangle.Top - boundingBox.Height - 50;
                Debug.WriteLine("Touching top");
                hasJumped = false;
                isOnObject = true;

            }
            else
            {
                isOnObject = false;
            }
            if (boundingBox.TouchLeftOf(newRectangle))
            {
                //Last int value depends on the size of the tilemap and sprite
                position.X = newRectangle.X - boundingBox.Width - 60;
                Debug.WriteLine("Touching left");

            }
            if (boundingBox.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.Right - boundingBox.Width - 20;
                Debug.WriteLine("Touching right");

            }


        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
        #endregion


    }
}
