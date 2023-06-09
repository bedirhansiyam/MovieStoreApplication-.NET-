using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateMovieModel Model;

    public CreateMovieCommand(IMapper mapper, IMovieStoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var movie = _dbContext.Movies.SingleOrDefault(x => x.MovieName == Model.MovieName);
        if(movie is not null)
            throw new InvalidOperationException("The movie already exist.");

        movie = _mapper.Map<Movie>(Model);        

        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();

        for(int i = 0; i<Model.ActorIds.Count; i++)
        {
            MovieActor newActor = new MovieActor();
            newActor.ActorId = Model.ActorIds[i];
            newActor.MovieId = movie.Id;
            _dbContext.MovieActors.Add(newActor);
            _dbContext.SaveChanges();
        }
    }

    public class CreateMovieModel
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }
        public List<int> ActorIds { get; set; }
    }
}