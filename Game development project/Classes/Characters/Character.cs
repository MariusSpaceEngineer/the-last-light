using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Animations;
using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Characters
{
    internal abstract class Character : Sprite
    {
        protected Texture2D attackSprite;
        protected Texture2D damageSprite;
        protected Texture2D deathSprite;
        protected Texture2D idleSprite;
        protected Texture2D moveSprite;
        protected Texture2D boundingBoxTexture;

        protected Rectangle boundingBox;
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        protected static Vector2 position;
        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }

        //protected Rectangle boundingBox;
        //protected Texture2D blokTexture;
        //public Rectangle BoundingBox
        //{
        //    get { return boundingBox; }
        //    set { boundingBox = value; }
        //}

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
