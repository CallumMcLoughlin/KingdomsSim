using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KingdomsSim.Classes.Game.Entities
{
    public class Population : Entity
    {
        public Population(int id, Color color, int strength, int defense, int health, int size, Vector2 position)
        {
            ID = id;
            EntityColor = color;
            Strength = strength;
            Defense = defense;
            BaseHealth = health;
            Health = BaseHealth;
            Size = size;
            X = (int)position.X;
            Y = (int)position.Y;

            CanBreed = true;
            CanMove = true;
        }

        public override void Tick()
        {
            Health--;

            if (Health <= 0)
            {
                Die();
                return;
            }

            if (!CanMove && TickCount <= 20)
            {
                TickCount++;
                return;
            }

            if (TickCount > 20)
            {
                CanMove = true;
                TickCount = 0;
            }

            if (Health > 75)
            {
                bool willBreed = random.Next(0, 3) == 0 ? true : false;
                if (willBreed)
                {
                    Breed();
                    CanMove = false;
                }
            }

            Moves move = GetMove();
            CanMove = EntityManager.HandleMove(this, move);
        }

        public override void Breed()
        {
            EntityManager.CreateEntity(this);
        }
    }
}
