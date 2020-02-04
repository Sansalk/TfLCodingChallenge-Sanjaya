using System.Collections.Generic;
using System.Threading.Tasks;
using PresentationLayer;
using TfLCodingChallenge_Sanjaya;

namespace BusinessLogicLayer
{
    public class RoadStatusDetails : IRoadStatusDetails
    {
        private readonly ILogger logger;
        private readonly IRoadStatus roadStatusDetail;
        private readonly IRequestHeader requestHeader;

        public RoadStatusDetails(IRoadStatus status, IRequestHeader requestHeader, ILogger logger)
        {
            roadStatusDetail = status;
            this.requestHeader = requestHeader;
            this.logger = logger;
        }

        public async Task<IList<RoadResponseObject>> GetRoadsStatusList(string roadId)
        {
            if (string.IsNullOrWhiteSpace(roadId))
            {
                logger?.Write(LogLevel.Warning, $"roadId invalid");
                return null;
            }

            IRequestHeader header = await requestHeader.GetAuthenticationHead();

            if (header == null)
            {
                logger?.Write(LogLevel.Warning, $"Could not read header values");
                return null;
            }

            if (string.IsNullOrWhiteSpace(header.BaseUrl))
            {
                logger?.Write(LogLevel.Warning, $"Could not read header Base Url");
                return null;
            }

            if (string.IsNullOrWhiteSpace(header.app_id))
            {
                logger?.Write(LogLevel.Warning, $"Could not read header App Id");
                return null;
            }

            if (string.IsNullOrWhiteSpace(header.app_key))
            {
                logger?.Write(LogLevel.Warning, $"Could not read header App key");
                return null;
            }

            return await roadStatusDetail.CheckRoadStatusAsync(new Authentication
            {
                id = header.id,
                key = header.key,
                app_id = header.app_id,
                app_key = header.app_key
            }, new RoadStatusRequest {baseUrl = header.BaseUrl, roadId = roadId});
        }
    }
}
