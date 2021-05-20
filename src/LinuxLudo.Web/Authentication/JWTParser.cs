using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace LinuxLudo.Web.Authentication
{
    public class JWTParser
    {
        public static IEnumerable<Claim> ParseClaims(string JWT)
        {
            var claims = new List<Claim>();
            var payload = JWT.Split('.')[1];
            var jsonBytes = ParseBase64(payload);

            // Create readable dictionary from bytes
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractRoles(claims, keyValuePairs);
            claims.AddRange(keyValuePairs.Select(pair => new Claim(pair.Key, pair.Value.ToString())));
            return claims;
        }

        private static void ExtractRoles(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            // Fetches the roles
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles is not null)
            {
                // Cleans/formats the fetched roles
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(",");

                // Add the individual roles
                if (parsedRoles.Length > 1)
                {
                    foreach (var role in parsedRoles)
                    {
                        // Add the role without quotes
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim('"')));
                    }
                }
                else
                {
                    // Add the first role
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }

                // Remove initial role to avoid duplication
                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }

        private static byte[] ParseBase64(string base64)
        {
            // Formats the inputted string based on length
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}