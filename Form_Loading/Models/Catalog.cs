using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Form_Loading.Models
{
    /// <summary>
    /// The full product range. Kept in one place so every form
    /// shows the same names, prices and images.
    /// </summary>
    public static class Catalog
    {
        public const string Handbags = "Handbags";
        public const string Jewellery = "Jewellery";
        public const string Decor = "Decorative Items";
        public const string Pots = "Handcrafted Pots";

        // Prices are shown in New Zealand dollars
        public static readonly CultureInfo Currency = CultureInfo.GetCultureInfo("en-NZ");

        private static readonly List<Product> products = new List<Product>
        {
            // Handbags
            new Product("HB01", "Rattan Box Handbag", Handbags, 15m, "image_4_bag", "Hand-woven rattan bag with a tan leather strap and buckle. Structured shape, ideal for summer outings."),
            new Product("HB02", "Crochet Half-Moon Tote", Handbags, 18m, "image_bag", "Two-tone crochet tote in natural and chocolate cotton, with rounded wooden handles."),
            new Product("HB03", "Woven Raffia Bucket Bag", Handbags, 22m, "image5_bag", "Roomy raffia bucket bag with bamboo handles and a soft tassel detail."),
            new Product("HB04", "Straw Basket Bag", Handbags, 19m, "bad_last4", "Compact straw basket bag finished with a green silk scarf tie."),
            new Product("HB05", "Linen Drawstring Backpack", Handbags, 35m, "image3_bag", "Natural linen backpack with a drawstring top, padded straps and an inner pocket."),
            new Product("HB06", "Cotton Crochet Market Tote", Handbags, 20m, "Aluren_Bag_Yarn_Peonies", "Open-stitch cream market tote with leather handles. Folds flat when not in use."),
            new Product("HB07", "Sunflower Crochet Shoulder Bag", Handbags, 25m, "CB5C9DC7_6B65_49F9_8639_7162DC565F61", "Soft white crochet shoulder bag with a hand-stitched sunflower motif."),
            new Product("HB08", "Sunflower Granny-Square Tote", Handbags, 29m, "OIP__1_", "Granny-square tote crocheted in sunshine yellow and cream."),

            // Jewellery
            new Product("JW01", "Beaded Charm Bracelet", Jewellery, 24m, "OIP__2_", "Multi-strand bracelet of glass, ceramic and stone beads with a bronze clasp."),
            new Product("JW02", "Silver Earring Collection", Jewellery, 36m, "jewelry_7015521_1920", "Set of handmade silver studs and drop earrings with pearl accents."),
            new Product("JW03", "Pearl & Leather Bracelet", Jewellery, 28m, "bracelet_knotted_white_pearl_b_1_r", "Freshwater pearls knotted on leather, finished with a starfish charm."),
            new Product("JW04", "Beaded Statement Necklace", Jewellery, 32m, "OIP__3_", "Long black and silver beaded necklace with a crystal drop."),
            new Product("JW05", "Terracotta Jhumka Set", Jewellery, 26m, "OIP__4_", "Hand-painted terracotta necklace with matching jhumka earrings."),
            new Product("JW06", "Hand-Painted Krishna Pendant", Jewellery, 22m, "a0dce657eac793078946b24396ad3372", "Miniature hand-painted pendant on a blue and green beaded chain."),
            new Product("JW07", "Folk-Art Portrait Pendant", Jewellery, 22m, "R__2_", "Wooden pendant with a hand-painted folk-art portrait, on a black cord."),
            new Product("JW08", "Fabric Button Stud Earrings", Jewellery, 18m, "_4f99c4d2a99abcded7ba5f7cb303c259", "Three pairs of embroidered fabric button studs in pink, yellow and blue."),

            // Decorative items
            new Product("DC01", "Pom-Pom Dreamcatcher", Decor, 19m, "_81yGVverYfS", "Pastel dreamcatcher with woven butterflies and pom-pom tassels."),
            new Product("DC02", "Macramé Moon Wall Hanging", Decor, 45m, "_51L6KEEb1QL__AC_", "Large cotton macramé hanging with a crescent moon and long fringe."),
            new Product("DC03", "Hanging Planter Nests", Decor, 27m, "R__3_1", "Pair of woven hanging nests, perfect for small plants or decor."),
            new Product("DC04", "Pom-Pom Garland Strands", Decor, 16m, "maxresdefault", "Set of colourful wool pom-pom strands for doors, walls or windows."),
            new Product("DC05", "Feather Dreamcatcher Trio", Decor, 34m, "e4fd9e2a42e4dcbc1e969d5a9b5d4718", "Three hand-woven dreamcatchers with white feathers and wooden beads."),
            new Product("DC06", "Cinnamon Stick Candle", Decor, 14m, "shutterstock_249878665", "Pillar candle wrapped in cinnamon sticks and tied with a red ribbon."),
            new Product("DC07", "Paper Petal Wall Art", Decor, 21m, "OIP__5_", "Rainbow paper-petal sunburst for a bright feature wall."),
            new Product("DC08", "Paper Flower Wreath Frames", Decor, 24m, "maxresdefault__1_", "Pair of geometric frames decorated with handmade paper flowers."),

            // Pots
            new Product("PT01", "Blue Pottery Vase Set", Pots, 65m, "pots1", "Hand-painted ceramic vases and jars in traditional blue pottery patterns."),
            new Product("PT02", "Hand-Painted Ceramic Plates", Pots, 48m, "_1", "Decorative wall plates, each painted by hand with floral and geometric designs."),
            new Product("PT03", "Studio Ceramic Cup Set", Pots, 30m, "cup_687147_1920", "Set of matte stoneware cups thrown on the wheel. Every piece is slightly unique."),
        };

        public static IReadOnlyList<Product> All
        {
            get { return products; }
        }

        public static IEnumerable<Product> InCategory(string category)
        {
            return products.Where(p => p.Category == category);
        }

        public static Product Find(string id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
