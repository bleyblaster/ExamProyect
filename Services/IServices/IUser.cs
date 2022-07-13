using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IUser
    {
        System.Collections.Generic.IEnumerable<Repository.Model.UserModel> GetAllUsers();
        Repository.Model.UserModel GetUser(int Id);
        bool InsertUser(Repository.Model.EditUserModel user);
        bool UpdateUser(Repository.Model.EditUserModel user);
        bool DeleteUser(Repository.Model.UserModel user, bool isLogicalDelete = true);
        bool IsValidUser(string user, string password);
    }
}
