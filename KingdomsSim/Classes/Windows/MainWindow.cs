using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KingdomsSim
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            WindowName = "Main";
        }

        public override void LoadContent(ContentHandler content)
        {
            content.Load("Maps/Europe", "Europe");
            Textures = content.GetTextures();
            Settings settings = new Settings("Europe", 30, 1, 0.1f);
            GameManager.Setup(settings, Textures);
        }

        public override void UnloadContent(ContentHandler content)
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, Input input)
        {
            if (input.IsNewKeyPress(Keys.Space))
                GameManager.ToggleSim();

            GameManager.Update(gameTime, input);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameManager.Draw(gameTime, spriteBatch);
        }
    }
}
