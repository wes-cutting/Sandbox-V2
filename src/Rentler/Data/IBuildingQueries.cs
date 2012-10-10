using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Common;

namespace Rentler.Data
{
    public interface IBuildingQueries
    {
        PaginatedList<Common.PropertyPreview> SearchUserBuildings(int userId, PropertySearch search);

        Common.PropertyPreview GetBuildingPreviewById(long buildingId);

        void DeleteBuilding(long buildingId);
    }
}
