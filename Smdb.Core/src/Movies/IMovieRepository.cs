using Smdb.Core.Shared;

namespace Smdb.Core.Movies;

public interface IMovieRepository
{
	public Task<PagedResult<Movie>?> ReadMovies(int page, int size);
	public Task<Movie?> CreateMovie(Movie newMovie);
	public Task<Movie?> ReadMovie(int id);
	public Task<Movie?> UpdateMovie(int id, Movie newData);
	public Task<Movie?> DeleteMovie(int id);
}
