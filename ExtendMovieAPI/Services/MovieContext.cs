using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtendMovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtendMovieAPI.Services
{
    public class MovieContext : DbContext
    {
        public DbSet<MovieListModel> MovieList { get; set; }
        public DbSet<MovieDetailModel> MovieDetails { get; set; }
        public DbSet<MovieHeaderModel> MovieHeaders { get; set; }
        
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("ExtendDB");
        //}
    }
}
