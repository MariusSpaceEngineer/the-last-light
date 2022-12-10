using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes
{
    internal class Sprite : ICloneable
    {
        //Characters have more textures so it should be removed or made in a list
        //protected Texture2D _texture;

        //No rotation in-game
        //protected float _rotation;

        //Keyboardreader has this already
        //protected KeyboardState _currentKey;

        //protected KeyboardState _previousKey;

        //These can remain
        public Vector2 Position;

        public Vector2 Origin;

        public Vector2 Direction;

        //Not needed
        //public float RotationVelocity = 3f;

        //Can be used
        public float LinearVelocity = 4f;

        //What does this do?
        public Sprite Parent;
  
        public bool IsRemoved = false;

        public Sprite()
        {
            //Should be assigned in another sub-class
            //_texture = texture;

            // The default origin in the centre of the sprite
            //Maybe assigned in every concrete class
            //Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
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
            return this.MemberwiseClone();
        }
    }
}

