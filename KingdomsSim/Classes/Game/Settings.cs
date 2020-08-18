namespace KingdomsSim
{
    public struct Settings
    {
        public string MapName { get; private set; }
        public int Kingdoms { get; private set; }
        public int KingdomTextureSize { get; private set; }
        public float TickSpeed { get; private set; }

        public Settings(string mapName, int kingdoms, int textureSize, float tickSpeed)
        {
            MapName = mapName;
            Kingdoms = kingdoms;
            KingdomTextureSize = textureSize;
            TickSpeed = tickSpeed;
        }
    }
}