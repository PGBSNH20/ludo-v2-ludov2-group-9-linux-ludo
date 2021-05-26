namespace LinuxLudo.Web.Game
{
    public class GameTile
    {
        public enum GameColor
        {
            Any,
            Red,
            Green,
            Blue,
            Yellow
        }

        public GameColor TileColor { get; set; }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public GameTile(GameColor tileColor, int xPos, int yPos)
        {
            TileColor = tileColor;
            XPos = xPos;
            YPos = yPos;
        }
    }
}