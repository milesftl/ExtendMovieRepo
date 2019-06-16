using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExtendMovieAPI.Models
{
    public class MovieListModel
    {
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }


        [JsonIgnore]
        public string _GenreIds { get; set; }
        [NotMapped]
        [JsonProperty("genre_ids")]
        public List<int> GenreIds
        {
            get
            {
                return string.IsNullOrEmpty(_GenreIds) ? null : JsonConvert.DeserializeObject<List<int>>(_GenreIds);
            }
            set
            {
                _GenreIds = JsonConvert.SerializeObject(value);
            }
        }

        [JsonProperty("id")]
        public long Id { get; set; }


        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
    }

    public class MovieListHeaderModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("total_results")]
        public int Total_Results { get; set; }
        [JsonProperty("total_pages")]
        public int Total_Pages { get; set; }
        [JsonProperty("results")]
        public List<MovieListModel> MovieList { get; set; }
    }

    public class GenreIDsList
    {
        [Key]
        [JsonProperty("genre_ids")]
        public int GenreId { get; set; }
    }
}
