using System.Collections.Generic;
using System.Threading.Tasks;
using PresentationLayer;

namespace BusinessLogicLayer
{
    public interface IRoadStatusDetails
    {
        Task<IList<RoadResponseObject>> GetRoadsStatusList(string roadId);
    }
}