using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json.Nodes;

namespace Game_development_project.Classes.Sprites
{
    public class Sprite : ICloneable
    {
        //Maybe add these in a interface as they are used in every class

        //These can remain

        public Vector2 Position;

        public Vector2 Origin;



        public bool IsRemoved = false;

        protected Rectangle boundingBox;
        protected Texture2D boundingBoxTexture;
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        protected Rectangle attackBox;
        public Rectangle AttackBox
        {
            get { return attackBox; }
            set { attackBox = value; }
        }

        public Sprite()
        {

        }

        //It shouldn't be needed as we have and IGameObject interface with it
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        //It shouldn't be needed as we have and IGameObject interface with it
        //Draw is specific for every class
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual void CheckCollision(Rectangle newRectangle)
        {

        }


    }
}

