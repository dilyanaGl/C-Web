namespace SimpleMvc.App.Models
{
    using System.Collections.Generic;

    public class UsersProfileViewModel
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}