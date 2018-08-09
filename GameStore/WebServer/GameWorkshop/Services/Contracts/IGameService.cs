using System;
using System.Collections.Generic;
using HTTPServer.GameWorkshop.ViewModels.Admin;

namespace HTTPServer.GameWorkshop.Services.Contracts
{
    public interface IGameService
    {
        bool AddGame(string name, string image, double size, decimal price, string url, string description,
            DateTime releaseDate);

        IEnumerable<ListGamesViewModel> List();

        bool EditGame(int id, string modelTitle, string modelImageThumbnail, double modelSize, decimal modelPrice,
            string modelYouTubeVideoId, string modelDescription, DateTime modelReleaseDate);

        EditGamesViewModel FetchGame(int id);
        bool DeleteGame(int id);

        IEnumerable<HomeViewModel> DisplayHome();

        GameDetailsViewModel GetDetails(int id);
    }
}