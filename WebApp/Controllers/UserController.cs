using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Dtos.UserAccount;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserManager<UserAccount> userManager, ILogger<UserController> logger, SignInManager<UserAccount> signInManager, IMapper mapper) : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager = userManager;
        private readonly SignInManager<UserAccount> _signInManager = signInManager;
        private readonly ILogger<UserController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            UserAccount user = _mapper.Map<UserAccount>(request);

            var res = await _userManager.CreateAsync(user, request.Password);

            if (res.Succeeded)
            {
                _logger.LogInformation("UserAccount created successfully!");
                return Ok();
            }

            return BadRequest(res.Errors);
        }

        [HttpPost("/Login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login(LoginRequest request)
        {
            _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return TypedResults.Problem("Invalid email or password.", statusCode: StatusCodes.Status401Unauthorized);
            }

            _logger.LogInformation("User logged in successfully!");
            return TypedResults.Empty;
        }
    }
}
