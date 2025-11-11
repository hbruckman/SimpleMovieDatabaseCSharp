using System;
using System.Net;
using Abcs.Http;

namespace Smdb.Core.Movies;

public class DefaultMovieService : IMovieService
{
	private readonly IMovieRepository movieRepository;

	public DefaultMovieService(IMovieRepository movieRepository)
	{
		this.movieRepository = movieRepository;
	}

	public async Task<Result<PagedResult<Movie>>> ReadMovies(int page, int size)
	{
		if(page <= 0) { new Result<PagedResult<Movie>>(new Exception("Page must be >= 1."), (int) HttpStatusCode.BadRequest); }
		if(size <= 0) { new Result<PagedResult<Movie>>(new Exception("Page size must be >= 1."), (int) HttpStatusCode.BadRequest); }

		var pagedResult = await movieRepository.ReadMovies(page, size);
		var result = pagedResult == null
			? new Result<PagedResult<Movie>>(new Exception($"Could not read movies from page {page} and size {size}."), (int) HttpStatusCode.NotFound)
			: new Result<PagedResult<Movie>>(pagedResult, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Movie>> CreateMovie(Movie newMovie)
	{
		var validationError = ValidateMovie(newMovie);

		if(validationError != null)
		{
			return new Result<Movie>(validationError, (int) HttpStatusCode.BadRequest);
		}

		var movie = await movieRepository.CreateMovie(newMovie);
		var result = movie == null
			? new Result<Movie>(new Exception($"Could not create movie {newMovie}."), (int) HttpStatusCode.NotFound)
			: new Result<Movie>(movie, (int) HttpStatusCode.Created);

		return result;
	}

	public async Task<Result<Movie>> ReadMovie(int id)
	{
		var movie = await movieRepository.ReadMovie(id);
		var result = movie == null
			? new Result<Movie>(new Exception($"Could not read movie with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Movie>(movie, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Movie>> UpdateMovie(int id, Movie newData)
	{
		var validationError = ValidateMovie(newData);

		if(validationError != null)
		{
			return new Result<Movie>(validationError, (int) HttpStatusCode.BadRequest);
		}

		var movie = await movieRepository.UpdateMovie(id, newData);
		var result = movie == null
			? new Result<Movie>(new Exception($"Could not update movie {newData} with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Movie>(movie, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Movie>> DeleteMovie(int id)
	{
		var movie = await movieRepository.DeleteMovie(id);
		var result = movie == null
			? new Result<Movie>(new Exception($"Could not delete movie with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Movie>(movie, (int) HttpStatusCode.OK);

		return result;
	}

	private static Exception? ValidateMovie(Movie? movieData)
	{
		if(movieData is null)
		{
			return new Exception("Movie payload is required.");
		}

		if(string.IsNullOrWhiteSpace(movieData.Title))
		{
			return new Exception("Title is required and cannot be empty.");
		}

		if(movieData.Title.Length > 256)
		{
			return new Exception("Title cannot be longer than 256 characters.");
		}

		if(movieData.Year < 1888 || movieData.Year > DateTime.UtcNow.Year)
		{
			return new Exception($"Year must be between 1888 and {DateTime.UtcNow.Year}.");
		}

		return null;
	}
}

