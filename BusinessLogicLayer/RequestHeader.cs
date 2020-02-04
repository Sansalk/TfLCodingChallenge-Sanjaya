using System;
using System.Configuration;
using System.Threading.Tasks;
using NLog;


namespace TfLCodingChallenge_Sanjaya
{
   public  class RequestHeader : IRequestHeader
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string id { get; } = "app_id";
        public string key { get; } = "app_key";
        public string app_id { set; get; }
        public string app_key { set; get; }
        public string BaseUrl { set; get; }
       
        public async Task<IRequestHeader> GetAuthenticationHead()
        {
            try
            {
                RequestHeader authhead = new RequestHeader();
                authhead.app_id = ConfigurationManager.AppSettings["appid"];
                authhead.app_key = ConfigurationManager.AppSettings["appkey"];
                authhead.BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                   
                return (await Task.Run(() => authhead));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "GetAuthenticationHead, Error {0} ", ex.ToString());
                throw;
            }
        }
    }
}
