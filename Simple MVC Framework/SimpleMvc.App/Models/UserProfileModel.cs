namespace SimpleMvc.App.Models
{
    using System.Collections.Generic;

    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}