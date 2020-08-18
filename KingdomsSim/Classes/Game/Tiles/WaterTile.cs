using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsSim.Classes.Game.Tiles
{
    public class WaterTile : Tile
    {
        public WaterTile()
        {
            Name = "Water";
            TileColor = new Color(7, 105, 224);
            ID = 0;
        }
    }
}
