using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace KingdomsSim
{
    public class ContentHandler
    {
        private Dictionary<string, Texture2D> Textures2D = new Dictionary<string, Texture2D>();
        private ContentManager Content;

        public ContentHandler(ContentManager contentManager)
        {
            Content = contentManager;
        }

        public void Load(string location, string name)
        {
            Texture2D Texture = Content.Load<Texture2D>(location);
            Textures2D[name] = Texture;
        }

        public void Unload()
        {
            Content.Unload();
            Textures2D.Clear();
        }

        public Dictionary<string, Texture2D> GetTextures()
        {
            return Textures2D;
        }
    }
}
