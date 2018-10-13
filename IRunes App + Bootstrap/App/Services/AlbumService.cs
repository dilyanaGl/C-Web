using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.Services
{
    using ViewModels;
    using Data;
    using Data.Models;

    public class AlbumService
    {
        private readonly IRunesDbContext context;

        public AlbumService()
        {

            this.context = new IRunesDbContext();

        }
        
        public void Add(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover
            };
            this.context.Albums.Add(album);
            context.SaveChanges();

        }

        public IEnumerable<AllAlbumsModel> All()
        => this.context.Albums.Select(p => new AllAlbumsModel
        {
            Id = p.Id,
            Name = p.Name
        })
        .ToArray();

        public AlbumDetailsModel Details(string id)
        => this.context.Albums.Where(p => p.Id == id)
        .Select(p => new AlbumDetailsModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Tracks.Select(k => k.Track.Price).Sum(),
            Cover = p.Cover,
            Tracks = p.Tracks.Select(k => new TrackViewModel
            {
                Id = k.Track.Id,
                Name = k.Track.Name
            })
        .ToArray()
        })
        .SingleOrDefault();

    }
}

