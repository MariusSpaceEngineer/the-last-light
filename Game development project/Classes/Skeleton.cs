using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes
{
    internal class Skeleton : IGameObject
    {
        private Texture2D attackSprite;
        private Texture2D damageSprite;
        private Texture2D deathSprite;
        private Texture2D idleSprite;
        private Texture2D moveSprite;


        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        private Direction direction;
        private float distance;
        private float oldDistance;

        private Vector2 position;
        private Vector2 origin;
        private Vector2 speed;

        private float rotation = 0f;


        public Skeleton(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, float newDistance)
        {
            this.attackSprite = attackSprite;
            this.damageSprite = damageSprite;
            this.deathSprite = deathSprite;
            this.idleSprite = idleSprite;
            this.moveSprite = moveSprite;


            attackAnimation = new Animation(18);
            attackAnimation.GetFramesFromTextureProperties(attackSprite.Width, attackSprite.Height, 18, 1);

            damageAnimation = new Animation(8);
            damageAnimation.GetFramesFromTextureProperties(damageSprite.Width, damageSprite.Height, 8, 1);

            deathAnimation = new Animation(15);
            deathAnimation.GetFramesFromTextureProperties(deathSprite.Width, deathSprite.Height, 15, 1);

            idleAnimation = new Animation(11);
            idleAnimation.GetFramesFromTextureProperties(idleSprite.Width, idleSprite.Height, 11, 1);

            moveAnimation = new Animation(13);
            moveAnimation.GetFramesFromTextureProperties(moveSprite.Width, moveSprite.Height, 13, 1);

            //animation = new Animation.Animation();
            //animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 10, 1);

            this.oldDistance = newDistance;
        
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(moveSprite, new Vector2(0, 0), moveAnimation.CurrentFrame.SourceRectangle, Color.White);
            if (speed.X > 0)
            {
                spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);

            }
            else
            {
                //spriteBatch.Draw(moveSprite, new Vector2(0, 0), moveAnimation.CurrentFrame.SourceRectangle, Color.White);
                spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);


            }
        }

        public void Update(GameTime gameTime)
        {
            position += speed;
            origin = new Vector2(attackSprite.Width / 2, attackSprite.Height / 2);

            if (distance <= 0)
            {
                direction = Direction.Right;
                speed.X = 1f;
            }
            else if (distance >= oldDistance)
            {
                direction = Direction.Left;
                speed.X = -1f;
            }
            if (direction == Direction.Right)
            {
                distance += 1;
            }
            else
            {
                distance -= 1;
            }

           float heroPosition = Hero.Position.X;

            heroPosition = heroPosition - position.X;

            if (heroPosition >= -10 && heroPosition <= 10)
            {
                if (heroPosition < -1)
                {
                    speed.X = -1f;
                }
                else if (heroPosition > 1)
                {
                    speed.X = 1f;
                }
                else if (heroPosition == 0)
                {
                    speed.X = 0f;
                }
            }


            moveAnimation.Update(gameTime);
        }
    }
}
