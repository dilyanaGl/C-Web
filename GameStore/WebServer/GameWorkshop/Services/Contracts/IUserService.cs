namespace HTTPServer.GameWorkshop.Services.Contracts
{
   public interface IUserService
   {
       bool Create(string email, string name, string password);
       bool IsAdmin(string email);
       bool Find(string email, string password);
   }
}
