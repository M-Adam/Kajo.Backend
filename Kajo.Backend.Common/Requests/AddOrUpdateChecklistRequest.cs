namespace Kajo.Backend.Common.Requests
{
    public class AddOrUpdateChecklistRequest : RequestBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
