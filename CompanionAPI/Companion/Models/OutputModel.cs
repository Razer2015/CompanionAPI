namespace CompanionAPI.Models
{
    public class OutputModel<T>
    {
        public T Model { get; set; }
        public ResponseStatus Response { get; set; }
    }
}
