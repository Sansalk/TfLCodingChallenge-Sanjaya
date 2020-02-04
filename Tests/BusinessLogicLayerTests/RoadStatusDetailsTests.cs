using BusinessLogicLayer;
using Moq;
using NUnit.Framework;
using NFluent;
using PresentationLayer;
using TfLCodingChallenge_Sanjaya;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Tests.BusinessLogicLayerTests
{
    [TestFixture]
    public class RoadStatusDetailsTests
    {
        private Mock<ILogger> logger;
        private Mock<IRoadStatus> roadStatus;
        private Mock<IRequestHeader> requestHeader;
        private IRoadStatusDetails roadStatusDetails;

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger>();
            roadStatus = new Mock<IRoadStatus>();
            requestHeader = new Mock<IRequestHeader>();
            roadStatusDetails = new RoadStatusDetails(roadStatus.Object, requestHeader.Object, logger.Object);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetRoadsStatusList_ReturnsNull_AndLogs_IfRoadIdIsNullEmptyOrWhitespace(string roadId)
        {
            var result = roadStatusDetails.GetRoadsStatusList(roadId).Result;

            logger.Verify(l => l.Write(LogLevel.Warning, "roadId invalid"), Times.Once);
            Check.That(result).IsNull();
        }

        [Test]
        public void GetRoadsStatusList_ReturnsNull_AndLogs_IfRoadIdIsNullEmptyOrWhitespaceNotNull_AndHeaderGetAuthenticationHeadIsNull()
        {
            requestHeader.Setup(r => r.GetAuthenticationHead()).Returns(Task.FromResult<IRequestHeader>(null));

            var result = roadStatusDetails.GetRoadsStatusList("test").Result;

            logger.Verify(l => l.Write(LogLevel.Warning, "Could not read header values"), Times.Once);
            Check.That(result).IsNull();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetRoadsStatusList_ReturnsNull_AndLogs_IfRoadIdIsNullEmptyOrWhitespaceNotNull_AndHeaderGetAuthenticationBaseUrlIsNull (string baseUrl)
        {
            requestHeader.Setup(r => r.GetAuthenticationHead()).Returns(Task.FromResult<IRequestHeader>(new RequestHeader
            {
                BaseUrl = baseUrl
            }));

            var result = roadStatusDetails.GetRoadsStatusList("test").Result;

            logger.Verify(l => l.Write(LogLevel.Warning, "Could not read header Base Url"), Times.Once);
            Check.That(result).IsNull();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetRoadsStatusList_ReturnsNull_AndLogs_IfRoadIdIsNullEmptyOrWhitespaceNotNull_AndHeaderGetAuthenticationAppIdIsNull(string appId)
        {
            requestHeader.Setup(r => r.GetAuthenticationHead()).Returns(Task.FromResult<IRequestHeader>(new RequestHeader
            {
                BaseUrl = "test",
                app_id = appId
            }));

            var result = roadStatusDetails.GetRoadsStatusList("test").Result;

            logger.Verify(l => l.Write(LogLevel.Warning, "Could not read header App Id"), Times.Once);
            Check.That(result).IsNull();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetRoadsStatusList_ReturnsNull_AndLogs_IfRoadIdIsNullEmptyOrWhitespaceNotNull_AndHeaderGetAuthenticationAppKeyIsNull(string appKey)
        {
            requestHeader.Setup(r => r.GetAuthenticationHead()).Returns(Task.FromResult<IRequestHeader>(new RequestHeader
            {
                BaseUrl = "test",
                app_id = "test",
                app_key = appKey
            }));

            var result = roadStatusDetails.GetRoadsStatusList("test").Result;

            logger.Verify(l => l.Write(LogLevel.Warning, "Could not read header App key"), Times.Once);
            Check.That(result).IsNull();
        }

        [Test]
        public void GetRoadsStatusList_Returns_RoadStatusCheckRoadStatusAsync_IfAllInputsValid()
        {
            requestHeader.Setup(r => r.GetAuthenticationHead()).Returns(Task.FromResult<IRequestHeader>(new RequestHeader
            {
                BaseUrl = "test",
                app_id = "test",
                app_key = "test"
            }));

            var expectedResult = new List<RoadResponseObject>
            {
                new RoadResponseObject
                {
                    id = "Test",
                    displayName = "Test",
                    statusSeverity = "Test",
                    statusSeverityDescription = "Test"
                },
                new RoadResponseObject
                {
                    id = "Test1",
                    displayName = "Test1",
                    statusSeverity = "Test1",
                    statusSeverityDescription = "Test1"
                }
            };

            roadStatus.Setup(r => r.CheckRoadStatusAsync(It.IsAny<Authentication>(), 
                It.IsAny<RoadStatusRequest>())).Returns(Task.FromResult(expectedResult));
            
            var result = roadStatusDetails.GetRoadsStatusList("test").Result;

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(expectedResult.Count);

            foreach(var expectedResultObject in expectedResult)
            {
                Check.That(result.Any(r => r.id == expectedResultObject.id && 
                    r.displayName == expectedResultObject.displayName && 
                    r.statusSeverity == expectedResultObject.statusSeverity && 
                    r.statusSeverityDescription == expectedResultObject.statusSeverityDescription)).IsTrue();
            }
        }
    }
}
