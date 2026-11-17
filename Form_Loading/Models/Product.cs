using System.Drawing;
using System.Reflection;

namespace Form_Loading.Models
{
    /// <summary>
    /// A single handcrafted item for sale in the shop.
    /// </summary>
    public class Product
    {
        public string Id { get; }
        public string Name { get; }
        public string Category { get; }
        public decimal Price { get; }
        public string Description { get; }

        // Name of the image in Properties/Resources.resx
        private readonly string imageKey;
        private Image image;

        public Product(string id, string name, string category, decimal price, string imageKey, string description)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            this.imageKey = imageKey;
        }

        public Image Image
        {
            get
            {
                if (image == null)
                {
                    // Read the strongly typed property (e.g. Resources.image_bag) so the
                    // key matches the C# name rather than the original file name
                    PropertyInfo prop = typeof(Properties.Resources).GetProperty(imageKey,
                        BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    image = prop?.GetValue(null) as Image;
                }
                return image;
            }
        }

        public string PriceText
        {
            get { return Price.ToString("C2", Catalog.Currency); }
        }
    }
}
