using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.ProjectileEnemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameObjects.Projectiles
{
    internal class Arrow : Projectile
    {
        public Arrow(Texture2D texture, Texture2D boundingBoxTexture) : base(texture, boundingBoxTexture)
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 35, 10);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (ProjectileEnemy.playerDirection is LeftDirection)
            {
                spriteBatch.Draw(texture, Position, null, Color.White, 0, Origin, 1, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(texture, Position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
            }
            
            spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Blue);
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            MoveBoundingBox(Position);
        }

        public override void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X - 20;
            boundingBox.Y = (int)position.Y - 8;
        }


    }
}
