using BusinessLayer.Models.PowerBiEmbed;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces.PowerBI
{
    public interface IEmbedDashboard
    {
        public List<DashboardEmbedInfo> GetEmbedInfo(Guid workspaceId);
    }
}
