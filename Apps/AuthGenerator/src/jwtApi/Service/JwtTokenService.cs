using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace src.Service;

public class CreateTokenOptions
{
    public Dictionary<string, string> Claims { get; set; }
}

public class JwtTokenService
{
    private static string _jwtKey = @$"0bd6d782a2313f9fe0d81b0cb8f41dcd4057c046a2e62506daf6728df7122299b1466277d69b7c9cab70c7be36dcb3dd75c59eb573c2516c325b9a80179fdd6bf9453d8be23ff0196cfe8e75bdcf8dd93f4e751ce83a54627050933e769e2976008d1d59a1af132539bc25d3fdcd1e11e489d42705c0d3c537b29503363a80c453a1a87e7166e5d7581924dc6f4bdc85e24b51170074459cf5d1ac5a66a75714460c04bcf1a5308604a5806eedd61376a917008f2b325c19fd44e1c77f92ffca003d27c3134e580fe00407f70a62b037e2b22048e2554e0f1349815cf141df7c8f6b81d690064da945d0d280e355ab076a9b7e40302a532cad53822c0476f8fb";
    // generally secrets should not be published but for the nature of this repository  with purely raw testing purposes it doesn't matter to much.
    // Note for any project where a secret can do harm, I should be hidden in an environment variable.
    public AuthenticationToken CreateToken(CreateTokenOptions claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var claimsList = new List<Claim>
        {
            new Claim("scope", "authenticated"),
        };

        foreach (var c in claims.Claims)
        {
            claimsList.Add(new Claim(c.Key, c.Value));
        } ;

        var tokenOptions = new JwtSecurityToken(
            signingCredentials: signingCredentials, claims: claimsList
        );
        
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var authToken = new AuthenticationToken
        {
            Value = token,
        };
        
        return authToken;
    }
}