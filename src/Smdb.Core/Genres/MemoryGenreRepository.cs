namespace Smdb.Core.Genres;

using Abcs.Http;

public class MemoryGenreRepository : IGenreRepository
{
	private MemoryDatabase db;

	public MemoryGenreRepository(MemoryDatabase db)
	{
		this.db = db;
	}

	public async Task<PagedResult<Genre>?> ReadGenres(int page, int size)
	{
		int totalCount = db.Genres.Count;
		int start = Math.Clamp((page - 1) * size, 0, totalCount);
		int length = Math.Clamp(size, 0, totalCount - start);
		var values = db.Genres.Slice(start, length);
		var result = new PagedResult<Genre>(totalCount, values);

		return await Task.FromResult(result);
	}

	public async Task<Genre?> CreateGenre(Genre newGenre)
	{
		newGenre.Id = db.NextGenreId();
		db.Genres.Add(newGenre);

		return await Task.FromResult(newGenre);
	}

	public async Task<Genre?> ReadGenre(int id)
	{
		Genre? result = db.Genres.FirstOrDefault(m => m.Id == id);

		return await Task.FromResult(result);
	}

	public async Task<Genre?> UpdateGenre(int id, Genre newData)
	{
		Genre? result = db.Genres.FirstOrDefault(m => m.Id == id);

		if(result != null)
		{
			result.Name = newData.Name;
		}

		return await Task.FromResult(result);
	}
	
	public async Task<Genre?> DeleteGenre(int id)
	{
		Genre? result = db.Genres.FirstOrDefault(m => m.Id == id);

		if(result != null)
		{
			db.Genres.Remove(result);
		}

		return await Task.FromResult(result);
	}
}
