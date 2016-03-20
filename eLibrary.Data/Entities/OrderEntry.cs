namespace ELibrary.Data.Entities
{
    public class OrderEntry
    {
        public int Id { get; set; }
        public Book BookItem { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
    }
}