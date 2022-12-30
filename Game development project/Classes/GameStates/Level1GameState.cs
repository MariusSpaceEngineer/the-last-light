using Default_Block;
using Default_Level;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class Level1GameState : In_GameState
    {
        
        public Level1GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            LoadContent(content);
            InitializeContent();
        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            backgroundLevel = content.Load<Texture2D>("Background/village_level1");
            
        }
        public override void InitializeContent()
        {
            level = new Level1(new Level1_BlockFactory());
            level.Generate(level.Map, 64);

            spriteList = new List<Sprite>() {
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(500, 400),
                     projectile = new Arrow(arrowTexture, game.blokTexture)
             },
                  new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(300, 350),
                     projectile = new Arrow(arrowTexture, game.blokTexture)
             },
                Hero.GetHero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroMoveSprite, heroJumpSprite, game.blokTexture, level),
                new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(250, 475), 2, game.blokTexture),
                new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(150, 475), 2, 50, game.blokTexture),
            };
        }


    }

}
