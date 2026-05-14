namespace Smdb.Api.Actors;

using Abcs.Http;

public class ActorsRouter : HttpRouter
{
	public ActorsRouter(ActorsController actorsController)
	{
		UseParametrizedRouteMatching();
		MapGet("/", actorsController.List);
		MapPost("/", HttpUtils.ReadRequestBodyAsText, actorsController.Create);
		MapGet("/:id", actorsController.Read);
		MapPut("/:id", HttpUtils.ReadRequestBodyAsText, actorsController.Update);
		MapDelete("/:id", actorsController.Delete);
	}
}
