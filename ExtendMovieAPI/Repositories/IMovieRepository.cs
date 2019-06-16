using ExtendMovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExtendMovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieDetailModel> GetMovieDetail(int Id);
        Task<IEnumerable<MovieListModel>> GetMovieList(int count , long cursor , string sort  );
        Task<IEnumerable<MovieListModel>> GetMovieListAsc(int count, long cursor, string sort);
    }
}
