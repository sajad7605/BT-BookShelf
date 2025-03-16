namespace BookStoreApp.Data
{
    public class SeedData()
    {
        public static async Task Seed(IApplicationBuilder ApplicationB)
        {
            using (var Scope = ApplicationB.ApplicationServices.CreateScope())
            {
                {
                RoleManager<IdentityRole> rolemanager= Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<User> userManager= Scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                if (!await rolemanager.RoleExistsAsync(Roles.user)){
                    await rolemanager.CreateAsync(new IdentityRole(Roles.user));
                }
                if(!await rolemanager.RoleExistsAsync(Roles.admin)){
                    await rolemanager.CreateAsync(new IdentityRole(Roles.admin));
                }
               
                var context=Scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var SA=new User{
                    Email="bahiraeimmai@gmail.com",
                    UserName="bahiraeimmai@gmail.com"
                };
                await userManager.CreateAsync(SA,"Password_123#");
                await userManager.AddToRoleAsync(SA,Roles.admin);
                context.SaveChanges();
                
            }


            }
        }
    }
}