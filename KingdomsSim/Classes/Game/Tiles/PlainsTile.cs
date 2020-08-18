using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsSim.Classes.Game.Tiles
{
    public class PlainsTile : Tile
    {
        public PlainsTile()
        {
            Name = "Plains";
            TileColor = new Color(21, 189, 30);
            ID = 1;
        }
    }
}
