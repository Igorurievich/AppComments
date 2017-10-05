using System;
using System.Threading.Tasks;

namespace App.Comments.Common.Interfaces {

    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
        //Task UpdateMatch(MatchViewModel match);
        //Task AddFeed(FeedViewModel feed);
        //Task AddChatMessage(ChatMessage message);
    }
}