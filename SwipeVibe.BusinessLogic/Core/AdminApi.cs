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
    /// Примитивная реализация IAdmin.
    /// Работает с тем же репозиторием, что и UserApi.
    /// </summary>
    public sealed class AdminApi : IAdmin
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public AdminApi(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IEnumerable<UserReturn> GetAllUsers()
            => _repo.All().Select(u => _mapper.Map<UserReturn>(u));

        public void Block(int id)
        {
            var user = _repo.ById(id);
            if (user != null)
                user.IsBlocked = true;
        }

        public void Unblock(int id)
        {
            var user = _repo.ById(id);
            if (user != null)
                user.IsBlocked = false;
        }

        public void SetRole(int id, Role newRole)
        {
            var user = _repo.ById(id);
            if (user != null)
            {
                user.Role = newRole;
                _repo.Update(user); // важно сохранить изменения в БД
            }
        }
    }
}