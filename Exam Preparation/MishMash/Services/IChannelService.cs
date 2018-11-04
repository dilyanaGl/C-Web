using System.Collections.Generic;
using Chushka.Models;

namespace Chushka.Services
{
    public interface IChannelService
    {
        void CreateChannel(CreateChannelModel model);
        ChannelDetailsModel Details(int id);
        ListChannelsModel ListChannels(string username);
        ListChannelsModel ListNotFollowed(int userId);
        FollowedChannelsModel SeeFollowing(string username);
        bool Follow(int channelId, string username);
        bool Unfollow(int channelId, string username);
    }
}