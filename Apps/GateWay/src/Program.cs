using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

const string _jwtKey = @$"0bd6d782a2313f9fe0d81b0cb8f41dcd4057c046a2e62506daf6728df7122299b1466277d69b7c9cab70c7be36dcb3dd75c59eb573c2516c325b9a80179fdd6bf9453d8be23ff0196cfe8e75bdcf8dd93f4e751ce83a54627050933e769e2976008d1d59a1af132539bc25d3fdcd1e11e489d42705c0d3c537b29503363a80c453a1a87e7166e5d7581924dc6f4bdc85e24b51170074459cf5d1ac5a66a75714460c04bcf1a5308604a5806eedd61376a917008f2b325c19fd44e1c77f92ffca003d27c3134e580fe00407f70a62b037e2b22048e2554e0f1349815cf141df7c8f6b81d690064da945d0d280e355ab076a9b7e40302a532cad53822c0476f8fb";
// generally secrets should not be published but for the nature of this repository  with purely raw testing purposes it doesn't matter to much.
// Note for any project where a secret can do harm, I should be hidden in an environment variable.

var builder = WebApplication.CreateBuilder(args);

//ocelot
builder.Configuration
    .AddJsonFile("configuration/ocelot.global.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey))
        };
    }
    );
builder.Services.AddOcelot(builder.Configuration);

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthorization();

app.UseAuthentication();

app.UseOcelot().Wait();

app.Run();