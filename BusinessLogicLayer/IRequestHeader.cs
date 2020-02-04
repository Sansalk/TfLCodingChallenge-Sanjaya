using System.Threading.Tasks;

namespace TfLCodingChallenge_Sanjaya
{
    public interface IRequestHeader
    {
        string id { get; }
        string key { get; }
        string app_id { set; get; }
        string app_key { set; get; }
        string BaseUrl { set; get; }

        Task<IRequestHeader> GetAuthenticationHead();
    }
}