namespace HTTPServer.Security
{
   public static class Session
   {
        public static User User { get; set; }

       public static bool IsUserLoggedIn()
       {
           return User != null;
       }


    }
}
