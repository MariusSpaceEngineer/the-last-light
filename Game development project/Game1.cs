using Default_Block;
using Game_development_project.Classes.Animations;
using Game_development_project.Classes.GameStates;
using Game_development_project.Classes.Miscellaneous;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project
{
    public class Game1 : Game
    {
        #region Private variables

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #endregion

        #region Get/Setters

        public Texture2D BoundingBoxTexture { get; set; }
        public static Texture2D TriggerBlokTexture { get; set; }

        public Camera GameCamera { get; set; }
   
        public GameState CurrentState { get; set; }
        public GameState NextState { get; set; }
        public GameState PreviousState { get; set; }

        #endregion



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region Methods

        protected override void Initialize()
        {
            //The window size
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            GameCamera = new Camera(GraphicsDevice.Viewport);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
      
            //Used to show the bounding box around the objects
            BoundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            BoundingBoxTexture.SetData(new[] { Color.White });

            //Used by triggerblock
            TriggerBlokTexture = new Texture2D(GraphicsDevice, 1, 1);
            TriggerBlokTexture.SetData(new[] { Color.YellowGreen });

            Block.Content = Content;

            //Starts in the MainMenuState
            CurrentState = new MainMenuState(this, _graphics.GraphicsDevice, Content);
            CurrentState.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Updates the current gamestate
            CurrentState.Update(gameTime);
            CurrentState.PostUpdate();

            //Changes the state
            if (NextState != null)
            {
                PreviousState = CurrentState;
                CurrentState = NextState;

                NextState = null;     
            }
            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            CurrentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        public void ChangeState(GameState state)
        {
            NextState = state;
        }

        #endregion
    }
}
