using Default_Block;
using Default_Level;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.MeleeEnemies;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.ProjectileEnemies;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
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
            backgroundLevel = content.Load<Texture2D>("Textures/Backgrounds/VillageBackgroundLevel1");
            
        }
        public override void InitializeContent()
        {
            level = new Level1(new Level1BlockFactory());
            level.Generate(level.Map, 64);
            

            spriteList = new List<Sprite>() {
                 Hero.GetHero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroMoveSprite, heroJumpSprite, game.boundingBoxTexture, level),

                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(1030, 450), 2, 50,game.boundingBoxTexture)
                 {
                     //Position = new Vector2(1030, 303),
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(1552, 475), 2, game.boundingBoxTexture),
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(1852, 465), 2, 50, game.boundingBoxTexture),
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(2890, 285), 2, game.boundingBoxTexture),
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(4180, 450), 2, 50,game.boundingBoxTexture)
                 {
                     //Position = new Vector2(300, 350),
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(4594, 335), 2, 50, game.boundingBoxTexture),
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(5518, 450), 4, 50,game.boundingBoxTexture)
                 {
                     //Position = new Vector2(300, 350),
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                   new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(5068, 450), 4, 50,game.boundingBoxTexture)
                 {
                     //Position = new Vector2(300, 350),
                     projectile = new Arrow(arrowTexture, game.boundingBoxTexture)
                 },
                 new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite,skeletonIdleSprite, skeletonMoveSprite, 100, new Vector2(6546,475), 3, game.boundingBoxTexture),
                 new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(6954, 475), 3, 100, game.boundingBoxTexture),
            };
        }


    }

}
