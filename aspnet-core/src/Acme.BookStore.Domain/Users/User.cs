using System;
using Volo.Abp.Identity;

namespace Acme.BookStore.Users
{
    public class User : IdentityUser
    {
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
    }
}
