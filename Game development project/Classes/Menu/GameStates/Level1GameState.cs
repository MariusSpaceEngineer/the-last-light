using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level1;
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
    internal class Level1GameState : In_GameState
    {

        public Level1GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Level = new Level1(new Level1BlockFactory());
            LoadContent(content);
            InitializeContent();
        }

        #region Override methods
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            BackgroundLevel = content.Load<Texture2D>("Textures/Backgrounds/VillageBackgroundLevel1");

        }

        public override void InitializeContent()
        {
            Level = GenerateLevel(Level, 64);
            SpriteList = GenerateLevelSpriteList();
        }

        public override List<Sprite> GenerateLevelSpriteList()
        {
            List<Sprite> spriteList = new List<Sprite>(){
                 Hero.GetHero(HeroAttackSprite, HeroDamageSprite, HeroDeathSprite, HeroIdleSprite, HeroMoveSprite, HeroJumpSprite, Game.BoundingBoxTexture, Level),

                 new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(1030, 450), 2, 50,Game.BoundingBoxTexture)
                 {
                    Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(1552, 475), 2, Game.BoundingBoxTexture),
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(1852, 465), 2, 50, Game.BoundingBoxTexture),
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 50, new Vector2(2890, 285), 2, Game.BoundingBoxTexture),
                 new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(4180, 450), 2, 50,Game.BoundingBoxTexture)
                 {
                    Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(4594, 335), 2, 50, Game.BoundingBoxTexture),
                 new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(5518, 450), 4, 50,Game.BoundingBoxTexture)
                 {
                     Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                   new Huntress(HuntressAttackSprite,HuntressDamageSprite, HuntressDeathSprite, HuntressIdleSprite, HuntressMoveSprite, new Vector2(5068, 450), 4, 50,Game.BoundingBoxTexture)
                 {
                     Projectile = new Arrow(ArrowTexture, Game.BoundingBoxTexture)
                 },
                 new Skeleton(SkeletonAttackSprite, SkeletonDamageSprite, SkeletonDeathSprite,SkeletonIdleSprite, SkeletonMoveSprite, 100, new Vector2(6546,475), 3, Game.BoundingBoxTexture),
                 new Bandit(BanditAttackSprite, BanditDamageSprite, BanditDeathSprite, BanditIdleSprite, BanditMoveSprite, new Vector2(6954, 475), 3, 100, Game.BoundingBoxTexture),
            };
            return spriteList;
        }

        #endregion

    }

}
