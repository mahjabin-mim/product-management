namespace ProductValidation.Caching
{
    public static class CacheKeys
    {
        public const string ProductList = "product_list";

        public static string ProductById(int id) => $"product_{id}";

        public const string CategoryList = "category_list";
    }
}