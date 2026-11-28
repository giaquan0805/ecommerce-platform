using ecommerce_platform.Domain.Entities;
using ecommerce_platform.Features.Auth.Models;

namespace ecommerce_platform.Features.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        JwtTokenResult GenerateToken( ApplicationUser user, IList<string> roles);
    }
}