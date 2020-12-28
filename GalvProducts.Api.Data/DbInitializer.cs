namespace GalvProducts.Api.Data
{
    /// <summary>
    /// Initialize if database does not exists
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
