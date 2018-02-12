using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBU.WorkoutTracker.Core.ViewModels;

namespace NBU.WorkoutTracker.Core.Contracts
{
    public interface IAdmin
    {
        Task<List<UserViewModel>> GetUsers();

        Task CreateUser(CreateUserViewModel vm);
    }
}
