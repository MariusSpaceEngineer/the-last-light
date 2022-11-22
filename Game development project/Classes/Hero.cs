using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project.Classes
{
    
    internal class Hero : IGameObject
    {
        //There can only be one hero: singelton applied
        private static Hero uniqueHero;

        public static Hero GetHero(Texture2D blokTexture, Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D moveSprite, IInputReader inputReader) {
            if (uniqueHero == null)
            {
                uniqueHero = new Hero(blokTexture,attackSprite, damageSprite, deathSprite, idleSprite, jumpSprite, jumpFallInBetween, moveSprite, new KeyboardReader());
            }
            return uniqueHero; 
        }
        //Same like the Skeleton class, maybe set this in an interface
        //All the sprites that hero has
        private Texture2D attackSprite;
        private Texture2D damageSprite;
        private Texture2D deathSprite;
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D jumpFallInBetween;
        private Texture2D moveSprite;
      

        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation jumpFallInBetweenAnimation;
        private Animation moveAnimation;


        private static Vector2 position;

        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }
            
        }

        private Vector2 speed;
        private IInputReader inputReader;

        //In ICanJump interface?
        private bool hasJumped = true;

        //It's not used
        SpriteStates spriteState = KeyboardReader.SpriteState;

        private Rectangle boundingBox;

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        private Texture2D blokTexture;


        private Hero(Texture2D blokTexture,Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D moveSprite, IInputReader inputReader)
        {
            this.attackSprite = attackSprite;
            this.damageSprite = damageSprite;
            this.deathSprite = deathSprite;
            this.idleSprite = idleSprite;
            this.jumpSprite = jumpSprite;
            this.jumpFallInBetween = jumpFallInBetween;
            this.moveSprite = moveSprite;


            //In a method maybe
            attackAnimation = new Animation(4);
            attackAnimation.GetFramesFromTextureProperties(attackSprite.Width, attackSprite.Height, 4, 1);
           

            damageAnimation = new Animation(1);
            damageAnimation.GetFramesFromTextureProperties(damageSprite.Width, damageSprite.Height, 1, 1);

            deathAnimation = new Animation(10);
            deathAnimation.GetFramesFromTextureProperties(deathSprite.Width, deathSprite.Height, 10, 1);

            idleAnimation = new Animation(10);
            idleAnimation.GetFramesFromTextureProperties(idleSprite.Width, idleSprite.Height, 10, 1);

            jumpAnimation = new Animation(3);
            jumpAnimation.GetFramesFromTextureProperties(jumpSprite.Width, jumpSprite.Height, 3, 1);

            jumpFallInBetweenAnimation = new Animation(2);
            jumpFallInBetweenAnimation.GetFramesFromTextureProperties(jumpFallInBetween.Width, jumpFallInBetween.Height, 2, 1);

            moveAnimation = new Animation(10);
            moveAnimation.GetFramesFromTextureProperties(moveSprite.Width, moveSprite.Height, 10, 1);

            this.inputReader = inputReader;
            Position = new Vector2(80, 0);
            this.speed = new Vector2(2, 0);

            this.blokTexture = blokTexture;
            this.blokTexture.SetData(new[] { Color.White });
            this.BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);



            //animation = new Animation.Animation();
            //animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 10, 1);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
            SpriteStates spriteState = KeyboardReader.SpriteState;
            Direction spriteDirection = KeyboardReader.SpriteDirection;
            SpriteEffects flipEffect = SpriteEffects.FlipHorizontally;

            //To many cases, not open for changes

            switch (spriteState)
            {
                case SpriteStates.Idle:

                    if (spriteDirection == Direction.Left)
                    {
                        spriteBatch.Draw(idleSprite, position, idleAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1, flipEffect, 0);
                        spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(idleSprite, position, idleAnimation.CurrentFrame.SourceRectangle, Color.White);
                        spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                    }

                    break;

                case SpriteStates.Left:
                    spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                    break;

                case SpriteStates.Right:
                    spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White) ;
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                    break;

                case SpriteStates.Up:
                    if (spriteDirection == Direction.Left)
                    {
                        spriteBatch.Draw(jumpSprite, position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1, flipEffect,0);
                        spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                    }

                    else
                    {
                        spriteBatch.Draw(jumpSprite, position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White);
                        spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                    }

                    break;
                case SpriteStates.Down:

                    if (spriteDirection == Direction.Left)
                    {
                        spriteBatch.Draw(jumpFallInBetween, position, jumpFallInBetweenAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1, flipEffect,0);
                    }
                    else
                    {
                        spriteBatch.Draw(jumpFallInBetween, position, jumpFallInBetweenAnimation.CurrentFrame.SourceRectangle, Color.White);

                    }

                    break;

                case SpriteStates.Attack:

                    if (spriteDirection == Direction.Left)
                    {
                        spriteBatch.Draw(attackSprite, position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0) ;
                    }
                    else
                    {
                        spriteBatch.Draw(attackSprite, position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);

                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {

            Move();
            Jump();

            //Not open for changes

            switch (KeyboardReader.SpriteState)
            {
                case SpriteStates.Idle:
                    idleAnimation.Update(gameTime);
                
                    break;

                case SpriteStates.Left:
                    moveAnimation.Update(gameTime);

                    break;

                case SpriteStates.Right:
                    moveAnimation.Update(gameTime);

                    break;

                case SpriteStates.Up:
                    jumpAnimation.Update(gameTime);
                    break;

                case SpriteStates.Down:
                    jumpFallInBetweenAnimation.Update(gameTime);
                    break;

                case SpriteStates.Attack:
                    attackAnimation.Update(gameTime);
                    break;


            }

        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
 
                direction.X *= speed.X;
                position += direction;

            boundingBox.X = (int)position.X + 52;
            boundingBox.Y = (int)position.Y + 40;




            //if (direction.X < 0)
            //{
            //    spriteState = SpriteStates.Left;
            //}
            //if (direction.X > 0)
            //{
            //    spriteState = SpriteStates.Right;
            //}
            //if (direction.Y < 0)
            //{
            //    spriteState = SpriteStates.Up;
            //}
            //if (direction.Y > 0)
            //{
            //    spriteState = SpriteStates.Down;
            //}
            //if (direction.X == 0 && direction.Y == 0)
            //{
            //    spriteState = SpriteStates.Idle;

            //}



        }

        //Not used right now, can be removed
        private Vector2 Limit(Vector2 velocityVector, float maxSpeed)
        {
            if (velocityVector.Length() > maxSpeed)
            {
                var ratio = maxSpeed / velocityVector.Length();
                velocityVector.X *= ratio;
                velocityVector.Y *= ratio;
            }
            return velocityVector;
        }

        public void Jump()
        {

            position.Y += speed.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 10f;
                speed.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped)
            {
                float i = 1;
                speed.Y += 0.15f * i;
                spriteState = SpriteStates.Down;
            }

            if (position.Y + jumpSprite.Height >= 450)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                speed.Y = 0f;
            }
        }

       




    }
}
