using Microsoft.AspNetCore.Identity;
using MyPizza.Infrastructure.Data.Identity;
using MyPizza.Web.Interfaces;
using System.Security.Claims;

namespace MyPizza.Web.Services
{
    public class UserService : IUserService
    {
        public Guid GetUserId(ClaimsPrincipal user)
        {
            if (user.Identity!.IsAuthenticated)
            {
                return Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            }
            return Guid.Empty;
        }
    }
}
