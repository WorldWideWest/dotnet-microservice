using Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendConfirmationEmail(User user);
        public Task<User> FindUserByEmail(string email);
    }
}
