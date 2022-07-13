using Domain.Model;
using Repository.Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    public class UserService : IUser
    {
        private IRepository<User> _UserRepository;
        public UserService(IRepository<User> userRepository)
        {
            _UserRepository = userRepository;
        }
        public User GetUser(int Id)
        {
            return _UserRepository.Get(Id);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _UserRepository.GetAll();
        }
        public bool InsertUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsActive = true;
            user.ModifiedDate = DateTime.Now;
            _UserRepository.Insert(user);
            return _UserRepository.SaveChanges();
        }
        public bool UpdateUser(User user)
        {
            var userObject = _UserRepository.Get(user.Id);
            if(userObject != null)
            {
                userObject.Email = user.Email;
                if (BCrypt.Net.BCrypt.Verify(user.Password, userObject.Password))
                {
                    userObject.Password = user.Password;
                }
                else
                {
                    userObject.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                
                userObject.ModifiedDate = DateTime.Now;
                _UserRepository.Update(userObject);
            }
            return _UserRepository.SaveChanges();
        }
        public bool DeleteUser(User user, bool isLogicalDelete = true)
        {
            if (isLogicalDelete)
            {
               var userObject = _UserRepository.Get(user.Id);
                if(userObject != null)
                {
                    userObject.IsActive = false;
                    _UserRepository.Update(userObject);
                }
            }
            else
            {
                _UserRepository.Delete(user);
            }

            return _UserRepository.SaveChanges();
        }
        public bool IsValidUser(string user, string password)
        {
            bool isValidUser = false;
            User userObject = _UserRepository.ByQuery(x => x.UserName == user && x.IsActive == true).FirstOrDefault();
            if(userObject != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, userObject.Password))
                {
                    isValidUser = true;
                }
            }

            return isValidUser;
        }
    }
}
