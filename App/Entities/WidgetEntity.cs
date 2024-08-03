namespace App.Entities
{
    public class WidgetEntity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SortOrder { get; set; }
    }
}