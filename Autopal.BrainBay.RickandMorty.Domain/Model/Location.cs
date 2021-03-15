using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Autopal.BrainBay.RickandMorty.Domain.Model
{
    public class Location : BaseEntity
    {
        /// <summary>
        ///     Gets the name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the type of the location.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Gets the dimension in which the location is located.
        /// </summary>
        public string Dimension { get; set; }

        /// <summary>
        ///     Gets list of character who have been last seen in the location.
        /// </summary>
        public ICollection<Character> Residents { get; set; }

        public static implicit operator Location(StringValues v)
        {
            throw new NotImplementedException();
        }
    }
}