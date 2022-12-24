using System.Diagnostics;
using System.Net;
using App.Contracts.DAL;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.Identity;
using WebApp.TelegramAuthentication;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// Account api controller
/// </summary>
[Route("api/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly Random _rnd = new();
    private readonly IAppUnitOfWork _uow;
    
    public AccountController(
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        ILogger<AccountController> logger,
        IAppUnitOfWork uow
        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _uow = uow;
    }

    /// <summary>
    /// Login using telegram Id
    /// </summary>
    /// <param name="loginData">Login object with data from telegram</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> LogIn([FromBody] Login loginData)
    {
        var telegramData = TelegramAuthUtils.ParseData(loginData.TelegramData, loginData.Hash);
        if (telegramData == null)
        {
            _logger.LogWarning("WebApi login failed, data problem");
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse
            {
                Type = "NotFound",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email/Password"] = new List<string>
                    {
                        "Data verification failed"
                    }
                }
            };
            return NotFound(errorResponse);
        }

        var appUser = _userManager.Users.FirstOrDefault(user => user.TelegramId.Equals(telegramData.TelegramId));
        if (appUser == null)
        {
            _logger.LogInformation("WebApi login failed, user with telegram id {} not found", telegramData.TelegramId);
            _logger.LogInformation("Try to register user");
            return await Register(loginData);
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get claimsPrincipal for user with telegram id {}", telegramData.TelegramId);
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse
            {
                Type = "NotFound",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email/Password"] = new List<string>
                    {
                        "User or Password problem"
                    }
                }
            };
            return NotFound(errorResponse);
        }

        await _uow.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Register new account
    /// </summary>
    /// <param name="registrationData">Login object with data from telegram</param>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] Login registrationData)
    {
        var telegramData = TelegramAuthUtils.ParseData(registrationData.TelegramData, registrationData.Hash);
        if (telegramData == null)
        {
            _logger.LogWarning("WebApi login failed, data problem");
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse
            {
                Type = "NotFound",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email/Password"] = new List<string>
                    {
                        "Data verification failed"
                    }
                }
            };
            return NotFound(errorResponse);
        }
        
        
        var appUser = _userManager.Users.FirstOrDefault(user => user.TelegramId.Equals(telegramData.TelegramId));
        if (appUser != null)
        {
            _logger.LogWarning("User with telegram id {} is already registered", telegramData.TelegramId);
            var errorResponse = new RestApiErrorResponse
            {
                Type = "BadRequest",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email"] = new List<string>
                    {
                        "User already registered"
                    }
                }
            };
            return BadRequest(errorResponse);
        }
        
        appUser = new AppUser
        {
            TelegramId = telegramData.TelegramId,
            UserName = telegramData.UserName,
            FirstName = telegramData.FirstName,
            LastName = telegramData.LastName
        };
        
        var result = await _userManager.CreateAsync(appUser);
        if (result.Succeeded == false)
        {
            _logger.LogWarning("Failed to create user with telegram Id \"{}\"", telegramData.TelegramId);
            return BadRequest(result);
        }
        
        // get full user from system with fixed data
        appUser = _userManager.Users.FirstOrDefault(user => user.TelegramId.Equals(telegramData.TelegramId));
        if (appUser == null)
        {
            _logger.LogWarning("User with telegram id {} is not found after registration", telegramData.TelegramId);
            var errorResponse = new RestApiErrorResponse
            {
                Type = "BadRequest",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email"] = new List<string>
                    {
                        $"User with telegram id {telegramData.TelegramId} is not found after registration"
                    }
                }
            };
            return BadRequest(errorResponse);
        }

        // get claims jwt
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get claimsPrincipal for user with telegram id {}", telegramData.TelegramId);
            var errorResponse = new RestApiErrorResponse
            {
                Type = "NotFound",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["Email/Password"] = new List<string>
                    {
                        "User or Password problem"
                    }
                }
            };
            return NotFound(errorResponse);
        }

        return Ok();
    }
}