namespace Smdb.Core.Genres;

using Abcs.Http;

public interface IGenreService
{
	public Task<Result<PagedResult<Genre>>> ReadGenres(int page, int size);
	public Task<Result<Genre>> CreateGenre(Genre genre);
	public Task<Result<Genre>> ReadGenre(int id);
	public Task<Result<Genre>> UpdateGenre(int id, Genre newData);
	public Task<Result<Genre>> DeleteGenre(int id);
}
