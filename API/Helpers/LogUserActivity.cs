namespace API.Helpers;

//Action Filters is used! for updating something! 
//whether it's before the request or after it
public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserById();
        var userRepository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
        var user = await userRepository.GetUserByIdAsync(userId);
        user.LastActive = DateTime.UtcNow;
        await userRepository.SaveAllChangesAsync();
    }
}
