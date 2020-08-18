using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KingdomsSim
{
    public class Main : Game
    {
        private static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const string GameName = "Kingdoms Sim";
        public const int WindowWidth = 1024;
        public const int WindowHeight = 720;
        public const bool IsBorderless = false;
        public const bool IsFullScreen = false;
        public const bool FixedTimeStep = true;
        public const bool SynchronizeVerticalRetrace = true;
        public static bool MouseVisible = true;

        public static bool IsRunning { get; private set; } = false; //If sim is running
        public static bool IsExiting { get; private set; } = false; //If game is exiting

        public static Camera MainCamera { get; private set; }

        private Input input = new Input();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = WindowWidth,
                PreferredBackBufferHeight = WindowHeight,
                IsFullScreen = IsFullScreen,
                SynchronizeWithVerticalRetrace = SynchronizeVerticalRetrace
            };

            IsFixedTimeStep = FixedTimeStep;
            IsMouseVisible = MouseVisible;

            Content.RootDirectory = "Content";

            Window.Title = GameName;
            Window.IsBorderless = IsBorderless;
        }

        protected override void Initialize()
        {
            base.Initialize();
            MainCamera = new Camera(graphics.GraphicsDevice.Viewport);
            WindowController.SetContentManager(Content);
            GameManager.Initialize();
            WindowController.ChangeWindow(new MainWindow());
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsExiting)
            {
                WindowController.UnloadContent();
                Exit();
                return;
            }

            base.Update(gameTime);
            input.Update(gameTime);
            MainCamera.UpdateCamera(graphics.GraphicsDevice.Viewport, input);
            WindowController.Update(gameTime, input);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, transformMatrix: MainCamera.Transform);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            WindowController.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public static GraphicsDevice GetGraphicsDevice()
        {
            return graphics.GraphicsDevice;
        }

        public static void Quit()
        {
            IsExiting = true;
        }
    }
}
