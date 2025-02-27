using System.Security.Claims;

namespace E_Commerce.middleware
{
    public class Customiddleware
    {

        private readonly RequestDelegate _next;
        private object context;

        public Customiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim != null)
                {

                    context.Items["UserId"] = idClaim.Value;
                }
            }
            await _next(context);
        }
    }
}
