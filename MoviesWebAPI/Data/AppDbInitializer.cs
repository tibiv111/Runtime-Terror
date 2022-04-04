using MoviesWebAPI.Data.Models;

namespace MoviesWebAPI.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                //feltoltom a Users tablat ha netalan ures lenne
                if (context != null && !context.Users.Any())
                {
                    context.Users.AddRange(
                        new Users()
                        {
                            UserName = "testuser1",
                            Password = "123456",
                            Email = "testemail@gmail.com"
                        },
                        new Users()
                        {
                            UserName = "testuser2",
                            Password = "23456789",
                            Email = "testuser2email@gmail.com"
                        },
                        new Users()
                        {
                            UserName = "testuser3",
                            Password = "testuser3",
                            FirstName = "Pityi",
                            LastName = "Palko",
                            Gender = "Male",
                            Email = "testuser3@gmail.com"
                        }
                   );

                    context.SaveChanges();
                }
            }
        }
    }
}
