using KingdomsSim.Classes.Game.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomsSim.Classes.Game
{
    public static class EntityManager
    {
        private static int EntityIDCount = 0;
        public static Entity[,] OccupiedTiles { get; private set; }

        public static void Setup()
        {
            OccupiedTiles = new Entity[GameManager.MapTiles.GetLength(0), GameManager.MapTiles.GetLength(1)];
        }

        public static void Update(GameTime gameTime)
        {
            int xLength = OccupiedTiles.GetLength(0);

            /*Parallel.For(0, OccupiedTiles.GetLength(1), y =>
            {
                for (int x = 0; x < xLength; x++)
                {
                    if (OccupiedTiles[x, y] != null)
                        OccupiedTiles[x, y].Tick();
                }
            });*/

            foreach (Entity e in OccupiedTiles)
            {
                if (e != null)
                    e.Tick();
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*
            int xLength = OccupiedTiles.GetLength(1);
            Parallel.For(0, OccupiedTiles.GetLength(0), y =>
            {
                for (int x = 0; x < xLength; ++x)
                {
                    if (OccupiedTiles[y, x] != null)
                    {
                        Entity e = OccupiedTiles[y, x];
                        spriteBatch.Draw(Entity.Texture, new Vector2(e.X, e.Y), e.EntityColor);
                    }
                }
            });
            */

            foreach (Entity e in OccupiedTiles)
            {
                if (e != null)
                    spriteBatch.Draw(Entity.Texture, new Vector2(e.X, e.Y), e.EntityColor);
            }
        }

        public static bool HandleMove(Entity entity, Moves move)
        {
            int x = entity.X;
            int y = entity.Y;
            switch (move)
            {
                case (Moves.Up):
                    x += entity.Size;
                    break;
                case (Moves.Down):
                    x -= entity.Size;
                    break;
                case (Moves.Left):
                    y -= entity.Size;
                    break;
                case (Moves.Right):
                    y += entity.Size;
                    break;
            }

            Entity TileEntity = GetOccupiedTileEntity(x, y);

            if (TileEntity != null && TileEntity.ID == entity.ID)
                return false;

            bool ValidMove = IsFreeTile(x, y);

            if (ValidMove)
            {
                SetNewPostion(entity, x, y);
                return true;
            }

            if (!ValidMove && TileEntity != null)
            {
                if (entity.Strength > TileEntity.Strength + 0.5 * TileEntity.Defense)
                {
                    TileEntity.Die();
                    SetNewPostion(entity, x, y);
                    return true;
                }
                else
                {
                    entity.Die();
                    return false;
                }
            }
            return false;
        }

        public static void HandleDeath(Entity entity)
        {
            OccupiedTiles[entity.X, entity.Y] = null;
            entity = null;
        }

        public static void CreateEntity(Entity parent)
        {
            Entity newEntity = null;
            Moves newMove = Entity.GetMove();
            if (parent.GetType() == typeof(Population))
            {
                newEntity = new Population(parent.ID, parent.EntityColor, GetBiasedValue(parent.Strength), GetBiasedValue(parent.Defense), GetBiasedValue((int)parent.BaseHealth), parent.Size,  new Vector2(parent.X, parent.Y));
            }

            if (!HandleMove(newEntity, newMove))
            {
                newEntity = null;
                return;
            }
            OccupiedTiles[newEntity.X, newEntity.Y] = newEntity;
        }

        public static int AssignID() //Get new ID for population
        {
            return EntityIDCount++;
        }

        private static int GetBiasedValue(int baseValue) //Not done
        {
            return Entity.random.Next(baseValue - 10, baseValue + 20);
        }

        private static bool IsFreeTile(int x, int y)
        {
            if (GameManager.MapTiles[x, y] > 0 && OccupiedTiles[x, y] == null)
                return true;
            return false;
        }

        private static void SetNewPostion(Entity en, int x, int y)
        {
            OccupiedTiles[en.X, en.Y] = null;
            en.X = x;
            en.Y = y;
            OccupiedTiles[x, y] = en;
        }

        private static Entity GetOccupiedTileEntity(int x, int y)
        {
            return OccupiedTiles[x, y];
        }
    }
}
