namespace Smdb.Core.Genres;

using Abcs.Http;

public interface IGenreRepository
{
	public Task<PagedResult<Genre>?> ReadGenres(int page, int size);
	public Task<Genre?> CreateGenre(Genre newGenre);
	public Task<Genre?> ReadGenre(int id);
	public Task<Genre?> UpdateGenre(int id, Genre newData);
	public Task<Genre?> DeleteGenre(int id);
}
