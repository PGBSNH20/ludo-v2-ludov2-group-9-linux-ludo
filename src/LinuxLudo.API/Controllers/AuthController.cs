using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LinuxLudo.API.Domain.Resources.Auth;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
        {
            var test = new ErrorResponse("Auth failed", 400, null).Respond();
            return Conflict(test);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
        {
            throw new NotImplementedException();
        }
    }
}