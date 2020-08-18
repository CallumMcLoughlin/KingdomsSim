using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomsSim
{
    public static class WindowController
    {
        private static Window CurrentWindow = null;
        private static ContentHandler Content = null;

        public static void SetContentManager(ContentManager contentManager)
        {
            if (Content == null) //Prevent reassigning ContentHandler after initially set
                Content = new ContentHandler(contentManager);
        }

        private static void SetCurrentWindow(Window value)
        {
            if (CurrentWindow != null)
                UnloadContent();
            CurrentWindow = value;
            LoadContent();
        }

        public static void ChangeWindow(Window newWindow)
        {
            SetCurrentWindow(newWindow);
        }

        public static void LoadContent()
        {
            CurrentWindow.LoadContent(Content);
        }

        public static void UnloadContent()
        {
            CurrentWindow.UnloadContent(Content);
        }

        public static void Update(GameTime gameTime, Input input)
        {
            CurrentWindow.Update(gameTime, input);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentWindow.Draw(gameTime, spriteBatch);
        }
    }
}
