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

        public Projectile(Texture2D texture)
          : base(texture)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
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
