namespace Smdb.Api;

using Abcs.Config;
using Abcs.Http;
using Smdb.Api.Movies;
using Smdb.Api.Genres;
using Smdb.Core.Movies;
using Smdb.Core.Genres;
using System.Net;

public class App
{
	private HttpRouter router;
	private HttpListener server;

	public App()
	{
		var db = new MemoryDatabase();

		var movieRepo = new MemoryMovieRepository(db);
		var movieServ = new DefaultMovieService(movieRepo);
		var movieCtrl = new MoviesController(movieServ);
		var movieRouter = new MoviesRouter(movieCtrl);

		var genreRepo = new MemoryGenreRepository(db);
		var genreServ = new DefaultGenreService(genreRepo);
		var genreCtrl = new GenresController(genreServ);
		var genreRouter = new GenresRouter(genreCtrl);

		var apiRouter = new HttpRouter();
		
		router = new HttpRouter();
		router.Use(HttpUtils.StructuredLogging);
		router.Use(HttpUtils.CentralizedErrorHandling);
		router.UseDefaultResponse();
		router.Use(HttpUtils.AddResponseCorsHeaders);
		router.Use(HttpUtils.ParseRequestUrl);
		router.Use(HttpUtils.ParseRequestQueryString);
		router.UseParametrizedRouteMatching();
		router.UseRouter("/api/v1", apiRouter);
		apiRouter.UseRouter("/movies", movieRouter);
		apiRouter.UseRouter("/genres", genreRouter);

		string host = Configuration.Get<string>("HOST", "http://127.0.0.1");
		string port = Configuration.Get<string>("PORT", "5000");
		string authority = $"{host}:{port}/";

		server = new HttpListener();
		server.Prefixes.Add(authority);

		Console.WriteLine("Server started at " + authority);
	}

	public async Task Start()
	{
		server.Start();

		while(server.IsListening)
		{
			HttpListenerContext ctx = await server.GetContextAsync();

			_ = router.HandleContextAsync(ctx);
		}
	}

	public void Stop()
	{
		if(server.IsListening)
		{
			server.Stop();
			server.Close();
			Console.WriteLine("Server stopped.");
		}
	}
}
