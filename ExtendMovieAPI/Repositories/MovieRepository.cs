using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtendMovieAPI.Models;
using ExtendMovieAPI.Services;

namespace ExtendMovieAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _dbcontext;

        public MovieRepository(MovieContext context)
        {
            _dbcontext = context;
        }

        public async Task<MovieDetailModel> GetMovieDetail(int Id)
        {
            return await _dbcontext.MovieDetails.FindAsync(Id);
        }

        /// <summary>
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cursor"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MovieListModel>> GetMovieList(int count, long cursor, string sort)
        {
            var myType = typeof(MovieListModel);
            var info = myType.GetProperty(sort);
            var passedType = info.PropertyType;
            IEnumerable<MovieListModel> movieList;
            var cursorMovie = await _dbcontext.MovieList.FindAsync(cursor);
            if (cursorMovie == null)
                movieList = _dbcontext.MovieList
                    .OrderByDescending(q => info.GetValue(q, null))
                    .ThenBy(q => q.Title)
                    .Take(count);
            else
                movieList = _dbcontext.MovieList
                    .Where(q => q.VoteAverage <= cursorMovie.VoteAverage)
                    .OrderByDescending(q => info.GetValue(q, null))
                    .ThenBy(q => q.Title)
                    .AsEnumerable()
                    .SkipWhile(q =>
                        q.VoteAverage == cursorMovie.VoteAverage && q.Title.CompareTo(cursorMovie.Title) <= 0)
                    .Take(count);
            return movieList;
        }


        /// <summary>
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cursor"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MovieListModel>> GetMovieListAsc(int count, long cursor, string sort)
        {
            var myType = typeof(MovieListModel);
            var info = myType.GetProperty(sort);
            IEnumerable<MovieListModel> movieList;

            var cursorMovie = await _dbcontext.MovieList.FindAsync(cursor);
            if (cursorMovie == null)
                movieList = _dbcontext.MovieList
                    .OrderBy(q => info.GetValue(q, null))
                    .ThenBy(q => q.Title)
                    .Take(count);
            else
                movieList = _dbcontext.MovieList
                    .Where(q => q.VoteAverage <= cursorMovie.VoteAverage)
                    .OrderBy(q => info.GetValue(q, null))
                    .ThenBy(q => q.Title)
                    .AsEnumerable()
                    .SkipWhile(q =>
                        q.VoteAverage == cursorMovie.VoteAverage && q.Title.CompareTo(cursorMovie.Title) <= 0)
                    .Take(count);
            return movieList;
        }
    }
}