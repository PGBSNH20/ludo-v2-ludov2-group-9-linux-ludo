using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using LinuxLudo.Core.Models;
using LinuxLudo.Web.Domain.Models;
using LinuxLudo.Web.Game;
using Microsoft.AspNetCore.Components;

namespace LinuxLudo.Web.Services
{
    public class GameRenderer
    {
        private readonly ElementReference redToken, greenToken, blueToken, yellowToken;
        private Canvas2DContext context;
        public int canvasWidth, canvasHeight;
        public string username;
        private const string canvasBgHex = "#9CA6D9";
        private const string overlayTextColor = "#FFFFFF";
        private const string tokenTextColor = "#FFFFFF";
        private const string tokenHighlightTextColor = "#FFFF00";
        private const float tokenTextLineWidth = 0.5f;
        private const string tileOutlineColor = "#000000";
        private const string tileBgColor = "#FFFFFF";
        private const float tileOutlineWidth = 3;
        private const string baseBgHex = "#000000";
        private const string fontFace = "Courier New";
        private int TileSize => canvasWidth / 16;
        private int TokenSize => canvasWidth / 18;
        private int TopOffset => TileSize * 2;
        private string statusMessage;
        private GameBoard board;
        private GameStatus gameStatus;

        public GameRenderer(string username, int canvasWidth, int canvasHeight, ElementReference redToken, ElementReference greenToken, ElementReference blueToken, ElementReference yellowToken)
        {
            this.username = username;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.redToken = redToken;
            this.greenToken = greenToken;
            this.blueToken = blueToken;
            this.yellowToken = yellowToken;
        }

        public async Task RenderGame(Canvas2DContext context, GameBoard board, GameStatus gameStatus, string statusMessage, char selectedToken)
        {
            this.context = context;
            this.board = board;
            this.gameStatus = gameStatus;
            this.statusMessage = statusMessage;

            // Draws a basic canvas background color
            await context.SetFillStyleAsync(canvasBgHex);
            if (context.FillStyle.Equals(canvasBgHex))
            {
                await context.FillRectAsync(0, 0, canvasWidth, canvasHeight);

                // Fill the middle part (between the paths and tiles)
                //await context.SetFillStyleAsync("#000000");
                //await context.FillRectAsync((TileSize / 2) + TileSize - tileOutlineWidth, TileSize * 3 + tileOutlineWidth, canvasWidth - (TileSize * 2.5), canvasHeight - (TileSize * 5));
            }

            await DrawBoardLayout();
            await DrawBases();
            await DrawPlayers(selectedToken);
            await DrawGameOverlay();
        }

        protected async Task DrawGameOverlay()
        {
            await context.SetStrokeStyleAsync(overlayTextColor);
            await context.SetFontAsync($"{canvasWidth / 20}px {fontFace}");
            await context.SetLineWidthAsync(1);

            // Draw a status text, starting at the top-left tile (with a max width of the first row as to not overlap with bases)
            await context.StrokeTextAsync(statusMessage, (board.Tiles[0].XPos * TileSize) + TileSize, TileSize * 1.5f,
            TileSize * 6);

            if (gameStatus.Players?.Count > 0)
            {
                await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");

                // Draw each players name on top of the base border
                foreach (Player player in gameStatus.Players)
                {
                    await context.SetStrokeStyleAsync(player.Name == username ? "#FFFFFF" : "#000000");

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

                    await context.StrokeTextAsync(player.Name == username ? "You" : player.Name, (xPos * TileSize) + TileSize / 2, (yPos * TileSize) + TileSize * 1.1, 2 * TileSize);
                }
            }
        }

        protected async Task DrawBases()
        {
            // Draw all the bases
            await context.SetFillStyleAsync("#DC143C");
            await DrawTokenBase(board.redBaseX1, board.redBaseY1);
            await context.SetFillStyleAsync("#00FF7F");
            await DrawTokenBase(board.greenBaseX1, board.greenBaseY1);
            await context.SetFillStyleAsync("#1E90FF");
            await DrawTokenBase(board.blueBaseX1, board.blueBaseY1);
            await context.SetFillStyleAsync("#F0E68C");
            await DrawTokenBase(board.yellowBaseX1, board.yellowBaseY1);
        }

        protected async Task DrawTokenBase(int x1, int y1)
        {
            const double multiplier = 2;

            // Draws the whole base
            await context.FillRectAsync(
                x1 * TileSize - TileSize / multiplier - tileOutlineWidth / 2,
                 y1 * TileSize + TileSize / multiplier - tileOutlineWidth / 2,
                 TileSize * 4,
                 TileSize * 4);


            // Fills the center with a background color
            await context.SetFillStyleAsync(baseBgHex);
            await context.FillRectAsync((x1 + 1) * TileSize - TileSize / multiplier - tileOutlineWidth, (y1 + 2) * TileSize - TileSize / multiplier - tileOutlineWidth / 2, multiplier * TileSize, multiplier * TileSize);
        }

        protected async Task DrawBoardLayout()
        {
            for (int i = 0; i < board.Tiles.Count; i++)
            {
                GameTile tile = board.Tiles[i];
                string strokeColor = tileOutlineColor; // Default tile color
                string fillColor = tileBgColor;

                switch (tile.TileColor)
                {
                    case GameTile.GameColor.Red:
                        strokeColor = "#DC143C";
                        break;
                    case
                GameTile.GameColor.Green:
                        strokeColor = "#00FF7F";
                        break;
                    case GameTile.GameColor.Blue:
                        strokeColor = "#1E90FF";
                        break;
                    case
                GameTile.GameColor.Yellow:
                        strokeColor = "#F0E68C";
                        break;
                    case
                GameTile.GameColor.Goal:
                        strokeColor = "#FF00FF";
                        break;
                }

                if (strokeColor != tileOutlineColor)
                {
                    fillColor = strokeColor;
                    strokeColor = "#800080";
                }


                // Draws the actual tile
                await context.SetStrokeStyleAsync(strokeColor);
                await context.SetLineWidthAsync(tileOutlineWidth);
                await context.StrokeRectAsync(GetTilePos(i, xPos: true), GetTilePos(i, xPos: false), TileSize - tileOutlineWidth, TileSize - tileOutlineWidth);
                await context.SetFillStyleAsync(fillColor);
                await context.FillRectAsync(GetTilePos(i, xPos: true) + TileSize / 8 - tileOutlineWidth, GetTilePos(i, xPos: false) + TileSize / 8 - tileOutlineWidth, TileSize - (TileSize / 4) + tileOutlineWidth, TileSize - (TileSize / 4) + tileOutlineWidth);
            }
        }

        protected async Task DrawPlayers(char selectedToken)
        {
            if (gameStatus.Players == null || gameStatus.Players.Count == 0)
            {
                return;
            }

            await context.SetStrokeStyleAsync(tokenTextColor);

            // Draws each player and its tokens
            foreach (Player player in gameStatus.Players)
            {
                await context.SetLineWidthAsync(tokenTextLineWidth);

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
                int baseIndex = 0;
                foreach (GameToken token in player.Tokens.Where(token => token.InBase).ToList())
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

                    xPos = xPos * TileSize + (TileSize / 2) + (baseIndex % 2 == 0 ? 1 * TileSize : 0) - tileOutlineWidth;
                    yPos = (yPos * TileSize) + (TileSize * 1.5) + (baseIndex == 1 || baseIndex == 2 ? 1 * TileSize : 0) - (tileOutlineWidth / 2);

                    await context.SetStrokeStyleAsync(tokenTextColor);
                    await context.DrawImageAsync(tokenToDraw, xPos, yPos, TileSize, TileSize);
                    await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");
                    await context.StrokeTextAsync(token.IdentifierChar.ToString(), xPos + TileSize / 3, yPos + TileSize / 1.5, TileSize);
                    baseIndex++;
                }

                // Draws each token, splits up the space if more than one is on the same tile
                foreach (GameToken token in player.Tokens.Where(token => !token.InBase).ToList())
                {
                    await context.SetStrokeStyleAsync(player.Name == username && token.IdentifierChar == selectedToken ? tokenHighlightTextColor : tokenTextColor);

                    int tokensOnSameTile = player.Tokens.Count(t => t.TilePos == token.TilePos && !t.InBase);
                    double xPos, yPos, width = TokenSize, height = TokenSize;

                    if (tokensOnSameTile > 1)
                    {
                        await context.SetFontAsync($"{Math.Max(8, (canvasWidth / 25) - (tokensOnSameTile * 5))}px {fontFace}");
                        int index = 0;
                        foreach (GameToken dupToken in player.Tokens.Where(t => t.TilePos == token.TilePos && !t.InBase))
                        {

                            xPos = GetTilePos(dupToken.TilePos, xPos: true) + (index % 2 != 0 ? (TokenSize / tokensOnSameTile) : 0);
                            yPos = GetTilePos(dupToken.TilePos, xPos: false) + (index == 1 || index == 2 ? (TokenSize / tokensOnSameTile) : 0);

                            width = (TokenSize / tokensOnSameTile) + (TileSize / tokensOnSameTile / 2);
                            height = width;

                            await context.SetStrokeStyleAsync(player.Name == username && dupToken.IdentifierChar == selectedToken ? tokenHighlightTextColor : tokenTextColor);
                            await context.DrawImageAsync(tokenToDraw, xPos, yPos, width, height);
                            await context.StrokeTextAsync(dupToken.IdentifierChar.ToString(), xPos + width / 3, yPos + (height / 1.5));
                            index++;
                        }
                    }
                    else
                    {
                        xPos = GetTilePos(token.TilePos, xPos: true);
                        yPos = GetTilePos(token.TilePos, xPos: false);

                        await context.SetFontAsync($"{canvasWidth / 25}px {fontFace}");
                        await context.DrawImageAsync(tokenToDraw, xPos, yPos, width, height);
                        await context.StrokeTextAsync(token.IdentifierChar.ToString(), xPos + width / 3.5, yPos + height / 1.5);
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
            return (board.Tiles[tileIndex].YPos * TileSize) - TileSize / 2 + TopOffset;
        }
    }
}