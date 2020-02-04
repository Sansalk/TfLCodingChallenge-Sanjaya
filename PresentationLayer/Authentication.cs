namespace PresentationLayer
{
  
    public sealed class Authentication : PresentationLayer.IAuthentication
    {

        public string id { get; set; } 
        public string key { get; set; }
        public string app_id { get; set; }
        public string app_key { get; set; }


    }


}
