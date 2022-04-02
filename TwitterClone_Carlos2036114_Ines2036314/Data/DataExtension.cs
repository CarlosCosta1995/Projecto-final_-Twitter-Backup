using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Data
{
    public static class DataExtension
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                // Creates the database if not exists
                if (context.Database.EnsureCreated())
                {
                    DataDbInitializer.InsertData(context);
                }
            }
        }
    }
}
