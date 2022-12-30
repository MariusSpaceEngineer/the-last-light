using Default_Block;
using Game_development_project.Classes;
using Game_development_project.Classes.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D boundingBoxTexture;
        public static Texture2D triggerBlokTexture;

        public Camera camera;
   
        public GameState _currentState;
        public GameState _nextState;
        public GameState _previousState;

        public void ChangeState(GameState state)
        {
            _nextState = state;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            camera = new Camera(GraphicsDevice.Viewport);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            boundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            boundingBoxTexture.SetData(new[] { Color.White });
            triggerBlokTexture = new Texture2D(GraphicsDevice, 1, 1);
            triggerBlokTexture.SetData(new[] { Color.Red });

            Block.Content = Content;
           
            _currentState = new MainMenuState(this, _graphics.GraphicsDevice, Content);
            _currentState.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentState.Update(gameTime);
            _currentState.PostUpdate();

            if (_nextState != null)
            {
                _previousState = _currentState;
                _currentState = _nextState;

                _nextState = null;

              
            }
            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }     
    }
}
