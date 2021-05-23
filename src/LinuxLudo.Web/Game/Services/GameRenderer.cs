using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace LinuxLudo.Web.Game
{
    public class GameRenderer
    {
        private readonly ElementReference redToken, greenToken, blueToken, yellowToken;
        private Canvas2DContext context;
        private readonly int canvasWidth;
        private int TileSize => canvasWidth / 16;
        private int TokenSize => canvasWidth / 18;
        private int TopOffset => TileSize * 2;
        private string currentStatus;
        private GameBoard board;
        private GameStatus gameStatus;

        public GameRenderer(int canvasWidth, ElementReference redToken, ElementReference greenToken, ElementReference blueToken, ElementReference yellowToken)
        {
            this.canvasWidth = canvasWidth;

            this.redToken = redToken;
            this.greenToken = greenToken;
            this.blueToken = blueToken;
            this.yellowToken = yellowToken;
        }

        public async void RenderGame(Canvas2DContext context, GameBoard board, GameStatus gameStatus, string currentStatus)
        {
            this.context = context;
            this.board = board;
            this.gameStatus = gameStatus;
            this.currentStatus = currentStatus;

            await DrawGameOverlay();
            await DrawBoardLayout();
            await DrawPlayers();
        }

        protected async Task DrawGameOverlay()
        {
            await context.SetStrokeStyleAsync("#FFFFFF");
            await context.SetFontAsync((canvasWidth / 25).ToString() + "px serif");

            // Draw a status text, starting at the top-left tile (with a max width of the first row as to not overlap with bases)
            await context.StrokeTextAsync(currentStatus, (board.Tiles[0].XPos * TileSize) + TileSize, TileSize + TileSize / 2,
            TileSize * 6);
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

                // Draws each token, splits up the space if more than one is on the same tile
                List<GameToken> playerTokens = player.Tokens;
                for (int i = 0; i < playerTokens.Count; i++)
                {
                    int tokensOnSameTile = playerTokens.Count(t => t.TilePos == playerTokens[i].TilePos);
                    double xPos, yPos, width = TokenSize, height = TokenSize;

                    if (tokensOnSameTile > 1)
                    {
                        await context.SetFontAsync(Math.Max(8, (canvasWidth / 25) - (tokensOnSameTile * 5)).ToString() + "px serif");
                        int index = 0;
                        foreach (GameToken token in playerTokens.Where(t => t.TilePos == playerTokens[i].TilePos))
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
                        await context.SetFontAsync((canvasWidth / 25).ToString() + "px serif");
                        xPos = GetTilePos(playerTokens[i].TilePos, xPos: true) + (TokenSize / 24);
                        yPos = GetTilePos(playerTokens[i].TilePos, xPos:
                        false) + (TokenSize / 24);

                        await context.DrawImageAsync(tokenToDraw, xPos, yPos, width, height);
                        await context.StrokeTextAsync(playerTokens[i].IdentifierChar.ToString(), xPos + width / 3.5, yPos + height / 1.5);
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