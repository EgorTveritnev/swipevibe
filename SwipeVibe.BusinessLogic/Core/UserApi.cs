using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Core
{
    /// <summary>
    /// Реализация IUser поверх абстрактного репозитория.
    /// Не зависит от того, где реально лежат данные
    /// (in‑memory, EF, Dapper — всё равно).
    /// </summary>
    public sealed class UserApi : IUser
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

        /* ---------- REGISTRATION ---------- */

        public int Register(UserRegister dto)
        {
            if (_repo.ByEmail(dto.Email) != null)
                throw new ArgumentException("E‑mail уже используется");

            var user = new User
            {
                Id = _repo.NextId(),
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password   // ⬅️ сохраняем строку как есть
            };

            _repo.Add(user);
            return user.Id;
        }

        /* ---------- LOGIN ---------- */

        public UserReturn Login(UserLogin dto)
        {
            var user = _repo.ByEmail(dto.Email)
                       ?? throw new UnauthorizedAccessException("Нет такого пользователя");

            if (user.IsBlocked)
                throw new UnauthorizedAccessException("Аккаунт заблокирован");

            // ⬇️ раньше Verify через BCrypt; теперь простое сравнение
            if (user.Password != dto.Password)
                throw new UnauthorizedAccessException("Неверный пароль");

            user.LastLogin = DateTime.UtcNow;

            var token = _session.Create(user.Id);    // можно вернуть в контроллер
            _repo.Update(user);

            return _m.Map<UserReturn>(user);
        }

        public void Logout(int userId) { /* noop */ }

        /* ---------- READ ---------- */
        public UserReturn GetById(int id) => _m.Map<UserReturn>(_repo.ById(id));
        public IEnumerable<UserReturn> GetAll() => _repo.All().Select(_m.Map<UserReturn>);

        /* ---------- UPDATE PROFILE ---------- */

        public void UpdateProfile(int id, UserUpdate dto)
        {
            var u = _repo.ById(id) ?? throw new KeyNotFoundException();

            if (!string.IsNullOrWhiteSpace(dto.Username)) u.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Email)) u.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                u.Password = dto.NewPassword;             // ⬅️ простая замена

            _repo.Update(u);
        }
    }
}
