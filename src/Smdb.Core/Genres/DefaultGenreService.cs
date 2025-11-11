using System;
using System.Net;
using Abcs.Http;

namespace Smdb.Core.Genres;

public class DefaultGenreService : IGenreService
{
	private readonly IGenreRepository genreRepository;

	public DefaultGenreService(IGenreRepository genreRepository)
	{
		this.genreRepository = genreRepository;
	}

	public async Task<Result<PagedResult<Genre>>> ReadGenres(int page, int size)
	{
		if(page <= 0) { new Result<PagedResult<Genre>>(new Exception("Page must be >= 1."), (int) HttpStatusCode.BadRequest); }
		if(size <= 0) { new Result<PagedResult<Genre>>(new Exception("Page size must be >= 1."), (int) HttpStatusCode.BadRequest); }

		var pagedResult = await genreRepository.ReadGenres(page, size);
		var result = pagedResult == null
			? new Result<PagedResult<Genre>>(new Exception($"Could not read genres from page {page} and size {size}."), (int) HttpStatusCode.NotFound)
			: new Result<PagedResult<Genre>>(pagedResult, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Genre>> CreateGenre(Genre newGenre)
	{
		var validationError = ValidateGenre(newGenre);

		if(validationError != null)
		{
			return new Result<Genre>(validationError, (int) HttpStatusCode.BadRequest);
		}

		var genre = await genreRepository.CreateGenre(newGenre);
		var result = genre == null
			? new Result<Genre>(new Exception($"Could not create genre {newGenre}."), (int) HttpStatusCode.NotFound)
			: new Result<Genre>(genre, (int) HttpStatusCode.Created);

		return result;
	}

	public async Task<Result<Genre>> ReadGenre(int id)
	{
		var genre = await genreRepository.ReadGenre(id);
		var result = genre == null
			? new Result<Genre>(new Exception($"Could not read genre with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Genre>(genre, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Genre>> UpdateGenre(int id, Genre newData)
	{
		var validationError = ValidateGenre(newData);

		if(validationError != null)
		{
			return new Result<Genre>(validationError, (int) HttpStatusCode.BadRequest);
		}

		var genre = await genreRepository.UpdateGenre(id, newData);
		var result = genre == null
			? new Result<Genre>(new Exception($"Could not update genre {newData} with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Genre>(genre, (int) HttpStatusCode.OK);

		return result;
	}

	public async Task<Result<Genre>> DeleteGenre(int id)
	{
		var genre = await genreRepository.DeleteGenre(id);
		var result = genre == null
			? new Result<Genre>(new Exception($"Could not delete genre with id {id}."), (int) HttpStatusCode.NotFound)
			: new Result<Genre>(genre, (int) HttpStatusCode.OK);

		return result;
	}

	private static Exception? ValidateGenre(Genre? genreData)
	{
		if(genreData is null)
		{
			return new Exception("Genre payload is required.");
		}

		if(string.IsNullOrWhiteSpace(genreData.Name))
		{
			return new Exception("Name is required and cannot be empty.");
		}

		if(genreData.Name.Length > 256)
		{
			return new Exception("Name cannot be longer than 256 characters.");
		}

		return null;
	}
}

