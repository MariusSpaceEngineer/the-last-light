using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Miscellaneous;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game_development_project.Classes.GameObjects.Projectiles
{
    internal abstract class Projectile : GameObject
    {
        public float _timer;
        public float LifeSpan = 0f;

        public float movementDirection;

        private bool heroTouchedByArrow = false;

        public Projectile(Texture2D texture, Texture2D boundingBoxTexture)
          : base(texture)
        {
            this.boundingBoxTexture = boundingBoxTexture;
            this.HorizontalVelocity = 2; 
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckCollision(Hero.GetHero().BoundingBox);

            if (_timer >= LifeSpan || heroTouchedByArrow)
            {
                IsRemoved = true;

            }

            if (heroTouchedByArrow)
            {
                CheckTargetHealth(Hero.GetHero());
                heroTouchedByArrow = false;
            }
            Position.X += movementDirection * HorizontalVelocity;
        }

        public void CheckTargetHealth(Character target)
        {
            if (target.Health > 0)
            {
                Debug.WriteLine("player hit");
                target.Health -= 25;

            }
            else
            {
                Debug.WriteLine("player dead");
                target.HasDied = true;
                target.CharacterState = new DeathState();
            }

        }

        public override void CheckCollision(Rectangle newRectangle)
        {
            if (BoundingBox.TouchLeftOf(newRectangle) || BoundingBox.TouchRightOf(newRectangle) || BoundingBox.TouchTopOf(newRectangle) || BoundingBox.TouchBottomOf(newRectangle))
            {
                heroTouchedByArrow = true;
            }
        }


    }
}
