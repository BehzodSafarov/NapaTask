using Microsoft.AspNetCore.Identity;

namespace SecondTask.Models;
    public class User
    {
      public string? Username { get; set; }
      public string? Email { get; set; }
      public string? Password { get; set; }

    }

   public class LoginUser
   {
    public string Username { get; set; }

    public string Password { get; set; }

   }
   public class Token
   {
    public string? token { get; set; }
   }