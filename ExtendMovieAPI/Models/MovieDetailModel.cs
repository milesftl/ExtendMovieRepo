using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExtendMovieAPI.Models
{
    public class Genre
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class ProductionCompany
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("logo_path")]
        public string Logo_Path { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("origin_country")]
        public string Origin_Country { get; set; }


    }

    public class ProductionCountry
    {
        [Key]
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

    }

    public class SpokenLanguage
    {
        [JsonProperty("iso_639_1")]
        public string Iso_639_1 { get; set; }
        [Key]
        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class MovieCollection
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
    }

    public class MovieDetailModel
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("belongs_to_collection")]
        public MovieCollection BelongsToCollection { get; set; }
        [JsonProperty("budget")]
        public int Budget { get; set; }
        [JsonIgnore]
        public string _Genres { get; set; }
        [NotMapped]
        [JsonProperty("genres")]
        public List<Genre> Genres
        {
            get
            {
                return string.IsNullOrEmpty(_Genres) ? null : JsonConvert.DeserializeObject<List<Genre>>(_Genres);
            }
            set
            {
                _Genres = JsonConvert.SerializeObject(value);
            }
        }
        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonIgnore]
        public string _ProductionCompanies { get; set; }
        [NotMapped]
        [JsonProperty("production_companies")]
        public List<ProductionCompany> ProductionCompanies
        {
            get
            {
                return string.IsNullOrEmpty(_ProductionCompanies) ? null : JsonConvert.DeserializeObject<List<ProductionCompany>>(_ProductionCompanies);
            }
            set
            {
                _ProductionCompanies = JsonConvert.SerializeObject(value);
            }
        }
        [JsonIgnore]
        public string _ProductionCountries { get; set; }

        [NotMapped]
        [JsonProperty("production_countries")]
        public List<ProductionCountry> ProductionCountries {
            get
            {
                return string.IsNullOrEmpty(_ProductionCountries) ? null : JsonConvert.DeserializeObject<List<ProductionCountry>>(_ProductionCountries);
            }
            set
            {
                _ProductionCountries = JsonConvert.SerializeObject(value);
            }
        }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("revenue")]
        public long Revenue { get; set; }
        [JsonProperty("runtime")]
        public int? Runtime { get; set; }
        [JsonIgnore]
        public string _SpokenLanguages { get; set; }
        [NotMapped]
        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages
        {
            get
            {
                return string.IsNullOrEmpty(_SpokenLanguages) ? null : JsonConvert.DeserializeObject<List<SpokenLanguage>>(_SpokenLanguages);
            }
            set
            {
                _SpokenLanguages = JsonConvert.SerializeObject(value);
            }
        }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("tagline")]
        public string Tagline { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("video")]
        public bool Video { get; set; }
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
    }
}
