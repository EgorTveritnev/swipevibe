using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Core
{
    public class UserApi : IUser
    {
        private readonly IUserRepository _repo;
        private readonly ISession _session;
        private readonly IMapper _m;

        public UserApi(IUserRepository repo, ISession session, IMapper mapper)
        {
            _repo = repo;
            _session = session;
            _m = mapper;
        }

        public UserReturn Authenticate(string email, string password)
        {
            var user = _repo.ByEmail(email);
            if (user == null || user.Password != password || user.IsBlocked)
                return null;

            _session.SetUserId(user.Id);

            // 🔥 РУЧНОЙ маппинг вместо AutoMapper
            return new UserReturn
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                RegisteredDate = user.CreatedAt,
                Role = user.Role,
                IsBlocked = user.IsBlocked
            };
        }

        public void Logout(int userId)
        {
            _session.Clear();
        }

        public int Register(UserRegister dto)
        {
            if (_repo.ByEmail(dto.Email) != null)
                throw new ArgumentException("Email уже используется");

            var user = new User
            {
                Id = _repo.NextId(),
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = Role.User,
                CreatedAt = DateTime.UtcNow,
                IsBlocked = false
            };

            _repo.Add(user);
            return user.Id;
        }

        public UserReturn GetById(int id) => _m.Map<UserReturn>(_repo.ById(id));
        public IEnumerable<UserReturn> GetAllUsers() => _repo.All().Select(_m.Map<UserReturn>);

        public void UpdateProfile(int id, UserUpdate dto)
        {
            var user = _repo.ById(id) ?? throw new KeyNotFoundException();

            if (!string.IsNullOrWhiteSpace(dto.Username)) user.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.NewPassword)) user.Password = dto.NewPassword;

            _repo.Update(user);
        }
    }
}