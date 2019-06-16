using System;
using System.Collections.Generic;
using ExtendMovieAPI.Models;
using ExtendMovieAPI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;

namespace ExtendMovieAPI
{
    public class DataGenerator //: IStartupFilter
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            GetMovieDataFromApi(serviceProvider);
        }

        public static async void GetMovieDataFromApi(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
            {
                var baseEnd = new Uri("https://api.themoviedb.org/3/");
                var queryDict = new Dictionary<string, string>
                {
                    {"api_key", Environment.GetEnvironmentVariable("MOVIE_API_KEY")},
                    {"language", "en-US"},
                    {"primary_release_year", ""}, //DateTime.Now.Year.ToString()                
                    {"sort_by", "vote_average.desc"},
                    {"include_adult", "false"},
                    {"include_video", "false"},
                    {"vote_count.gte", "300"},
                    {"page", "1"}
                };
                for (var i = 0; i > -5; i--)
                {
                    var movieYear = DateTime.Now.AddYears(i).Year.ToString();
                    queryDict["primary_release_year"] = movieYear;
                    var url = BuildQueryString(@"https://api.themoviedb.org/3/discover/movie", queryDict);
                    var client = new RestClient(url);
                    var response = client.Execute(new RestRequest());
                    var curMovieList = JsonConvert.DeserializeObject<MovieListHeaderModel>(response.Content);

                    foreach (var curMovie in curMovieList.MovieList) context.MovieList.Add(curMovie);
                }

                await context.SaveChangesAsync();

                foreach (var curMovie in context.MovieList)
                {
                    var url = BuildQueryString(@"https://api.themoviedb.org/3/movie/" + curMovie.Id, queryDict);
                    var client = new RestClient("https://api.themoviedb.org/3/");
                    var request = new RestRequest("movie/{id}", Method.GET);
                    request.AddUrlSegment("id", curMovie.Id);
                    request.AddQueryParameter("api_key", Environment.GetEnvironmentVariable("MOVIE_API_KEY"));
                    request.AddQueryParameter("language", "en-US");

                    var response = client.Execute(request);
                    var movieDetails = JsonConvert.DeserializeObject<MovieDetailModel>(response.Content);

                    context.MovieDetails.Add(movieDetails);
                }

                await context.SaveChangesAsync();
            }
        }

        private static string BuildQueryString(string baseUrl, Dictionary<string, string> queryKvp)
        {
            return QueryHelpers.AddQueryString(baseUrl, queryKvp);
        }
    }
}