using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Models.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(GlassProposalContext context)
             : base(context)
        {

        }

        public Users CreateUser(UserViewModel model)
        {
            var user = new Users
            {
                Id = Guid.NewGuid(),
                BonusBalance = model.BonusBalance,
                Email = model.Email,
                MentorId = model.MentorId,
                Name = model.Name,
                Password = model.Password,
                SecurityLevel = model.SecurityLevel,
                UserLevel = model.UserLevel,
                VacationDaysLeft = model.VacationDaysLeft
            };
            
            foreach (var userType in model.UserTypes)
            {
                var userTypeEntity = new UserTypes
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    UserType = userType
                };

                user.UserTypes.Add(userTypeEntity);
                Db.UserTypes.Add(userTypeEntity);
            }
            
            Db.Users.Add(user);

            return user;
        }

        public IQueryable<Users> GetAll()
        {
            return Db.Users.AsNoTracking();
        }

        public Users GetById(Guid id)
        {
            return Db.Users.Include(user => user.Bonuses)
                           .Include(user => user.Dislikes)
                           .Include(user => user.Likes)
                           .Include(user => user.Mentor)
                           .Include(user => user.Vacations)
                           .Include(user => user.UserTypes)
                           .AsNoTracking()
                           .FirstOrDefault(user => user.Id == id);
        }

        public IQueryable<Users> GetDecisionMakersForFirstStage(int processType)
        {
            var processFirstStage = Db.Processes.Include(p => p.Stages)
                                                .ThenInclude(s => s.StageReceivers)
                                                .AsNoTracking()
                                                .FirstOrDefault(p => p.ProcessType == processType) ?
                                                .Stages
                                                .First();

            if (processFirstStage == null)
                return Enumerable.Empty<Users>().AsQueryable();

            var receiversTypes = processFirstStage.StageReceivers.Select(s => s.ReceiverType);
            var decisionMakers = Db.Users.Include(user => user.UserTypes).Where(u => u.UserTypes.Any(x => receiversTypes.Contains(x.UserType)));

            return decisionMakers;
        }

        public IQueryable<Users> GetDecisionMakersForNextStage(Guid proposalId)
        {
            var proposalNextStage = Db.Proposals.Include(proposal => proposal.CurrentStage)
                                                .ThenInclude(stage => stage.NextStage)
                                                .ThenInclude(s => s.StageReceivers)
                                                .FirstOrDefault(proposal => proposal.Id == proposalId)?
                                                .CurrentStage
                                                .NextStage;

            if (proposalNextStage == null)
                return Enumerable.Empty<Users>().AsQueryable();

            var receiversTypes = proposalNextStage.StageReceivers.Select(s => s.ReceiverType);
            var decisionMakers = Db.Users.Include(user => user.UserTypes).Where(u => u.UserTypes.Any(x => receiversTypes.Contains(x.UserType)));

            return decisionMakers;
        }

        public Users Update(Guid id, UserViewModel model)
        {
            var user = Db.Users.FirstOrDefault(u => u.Id == id);

            user.Name = model.Name;
            user.Password = model.Password;
            user.SecurityLevel = model.SecurityLevel;
            user.MentorId = model.MentorId;
            user.UserLevel = model.UserLevel;
            user.VacationDaysLeft = model.VacationDaysLeft;
            user.BonusBalance = model.BonusBalance;
            user.Email = model.Email;

            var userTypes = Db.UserTypes.Where(x => x.UserId == id);
            Db.UserTypes.RemoveRange(userTypes);
            Db.SaveChanges();

            foreach (var userType in model.UserTypes)
            {
                var userTypeEntity = new UserTypes
                {
                    Id = Guid.NewGuid(),
                    UserId = id,
                    UserType = userType
                };

                user.UserTypes.Add(userTypeEntity);
                Db.UserTypes.Add(userTypeEntity);
            }
            
            Db.Users.Update(user);
            return user;
        }
    }
}
