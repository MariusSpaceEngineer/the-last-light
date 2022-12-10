using Game_development_project.Classes.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters
{
    internal class Bandit : Enemy, IGameObject
    {
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        public Bandit(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance)
        {
            this.attackAnimation = CreateAnimation(attackSprite, 8, 8, 1);
            //this.damageAnimation = CreateAnimation(damageSprite, 8, 8, 1);
            this.deathAnimation = CreateAnimation(deathSprite, 5, 5, 1);
            this.idleAnimation = CreateAnimation(idleSprite, 4, 4, 1);
            this.moveAnimation = CreateAnimation(moveSprite, 8, 8, 1);

            oldDistance = distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (LinearVelocity > 0)
            {
                spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);

            }
            else
            {
                spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);



            }
        }

        public void Update(GameTime gameTime)
        {
            Patrol();
            moveAnimation.Update(gameTime);
        }
    }
}
