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
        public bool HasJumped { get; set; } = false;
        public float FallVelocity { get; set; } = 0;


        //The Sprite class has a bouding box, can this one be removed?
        private Rectangle boundingBox;

        //The hero states and the different directions that the hero can be (left, right)
        //Could be added in character but will leave it here for now
        //State can be made an abstract class and can have his own update and draw method
        //public State state;
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


        //Used by the gamemanager to change the level of the hero for the correct collision
        public Level level;

        //If the bool is true it triggers the gameover screen
        //Is equal to 2 hits of a melee enemy, maybe a beter way to do it than this?
        //Used to stop the animation

        //Used by the jump method, should be put on top
        bool isOnObject = false;


        #region Initialize

        //Used in to Jump method en initialized in the constructor, maybe it can be added to the IJumpBehavior?


        //Some sprites are assigned in the base constructor
        private Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D jumpSprite, Texture2D boundingBox, Level level) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            //Maybe find a way to remove this also
            this.jumpSprite = jumpSprite;



            //Maybe create those animations in the character state?
            this.attackAnimation = CreateAnimation(this.attackSprite, 4, 4, 1);
            this.damageAnimation = CreateAnimation(this.damageSprite, 1, 1, 1);
            this.deathAnimation = CreateAnimation(this.deathSprite, 10, 10, 1);
            this.idleAnimation = CreateAnimation(this.idleSprite, 10, 10, 1);
            this.jumpAnimation = CreateAnimation(this.jumpSprite, 3, 3, 1);
            this.moveAnimation = CreateAnimation(this.moveSprite, 10, 10, 1);

            this.inputReader = new KeyboardReader();

            //Start position of the hero
            //Maybe should be adjustable in the constructor?
            Position = new Vector2(-1, 0);
            //The default moving speed of the hero
            //Maybe rename it to something else
            HorizontalVelocity = 6;

            //Used for the bouding box, will later be removed or set to color white
            this.boundingBoxTexture = boundingBox;
            //This is set in the sprite class

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

            //Needed for the CheckCollision() method
            this.level = level;
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
            this.Position = new Vector2(-1, 0);
            this.Health = 100;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //Maybe it's better if we use a for loop with breaks that only triggers when the hero state and direction are the same as the one in the loop

            //flips the sprite horizontally; 
            SpriteEffects flipEffect = SpriteEffects.FlipHorizontally;

            this.direction = KeyboardReader.herodirection;
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

                if (direction is LeftDirection)
                {


                    spriteBatch.Draw(idleSprite, Position, idleAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
                }
                else
                {
                    spriteBatch.Draw(idleSprite, Position, idleAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);

                }
            }
            else if (CharacterState is MoveState)
            {
                //else if (state is MoveState)
                //{
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                }
            }
            else if (CharacterState is JumpState)
            {

                //else if (state is JumpState)
                //{
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(jumpSprite, Position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(jumpSprite, Position, jumpAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                }
            }
            else if (CharacterState is AttackState)
            {
                if (direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, flipEffect, 0);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Pink);
                }
                else if (direction is RightDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Pink);
                }
            }
            else if (CharacterState is DamagedState)
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
                IsHit = false;
            }
            else if (CharacterState is DeathState)
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
            if (!HasDied)
            {
                //Maybe remove the parameter level from Move method and instead make a different method
                Move(level);
                Jump(-6f, 0.15f);
            }
            else
            {
                //Stops the player from moving when dead
                //Could remain but it's not necessary if we change the gamestate
                this.HorizontalVelocity = 0;

            }

            //This loop can be added to a method: checkAttackBoxCollision()?
            foreach (var sprite in sprites)
            {
                Attack(sprite);

            }


            //Also here, maybe added to the state class and use a foreach loop 
            //Needs to use the reader else the animation will give an error when drawing on screen
            if (KeyboardReader.characterState is IdleState)
            {
                idleAnimation.Update(gameTime);

            }
            else if (CharacterState is DeathState)
            {

                deathAnimation.Update(gameTime);
            }
            else if (CharacterState is DamagedState)
            {

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

        #endregion

        #region Private methods

        public override void Attack(Sprite sprite)
        {
            if (sprite.AttackBox.Intersects(this.boundingBox))
            {
                if (Health > 0)
                {
                    Debug.WriteLine("player hit");
                    this.IsHit = true;
                    CharacterState = new DamagedState();

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
            if (this.direction is LeftDirection)
            {
                this.attackBox = new Rectangle((int)BoundingBox.Left - 50, (int)BoundingBox.Y, 30, 40);


            }
            else
            {
                this.attackBox = new Rectangle((int)BoundingBox.Right + 10, (int)BoundingBox.Y, 30, 40);

            }
        }


        private void CheckCollisionWithLevel(Level level)
        {
            //Checks if the bounding box intersects with ther other rectangles from the tileset
            foreach (Block block in level.TileList)
            {

                CheckCollision(block.Rectangle);
                //Maybe put in the collision method
                if (block is DirtBlock)
                {
                    if (boundingBox.TouchTopOf(block.Rectangle))
                    {
                        Debug.WriteLine("player touches trigger");
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
            }

            if (HasJumped)
            {
                float i = 1;
                FallVelocity += fallSpeed * i;

            }
            //Stops with jumping when hits the buttom of the screen
            //Not used can be removed
            if (Position.Y + jumpSprite.Height > 600)
            {
                HasJumped = false;
            }
            else if (isOnObject == false)
            {
                HasJumped = true;
            }

            if (HasJumped == false)
            {
                FallVelocity = 0f;
            }
        }

        public override void CheckCollision(Rectangle newRectangle)
        {
            if (boundingBox.TouchTopOf(newRectangle))
            {

                //Last int value depends on the size of the tilemap and sprite
                Position.Y = newRectangle.Top - boundingBox.Height - 50;
                Debug.WriteLine("Touching top");
                HasJumped = false;
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
                HasDied = true;
                CharacterState = new DeathState();
            }

            #endregion


        }
    }
}
