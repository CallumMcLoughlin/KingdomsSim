using KingdomsSim.Classes.Game;
using KingdomsSim.Classes.Game.Entities;
using KingdomsSim.Classes.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsSim
{
    public static class GameManager
    {
        public static Random random = new Random();

        public static string MapName { get; private set; }
        public static int[,] MapTiles { get; private set; }
        public static bool IsSimulating { get; private set; } = false;

        private static List<Tile> TileTypes = new List<Tile>();
        private static Dictionary<Color, int> TileID = new Dictionary<Color, int>();

        private static float TimeBetweenTicks = 0;
        private static float TickDelay = 0.1f;

        private static Dictionary<string, Texture2D> Textures { get; set; } = null;

        public static void Initialize()
        {
            Type[] Types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type t in ReflectiveEnumerator.FindDerivedTypes(Assembly.GetExecutingAssembly(), typeof(Tile)))
            {
                Console.WriteLine(t.Name);
                Tile tile = (Tile)Activator.CreateInstance(t);
                TileTypes.Add(tile);
                TileID[tile.TileColor] = tile.ID;
                Console.WriteLine(tile.TileColor.ToString());
            }
        }

        public static void ChangeMap(string mapName)
        {
            MapTiles = ConvertToArray(Textures[mapName]);
            MapName = mapName;
            Texture2D map = Textures[MapName];
            Main.MainCamera.MoveCamera(new Vector2(map.Width / 2, map.Height / 2));
        }

        public static void Setup(Settings settings, Dictionary<string, Texture2D> textures)
        {
            TickDelay = settings.TickSpeed;
            Textures = textures;
            ChangeMap(settings.MapName);
            EntityManager.Setup();
            PopulateMap(settings.Kingdoms, settings.KingdomTextureSize, settings.KingdomTextureSize);
        }

        private static void PopulateMap(int kingdoms, int kingdomSize, int popSize)
        {
            Console.WriteLine($"Populating map with {kingdoms} kingdoms.");

            Texture2D someTexture = GenerateTexture(Main.GetGraphicsDevice(), popSize, Color.White);
            Entity.Texture = someTexture;

            for (int i = 0; i < kingdoms; i++)
            {
                Color someColor = new Color(random.Next(256), random.Next(256), random.Next(256));
                Population somePopulation = new Population(EntityManager.AssignID(), someColor, random.Next(20, 100), random.Next(20, 50), random.Next(100, 500), popSize, GetRandomValidPosition());
                EntityManager.CreateEntity(somePopulation);
                Console.WriteLine($"Placed kingdom at X:{somePopulation.X} Y:{somePopulation.Y}");
            }
        }

        private static Vector2 GetRandomValidPosition()
        {
            int x = random.Next(MapTiles.GetLength(0));
            int y = random.Next(MapTiles.GetLength(1));

            while (MapTiles[x, y] == 0 || !(EntityManager.OccupiedTiles[x, y] == null))
            {
                x = random.Next(MapTiles.GetLength(0));
                y = random.Next(MapTiles.GetLength(1));
            }

            return new Vector2(x, y);

        }

        public static void Update(GameTime gameTime, Input input)
        {
            if (!IsSimulating)
                return;

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeBetweenTicks += timer;
            if (TimeBetweenTicks >= TickDelay)
            {
                EntityManager.Update(gameTime);
                TimeBetweenTicks = 0;
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures[MapName], new Vector2(0, 0), Color.White);
            EntityManager.Draw(gameTime, spriteBatch);
        }

        private static int[,] ConvertToArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            }

            int[,] intMap = new int[texture.Width, texture.Height];
            for (int i = 0; i < intMap.GetLength(0); i++)
                for (int j = 0; j < intMap.GetLength(1); j++)
                {
                    Color c = colors2D[i, j];
                    if (!TileID.ContainsKey(c))
                    {
                        intMap[i, j] = 0;
                        continue;
                    }
                    intMap[i, j] = TileID[colors2D[i, j]];
                }

            return intMap;
        }

        public static void ToggleSim()
        {
            IsSimulating = !IsSimulating;
            Console.WriteLine($"IsSimulating {IsSimulating}");
        }

        public static Texture2D GenerateTexture(GraphicsDevice device, int size, Color color)
        {
            Texture2D texture = new Texture2D(device, size, size);

            Color[] data = new Color[size * size];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = color;
            }

            texture.SetData(data);
            return texture;
        }
    }
}
