using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SIS.MvcFramework;

namespace Chushka.Services
{
    using Models;
    using Data;
    using Data.Models;

    public class ChannelService : IChannelService
    {
        private readonly MishMashDbContext context;
        public ChannelService()
        {
            this.context = new MishMashDbContext();
        }
        public ListChannelsModel ListChannels(string username)
        {
            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            if (user == null)
            {
                return null;
            }
            var channels = context.UserChannels.Where(p => p.UserId == user.Id)
              .Select(p => new ListChannelModel
             {
                 Name = p.Channel.Name,
                 Type = p.Channel.Type.ToString(),
                 Followers = context.UserChannels.Count(k => k.ChannelId == p.Id),
                 Id = p.ChannelId
             })
            .ToArray();

            var followerIds = this.context.UserChannels
                .Where(p => channels.Any(k => k.Id == p.ChannelId) && p.UserId != user.Id)
                .Select(p => p.UserId).ToArray();

            var suggestedChannels = this.context.UserChannels
                .Where(p => followerIds.Contains(p.UserId) && !channels.Any(k => k.Id == p.ChannelId))
                    .Select(p => new ListChannelModel
                    {
                        Name = p.Channel.Name,
                        Type = p.Channel.Type.ToString(),
                        Followers = p.Channel.Followers.Count,
                        Id = p.ChannelId
                    })
                .ToArray();


            var seeOthers = this.context.Channels
                .Where(p => !suggestedChannels.Any(k => k.Id == p.Id)
                && !channels.Any(k => k.Id == p.Id))
           .Select(p => new ListChannelModel
           {
               Name = p.Name,
               Type = p.Type.ToString(),
               Followers = p.Followers.Count(),
               Id = p.Id
           })
           .ToArray();

            var model = new ListChannelsModel
            {
                Following = channels,
                Suggested = suggestedChannels,
                SeeOther = seeOthers
            };

            return model;

        }

        public ListChannelsModel ListNotFollowed(int userId)
        {

            var channels = this.context.Channels.Where(k => !k.Followers.Select(p => p.UserId).Contains(userId))
           .Select(p => new ListChannelModel
           {
               Name = p.Name,
               Type = p.Type.ToString(),
               Followers = p.Followers.Count(),
               Id = p.Id
           })
           .ToArray();

            var model = new ListChannelsModel
            {
                Suggested = channels
            };

            return model;

        }


        public void CreateChannel(CreateChannelModel model)
        {

            var tagNames = model.Tags.Split(new[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);


            var type = Enum.Parse<ChannelType>(model.Type, true);
            var channel = new Channel
            {
                Name = model.Name,
                Description = model.Description,
                Type = type

            };

            context.Channels.Add(channel);
            context.SaveChanges();

            var tags = new List<Tag>();

            foreach (var name in tagNames)
            {
                var tag = this.context.Tags.FirstOrDefault(p => p.Name == name);
                if (tag == null)
                {
                    tag = new Tag
                    {
                        Name = name

                    };
                }
                tags.Add(tag);
            }

            context.AddRange(tags);
            context.SaveChanges();

            var channelTags = new List<ChannelTag>();
            foreach (var tag in tags)
            {
                var channelTag = new ChannelTag
                {
                    Channel = channel,
                    Tag = tag
                };

                channelTags.Add(channelTag);
            }
            context.ChannelTags.AddRange(channelTags);
            context.SaveChanges();

        }

        public FollowedChannelsModel SeeFollowing(string username)
        {
            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            if (user == null)
            {
                return null;
            }

            var channels = this.context.UserChannels.Where(p => p.UserId == user.Id)                
                .Select(p => new FollowingChannelModel
            {
                Name = p.Channel.Name,
                Type = p.Channel.Type.ToString(),
                FollowersCount = p.Channel.Followers.Count(),
                Id = p.ChannelId

            })
            .ToArray();

            var model = new FollowedChannelsModel
            {
                Channels = channels
            };
            return model;

        }

        public ChannelDetailsModel Details(int id)
        {
            return this.context.Channels.Where(p => p.Id == id)
            .Select(p => new ChannelDetailsModel
            {
                Name = p.Name,
                Type = p.Type.ToString(),
                Tags = String.Join(", ", p.Tags.Select(k => k.Tag.Name)),
                Description = p.Description
            })
            .SingleOrDefault();

        }

        public bool Follow(int channelId, string username)
        {
            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            var channel = this.context.Channels.SingleOrDefault(p => p.Id == channelId);
            if (user == null || channel == null)
            {
                return false;
            }

            if(this.context.UserChannels.Any(p => p.UserId == user.Id && p.ChannelId == channelId))
            {
                return false;
            }

            var userChannel = new UserChannel
            {
                User = user,
                Channel = channel
            };
            user.ChannelsFollowing.Add(userChannel);
            context.SaveChanges();
            channel.Followers.Add(userChannel);
            context.SaveChanges();

            return true;

        }

        public bool Unfollow(int channelId, string username)
        {
            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            var channel = this.context.Channels.SingleOrDefault(p => p.Id == channelId);
            if (user == null || channel == null)
            {
                return false;
            }            

            var userChannels = this.context.UserChannels.Where(p => p.ChannelId == channelId && p.UserId == user.Id);
            this.context.UserChannels.RemoveRange(userChannels);
            this.context.SaveChanges();
            //var userChannel = channel.Followers.Where(p => p.UserId == user.Id).FirstOrDefault();

            //user.ChannelsFollowing.Remove(userChannel);
            //channel.Followers.Remove(userChannel);


            return true;
        }
    }
}
