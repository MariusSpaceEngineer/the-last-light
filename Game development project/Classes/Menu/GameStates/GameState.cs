using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    public abstract class GameState
    {
        #region Private variables

        private Game1 game;
        private GraphicsDevice graphicsDevice;
        private ContentManager content;

        #endregion

        #region Get/Setters

        public Game1 Game
        {
            get { return game; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        #endregion


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.Content = content;
        }

        #region Virtual methods

        public virtual void LoadContent(ContentManager content)
        {

        }
        public virtual void InitializeContent()
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }


        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void PostUpdate()
        {

        }

        #endregion


    }
}

