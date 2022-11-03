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


        public Skeleton(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite)
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(moveSprite, new Vector2(0, 0), moveAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            moveAnimation.Update(gameTime);
        }
    }
}
