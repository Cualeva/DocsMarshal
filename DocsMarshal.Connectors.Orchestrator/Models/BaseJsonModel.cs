namespace DocsMarshal.Connectors.Orchestrator.Models
{
    public class BaseJsonModel
    {
        public bool Error { get; set; }
        public string ErrorDescription { get; set; }
        public bool LoggedOut { get; set; }
    }

    public class BaseJsonModel<T> : BaseJsonModel
    {
        public T Data { get; set; }
    }
}
