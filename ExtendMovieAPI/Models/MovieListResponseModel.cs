using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExtendMovieAPI.Models
{
    public class MovieListResponseModel
    {
        public long Cursor { get; set; }

        public IEnumerable<MovieListModel> MiniMovieList { get; set; }
    }

    public class MiniMovieResponseModel
    {
        [JsonProperty("poster_path")] public string PosterPath { get; set; }

        [JsonProperty("adult")] public bool? Adult { get; set; }

        [JsonProperty("overview")] public string Overview { get; set; }

        [JsonProperty("release_date")] public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("genre_ids")] public List<int> GenreIds { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("original_title")] public string OriginalTitle { get; set; }

        [JsonProperty("original_language")] public string OriginalLanguage { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("backdrop_path")] public string BackdropPath { get; set; }

        [JsonProperty("popularity")] public double? Popularity { get; set; }

        [JsonProperty("vote_count")] public long? VoteCount { get; set; }

        [JsonProperty("video")] public bool? Video { get; set; }

        [JsonProperty("vote_average")] public double? VoteAverage { get; set; }
    }
}