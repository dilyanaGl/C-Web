using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.Services
{
    using Data;
    using Data.Models;
    using ViewModels;

    public class TrackService
    {
        private readonly IRunesDbContext context;

        public TrackService()
        {
            this.context = new IRunesDbContext();

        }


        public bool Create(string name, string link, decimal price, string albumId)
        {
            if(!context.Albums.Any(p => p.Id == albumId))
            {
                return false;
            }

            var track = new Track
            {
                Name = name,
                 Link = link,
                Price = price,
            };

            var trackAlbum = new AlbumTrack
            {
                Track = track,
                Album = context.Albums.SingleOrDefault(p => p.Id == albumId)
            };

            context.AlbumTracks.Add(trackAlbum);
            context.SaveChanges();

            

            return true;
        }


        public IEnumerable<TrackViewModel> All(string albumId)
        => this.context.Tracks.Where(p => p.Albums.Any(k => k.AlbumId == albumId))
        .Select(p => new TrackViewModel
        {
            Id = p.Id,
            Name = p.Name
        })
        .ToArray();

        public TrackDetailsModel Details(string id, string albumId)
        => context.AlbumTracks.Where(p => p.TrackId == id && p.AlbumId == albumId)
        .Select(p => new TrackDetailsModel
        {
            Id = p.TrackId,
            Name = p.Track.Name,
            Link = p.Track.Link,
            Price = p.Track.Price,
            AlbumId = p.AlbumId
        })
        .SingleOrDefault();

        public string GetId(string name, string albumId)
            => this.context
            .AlbumTracks
            .Where(p => p.AlbumId == albumId)
            .FirstOrDefault(p => p.Track.Name == name).TrackId;

        //public TrackDetailsModel Find(string trackId, string albumId)
        //{
        //    var track = this.context.AlbumTracks.Where(p => p.TrackId == trackId && p.AlbumId == albumId)
        //    .Select(p => new TrackDetailsModel
        //    {
        //        Name = p.Track.Name,
        //        Link = p.Track.Link,
        //        Price = p.Price,
        //        AlbumId = p.AlbumId
        //    });

        //}
    }
}
