using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LinuxLudo.API.Domain.Models.Auth;
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
            if (!ModelState.IsValid)
            {
                return Ok(new ErrorResponse(ModelState.Values.First().Errors.First().ErrorMessage, 500, null));
            }

            var user = _mapper.Map<SignUpResource, User>(resource);

            var res = await _authService.SingUpAsync(user, resource.Password);

            if (res.Status == "Error")
                return BadRequest(res);

            return Created(string.Empty, res);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
        {
            if (!ModelState.IsValid)
                return Json(new ErrorResponse(ModelState.Values.First().Errors.First().ErrorMessage, 500, null));

            var user = _mapper.Map<SignInResource, User>(resource);

            var res = await _authService.SignInAsync(user, resource.Password);

            if (res.Status == "Error")
                return BadRequest(res);

            return Ok(res);
        }
    }
}