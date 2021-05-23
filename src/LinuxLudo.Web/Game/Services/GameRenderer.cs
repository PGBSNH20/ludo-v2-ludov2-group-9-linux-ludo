using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace LinuxLudo.Web.Game
{
    public class GameRenderer
    {
        public ElementReference redToken, greenToken, blueToken, yellowToken;
        private Canvas2DContext context;
        private readonly int canvasWidth, canvasHeight;
        private readonly string userName;
        private const string canvasBgHex = "#3D4849";
        private const string baseBgHex = "#493E3D";
        private const string fontFace = "Courier New";
        private int TileSize => canvasWidth / 16;
        private int TokenSize => canvasWidth / 18;
        private int TopOffset => TileSize * 2;
        private string currentStatus;
        private GameBoard board;
        private GameStatus gameStatus;

        public GameRenderer(string userName, int canvasWidth, int canvasHeight)
        {
            this.userName = userName;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
        }

        public async Task RenderGame(Canvas2DContext context, GameBoard board, GameStatus gameStatus, string currentStatus)
        {
            if (redToken.Context == null)
            {
                return;
            }

            this.context = context;
            this.board = board;
            this.gameStatus = gameStatus;
            this.currentStatus = currentStatus;

            // Draws a basic canvas background color
            await context.SetFillStyleAsync(canvasBgHex);
            if (context.FillStyle.Equals(canvasBgHex))
            {
                await context.FillRectAsync(0, 0, canvasWidth, canvasHeight);

                // Fill the middle part (between the paths and tiles)
                await context.SetFillStyleAsync("#000000");
                await context.FillRectAsync((TileSize / 2) + TileSize, TileSize * 3, canvasWidth - (TileSize * 2.5), canvasHeight - (TileSize * 5));
            }

            await DrawBoardLayout();
            await DrawBases();
            await DrawPlayers();
            await DrawGameOverlay();
        }

        protected async Task DrawGameOverlay()
        {
            await context.SetStrokeStyleAsync("#FFFFFF");
            await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");

            // Draw a status text, starting at the top-left tile (with a max width of the first row as to not overlap with bases)
            await context.StrokeTextAsync(currentStatus, (board.Tiles[0].XPos * TileSize) + TileSize, TileSize + TileSize / 2,
            TileSize * 6);


            if (gameStatus.Players.Count > 0)
            {
                await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");

                // Draw each players name on top of the base border
                foreach (Player player in gameStatus.Players)
                {
                    await context.SetStrokeStyleAsync(player.Name == userName ? "#FFFFFF" : "#000000");

                    int xPos = 0, yPos = 0;
                    switch (player.Color)
                    {
                        case "red":
                            xPos = board.redBaseX1;
                            yPos = board.redBaseY1;
                            break;
                        case "green":
                            xPos = board.greenBaseX1;
                            yPos = board.greenBaseY1;
                            break;
                        case "blue":
                            xPos = board.blueBaseX1;
                            yPos = board.blueBaseY1;
                            break;
                        case "yellow":
                            xPos = board.yellowBaseX1;
                            yPos = board.yellowBaseY1;
                            break;
                    }

                    await context.StrokeTextAsync(player.Name == userName ? "You" : player.Name, (xPos * TileSize) + TileSize / 2, (yPos * TileSize) + TileSize * 1.1, 2 * TileSize);
                }
            }
        }

        protected async Task DrawBases()
        {
            // Draw all the bases
            await context.SetFillStyleAsync("#DC143C");
            await DrawTokenBase(board.redBaseX1, board.redBaseY1, board.redBaseY2);
            await context.SetFillStyleAsync("#00FF7F");
            await DrawTokenBase(board.greenBaseX1, board.greenBaseY1, board.greenBaseY2);
            await context.SetFillStyleAsync("#1E90FF");
            await DrawTokenBase(board.blueBaseX1, board.blueBaseY1, board.blueBaseY2);
            await context.SetFillStyleAsync("#F0E68C");
            await DrawTokenBase(board.yellowBaseX1, board.yellowBaseY1, board.yellowBaseY2);
        }

        protected async Task DrawTokenBase(int x1, int y1, int y2)
        {
            // Draws the top and bottom parts
            for (int x = x1; x < x1 + 4; x++)
            {
                await context.FillRectAsync(x * TileSize - TileSize / 2, y1 * TileSize + TileSize / 2, TileSize, TileSize);
                await context.FillRectAsync(x * TileSize - TileSize / 2, (y2 - 1) * TileSize + TileSize / 2, TileSize, TileSize);
            }

            // Draws the sides
            for (int y = y1 + 1; y < y2; y++)
            {
                await context.FillRectAsync(x1 * TileSize - TileSize / 2, y * TileSize + TileSize / 2, TileSize, TileSize);
                await context.FillRectAsync((x1 + 3) * TileSize - TileSize / 2, y * TileSize + TileSize / 2, TileSize, TileSize);
            }

            // Fills the center with a background color
            await context.SetFillStyleAsync(baseBgHex);
            await context.FillRectAsync((x1 + 1) * TileSize - TileSize / 2, (y1 + 2) * TileSize - TileSize / 2, 2 * TileSize, 2 * TileSize);
        }

        protected async Task DrawBoardLayout()
        {
            for (int i = 0; i < board.Tiles.Count; i++)
            {
                GameTile tile = board.Tiles[i];
                string color = "#FFFFFF"; // White tile color for all tokens,
                switch (tile.TileColor)
                {
                    case GameTile.GameColor.Red:
                        color = "#DC143C";
                        break;
                    case
                GameTile.GameColor.Green:
                        color = "#00FF7F";
                        break;
                    case GameTile.GameColor.Blue:
                        color = "#1E90FF";
                        break;
                    case
                GameTile.GameColor.Yellow:
                        color = "#F0E68C";
                        break;
                }

                // Draws the actual tile await await
                await context.SetFillStyleAsync(color);
                await context.FillRectAsync(GetTilePos(i, xPos: true), GetTilePos(i, xPos: false), TileSize, TileSize);
            }
        }

        protected async Task DrawPlayers()
        {
            if (gameStatus.Players == null || gameStatus.Players.Count <= 0)
            {
                currentStatus = "No players in game!";
                return;
            }

            await context.SetStrokeStyleAsync("#FFFFFF");
            foreach (Player player in gameStatus.Players)
            {
                // Set the drawing color to match the players tokens await await
                ElementReference tokenToDraw;
                switch (player.Color)
                {
                    case "red":
                        tokenToDraw = redToken;
                        break;
                    case "green":
                        tokenToDraw = greenToken;
                        break;
                    case "blue":
                        tokenToDraw = blueToken;
                        break;
                    case "yellow":
                        tokenToDraw = yellowToken;
                        break;
                }

                // Draws the tokens in base first
                for (int i = 0; i < player.Tokens.Where(token => token.InBase).ToList().Count; i++)
                {
                    double xPos = 0, yPos = 0;
                    switch (player.Color)
                    {
                        case "red":
                            xPos = board.redBaseX1;
                            yPos = board.redBaseY1;
                            break;
                        case "green":
                            xPos = board.greenBaseX1;
                            yPos = board.greenBaseY1;
                            break;
                        case "blue":
                            xPos = board.blueBaseX1;
                            yPos = board.blueBaseY1;
                            break;
                        case "yellow":
                            xPos = board.yellowBaseX1;
                            yPos = board.yellowBaseY1;
                            break;
                    }


                    xPos = xPos * TileSize + (TileSize / 2) + (i == 1 || i == 3 ? 1 * TileSize : 0);
                    yPos = (yPos * TileSize) + (TileSize * 1.5) + (i == 1 || i == 2 ? 1 * TileSize : 0);

                    await context.DrawImageAsync(tokenToDraw,
                    xPos,
                    yPos,
                    TileSize,
                    TileSize);

                    await context.StrokeTextAsync(player.Tokens[i].IdentifierChar.ToString(), xPos + TileSize / 3, yPos + TileSize / 1.5, TileSize);
                }

                // Draws each token, splits up the space if more than one is on the same tile
                for (int i = 0; i < player.Tokens.Count; i++)
                {
                    if (player.Tokens[i].InBase)
                        continue;

                    int tokensOnSameTile = player.Tokens.Count(t => t.TilePos == player.Tokens[i].TilePos && !t.InBase);
                    double xPos, yPos, width = TokenSize, height = TokenSize;

                    if (tokensOnSameTile > 1)
                    {
                        await context.SetFontAsync($"{Math.Max(8, (canvasWidth / 25) - (tokensOnSameTile * 5))}px {fontFace}");
                        int index = 0;
                        foreach (GameToken token in player.Tokens.Where(t => t.TilePos == player.Tokens[i].TilePos && !t.InBase))
                        {
                            xPos = GetTilePos(token.TilePos, xPos: true) + (TokenSize / 24) + ((index == 1 || index == 3) ? (TokenSize / 2)
                            : 0);

                            yPos = GetTilePos(token.TilePos, xPos: false) + (TokenSize / 24) + ((index == 1 || index == 2) ? (TokenSize /
                            2) : 0);

                            width = (TokenSize / tokensOnSameTile) + tokensOnSameTile;
                            height = (TokenSize / tokensOnSameTile) + tokensOnSameTile;

                            await context.DrawImageAsync(tokenToDraw, xPos, yPos, width, height);
                            await context.StrokeTextAsync(token.IdentifierChar.ToString(), xPos + width / 3, yPos + (height / 1.5));
                            index++;
                        }
                    }
                    else
                    {
                        await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");
                        xPos = GetTilePos(player.Tokens[i].TilePos, xPos: true) + (TokenSize / 24);
                        yPos = GetTilePos(player.Tokens[i].TilePos, xPos:
                        false) + (TokenSize / 24);

                        await context.DrawImageAsync(tokenToDraw, xPos, yPos, width, height);
                        await context.StrokeTextAsync(player.Tokens[i].IdentifierChar.ToString(), xPos + width / 3.5, yPos + height / 1.5);
                    }
                }

            }
        }

        private double GetTilePos(int tileIndex, bool xPos)
        {
            if (xPos)
            {
                return (board.Tiles[tileIndex].XPos * TileSize) + TileSize / 2;
            }

            // Else return yPos
            return (board.Tiles[tileIndex].YPos * TileSize) - TileSize / 2 +
            TopOffset;
        }


    }
}