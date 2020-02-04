using System.Collections.Generic;
using System.Threading.Tasks;


namespace PresentationLayer
{
    public interface IRoadStatus
    {
        Task<List<RoadResponseObject>> CheckRoadStatusAsync(Authentication auth,RoadStatusRequest roadrequest);
    }
}
