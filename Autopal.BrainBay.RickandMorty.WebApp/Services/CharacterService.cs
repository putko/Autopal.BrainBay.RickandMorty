using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autopal.BrainBay.RickandMorty.Domain;
using Autopal.BrainBay.RickandMorty.Domain.Model;
using Autopal.BrainBay.RickandMorty.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Autopal.BrainBay.RickandMorty.WebApp.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly RickandMortyContext db;

        public CharacterService(RickandMortyContext db)
        {
            this.db = db;
        }

        public PaginatedItemsViewModel<Character> GetCharactersPaginated(int pageSize, int pageIndex)
        {
            var totalItems = db.Characters.LongCount();

            var itemsOnPage = db.Characters
                .Include(x => x.Location)
                .Include(x => x.Origin)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return new PaginatedItemsViewModel<Character>(
                pageIndex, pageSize, totalItems, itemsOnPage);
        }

        public PaginatedItemsViewModel<Location> GetLocationsPaginated(int pageSize, int pageIndex)
        {
            var totalItems = db.Locations.LongCount();

            var itemsOnPage = db.Locations
                .Include(x => x.Residents)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return new PaginatedItemsViewModel<Location>(
                pageIndex, pageSize, totalItems, itemsOnPage);
        }

        public Character FindCharacter(int id)
        {
            return db.Characters.Include(x => x.Location)
                .Include(x => x.Origin).FirstOrDefault(ci => ci.Id == id);
        }

        public Location FindLocation(string name)
        {
            return db.Locations.Include(x => x.Residents)
                .FirstOrDefault(ci => ci.Name == name);
        }

        public IEnumerable GetCharacterStatuses()
        {
            return from CharacterStatus d in Enum.GetValues(typeof(CharacterStatus))
                select new {Id = (int) d, Name = d.ToString()};
        }

        public IEnumerable GetCharacterGenders()
        {
            return from CharacterGender d in Enum.GetValues(typeof(CharacterGender))
                select new {Id = (int) d, Name = d.ToString()};
        }

        public IList<Location> GetLocations()
        {
            return db.Locations
                .OrderBy(c => c.Name)
                .ToList();
        }

        public void CreateCharacter(Character Character)
        {
            db.Characters.Add(Character);
            db.SaveChanges();
        }

        public void UpdateCharacter(Character Character)
        {
            db.Entry(Character).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveCharacter(Character Character)
        {
            db.Characters.Remove(Character);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}