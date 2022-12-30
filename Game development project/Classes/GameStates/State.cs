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
    public abstract class State
    {
        protected Game1 game;

        protected GraphicsDevice graphicsDevice;

        protected ContentManager content;

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.content = content;
        }
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


    }
}

