using Smdb.Core.Movies;
using Smdb.Core.Genres;
using Smdb.Core.MoviesGenres;

public class MemoryDatabase
{
	public List<Movie> Movies { get; }
	public List<Genre> Genres { get; }
	public List<MovieGenre> MoviesGenres { get; }
	private int nextMovieId;
	private int nextGenreId;
	private int nextMovieGenreId;

	public MemoryDatabase()
	{
		Movies = [];
		Genres = [];
		MoviesGenres = [];
		SeedMovies();
		SeedGenres();
		SeedMoviesGenres();
		nextMovieId = Movies.Count;
		nextGenreId = Genres.Count;
		nextMovieGenreId = MoviesGenres.Count;
	}

	private void SeedMovies()
	{
		Movies.AddRange(new Movie[]
		{
			new Movie(1, "The Godfather", 1972, "A mafia patriarch hands the family empire to his reluctant son."),
			new Movie(2, "The Godfather Part II", 1974, "Michael consolidates power as flashbacks trace Vito Corleone’s rise."),
			new Movie(3, "The Dark Knight", 2008, "Batman faces the Joker, who pushes Gotham into chaos."),
			new Movie(4, "The Shawshank Redemption", 1994, "An innocent banker forms a life-saving friendship in prison."),
			new Movie(5, "Pulp Fiction", 1994, "Interlocking LA crime stories unfold with dark humor."),
			new Movie(6, "Schindler's List", 1993, "A businessman saves Jewish workers during the Holocaust."),
			new Movie(7, "The Lord of the Rings: The Return of the King", 2003, "The final push to destroy the One Ring decides Middle-earth’s fate."),
			new Movie(8, "Fight Club", 1999, "An insomnia-plagued worker joins a charismatic anarchist’s secret club."),
			new Movie(9, "Forrest Gump", 1994, "A kind man unwittingly drifts through historic American moments."),
			new Movie(10, "Inception", 2010, "A thief enters dreams to plant an idea in a target’s mind."),
			new Movie(11, "The Matrix", 1999, "A hacker learns reality is a simulated prison for humanity."),
			new Movie(12, "Se7en", 1995, "Two detectives hunt a killer using the seven deadly sins."),
			new Movie(13, "Goodfellas", 1990, "Henry Hill’s rise and fall inside the New York mob."),
			new Movie(14, "The Silence of the Lambs", 1991, "An FBI trainee consults Hannibal Lecter to catch a serial killer."),
			new Movie(15, "Star Wars: Episode IV – A New Hope", 1977, "A farm boy joins rebels to destroy the Empire’s Death Star."),
			new Movie(16, "The Empire Strikes Back", 1980, "The Rebels scatter as Luke confronts Darth Vader."),
			new Movie(17, "Interstellar", 2014, "Astronauts travel through a wormhole to save a dying Earth."),
			new Movie(18, "Parasite", 2019, "A poor family infiltrates a wealthy household with unforeseen fallout."),
			new Movie(19, "Spirited Away", 2001, "A girl navigates a spirit bathhouse to free her parents."),
			new Movie(20, "City of God", 2002, "Two boys take diverging paths amid Rio’s gang wars."),
			new Movie(21, "Saving Private Ryan", 1998, "A squad risks everything to bring a paratrooper home."),
			new Movie(22, "The Green Mile", 1999, "Death-row guards encounter a prisoner with miraculous gifts."),
			new Movie(23, "Gladiator", 2000, "A betrayed general becomes Rome’s fiercest arena fighter."),
			new Movie(24, "The Lion King", 1994, "An exiled lion cub returns to claim his destiny."),
			new Movie(25, "Back to the Future", 1985, "A teen time-travels and risks erasing his own existence."),
			new Movie(26, "The Departed", 2006, "An infiltrator and a mole play cat-and-mouse in Boston."),
			new Movie(27, "Whiplash", 2014, "A jazz drummer endures a brutal mentor in pursuit of greatness."),
			new Movie(28, "The Prestige", 2006, "Rival magicians wage a dangerous war of one-upmanship."),
			new Movie(29, "The Usual Suspects", 1995, "A survivors’ tale hints at the legend of Keyser Söze."),
			new Movie(30, "Terminator 2: Judgment Day", 1991, "A reprogrammed cyborg protects the future leader of humanity."),
			new Movie(31, "Alien", 1979, "A crew is stalked by a lethal lifeform aboard a spaceship."),
			new Movie(32, "Aliens", 1986, "Ripley returns to face a hive of xenomorphs on LV-426."),
			new Movie(33, "Blade Runner", 1982, "A detective hunts rogue androids in a neon-soaked future."),
			new Movie(34, "Apocalypse Now", 1979, "A captain journeys upriver to terminate a renegade officer."),
			new Movie(35, "One Flew Over the Cuckoo's Nest", 1975, "A rebel patient challenges a tyrannical nurse in a psych ward."),
			new Movie(36, "Taxi Driver", 1976, "A disturbed NYC cabbie spirals toward violence."),
			new Movie(37, "Oldboy", 2003, "A man seeks answers after 15 years of inexplicable captivity."),
			new Movie(38, "Amélie", 2001, "A shy Parisian decides to secretly improve others’ lives."),
			new Movie(39, "The Pianist", 2002, "A Jewish pianist struggles to survive Warsaw’s ghetto."),
			new Movie(40, "American Beauty", 1999, "A suburban man’s midlife crisis upends his family."),
			new Movie(41, "No Country for Old Men", 2007, "A stolen briefcase triggers relentless pursuit across Texas."),
			new Movie(42, "There Will Be Blood", 2007, "An oilman’s ambition consumes everything around him."),
			new Movie(43, "Mad Max: Fury Road", 2015, "A desert chase pits a warlord against a defiant road warrior."),
			new Movie(44, "La La Land", 2016, "A musician and an actress chase dreams in modern LA."),
			new Movie(45, "Joker", 2019, "A marginalized comedian’s breakdown sparks violent unrest."),
			new Movie(46, "Avengers: Infinity War", 2018, "Earth’s heroes battle Thanos for the fate of half the universe."),
			new Movie(47, "Avengers: Endgame", 2019, "Survivors attempt a time-heist to undo cosmic devastation."),
			new Movie(48, "Toy Story", 1995, "Rivalry between a cowboy doll and a space ranger turns to friendship."),
			new Movie(49, "Inside Out", 2015, "A girl’s emotions guide her through a difficult move."),
			new Movie(50, "The Social Network", 2010, "Facebook’s founding sparks friendship and legal battles.")
		});
	}

	private void SeedGenres()
	{
		Genres.AddRange(new Genre[]
		{
			new Genre(1, "Action"),
			new Genre(2, "Adventure"),
			new Genre(3, "Animation"),
			new Genre(4, "Biography"),
			new Genre(5, "Comedy"),
			new Genre(6, "Crime"),
			new Genre(7, "Drama"),
			new Genre(8, "Family"),
			new Genre(9, "Fantasy"),
			new Genre(10, "History"),
			new Genre(11, "Horror"),
			new Genre(12, "Music"),
			new Genre(13, "Mystery"),
			new Genre(14, "Romance"),
			new Genre(15, "Sci-Fi"),
			new Genre(16, "Thriller"),
			new Genre(17, "War")
		});
	}

	private void SeedMoviesGenres()
	{
		MoviesGenres.AddRange(new MovieGenre[]
		{
			new MovieGenre(1, 6),  new MovieGenre(1, 7),
			new MovieGenre(2, 6),  new MovieGenre(2, 7),
			new MovieGenre(3, 1),  new MovieGenre(3, 6),  new MovieGenre(3, 7),
			new MovieGenre(4, 7),
			new MovieGenre(5, 6),  new MovieGenre(5, 7),
			new MovieGenre(6, 4),  new MovieGenre(6, 7),  new MovieGenre(6, 10),
			new MovieGenre(7, 2),  new MovieGenre(7, 9),  new MovieGenre(7, 7),
			new MovieGenre(8, 7),
			new MovieGenre(9, 7),  new MovieGenre(9, 14),
			new MovieGenre(10, 1), new MovieGenre(10, 15), new MovieGenre(10, 16),
			new MovieGenre(11, 1), new MovieGenre(11, 15),
			new MovieGenre(12, 6), new MovieGenre(12, 7), new MovieGenre(12, 16),
			new MovieGenre(13, 4), new MovieGenre(13, 6), new MovieGenre(13, 7),
			new MovieGenre(14, 6), new MovieGenre(14, 7), new MovieGenre(14, 16),
			new MovieGenre(15, 1), new MovieGenre(15, 2), new MovieGenre(15, 15),
			new MovieGenre(16, 1), new MovieGenre(16, 2), new MovieGenre(16, 9),
			new MovieGenre(17, 2), new MovieGenre(17, 7), new MovieGenre(17, 15),
			new MovieGenre(18, 5), new MovieGenre(18, 7), new MovieGenre(18, 16),
			new MovieGenre(19, 3), new MovieGenre(19, 2), new MovieGenre(19, 8),
			new MovieGenre(20, 6), new MovieGenre(20, 7),
			new MovieGenre(21, 7), new MovieGenre(21, 17),
			new MovieGenre(22, 6), new MovieGenre(22, 7), new MovieGenre(22, 9),
			new MovieGenre(23, 1), new MovieGenre(23, 2), new MovieGenre(23, 7),
			new MovieGenre(24, 3), new MovieGenre(24, 2), new MovieGenre(24, 7),
			new MovieGenre(25, 2), new MovieGenre(25, 5), new MovieGenre(25, 15),
			new MovieGenre(26, 6), new MovieGenre(26, 7), new MovieGenre(26, 16),
			new MovieGenre(27, 7), new MovieGenre(27, 12),
			new MovieGenre(28, 7), new MovieGenre(28, 13), new MovieGenre(28, 15),
			new MovieGenre(29, 6), new MovieGenre(29, 13), new MovieGenre(29, 16),
			new MovieGenre(30, 1), new MovieGenre(30, 15),
			new MovieGenre(31, 11), new MovieGenre(31, 15),
			new MovieGenre(32, 1), new MovieGenre(32, 15), new MovieGenre(32, 16),
			new MovieGenre(33, 15), new MovieGenre(33, 16),
			new MovieGenre(34, 7), new MovieGenre(34, 17),
			new MovieGenre(35, 7),
			new MovieGenre(36, 6), new MovieGenre(36, 7),
			new MovieGenre(37, 1), new MovieGenre(37, 7), new MovieGenre(37, 13),
			new MovieGenre(38, 5), new MovieGenre(38, 14),
			new MovieGenre(39, 4), new MovieGenre(39, 7), new MovieGenre(39, 12),
			new MovieGenre(40, 7),
			new MovieGenre(41, 6), new MovieGenre(41, 7), new MovieGenre(41, 16),
			new MovieGenre(42, 7),
			new MovieGenre(43, 1), new MovieGenre(43, 2), new MovieGenre(43, 15),
			new MovieGenre(44, 5), new MovieGenre(44, 7), new MovieGenre(44, 12),
			new MovieGenre(45, 6), new MovieGenre(45, 7), new MovieGenre(45, 16),
			new MovieGenre(46, 1), new MovieGenre(46, 2), new MovieGenre(46, 15),
			new MovieGenre(47, 1), new MovieGenre(47, 2), new MovieGenre(47, 15),
			new MovieGenre(48, 3), new MovieGenre(48, 2), new MovieGenre(48, 5),
			new MovieGenre(49, 3), new MovieGenre(49, 2), new MovieGenre(49, 5),
			new MovieGenre(50, 4), new MovieGenre(50, 7)
		});
	}

	public int NextMovieId()
	{
		return ++nextMovieId;
	}

	public int NextGenreId()
	{
		return ++nextGenreId;
	}

	public int NextMovieGenreId()
	{
		return ++nextMovieGenreId;
	}
}
