using System.Collections.Generic;

namespace WebSite.ViewModels.Users
{
    public class UsersListViewModel
    {
        public UsersListViewModel(IEnumerable<UserViewModel> users)
        {
            Users = users;
        }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}