namespace SimpleMvc.App.Models
{
    using System.Collections.Generic;

    public class AllUsernamesViewModel
    {
        public ICollection<UsernameViewModel> Usernames { get; set; } = new List<UsernameViewModel>();
    }
}