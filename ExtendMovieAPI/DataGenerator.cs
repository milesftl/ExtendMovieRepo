using ExtendMovieAPI.Models;
using ExtendMovieAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExtendMovieAPI
{
    public class DataGenerator //: IStartupFilter
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {         
            GetMovieDataFromAPI(serviceProvider);
        }
        private Uri BaseEnd { get; set; }
        public async static void GetMovieDataFromAPI(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
            {



                Uri BaseEnd = new Uri("https://api.themoviedb.org/3/");
                Dictionary<string, string> queryDict = new Dictionary<string, string>();
                queryDict.Add("api_key", Environment.GetEnvironmentVariable("MOVIE_API_KEY"));
                queryDict.Add("language", "en-US");
                queryDict.Add("primary_release_year", "");//DateTime.Now.Year.ToString()                
                queryDict.Add("sort_by", "vote_average.desc");
                queryDict.Add("include_adult", "false");
                queryDict.Add("include_video", "false");
                queryDict.Add("vote_count.gte", "300");
                queryDict.Add("page", "1");
                for(int i = 0; i > -5; i--)
                {
                    string movieYear = DateTime.Now.AddYears(i).Year.ToString();
                    queryDict["primary_release_year"] = movieYear;
                    string url = BuildQueryString(@"https://api.themoviedb.org/3/discover/movie", queryDict);
                    RestClient client = new RestClient(url);
                    var response = client.Execute(new RestRequest());
                    MovieListHeaderModel curMovieList = JsonConvert.DeserializeObject<MovieListHeaderModel>(response.Content);

                    foreach (MovieListModel curMovie in curMovieList.MovieList)
                    {
                        context.MovieList.Add(curMovie);
                    }
                    
                }
                await context.SaveChangesAsync();

                foreach (MovieListModel curMovie in context.MovieList)
                {
                    string url = BuildQueryString(@"https://api.themoviedb.org/3/movie/" + curMovie.Id, queryDict);
                    RestClient client = new RestClient("https://api.themoviedb.org/3/");
                    RestRequest request = new RestRequest("movie/{id}", Method.GET);
                    request.AddUrlSegment("id", curMovie.Id);
                    request.AddQueryParameter("api_key", Environment.GetEnvironmentVariable("MOVIE_API_KEY"));
                    request.AddQueryParameter("language", "en-US");

                    IRestResponse response = client.Execute(request);
                    MovieDetailModel movieDetails = JsonConvert.DeserializeObject<MovieDetailModel>(response.Content);

                    context.MovieDetails.Add(movieDetails);
                    
                }
                await context.SaveChangesAsync();
            }
            
        }

        private static string BuildQueryString(string baseUrl, Dictionary<string, string> queryKVP)
        {
            return QueryHelpers.AddQueryString(baseUrl, queryKVP);          
        }

    }
}
