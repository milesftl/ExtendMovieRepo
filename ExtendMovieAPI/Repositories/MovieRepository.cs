using ExtendMovieAPI.Models;
using ExtendMovieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExtendMovieAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MovieContext _dbcontext;

        public MovieRepository(MovieContext context)
        {
            _dbcontext = context;
        }

        public async Task<MovieDetailModel> GetMovieDetail(int Id)
        {
            return await _dbcontext.MovieDetails.FindAsync(Id);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cursor"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MovieListModel>> GetMovieList(int count, long cursor, string sort)
        {
            Type myType = typeof(MovieListModel);
            PropertyInfo info = myType.GetProperty(sort);
            Type passedType = info.PropertyType;
            IEnumerable<MovieListModel> movieList;
            MovieListModel cursormovie = await _dbcontext.MovieList.FindAsync(cursor);
            if (cursormovie == null)
            {
                movieList = _dbcontext.MovieList
                     .OrderByDescending(q => info.GetValue(q, null))
                     .ThenBy(q => q.Title)
                     .Take(count);
            }
            else
            {
                movieList = _dbcontext.MovieList
                     .Where(q => q.VoteAverage <= cursormovie.VoteAverage)
                     .OrderByDescending(q => info.GetValue(q, null))
                     .ThenBy(q => q.Title)
                     .AsEnumerable()
                     .SkipWhile(q => q.VoteAverage == cursormovie.VoteAverage && q.Title.CompareTo(cursormovie.Title) <= 0)
                     .Take(count);
            }
            return movieList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cursor"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MovieListModel>> GetMovieListAsc(int count, long cursor, string sort)
        {
            Type myType = typeof(MovieListModel);
            PropertyInfo info = myType.GetProperty(sort);
            IEnumerable<MovieListModel> movieList;

            MovieListModel cursormovie = await _dbcontext.MovieList.FindAsync(cursor);
            if (cursormovie == null)
            {
                movieList = _dbcontext.MovieList
                     .OrderBy(q => info.GetValue(q, null))
                     .ThenBy(q => q.Title)
                     .Take(count);
            }
            else
            {
                movieList = _dbcontext.MovieList
                     .Where(q => q.VoteAverage <= cursormovie.VoteAverage)
                     .OrderBy(q => info.GetValue(q, null))
                     .ThenBy(q => q.Title)
                     .AsEnumerable()
                     .SkipWhile(q => q.VoteAverage == cursormovie.VoteAverage && q.Title.CompareTo(cursormovie.Title) <= 0)
                     .Take(count);
            }
            return movieList;
        }
    }
}
