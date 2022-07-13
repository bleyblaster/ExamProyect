using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IUser
    {
        System.Collections.Generic.IEnumerable<Domain.Model.User> GetAllUsers();
        Domain.Model.User GetUser(int Id);
        bool InsertUser(Domain.Model.User user);
        bool UpdateUser(Domain.Model.User user);
        bool DeleteUser(Domain.Model.User user, bool isLogicalDelete = true);
        bool IsValidUser(string user, string password);
    }
}
