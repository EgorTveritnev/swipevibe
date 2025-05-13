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
        private readonly IMapper _m;

        public AdminApi(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _m = mapper;
        }

        public IEnumerable<UserReturn> GetAllUsers() =>
            _repo.All().Select(u => _m.Map<UserReturn   >(u));

        public void Block(int id)
        {
            var u = _repo.ById(id);
            if (u != null) { u.IsBlocked = true; _repo.Update(u); }
        }

        public void Unblock(int id)
        {
            var u = _repo.ById(id);
            if (u != null) { u.IsBlocked = false; _repo.Update(u); }
        }

        public void ChangeRole(int id, Role newRole)
        {
            var u = _repo.ById(id);
            if (u != null) { u.Role = newRole; _repo.Update(u); }
        }
    }
}