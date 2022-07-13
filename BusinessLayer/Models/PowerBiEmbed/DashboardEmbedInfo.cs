using System;
using Microsoft.PowerBI.Api.Models;

namespace BusinessLayer.Models.PowerBiEmbed
{
    public class DashboardEmbedInfo
    {
            public Guid DashboardId { get; set; }

            public string EmbedUrl { get; set; }

            public EmbedToken EmbedToken { get; set; }
    }
}
