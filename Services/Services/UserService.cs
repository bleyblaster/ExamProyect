using AutoMapper;
using Domain.Model;
using Repository.Repository;
using Repository.Model;
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
        private IMapper _mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _UserRepository = userRepository;
            _mapper = mapper;
        }
        public UserModel GetUser(int Id)
        {
            var userEntity = _UserRepository.Get(Id);
            var userModel = _mapper.Map<UserModel>(userEntity);
            return userModel;
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            var userEntity = _UserRepository.GetAll();
            var userModel = _mapper.Map<List<UserModel>>(userEntity);
            return userModel;
        }
        public bool InsertUser(EditUserModel user)
        {
            var userEntity = _mapper.Map<User>(user);

            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);
            userEntity.IsActive = true;
            userEntity.ModifiedDate = DateTime.Now;
            _UserRepository.Insert(userEntity);
            return _UserRepository.SaveChanges();
        }
        public bool UpdateUser(EditUserModel user)
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
            return userObject != null && _UserRepository.SaveChanges();
        }
        public bool DeleteUser(UserModel user, bool isLogicalDelete = true)
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
                var userEntity = _mapper.Map<User>(user);
                _UserRepository.Delete(userEntity);
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
