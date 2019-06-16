using ExtendMovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtendMovieAPI.Services
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<MovieListModel> MovieList { get; set; }
        public DbSet<MovieDetailModel> MovieDetails { get; set; }
        public DbSet<MovieHeaderModel> MovieHeaders { get; set; }
    }
}