using BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IUserBAL
    {
        List<UserModel> Get();
        bool Add(UserModel userModel);
        bool Update(UserModel userModel);
        bool Delete(int? Id);
        UserModel FindValidUser(string UserName, string Password);
    }
}
