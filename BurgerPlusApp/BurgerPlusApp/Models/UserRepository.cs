using System;
using System.Collections.Generic;
using System.Linq;
using BurgerPlusApp.Models;
using BurgerPlusApp.Utils;

namespace BurgerPlusApp.Data
{
    public class UserRepository
    {
        private List<User> _пользователи;
        private int _следующийId;

        public UserRepository()
        {
            _пользователи = FileHelper.Загрузить();

            if (_пользователи.Count > 0)
            {
                _следующийId = _пользователи.Max(u => u.Id) + 1;
            }
            else
            {
                _следующийId = 1;
            }
        }

        public List<User> GetAll()
        {
            return _пользователи;
        }

        public User GetByLogin(string login)
        {
            return _пользователи.FirstOrDefault(u => u.Login == login);
        }

        public void Add(User user)
        {
            if (LoginExists(user.Login))
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }

            user.Id = _следующийId++;
            _пользователи.Add(user);
            FileHelper.Сохранить(_пользователи);
        }

        public void Update(User user)
        {
            var существующий = _пользователи.FirstOrDefault(u => u.Id == user.Id);
            if (существующий == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (_пользователи.Any(u => u.Login == user.Login && u.Id != user.Id))
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }

            существующий.Login = user.Login;
            существующий.Password = user.Password;
            существующий.Role = user.Role;
            существующий.IsBlocked = user.IsBlocked;

            FileHelper.Сохранить(_пользователи);
        }

        public void Block(int userId)
        {
            var пользователь = _пользователи.FirstOrDefault(u => u.Id == userId);
            if (пользователь != null)
            {
                пользователь.IsBlocked = true;
                FileHelper.Сохранить(_пользователи);
            }
        }

        public void Unblock(int userId)
        {
            var пользователь = _пользователи.FirstOrDefault(u => u.Id == userId);
            if (пользователь != null)
            {
                пользователь.IsBlocked = false;
                FileHelper.Сохранить(_пользователи);
            }
        }

        public bool LoginExists(string login)
        {
            return _пользователи.Any(u => u.Login == login);
        }
    }
}