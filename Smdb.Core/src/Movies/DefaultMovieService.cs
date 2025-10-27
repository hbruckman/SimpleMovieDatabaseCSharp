using System.Net;
using Smdb.Core.Shared;

namespace Smdb.Core.Movies;

public class DefaultMovieService : IMovieService
{
	private IMovieRepository movieRepository;

	public DefaultMovieService(IMovieRepository movieRepository)
	{
		this.movieRepository = movieRepository;
	}

	public async Task<Result<PagedResult<Movie>>> ReadMovies(int page, int size)
	{
		try
		{
			var pagedResult = await movieRepository.ReadMovies(page, size);
			var result = pagedResult == null ?
				new Result<PagedResult<Movie>>(new Exception("Could not read movies."), (int) HttpStatusCode.NotFound) :
				new Result<PagedResult<Movie>>(pagedResult);

			return result;
		}
		catch(Exception e)
		{
			return new Result<PagedResult<Movie>>(e, (int) HttpStatusCode.InternalServerError);
		}
	}

	public async Task<Result<Movie>> CreateMovie(Movie newMovie)
	{
		try
		{
			var movie = await movieRepository.CreateMovie(newMovie);
			var result = movie == null ?
				new Result<Movie>(new Exception("Could not create movie."), (int) HttpStatusCode.NotFound) :
				new Result<Movie>(movie);

			return result;
		}
		catch(Exception e)
		{
			return new Result<Movie>(e, (int) HttpStatusCode.InternalServerError);
		}
	}

	public async Task<Result<Movie>> ReadMovie(int id)
	{
		try
		{
			var movie = await movieRepository.ReadMovie(id);
			var result = movie == null ?
				new Result<Movie>(new Exception("Could not read movie."), (int) HttpStatusCode.NotFound) :
				new Result<Movie>(movie);

			return result;
		}
		catch(Exception e)
		{
			return new Result<Movie>(e, (int) HttpStatusCode.InternalServerError);
		}
	}

	public async Task<Result<Movie>> UpdateMovie(int id, Movie newData)
	{
		try
		{
			var movie = await movieRepository.UpdateMovie(id, newData);
			var result = movie == null ?
				new Result<Movie>(new Exception("Could not update movie."), (int) HttpStatusCode.NotFound) :
				new Result<Movie>(movie);

			return result;
		}
		catch(Exception e)
		{
			return new Result<Movie>(e, (int) HttpStatusCode.InternalServerError);
		}
	}

	public async Task<Result<Movie>> DeleteMovie(int id)
	{
		try
		{
			var movie = await movieRepository.DeleteMovie(id);
			var result = movie == null ?
				new Result<Movie>(new Exception("Could not delete movie."), (int) HttpStatusCode.NotFound) :
				new Result<Movie>(movie);

			return result;
		}
		catch(Exception e)
		{
			return new Result<Movie>(e, (int) HttpStatusCode.InternalServerError);
		}
	}
}
