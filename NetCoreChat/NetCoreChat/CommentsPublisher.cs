using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Comments.Web
{
    public class CommentsPublisher : Hub
    {
		public Task PublishReport(string data)
		{
			return Clients.All.InvokeAsync("Send", data);
		}
	}
}
