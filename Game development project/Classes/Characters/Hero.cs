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
using Game_development_project.Classes.Animations;
using System.Reflection.PortableExecutable;
using Game_development_project.Classes.Input;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using Game_development_project.Classes.Level_Design.TypeBlocks;

namespace Game_development_project.Classes.Characters
{

    internal class Hero : Character, IGameObject, IJumpBehavior
    {

        #region Private variables
        //There can only be one hero: singelton applied
        private static Hero uniqueHero;

        //Maybe add in the IJumpBehavior properties for those variables?
        //IJumpBehavior is a interface, can't add the Texture to it, maybe as a property?
        private Texture2D jumpSprite;
        //It's not used, might as well remove it
        private Texture2D fallSprite;

        //Needed to keep the animations
        //Maybe add the right animation to the right state?
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation fallAnimation;
        private Animation moveAnimation;

      

        //It is assigned in the constructor and it's used in the Move() method
        //The sprite class has a LinearVelocity variabel
        //private Vector2 speed;

        //Used to determine the direction of the player
        private IInputReader inputReader;

        //In ICanJump interface?
        //Used to determine if the hero is in the air, needs some work though
        private bool hasJumped = false;

     
        //The Sprite class has a bouding box, can this one be removed?
        private Rectangle boundingBox;

        //Texture for the bounding box
        private Texture2D blokTexture;

        //The levels
        //private Level1 level1;

        //The hero states and the different directions that the hero can be (left, right)
        //Could be added in character but will leave it here for now
        //State can be made an abstract class and can have his own update and draw method
        public State state;
        private Direction direction;

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
        private Rectangle attackBox;

        public Rectangle AttackBox
        {
            get { return attackBox; }
            set { attackBox = value; }
        }

        //Used by the gamemanager to change the level of the hero for the correct collision
        public Level level;

        //If the bool is true it triggers the gameover screen
        public bool hasDied = false;
        //Is equal to 2 hits of a melee enemy, maybe a beter way to do it than this?
        public int lifes = 100;
        //Used to stop the animation
        private bool isHit = false;


        #region Initialize

        //Used in to Jump method en initialized in the constructor, maybe it can be added to the IJumpBehavior?
        private float LinearFallVelocity = 0;

        //Some sprites are assigned in the base constructor
        private Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D boundingBox, Level level) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            //Maybe find a way to remove this also
            this.jumpSprite = jumpSprite;
            //Not used can be removed
            //this.fallSprite = jumpFallInBetween;

            
            //Maybe create those animations in the character state?
            this.attackAnimation = CreateAnimation(this.attackSprite, 4, 4, 1);
            this.damageAnimation = CreateAnimation(this.damageSprite, 1, 1, 1);
            this.deathAnimation = CreateAnimation(this.deathSprite, 10, 10, 1);
            this.idleAnimation = CreateAnimation(this.idleSprite, 10, 10, 1);
            this.jumpAnimation = CreateAnimation(this.jumpSprite, 3, 3, 1);
            //Not used can be removed
            //this.fallAnimation = CreateAnimation(this.fallSprite, 2, 2, 1);
            this.moveAnimation = CreateAnimation(this.moveSprite, 10, 10, 1);

            this.inputReader = new KeyboardReader();

            //Start position of the hero
            //Maybe should be adjustable in the constructor?
            Position = new Vector2(-1, 0);
            //The default moving speed of the hero
            //Maybe rename it to something else
            //speed = new Vector2(4, 0);
            LinearVelocity = 6;

            //Used for the bouding box, will later be removed or set to color white
            this.blokTexture = boundingBox;
            //This is set in the sprite class
            //this.blokTexture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

            //Needed for the CheckCollision() method
            this.level = level;
        }

        public static Hero GetHero()
        { 
            return uniqueHero;
        }
        public static Hero GetHero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D fallSprite, Texture2D boundingBox, Level level)
        {
            if (uniqueHero == null)
            {
                uniqueHero = new Hero(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, jumpSprite, fallSprite, boundingBox, level);
            }
            return uniqueHero;
        }
        #endregion

        #region Public methods
        //Resets the position and life of the hero because he's unique he doesn't get initialized again so he has to be moved manually
        //Maybe if we use the method of GetHero with parameters will do the same?
        public void ResetHero()
        {
            this.Position = new Vector2(-1, 0);
            this.lifes = 100;
        }

        
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Maybe it's better if we use a for loop with breaks that only triggers when the hero state and direction are the same as the one in the loop

            //flips the sprite horizontally; 
            SpriteEffects flipEffect = SpriteEffects.FlipHorizontally;

            this.direction = KeyboardReader.herodirection;
            //In the KeyboardReader there are also states for the hero
            //Maybe find a way to keep his states in one class instead of different ones?
            this.state = KeyboardReader.characterState;

            

            if (hasDied)
            {
                this.state = new DeathState();
            }
            else
            {
                //This conditional is needed otherwise he is in DamagedState all the time 
                if (isHit)
                {
                    this.state = new DamagedState();
                }
            }

            if (state is IdleState)
            {
                if (direction is LeftDirection)
                {
                    //spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White);

                    spriteBatch.Draw(idleSprite, Position, idleAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else
                {
                    spriteBatch.Draw(idleSprite, Position, idleAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                }
            }
            else if (state is MoveState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
            }
            else if (state is JumpState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(jumpSprite, Position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(jumpSprite, Position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
            }
            else if (state is AttackState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
            }
            else if (state is DamagedState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    //spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    //spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
                //Needed so that the animation stops
                isHit = false;
            }
            else if (state is DeathState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(deathSprite, Position, deathAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    //spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(deathSprite, Position, deathAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                    //spriteBatch.Draw(blokTexture, AttackBox, Color.Pink);
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Nothing in the base, can be removed
            base.Update(gameTime, sprites);
            //Used to reset the position of the attackbox, otherwise it remains the same place triggering weird collision of which the hero or enemy can die
            this.attackBox = new Rectangle();

            //If the player is still alive he can move and jump
            if (!hasDied)
            {
                //Maybe remove the parameter level from Move method and instead make a different method
                Move(level);
                Jump(-6f, 0.15f);
            }
            else
            {
                //Stops the player from moving when dead
                //Could remain but it's not necessary if we change the gamestate
                this.LinearVelocity = 0;

            }

            //This loop can be added to a method: checkAttackBoxCollision()?
            foreach (var sprite in sprites)
            {
              
                if (sprite.AttackBox.Intersects(this.boundingBox))
                {
                    if (lifes > 0 )
                    {
                        Debug.WriteLine("player hit");
                        this.isHit = true;
                        this.state = new DamagedState();
                        lifes--;

                    }
                    else
                    {
                        Debug.WriteLine("player dead");
                        hasDied = true;
                        this.state = new DeathState();
                        //this.LinearVelocity = 0;
                    }     
                }
            }

            //Also here, maybe added to the state class and use a foreach loop 
            //Needs to use the reader else the animation will give an error when drawing on screen
            if (KeyboardReader.characterState is IdleState)
            {
                idleAnimation.Update(gameTime);
               
            }
            else if (state is DeathState)
            {
                deathAnimation.Update(gameTime);
            }
            else if (state is DamagedState)
            {
               // damageAnimation.Update(gameTime);
                moveAnimation.Update(gameTime);

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
        //public void Update(GameTime gameTime, Level level)
        //{
        //    Move(level);
        //    Jump(-6f, 0.15f);

        //    //Needs to use the reader else the animation will give an error when drawing on screen
        //    if (KeyboardReader.characterState is IdleState)
        //    {
        //        idleAnimation.Update(gameTime);
        //    }
        //    else if (KeyboardReader.characterState is MoveState)
        //    {
        //        moveAnimation.Update(gameTime);
        //    }
        //    else if (KeyboardReader.characterState is JumpState)
        //    {
        //        jumpAnimation.Update(gameTime);
        //    }
        //    else if (KeyboardReader.characterState is AttackState)
        //    {
        //        attackAnimation.Update(gameTime);
        //    }
        //}
        #endregion

        #region Private methods

        public void Move(Level level)
        {
            //Reads input
            var direction = inputReader.ReadInput();

            //Adjusts x-direction of character 
            //direction.X *= speed.X;
            direction.X *= LinearVelocity;
            //Updates the position with the new direction
            //position += direction;
            Position += direction;

            //MoveBoundingBox(position);
            //This can remain as they are dependent of one another
            MoveBoundingBox(Position);
            if (state is AttackState)
            {
                MoveAttackBox(Position);
            }
          
            //This can be his own method in the update
            CheckCollisionWithLevel(level);

        }

        private void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 52;
            boundingBox.Y = (int)position.Y + 40;
        }

        private void MoveAttackBox(Vector2 position)
        {
            if (this.direction is LeftDirection)
            {
                this.attackBox = new Rectangle((int)BoundingBox.Left- 50, (int)BoundingBox.Y, 30, 40);


            }
            else
            {
                this.attackBox = new Rectangle((int)BoundingBox.Right - 10, (int)BoundingBox.Y, 30, 40);

            }
        }
        //Used by the jump method, should be put on top
        bool isOnObject = false;
        private void CheckCollisionWithLevel(Level level)
        {
            //Checks if the bounding box intersects with ther other rectangles from the tileset
            foreach (Block block in level.TileList)
            {
                
                Collision(block.Rectangle, level.Width, level.Height);
                //Maybe put in the collision method
                if (block is DirtBlock)
                {
                    if (boundingBox.TouchTopOf(block.Rectangle))
                    {
                        Debug.WriteLine("player touches trigger");
                    }
                }
              
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

        public void Jump(float jumpHeight,float fallSpeed)
        {
            //Maybe use the collision helper to stop the player from falling, it may fix the little jumping when on a new block
           
            Position.Y += LinearFallVelocity;

            //Use the inputreader here maybe?
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                //position.Y -= jumpHeight;
                LinearFallVelocity = jumpHeight;
                hasJumped = true;
            }

            if (hasJumped)
            {
                float i = 1;
                LinearFallVelocity += fallSpeed * i;
                
            }
            //Stops with jumping when hits the buttom of the screen
            //Not used can be removed
            if (Position.Y + jumpSprite.Height > 600)
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
                LinearFallVelocity = 0f;
            }
        }

        public override void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (boundingBox.TouchTopOf(newRectangle))
            {
               
                //position.Y = newRectangle.Y - boundingBox.Height;
                //Last int value depends on the size of the tilemap and sprite
                Position.Y = newRectangle.Top - boundingBox.Height - 50;
                Debug.WriteLine("Touching top");
                hasJumped = false;
                isOnObject = true;

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
                hasDied = true;
                state = new DeathState();
            }
            //if (boundingBox.Right >= 1200)
            //{
            //    Debug.WriteLine("Player touches the right of the screen");

            //}
            //Should later be assigned to a variable because now it's hardcoded
            //if (position.X > 1200 - boundingBox.Width -50 )
            //{
            //    position.X= 1200 - boundingBox.Width - 50;
            //    Debug.WriteLine("Touching right border");

            //}
            //Should later be assigned to a variable because now it's hardcoded
            //doesn't work
            //if (position.Y < -600)
            //{
            //    position.Y = -600 + boundingBox.Width + 50;
            //    Debug.WriteLine("Touching top border");

            //}


        }

        //Why is this here??
        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
        #endregion


    }
}
