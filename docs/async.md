# Asynchronous communication
All communication between clients and the game server is done using the [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) framework.</br>
Messages are sent back-and-forth between users and the server through the [MessagePack protocol](https://msgpack.org/).</br>
The reason for using this protocol over the default **JSON** is speed (latency).</br>
This also equates to lesser bandwidth which is always a plus if possible.

The MessagePack protocol works by serializing the data into byte-arrays which are then de-serialized upon arrival.
# SignalR infrastructure
The SignalR framework works by receiving/sending data through **hubs**.</br>
Instead of creating individual hubs for each game there is only one [hub](https://github.com/PGBSNH20/ludo-v2-ludov2-group-9-linux-ludo/blob/main/src/LinuxLudo.Web/Hubs/HubController.cs) in the game which handles all traffic, across **all** games & users.</br>

The hub then splits its users up into groups which are correlated to the game's **UUID**.

# GameHub methods
The player's turn is updated through UpdatePlayerTurn() after each of the following methods has executed fully:
- MoveToken()
- BringOutToken()

The hub controlling the games use the following methods:

## JoinGame
```csharp
public async Task JoinGame(string username, Guid gameId)
```
Called whenever a user clicks the join button on a game page.
Responsible for adding the user to the specified games group </br>
by fetching 
```csharp 
Context.ConnectionId
```
and storing it in the **hubs** repository list of connected users.

## NotifyRollDice
```csharp
public async Task NotifyRollDice(string username, int roll)
```
A helper method used to notify connected clients that a player has rolled the dice.

## MoveToken
```csharp
public async Task MoveToken(byte[] receivedMessage)
```
Called when the player whose turn it is click the roll dice button.
Responsible for calculating the updated tokens position and almost broadcasting the change to connected clients through **NotifyRollDice()**
It is also responsible for determining what (if any) tokens have been knocked out and also as a result if a player has won.
It does this by analyzing the result of the [GameEngine](https://github.com/PGBSNH20/ludo-v2-ludov2-group-9-linux-ludo/blob/main/src/LinuxLudo.API/Domain/GameEngine.cs)'s **MoveToken()** method.

## BringOutToken
```csharp
public async Task BringOutToken(byte[] receivedMessage)
```
Called when a player clicks the bring out token button.
Responsible for rolling the dice and analyzing the result and determining whether or not the player brings out a token (roll number 6).
It then either broadcasts a successful token "bring-out" or simply the rolled number.

## NotifyTokenKnockout
```csharp
public async Task NotifyTokenKnockout(string tokenHolderName, char tokenIdentifierChar)
```
A helper method used to notify connected clients on which token has been knocked out (the players name holding the token and the tokens identifier letter).

## UpdatePlayerTurn
```csharp
private async Task UpdatePlayerTurn(OpenGame game)
```
A helper method used to notify connected clients on whose turn it is. Sends the color of whose turn it is.
Called after each full execution of:
- MoveToken()
- BringOutToken()

## SendConnectionChanged
```csharp
private async Task SendConnectionChanged(string gameId, string username, List<Player> players)
```
Called whenever a player connects/disconnects from the game.
Used to notify clients of whoever joined/leaved and also sends an updated list of **connected** players.

## OnDisconnectedAsync
```csharp
public override async Task OnDisconnectedAsync(Exception exception)
```
Called internally from SignalR whenever a connected client disconnects for **any** reason.
This **overridden** method automatically removes the disconnected player from its **game**.
Subsequently it notifies the **connected** clients the name of the disconnected player and then updates the game's turn
if the game's current turn was set to the **disconnected** player.




