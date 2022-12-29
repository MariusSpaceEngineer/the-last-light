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
    internal class Bandit : MeleeEnemy //IGameObject
    {
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        public Bandit(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
            this.attackAnimation = CreateAnimation(attackSprite, 8, 8, 1);
            this.damageAnimation = CreateAnimation(damageSprite, 3, 3, 1);
            this.deathAnimation = CreateAnimation(deathSprite, 5, 5, 1);
            this.idleAnimation = CreateAnimation(idleSprite, 4, 4, 1);
            this.moveAnimation = CreateAnimation(moveSprite, 8, 8, 1);

            PatrolDistance = distance;

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 30, 40);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (CharacterState is MoveState)
            {
                if (Direction is LeftDirection)
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);

                }
                else
                {
                    spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);

                }

            }
            else if (CharacterState is AttackState)
            {
                if (Direction is LeftDirection)
                {
                    spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);

                }
                else
                {
                   spriteBatch.Draw(attackSprite, Position, attackAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                   spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                   spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);


                }
            }
            else if (CharacterState is DamagedState)
            {
                if (Direction is LeftDirection)
                {
                    spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                  
                }
                else
                {
                    spriteBatch.Draw(damageSprite, Position, damageAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                 



                }
            }
            else if (CharacterState is DeathState)
            {
                if (Direction is LeftDirection)
                {
                    spriteBatch.Draw(deathSprite, Position, deathAnimation.CurrentFrame.SourceRectangle, Color.White);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                
                }
                else
                {
                    spriteBatch.Draw(deathSprite, Position, deathAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                    //spriteBatch.Draw(this.blokTexture, BoundingBox, Color.Blue);
                    spriteBatch.Draw(boundingBoxTexture, AttackBox, Color.Green);
                  



                }
            }

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            AttackBox = new Rectangle();
            Patrol();

            if (CharacterState is MoveState)
            {
                moveAnimation.Update(gameTime);

            }
            else if (CharacterState is DamagedState)
            {
                damageAnimation.Update(gameTime);

            }
            else if (CharacterState is AttackState)
            {
                    attackAnimation.Update(gameTime);
                    MoveAttackBox();
                    
            }
            MoveBoundingBox(Position);
        }

        public override void MoveAttackBox()
        {
            if (Direction is LeftDirection)
            {
                attackBox = new Rectangle((int)BoundingBox.Left - 5, (int)BoundingBox.Y, 15, 40);
            }
            else
            {
                attackBox = new Rectangle((int)BoundingBox.Right - 10, (int)BoundingBox.Y, 15, 40);      
            }
        }

        public override void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 10;
            boundingBox.Y = (int)position.Y + 5;
        }
    }
}
