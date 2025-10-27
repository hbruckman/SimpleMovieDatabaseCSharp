using System.Collections;
using System.Net;
using System.Text.Json;
using Abcs.Http;
using Smdb.Core;
using Smdb.Core.Movies;
using Smdb.Core.Shared;

namespace Smdb.Api.Movies;

public class MoviesController
{
	private IMovieService movieService;

	public MoviesController(IMovieService movieService)
	{
		this.movieService = movieService;
	}

	// curl -X GET "http://localhost:8080/movies?page=1&size=10"
	public async Task ReadMovies(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
	{
		int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
		int size = int.TryParse(req.QueryString["size"], out int s) ? s : int.MaxValue;

		var result = await movieService.ReadMovies(page, size);

		await PrepareResponse(req, res, props, result);

		await next();
	}

	// curl -X POST "http://localhost:8080/movies" -H "Content-Type: application/json" -d "{ \"id\": -1, \"title\": \"Inception\", \"year\": 2010, \"genre\": \"Science Fiction\", \"description\": \"A skilled thief who enters dreams to steal secrets.\", \"rating\": 9 }"
	public async Task CreateMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
	{
		var text = (string) props["req.text"]!;
		var movie = JsonSerializer.Deserialize<Movie>(text, JsonUtils.DefaultOptions);
		var result = await movieService.CreateMovie(movie!);

		await PrepareResponse(req, res, props, result);

		await next();
	}

	// curl -X GET "http://localhost:8080/movies/1"
	public async Task ReadMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
	{
		var uParams = (Hashtable) props["urlParams"]!;
		int id = int.TryParse((string) uParams["id"]!, out int i) ? i : -1;

		var result = await movieService.ReadMovie(id);

		await PrepareResponse(req, res, props, result);

		await next();
	}

	// curl -X PUT "http://localhost:8080/movies/1" -H "Content-Type: application/json" -d "{ \"title\": \"Joker 2\", \"year\": 2020, \"genre\": \"Crime\", \"description\": \"A man that is a joke.\", \"rating\": 7.9 }"
	public async Task UpdateMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
	{
		var uParams = (Hashtable) props["urlParams"]!;
		int id = int.TryParse((string) uParams["id"]!, out int i) ? i : -1;
		var text = (string) props["req.text"]!;
		var movie = JsonSerializer.Deserialize<Movie>(text, JsonUtils.DefaultOptions);
		var result = await movieService.UpdateMovie(id, movie!);

		await PrepareResponse(req, res, props, result);

		await next();
	}

	// curl -X DELETE http://localhost:8080/movies/1
	public async Task DeleteMovie(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
	{
		var uParams = (Hashtable) props["urlParams"]!;
		int id = int.TryParse((string) uParams["id"]!, out int i) ? i : -1;

		var result = await movieService.DeleteMovie(id);

		await PrepareResponse(req, res, props, result);

		await next();
	}

	private async Task PrepareResponse<T>(HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Result<T> result)
	{
		if(result.IsError)
		{
			await HttpUtils.SendResponse(req, res, props, result.StatusCode, JsonSerializer.Serialize(result.Error!, JsonUtils.DefaultOptions));
			return;
		}

		await HttpUtils.SendResponse(req, res, props, result.StatusCode, JsonSerializer.Serialize(result.Payload!, JsonUtils.DefaultOptions));
	}
}
