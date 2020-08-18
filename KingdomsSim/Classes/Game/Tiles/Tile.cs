using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsSim.Classes.Game.Tiles
{
    public abstract class Tile 
    {
        public string Name { get; protected set; }
        public Color TileColor { get; protected set; }
        public int ID { get; protected set; }
    }
}
