using Game_development_project.Classes.Characters;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class Level2GameState : In_GameState
    {

       

        public Level2GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {  
            LoadContent(content);
            InitializeContent();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            backgroundLevel = content.Load<Texture2D>("Background/castle_level2");
        }

        public override void InitializeContent()
        {
            level = new Level2(new Level2_BlockFactory());
            level.Generate(level.Map, 64);

            spriteList = new List<Sprite>() {
                 Hero.GetHero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroMoveSprite, heroJumpSprite, game.boundingBoxTexture, level),
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(444, 475), 2, game.boundingBoxTexture),
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(1750, 475), 2, game.boundingBoxTexture),
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(1840, 475), 2, 50, game.boundingBoxTexture),
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(2770, 475), 2, game.boundingBoxTexture),
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(2946, 245), 0.5f, 10,game.boundingBoxTexture)
                 {
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                  new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(4190, 450), 2, 50,game.boundingBoxTexture)
                 {
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(4653, 410), 2, 50, game.boundingBoxTexture),
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(6388, 475), 4, game.boundingBoxTexture),
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(6766, 475), 4, 50, game.boundingBoxTexture),
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(7408, 450), 2, 50,game.boundingBoxTexture)
                 {
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
            };
        }


    }
}
