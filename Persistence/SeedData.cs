using Domain;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence;

public class SeedData{

    public static void SeedAsync(UserManager<User> userManager){
        try{
            if(!userManager.Users.Any()){
                var result=userManager.CreateAsync(new User
                {
                    UserName="Admin",
                    Email= "admin@gmail.com",
                    FullName="Admin Post",
                    PhoneNumber="123"
                },"Senha_11").GetAwaiter().GetResult();

                var userFile=File.ReadAllText("../Persistence/Seeds/UserSeed.json");
                var user=JsonSerializer.Deserialize<User>(userFile);

                var result2=userManager.CreateAsync(new User
                {
                    UserName=user.UserName,
                    Email= user.Email,
                    FullName=user.FullName,
                    PhoneNumber=user.PhoneNumber
                },"Senha_22").GetAwaiter().GetResult();

                if(result.Errors is not null && result2.Errors is not null){
                throw new Exception(result.Errors+"\t"+result2.Errors);
                }

            }
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
}