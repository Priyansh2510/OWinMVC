using BAL.Common;
using BAL.Interface;
using BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class UserBAL : IUserBAL
    {
        private DBConnection  _dBConnection= null;

        public UserBAL(DBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }

        public bool Add(UserModel userModel)
        {
            bool Result = false;
            try
            {
                string Query = $"INSERT INTO UserMaster (UserName,Password,FirstName,LastName,DOB,IsActive,CreatedOn) VALUES ('{userModel.UserName}','{userModel.Password}','{userModel.FirstName}','{userModel.LastName}','{userModel.DOB}',1,GETDATE())";
                Result = _dBConnection.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Result;
        }

        public bool Delete(int? Id)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> Get()
        {
            List<UserModel> userModels = new List<UserModel>();
            try
            {
                string Query = $"SELECT UserId,UserName,Password,FirstName,LastName,DOB From UserMaster Where IsActive = 1";
                DataTable dt =_dBConnection.ExecuteQuery(Query);

                userModels = Helper.ConvertDataTableToList<UserModel>(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
            return userModels;

        }

        public bool Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public UserModel FindValidUser(string UserName , string Password)
        {
            UserModel userModel = new UserModel();
            try
            {
                string Query = $"SELECT Top 1 UserId,UserName,Password,FirstName,LastName,DOB From UserMaster Where UserName = '{UserName}' and Password = '{Password}' and IsActive = 1";
                DataTable dt = _dBConnection.ExecuteQuery(Query);

                List<UserModel> userModels = Helper.ConvertDataTableToList<UserModel>(dt);

                if (userModels.Count > 0)
                {
                    userModel = userModels[0];
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return userModel;
        }
    }
}
