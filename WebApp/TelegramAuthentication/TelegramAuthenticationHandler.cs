using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.TelegramAuthentication;

public class TelegramAuthenticationHandler : AuthenticationHandler<TelegramAuthenticationOptions>
{
    private readonly IAppBll _bll;
    
    public TelegramAuthenticationHandler(
        IOptionsMonitor<TelegramAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAppBll bll
    ) : base(options, logger, encoder, clock)
    {
        _bll = bll;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("AuthorizationData") || !Request.Headers.ContainsKey("AuthorizationHash"))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var authorizationData = Request.Headers["AuthorizationData"].ToString().Replace("&", "\n");
        var authorizationHash = Request.Headers["AuthorizationHash"];
        if (authorizationData.IsNullOrEmpty() || authorizationHash.IsNullOrEmpty())
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        var telegramData = TelegramAuthUtils.ParseData(authorizationData, authorizationHash);
        if (telegramData == null)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var userId = (await _bll.Users.GetAllAsync()).FirstOrDefault(user => user.TelegramId.Equals(telegramData.TelegramId));
        
        if (userId == null)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.Id.ToString()),
            new(ClaimTypes.UserData, authorizationData),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}