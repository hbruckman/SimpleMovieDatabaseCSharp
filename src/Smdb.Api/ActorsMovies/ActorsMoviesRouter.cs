namespace Smdb.Api.ActorsMovies;

using Abcs.Http;

public class ActorsMoviesRouter : HttpRouter
{
	public ActorsMoviesRouter(ActorsMoviesController actorsmoviesController)
	{
		UseParametrizedRouteMatching();
		MapGet("/", actorsmoviesController.List);
		MapPost("/", HttpUtils.ReadRequestBodyAsText, actorsmoviesController.Create);
		MapGet("/:id", actorsmoviesController.Read);
		MapPut("/:id", HttpUtils.ReadRequestBodyAsText, actorsmoviesController.Update);
		MapDelete("/:id", actorsmoviesController.Delete);
	}
}
