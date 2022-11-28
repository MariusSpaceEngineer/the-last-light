using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Animations;

namespace Game_development_project.Classes.Characters
{
    internal abstract class Character
    {
        protected Texture2D attackSprite;
        protected Texture2D damageSprite;
        protected Texture2D deathSprite;
        protected Texture2D idleSprite;
        protected Texture2D moveSprite;

        public Character(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite)
        {
            this.attackSprite = attackSprite;
            this.damageSprite = damageSprite;
            this.deathSprite = deathSprite;
            this.idleSprite = idleSprite;
            this.moveSprite = moveSprite;
        }

        public Animation CreateAnimation(Texture2D sprite, int fps, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            Animation animation = new Animation(fps);
            animation.GetFramesFromTextureProperties(sprite.Width, sprite.Height, numberOfWidthSprites, numberOfHeightSprites);
            return animation;
        }
    }
}
