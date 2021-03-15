﻿using System.Text.Json.Serialization;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client.Model
{
    internal class Episode
    {
        /// <summary>
        ///     The id of the episode.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the episode.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The air date of the episode.
        /// </summary>
        public string Air_date { get; set; }

        /// <summary>
        ///     The code of the episode.
        /// </summary>
        [JsonPropertyName("Episode")]
        public string EpisodeInformation { get; set; }

        /// <summary>
        ///     List of characters who have been seen in the episode.
        /// </summary>
        public string[] Characters { get; set; }

        /// <summary>
        ///     Link to the episode's own endpoint.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Time at which the episode was created in the database.
        /// </summary>
        public string Created { get; set; }
    }
}