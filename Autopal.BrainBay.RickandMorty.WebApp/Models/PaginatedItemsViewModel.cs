using System;
using System.Collections.Generic;
using Autopal.BrainBay.RickandMorty.Domain.Model;

namespace Autopal.BrainBay.RickandMorty.WebApp.Models
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : BaseEntity
    {
        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            ActualPage = pageIndex;
            ItemsPerPage = pageSize;
            TotalItems = count;
            TotalPages = (int) Math.Ceiling((decimal) count / pageSize);
            Data = data;
        }

        public int ActualPage { get; }

        public int ItemsPerPage { get; }

        public long TotalItems { get; }

        public int TotalPages { get; set; }

        public IEnumerable<TEntity> Data { get; }
    }
}