using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level2;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.MeleeEnemies;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.ProjectileEnemies;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game_development_project.Classes.GameStates
{
    internal class Level2GameState : In_GameState
    {
        public Level2GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Level = new Level2(new Level2BlockFactory());
            LoadContent(content);
            InitializeContent();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            BackgroundLevel = content.Load<Texture2D>("Textures/Backgrounds/CastleBackgroundLevel2");
        }

        public override void InitializeContent()
        {
            Level = GenerateLevel(Level, 64);
            SpriteList = GenerateLevelSpriteList();
           
        }

        public override List<Sprite> GenerateLevelSpriteList()
        {
            List<Sprite> spriteList = new List<Sprite>() {
                 Hero.GetHero(HeroAttackSprite, HeroDamageSprite, HeroDeathSprite, HeroIdleSprite, HeroMoveSprite, HeroJumpSprite, Game.BoundingBoxTexture, Level),
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(444, 475), 2, Game.BoundingBoxTexture),
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(1750, 475), 2, Game.BoundingBoxTexture),
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(1840, 475), 2, 50, Game.BoundingBoxTexture),
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(2770, 475), 2, Game.BoundingBoxTexture),
                 new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(2946, 245), 0.5f, 10,Game.BoundingBoxTexture)
                 {
                     Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                  new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(4190, 450), 2, 50,Game.BoundingBoxTexture)
                 {
                     Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(4653, 410), 2, 50, Game.BoundingBoxTexture),
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(6388, 475), 4, Game.BoundingBoxTexture),
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(6766, 475), 4, 50, Game.BoundingBoxTexture),
                 new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(7408, 450), 2, 50,Game.BoundingBoxTexture)
                 {
                     Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
            };

            return spriteList;
        }

    }
}
