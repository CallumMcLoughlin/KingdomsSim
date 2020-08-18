using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace KingdomsSim
{
    public abstract class Window
    {
        public string WindowName { get; protected set; }
        protected Dictionary<string, Texture2D> Textures { get; set; }

        public abstract void LoadContent(ContentHandler content);
        public abstract void UnloadContent(ContentHandler content);
        public abstract void Update(GameTime gameTime, Input input);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
