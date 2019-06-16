using System.Linq;
using System.Threading.Tasks;
using ExtendMovieAPI.Models;
using ExtendMovieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExtendMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepo = movieRepository;
        }

        // GET api/movies
        /// <summary>
        ///     Method returns a truncated list of the movie database depending on specific parameters.
        /// </summary>
        /// <param name="count">Total number of movies to be returned.  If the </param>
        /// <param name="cursor">The id of the movie that represents our cursor</param>
        /// <param name="sort">The property name as a string of the property we wish to sort by.  Defaults to VoteAverage</param>
        /// <param name="sortDesc">Direction of the sort.  Defaults to descending</param>
        /// <returns>A list of sorted movies with limited data.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int count = 10, long cursor = -1, string sort = "VoteAverage",
            bool sortDesc = true)
        {
            var myType = typeof(MovieListModel);
            var info = myType.GetProperty(sort);
            if (info == null) return BadRequest();
            var model = new MovieListResponseModel();

            if (sortDesc)
                model.MiniMovieList = await _movieRepo.GetMovieList(count, cursor, sort);
            else
                model.MiniMovieList = await _movieRepo.GetMovieListAsc(count, cursor, sort);

            //the user sorted and requested the last movie in the database, no movies are left
            if (!model.MiniMovieList.Any())
                return NotFound(cursor);
            //something terrible has happened
            if (model.MiniMovieList == null) return BadRequest();
            model.Cursor = model.MiniMovieList.Last().Id;
            return Ok(model);
        }

        // GET api/movies/5
        /// <summary>
        ///     Method searches and returns entire movie details by its id.
        /// </summary>
        /// <param name="id">int value representing the movie Id</param>
        /// <returns>MovieDetail</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = new MovieDetailResponseModel
            {
                MovieDetail = await _movieRepo.GetMovieDetail(id)
            };
            if (model.MovieDetail == null) return NotFound(id);
            return Ok(model);
        }
    }
}