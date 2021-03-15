using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model;
using Location = Autopal.BrainBay.RickandMorty.Domain.Model.Location;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Business.Mapping
{
    /// <inheritdoc />
    internal class LocationConverter : ITypeConverter<CharacterLocation, Location>
    {
        internal const string LocationKeyWord = "Locations";

        /// <inheritdoc />
        public Location Convert(CharacterLocation source, Location destination, ResolutionContext context)
        {
            if (source == null) return null;

            var locationsExists = context.Options.Items.TryGetValue(LocationKeyWord, out var locations);
            return locationsExists
                ? ((IEnumerable<Location>) locations).SingleOrDefault(x =>
                    string.Equals(x.Name, source.Name, StringComparison.InvariantCultureIgnoreCase))
                : null;
        }
    }
}