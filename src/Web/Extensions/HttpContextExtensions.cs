namespace Web.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserIdFromClaims(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(claim => claim.Type == Env.Identity.Claims.Id)?.Value;
    }
}