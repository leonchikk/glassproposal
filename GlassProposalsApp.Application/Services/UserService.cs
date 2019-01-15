using AutoMapper;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Models.Users;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ProfileResponseModel GetUserProfileData(Guid userId)
        {
            var userInfo = _unitOfWork.Users.GetById(userId);
            return _mapper.Map<Users, ProfileResponseModel>(userInfo);
        }
    }
}
