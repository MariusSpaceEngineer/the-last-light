using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.Enemies;
using Game_development_project.Classes.Characters.Character_States;

namespace Game_development_project.Classes.Characters
{
    internal class Skeleton : MeleeEnemy // IGameObject
    {
      //The sprites and variables needed for the patrol are assigned in the enemy class


        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        //Depending on the distance and speed, the skeleton will patrol in a different way
        public Skeleton(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, float distance, Vector2 position, float speed, Texture2D boundingBoxTexture): base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
           

            this.attackAnimation = CreateAnimation(attackSprite, 18, 18, 1);
            this.damageAnimation = CreateAnimation(damageSprite, 8, 8,1);
            this.deathAnimation = CreateAnimation(deathSprite, 15, 15 ,1);
            this.idleAnimation = CreateAnimation(idleSprite, 11, 11 ,1);
            this.moveAnimation = CreateAnimation(moveSprite, 13, 13, 1);
            
            oldDistance = distance;

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

        }
       
       
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.characterState is MoveState)
            {
                if (this.direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);

                    spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                }
                else
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);

                    spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                }

            }
            else if (this.characterState is AttackState)
            {
                if (this.direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);

                }
                else
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);

                }
            }
            else if (this.characterState is DamagedState)
            {
                if (this.direction is LeftDirection)
                {
                    spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);
                    //this.characterState = new MoveState();
                }
                else
                {
                    spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);
                    //this.characterState = new MoveState();



                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            this.attackBox = new Rectangle();

            Patrol();
            if (this.characterState is MoveState)
            {
                moveAnimation.Update(gameTime);

            }
            else if (this.characterState is DamagedState)
            {
                damageAnimation.Update(gameTime);

            }
            else if (this.characterState is AttackState)
            {
                attackAnimation.Update(gameTime);
                if (this.direction is LeftDirection)
                {
                    this.attackBox = new Rectangle((int)BoundingBox.X, (int)BoundingBox.Y, 15, 40);


                }
                else
                {
                    this.attackBox = new Rectangle((int)BoundingBox.Right - 10, (int)BoundingBox.Y, 15, 40);
                }
            }
            //moveAnimation.Update(gameTime);
            MoveBoundingBox(Position);

        }
        //public void Update(GameTime gameTime)
        //{
           
        //}

        private void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }
    }
}
