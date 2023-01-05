using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Miscellaneous;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.MeleeEnemies
{
    internal class MeleeEnemy : Enemy
    {

        public MeleeEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
            ChasingSpeed = speed;
        }

        public override void Patrol()
        {
            base.Patrol();

            Hero hero = Hero.GetHero();
            float heroPosition = hero.BoundingBox.X;
            heroPosition = heroPosition - Position.X;

            if (heroPosition >= -150 && heroPosition <= 150)
            {
                if (heroPosition < -1)
                {
                    Direction = new LeftDirection();
                    CharacterState = new MoveState();

                    if (BoundingBox.TouchRightOf(hero.BoundingBox))
                    {
                        Attack(hero);
                    }
                    else
                    {
                        HorizontalVelocity = Direction.movementDirection.X * ChasingSpeed;

                    }
                }
                else if (heroPosition > 1)
                {
                    Direction = new RightDirection();
                    CharacterState = new MoveState();

                    if (BoundingBox.TouchLeftOf(hero.BoundingBox))
                    {
                        Attack(hero);
                    }
                    else
                    {
                        HorizontalVelocity = Direction.movementDirection.X * ChasingSpeed;

                    }
                }
                CheckEnemyHealth(hero);

            }
        }

        public override void Attack(Sprite target)
        {

            Debug.WriteLine("Enemy attacking hero");
            HorizontalVelocity = 0;
            CharacterState = new AttackState();

        }


    }
}
