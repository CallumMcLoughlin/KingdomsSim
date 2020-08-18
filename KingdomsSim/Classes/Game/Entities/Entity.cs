using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsSim.Classes.Game.Entities
{
    public enum Moves { Up, Down, Left, Right }

    public abstract class Entity
    {
        public static Random random = new Random();

        public static Texture2D Texture { get; set; }
        public Color EntityColor { get; protected set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ID { get; protected set; }
        public int Size { get; protected set; }
        public int Strength { get; protected set; }
        public int Defense { get; protected set; }
        public int BaseHealth { get; protected set; }
        public int Health { get; protected set; }
        public bool CanBreed { get; protected set; }
        public bool CanMove { get; protected set; }

        public int TickCount { get; protected set; }

        public abstract void Tick();
        public abstract void Breed();

        public static Moves GetMove()
        {
            Array AllMoves = Enum.GetValues(typeof(Moves));
            return (Moves)AllMoves.GetValue(random.Next(AllMoves.Length));
        }

        public void Die()
        {
            EntityManager.HandleDeath(this);
        }
    }
}
