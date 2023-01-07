using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game_development_project.Classes.Sprites
{
    public abstract class Sprite : ICloneable
    {

        #region Public variables

        //Those have to be public variables in order to work
        public Vector2 Position;

        public Vector2 Origin;

        #endregion

        #region Protected variables

        protected Rectangle boundingBox;
        protected Rectangle attackBox;

        #endregion

        #region Get/Setters

        public bool IsRemoved { get; set; } = false;

        public Texture2D BoundingBoxTexture { get; set; }
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }
        public Rectangle AttackBox
        {
            get { return attackBox; }
            set { attackBox = value; }
        }

        #endregion

        public Sprite()
        {

        }

        #region Virtual methods


        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

     
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }


        public virtual void CheckCollision(Rectangle newRectangle)
        {

        }

        #endregion

        #region Public methods

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion



    }
}

