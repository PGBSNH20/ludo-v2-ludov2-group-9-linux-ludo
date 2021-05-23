using System.Collections.Generic;
using static LinuxLudo.Web.Game.GameTile;

namespace LinuxLudo.Web.Game
{
    public class GameBoard
    {
        public List<GameTile> Tiles = new()
        {
            // TOP MOST ROW
            new GameTile(GameColor.Any, 4, 1),
            new GameTile(GameColor.Any, 5, 1),
            new GameTile(GameColor.Any, 6, 1),
            //

            // YELLOW PATH
            new GameTile(GameColor.Yellow, 7, 1), // INDEX 3
            new GameTile(GameColor.Yellow, 7, 2),
            new GameTile(GameColor.Yellow, 7, 3),
            new GameTile(GameColor.Yellow, 7, 4),
            new GameTile(GameColor.Yellow, 7, 5),
            new GameTile(GameColor.Yellow, 7, 6),
            new GameTile(GameColor.Yellow, 7, 7),
            //

            new GameTile(GameColor.Any, 8, 1),
            new GameTile(GameColor.Any, 9, 1),
            new GameTile(GameColor.Any, 10, 1),
            new GameTile(GameColor.Any, 10, 2),
            new GameTile(GameColor.Any, 10, 3),
            new GameTile(GameColor.Any, 10, 4),
            new GameTile(GameColor.Any, 11, 5),
            new GameTile(GameColor.Any, 12, 5),
            new GameTile(GameColor.Any, 13, 5),
            new GameTile(GameColor.Any, 14, 5),
            new GameTile(GameColor.Any, 14, 6),
            new GameTile(GameColor.Any, 14, 7),

            // BLUE PATH
            new GameTile(GameColor.Blue, 14, 8), // INDEX 22
            new GameTile(GameColor.Blue, 13, 8),
            new GameTile(GameColor.Blue, 12, 8),
            new GameTile(GameColor.Blue, 11, 8),
            new GameTile(GameColor.Blue, 10, 8),
            new GameTile(GameColor.Blue, 9, 8),
            new GameTile(GameColor.Blue, 8, 8),
            //

            new GameTile(GameColor.Any, 14, 9),
            new GameTile(GameColor.Any, 14, 10),
            new GameTile(GameColor.Any, 14, 11),
            new GameTile(GameColor.Any, 13, 11),
            new GameTile(GameColor.Any, 12, 11),
            new GameTile(GameColor.Any, 11, 11),
            new GameTile(GameColor.Any, 10, 12),
            new GameTile(GameColor.Any, 10, 13),
            new GameTile(GameColor.Any, 10, 14),
            new GameTile(GameColor.Any, 10, 15),
            new GameTile(GameColor.Any, 9, 15),
            new GameTile(GameColor.Any, 8, 15),

            // GREEN PATH
            new GameTile(GameColor.Green, 7, 15), // INDEX 43
            new GameTile(GameColor.Green, 7, 14),
            new GameTile(GameColor.Green, 7, 13),
            new GameTile(GameColor.Green, 7, 12),
            new GameTile(GameColor.Green, 7, 11),
            new GameTile(GameColor.Green, 7, 10),
            new GameTile(GameColor.Green, 7, 9),
            //

            new GameTile(GameColor.Any, 6, 15),
            new GameTile(GameColor.Any, 5, 15),
            new GameTile(GameColor.Any, 4, 15),
            new GameTile(GameColor.Any, 4, 14),
            new GameTile(GameColor.Any, 4, 13),
            new GameTile(GameColor.Any, 4, 12),
            new GameTile(GameColor.Any, 3, 11),
            new GameTile(GameColor.Any, 2, 11),
            new GameTile(GameColor.Any, 1, 11),
            new GameTile(GameColor.Any, 0, 11),
            new GameTile(GameColor.Any, 0, 10),
            new GameTile(GameColor.Any, 0, 9),

            // RED PATH
            new GameTile(GameColor.Red, 0, 8), // INDEX 60
            new GameTile(GameColor.Red, 1, 8),
            new GameTile(GameColor.Red, 2, 8),
            new GameTile(GameColor.Red, 3, 8),
            new GameTile(GameColor.Red, 4, 8),
            new GameTile(GameColor.Red, 5, 8),
            new GameTile(GameColor.Red, 6, 8),
            //

            new GameTile(GameColor.Any, 0, 7),
            new GameTile(GameColor.Any, 0, 6),
            new GameTile(GameColor.Any, 0, 5),
            new GameTile(GameColor.Any, 1, 5),
            new GameTile(GameColor.Any, 2, 5),
            new GameTile(GameColor.Any, 3, 5),
            new GameTile(GameColor.Any, 4, 4),
            new GameTile(GameColor.Any, 4, 3),
            new GameTile(GameColor.Any, 4, 2),


            // Center tile
            new GameTile(GameColor.Any, 7, 8)
        };
    
        
    }
}