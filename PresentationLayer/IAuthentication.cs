namespace PresentationLayer
{
    public interface IAuthentication
    {

        string id
        {
            get;
        }
        string key
        {
            get;
        }
        string app_id
        {
            get;
        }
        string app_key
        {
            get;
        }

    }
}