namespace Smdb.Api;

using Abcs.Config;
using Abcs.Http;
using Smdb.Api.Movies;
using Smdb.Core.Movies;
using System.Net;

public class App
{
	private HttpRouter router;
	private HttpListener server;

	public App()
	{
		var db = new MemoryDatabase();
		var mrepo = new MemoryMovieRepository(db);
		var mserv = new DefaultMovieService(mrepo);
		var mctrl = new MoviesController(mserv);
		var mRouter = new MoviesRouter(mctrl);
		var apiRouter = new HttpRouter();
		
		router = new HttpRouter();
		router.Use(HttpUtils.CentralizedErrorHandling);
		router.Use(HttpUtils.StructuredLogging);
		router.Use(HttpUtils.ParseRequestUrl);
		router.Use(HttpUtils.ParseRequestQueryString);
		router.UseDefaultResponse();
		router.UseParametrizedRouteMatching();
		router.UseRouter("/api/v1", apiRouter);
		apiRouter.UseRouter("/movies", mRouter);

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
