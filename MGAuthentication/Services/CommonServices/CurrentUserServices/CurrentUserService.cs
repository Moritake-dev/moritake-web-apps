using IdentityModel;
using MGAuthentication.Data.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace MGAuthentication.Services.CommonServices.CurrentUserServices
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //var claims = _httpContextAccessor.HttpContext?.User?.Claims?.ToList();
            //this.UserId = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            //this.UserId = GetUserId();
        }

        public string GetUserId()
        {
            var user = _httpContextAccessor?.HttpContext?.User as ClaimsPrincipal;
            var userId = user?.Claims?.ElementAt(0)?.Value;
            return userId;
        }

        //public string UserId { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier); } }
        public string UserId { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Subject); } }
    }
}