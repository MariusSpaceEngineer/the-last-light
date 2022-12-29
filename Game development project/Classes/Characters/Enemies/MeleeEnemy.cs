using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Enemies
{
    internal class MeleeEnemy : Enemy
    {
        protected bool hasDied = false;
        protected int lifes = 100;
        protected bool isHit = false;

        public MeleeEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
        }
        public override void Patrol()
        {
            base.Patrol();

            Hero hero = Hero.GetHero();
            float heroPosition = hero.BoundingBox.X;
            //float heroRightBorder = hero.BoundingBox.Right;
            //float heroLeftBorder = hero.BoundingBox.Left;





            heroPosition = heroPosition - Position.X;
            //heroRightBorder = heroRightBorder - Position.X;
            //heroLeftBorder = heroLeftBorder - Position.X;

            if (heroPosition >= -150 && heroPosition <= 150)
            {
                if (heroPosition < -1)
                {
                    this.direction = new LeftDirection();
                    this.characterState = new MoveState();
                    //if (heroRightBorder == 0f)
                    //{
                    //    Debug.WriteLine("Touching player right");
                    //    LinearVelocity = 0;
                    //}
                    if (this.boundingBox.TouchRightOf(hero.BoundingBox))
                    {
                        Debug.WriteLine("Touching player right");
                        HorizontalVelocity = 0;
                        this.characterState = new AttackState();
                    }
                    else
                    {
                        HorizontalVelocity = -1f;

                    }
                }
                else if (heroPosition > 1)
                {
                    this.direction = new RightDirection();
                    this.characterState = new MoveState();
                    //if (heroLeftBorder == 0f)
                    //{
                    //    Debug.WriteLine("Touching player left");
                    //    LinearVelocity = 0;
                    //}
                    if (this.boundingBox.TouchLeftOf(hero.BoundingBox))
                    {
                        Debug.WriteLine("Touching player left");
                        HorizontalVelocity = 0;
                        this.characterState = new AttackState();
                    }
                    else
                    {
                        HorizontalVelocity = 1f;

                    }
                }
                if (hero.AttackBox.Intersects(this.boundingBox))
                {
                    if (lifes > 0)
                    {
                        this.characterState = new DamagedState();
                        this.lifes--;
                    }
                    else
                    {
                        this.characterState = new DeathState();
                        this.IsRemoved = true;
                    }

                   

                }
                //if (heroPosition == 0f)
                //{
                //    LinearVelocity = 0f;
                //}
            }
        }
    }
}
