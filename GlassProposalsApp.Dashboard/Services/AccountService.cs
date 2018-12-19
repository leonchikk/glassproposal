using AutoMapper;
using GlassProposalsApp.Dashboard.Interfaces;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Models.ViewModels.Dashboard;
using GlassProposalsApp.Data.ReponseModels.Dashboard;
using GlassProposalsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Dashboard.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserResponseModel CreateUser(UserViewModel model)
        {
            var user = _unitOfWork.Users.CreateUser(model);
            _unitOfWork.Save();

            return _mapper.Map<Users, UserResponseModel>(user);
        }

        public void DeleteUser(Guid id)
        {
            _unitOfWork.Users.Remove(id);
            _unitOfWork.Save();
        }

        public IEnumerable<UserResponseModel> GetAllUsers()
        {
            var users = _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<Users>, IEnumerable<UserResponseModel>>(users);
        }

        public UserResponseModel GetUserById(Guid id)
        {
            var user = _unitOfWork.Users.GetById(id);
            return _mapper.Map<Users, UserResponseModel>(user);
        }

        public UserResponseModel UpdateUser(Guid id, UserViewModel model)
        {
            var user = _unitOfWork.Users.Update(id, model);
            return _mapper.Map<Users, UserResponseModel>(user);
        }
    }
}
