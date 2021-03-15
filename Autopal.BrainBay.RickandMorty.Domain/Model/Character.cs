namespace Autopal.BrainBay.RickandMorty.Domain.Model
{
    public class Character : BaseEntity
    {
        /// <summary>
        ///     Gets the name of the character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the status of the character ('Alive', 'Dead' or 'unknown').
        /// </summary>
        public CharacterStatus Status { get; set; }

        /// <summary>
        ///     Gets the species of the character.
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        ///     Gets the type or subspecies of the character.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Gets the gender of the character ('Female', 'Male', 'Genderless' or 'unknown').
        /// </summary>
        public CharacterGender Gender { get; set; }

        /// <summary>
        ///     Gets name and link to the character's last known location endpoint.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        ///     Gets name and link to the character's origin location.
        /// </summary>
        public Location Origin { get; set; }
    }
}