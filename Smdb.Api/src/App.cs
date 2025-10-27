namespace Smdb.Api;

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

		router = new HttpRouter();
		router.Use(HttpUtils.CentralizedErrorHandling);
		router.UseRouteMatching();
		router.UseRouter("/movies", mRouter);

		string host = "http://localhost:8080/";
		server = new HttpListener();
		server.Prefixes.Add(host);
		Console.WriteLine("Server started at " + host + "movies");
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
