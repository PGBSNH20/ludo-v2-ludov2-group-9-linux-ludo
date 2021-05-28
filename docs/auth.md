# Authentication
Authentication on the website is **optional** and is **not** required to play a game. Users may play as **guests** with a custom inputted name or choose to login to their accounts.</br>

All authentication based actions (logging in/signing up/verifiying users) is done through our own back-end. There are no third-party **services** that are being used, excluding frameworks/libraries et.c.</br>

The main component responsible for executing these actions is the [AuthController](https://github.com/PGBSNH20/ludo-v2-ludov2-group-9-linux-ludo/blob/main/src/LinuxLudo.API/Controllers/AuthController.cs).
It is structured as seen below.

# AuthController
- ## SignUp
```csharp
[HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
```
This method handles the signup requests.
It does so by taking an input **resource** as username/password and then it runs queries through our database.
It then returns a response message, verifying of its potential success, and/or response codes/message.

- ## SignIn
```csharp
[HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
```
Responsible for signing in and verifying user details.
This method works similarly to SignUp, but rather than creating new entries in the database, it **verifies** them.
If the login request is valid (user credentials match database) an **JWT** authentication token is sent back, along with a success code/message.
If for some reason the request wasn't valid, explicit details are sent back to the client explaining what the issue is.

