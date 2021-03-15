﻿using System;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model.Mapping
{
    public static class MapperHelpers
    {
        /// <summary>
        ///     Simple string to enum parser.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>Enum value.</returns>
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        ///     Simple string to uri parser.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>Uri object.</returns>
        public static Uri ToUri(this string value)
        {
            return string.IsNullOrEmpty(value) || !Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute)
                ? null
                : new Uri(value);
        }

        /// <summary>
        ///     Simple string to datetime parser.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>Datetime object.</returns>
        public static DateTime ToDateTime(this string value)
        {
            return string.IsNullOrEmpty(value) ? DateTime.MinValue : DateTime.Parse(value);
        }
    }
}