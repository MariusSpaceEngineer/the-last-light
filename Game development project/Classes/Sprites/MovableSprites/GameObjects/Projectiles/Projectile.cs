using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Miscellaneous;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game_development_project.Classes.GameObjects.Projectiles
{
    internal abstract class Projectile : GameObject
    {
        #region Private variables

        private float _timer;
        private bool heroTouchedByArrow = false;

        #endregion

        #region Get/Setters

        public float MovementDirection { get; set; }
        public float LifeSpan { get; set; } = 0f;

        #endregion


        public Projectile(Texture2D texture, Texture2D boundingBoxTexture)
          : base(texture)
        {
            this.BoundingBoxTexture = boundingBoxTexture;
            this.HorizontalVelocity = 2; 
        }

        #region Override methods

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Checks to see if the player is touched by the projectile
            CheckCollision(Hero.GetHero().BoundingBox);

            //If the player is touched by the arrow or the timer is up then the projectile will be removed
            if (_timer >= LifeSpan || heroTouchedByArrow)
            {
                IsRemoved = true;

            }

            
            if (heroTouchedByArrow)
            {
                //Will change the health of the player if he's hit
                CheckTargetHealth(Hero.GetHero());
                heroTouchedByArrow = false;
            }
            Position.X += MovementDirection * HorizontalVelocity;
        }

     

        public override void CheckCollision(Rectangle newRectangle)
        {
            if (BoundingBox.TouchLeftOf(newRectangle) || BoundingBox.TouchRightOf(newRectangle) || BoundingBox.TouchTopOf(newRectangle) || BoundingBox.TouchBottomOf(newRectangle))
            {
                heroTouchedByArrow = true;
            }
        }

        #endregion

        #region Private methods

        private void CheckTargetHealth(Character target)
        {
            if (target.Health > 0)
            {
                //Debug.WriteLine("player hit");
                target.Health -= 25;

            }
            else
            {
                //Debug.WriteLine("player dead");
                target.HasDied = true;
                target.CharacterState = new DeathState();
            }

        }

        #endregion


    }
}
