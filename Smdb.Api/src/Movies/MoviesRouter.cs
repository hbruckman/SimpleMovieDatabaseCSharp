using Abcs.Http;

namespace Smdb.Api.Movies;

public class MoviesRouter : HttpRouter
{
	public MoviesRouter(MoviesController moviesController)
	{
		UseRouteMatching();
		MapGet("/", moviesController.ReadMovies);
		MapPost("/", HttpUtils.ReadRequestBodyAsText, moviesController.CreateMovie);
		MapGet("/:id", moviesController.ReadMovie);
		MapPut("/:id", HttpUtils.ReadRequestBodyAsText, moviesController.UpdateMovie);
		MapDelete("/:id", moviesController.DeleteMovie);
	}
}
