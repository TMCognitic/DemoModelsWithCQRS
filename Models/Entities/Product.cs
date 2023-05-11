namespace Models.Entities
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double Price { get; set; }
        internal Product(int id, string name, double price)
            : this(name, price)
        {
            Id = id;
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
