using System.Collections.Generic;

namespace SimpleMvc.App.ViewModels
{
    public class AllUsernamesViewModel
    {
        public AllUsernamesViewModel()
        {
            this.Usernames = new List<UserViewModel>();
        }

        public ICollection<UserViewModel> Usernames { get; set; } 
    }
}