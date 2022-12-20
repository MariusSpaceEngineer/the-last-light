using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Characters.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameObjects.Projectiles
{
    internal abstract class Projectile : GameObject
    {
        public float _timer;
        public float LifeSpan = 0f;

        protected Rectangle boundingBox;
        protected Texture2D blokTexture;
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public Projectile(Texture2D texture, Texture2D boundingBoxTexture)
          : base(texture)
        {
            this.blokTexture = boundingBoxTexture;
       

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
          

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position.X += Direction.X * LinearVelocity;
            
           
        }

       
    }
}
