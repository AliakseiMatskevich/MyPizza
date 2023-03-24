using System.Security.Claims;

namespace MyPizza.Web.Interfaces
{
    public interface IUserService
    {
        Guid GetUserId(ClaimsPrincipal user);
    }
}
