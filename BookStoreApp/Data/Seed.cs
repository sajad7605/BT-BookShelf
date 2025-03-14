namespace BookStoreApp.Data
{
    public class SeedData()
    {
        public static void Seed(IApplicationBuilder ApplicationB)
        {
            using (var Scope = ApplicationB.ApplicationServices.CreateScope())
            {
                var DBContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DBContext.SaveChanges();

            }
        }
    }
}