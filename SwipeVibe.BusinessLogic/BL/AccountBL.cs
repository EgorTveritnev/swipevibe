using System;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.BL
{
    public class AccountBL : IUser
    {
        private readonly IUserRepository _repo;
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public AccountBL(IUserRepository repo, ISession session, IMapper mapper)
        {
            _repo = repo;
            _session = session;
            _mapper = mapper;
        }

        // Статический фабричный метод для создания экземпляра с настроенными зависимостями
        public static AccountBL CreateInstance()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>()
                   .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()));
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            });
            var mapper = mapperConfig.CreateMapper();

            var repo = new UserRepositoryBL();
            var session = new SessionBL();

            return new AccountBL(repo, session, mapper);
        }

        public int Register(UserRegister dto)
        {
            // Валидация email
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email обязателен");

            if (_repo.ByEmail(dto.Email) != null)
                throw new ArgumentException("Email уже используется");

            // Валидация пароля
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ArgumentException("Пароль должен содержать минимум 6 символов");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = Domain.Enums.Role.User,
                CreatedAt = DateTime.UtcNow,
                IsBlocked = false
            };

            _repo.Add(user);
            return user.Id;
        }

        public UserReturn Authenticate(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = _repo.ByEmail(email);
            if (user == null || user.Password != password || user.IsBlocked)
                return null;

            _session.SetUserId(user.Id);
            user.LastLogin = DateTime.Now;
            _repo.Update(user);

            return new UserReturn
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                RegisteredDate = user.CreatedAt,
                Role = user.Role.ToString(),
                IsBlocked = user.IsBlocked
            };
        }

        public void Logout(int userId)
        {
            _session.Clear();
        }

        public UserReturn GetById(int id)
        {
            var user = _repo.ById(id);
            return user != null ? _mapper.Map<UserReturn>(user) : null;
        }

        public System.Collections.Generic.IEnumerable<UserReturn> GetAllUsers()
        {
            return _repo.All().Select(_mapper.Map<UserReturn>);
        }

        public void UpdateProfile(int id, UserUpdate dto)
        {
            var user = _repo.ById(id) ?? throw new System.Collections.Generic.KeyNotFoundException("Пользователь не найден");

            // Валидация email при изменении
            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != user.Email)
            {
                if (_repo.ByEmail(dto.Email) != null)
                    throw new ArgumentException("Email уже используется");
                user.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                if (dto.NewPassword.Length < 6)
                    throw new ArgumentException("Пароль должен содержать минимум 6 символов");
                user.Password = dto.NewPassword;
            }

            _repo.Update(user);
        }

        public void GeneratePasswordResetCode(string email)
        {
            var user = _repo.ByEmail(email);
            if (user == null)
                throw new ArgumentException("Пользователь не найден");            user.ResetPasswordCode = Guid.NewGuid().ToString();
            user.ResetPasswordCodeExpiration = DateTime.UtcNow.AddHours(1);
            
            _repo.Update(user);
        }

        public void UpdateAvatar(int userId, string avatarUrl)
        {
            var user = _repo.ById(userId) ?? throw new System.Collections.Generic.KeyNotFoundException("Пользователь не найден");
            user.AvatarUrl = avatarUrl;
            _repo.Update(user);
        }
    }
}
