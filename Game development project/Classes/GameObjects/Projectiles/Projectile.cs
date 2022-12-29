﻿using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Characters.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameObjects.Projectiles
{
    internal abstract class Projectile : GameObject
    {
        public float _timer;
        public float LifeSpan = 0f;

        //protected Rectangle boundingBox;
        //protected Texture2D boundingBoxTexture;
        //public Rectangle BoundingBox
        //{
        //    get { return boundingBox; }
        //    set { boundingBox = value; }
        //}

        public Projectile(Texture2D texture, Texture2D boundingBoxTexture)
          : base(texture)
        {
            this.boundingBoxTexture = boundingBoxTexture;
            this.HorizontalVelocity = 2;
       

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan || boundingBox.Intersects(Hero.GetHero().BoundingBox))
            {
                IsRemoved = true;
            }
            if (boundingBox.Intersects(Hero.GetHero().BoundingBox))
            {
                CheckTargetHealth(Hero.GetHero());
            }

            Position.X += Direction.X * HorizontalVelocity;
     
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


    }
}
