using System.Collections.Generic;
using System.Threading.Tasks;
using ExtendMovieAPI.Models;

namespace ExtendMovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieDetailModel> GetMovieDetail(int Id);
        Task<IEnumerable<MovieListModel>> GetMovieList(int count, long cursor, string sort);
        Task<IEnumerable<MovieListModel>> GetMovieListAsc(int count, long cursor, string sort);
    }
}