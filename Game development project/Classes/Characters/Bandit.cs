using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Characters.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters
{
    internal class Bandit : MeleeEnemy, IGameObject
    {
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        public Bandit(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
            this.attackAnimation = CreateAnimation(attackSprite, 8, 8, 1);
            //this.damageAnimation = CreateAnimation(damageSprite, 8, 8, 1);
            this.deathAnimation = CreateAnimation(deathSprite, 5, 5, 1);
            this.idleAnimation = CreateAnimation(idleSprite, 4, 4, 1);
            this.moveAnimation = CreateAnimation(moveSprite, 8, 8, 1);

            oldDistance = distance;

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 30, 40);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.characterState is MoveState)
            {
                if (this.direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                }
                else
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                }

            }
            if (this.characterState is AttackState)
            {
                if (this.direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);
                }
                else
                {
                   spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                   spriteBatch.Draw(this.blokTexture, AttackBox, Color.Green);


                }
            }
        
            //if (LinearVelocity > 0)
            //{
            //    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
            //    spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);

            //}
            //else
            //{
            //    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
            //    spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);



            //}
        }

        public void Update(GameTime gameTime)
        {
            Patrol();
            moveAnimation.Update(gameTime);
            if (this.characterState is AttackState)
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
           
            MoveBoundingBox(Position);

        }

        private void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 10;
            boundingBox.Y = (int)position.Y + 5;
        }
    }
}
