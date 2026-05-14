namespace Smdb.Api.Users;

using Abcs.Http;

public class UsersRouter : HttpRouter
{
	public UsersRouter(UsersController usersController)
	{
		UseParametrizedRouteMatching();
		MapGet("/", usersController.List);
		MapPost("/", HttpUtils.ReadRequestBodyAsText, usersController.Create);
		MapGet("/:id", usersController.Read);
		MapPut("/:id", HttpUtils.ReadRequestBodyAsText, usersController.Update);
		MapDelete("/:id", usersController.Delete);
	}
}
