using BusinessLogicLayer;
using DataAccessLayer;
using PresentationLayer;

namespace TfLCodingChallenge_Sanjaya
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Logger>();
            Bind<IRoadStatus>().To<RoadStatus>();
            Bind<IRoadStatusDetails>().To<RoadStatusDetails>();
            Bind<IRequestHeader>().To<RequestHeader>();
        }
    }
}
