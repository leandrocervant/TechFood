using System.Linq;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.Claims?.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null)
            {
                return Convert.ToInt32(userIdClaim.Value);
            }

            throw new Exception("The userId is not present in claims.");
        }
    }
}
