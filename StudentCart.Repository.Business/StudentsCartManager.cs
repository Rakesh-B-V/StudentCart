using MongoDB.Driver;
using StudentCart.Repository.Business.Contracts;
using StudentCart.Repository.Business.Models;
using StudentCart.Repository.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCart.Repository.Business
{
    public class StudentsCartManager : IStudentsCartManager
    {
        IStudentsCartRepository _studentsCartRepo;
        public StudentsCartManager(IStudentsCartRepository studentsCartRepository)
        {
            this._studentsCartRepo = studentsCartRepository;
        }

        /// <summary>
        /// This method is used to Get All the categories available.
        /// </summary>
        /// <returns></returns>
        public async Task<List<String>> GetCategoriesList()
        {
            var categoryCol = this._studentsCartRepo.GetCollectionIMongo<Categories>();
            var categoryList = categoryCol.Aggregate().ToList().SelectMany(s => s.CategoryItems).ToList();

            return categoryList;
        }

        /// <summary>
        /// This method is used to create new user Account.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<String> SignUpProcess(String userName, String password)
        {
            var signUpCol = this._studentsCartRepo.GetCollectionIMongo<SignUp>();
            var categoryList = signUpCol.Aggregate().ToList();
            if (categoryList.Select(s => s.UserName).Contains(userName))
            {
                return AppConstatnts.DUPLICATEUSER;
            }
            else
            {
                await signUpCol.InsertOneAsync(new SignUp()
                {
                    UserName = userName,
                    Password = password
                });
                return AppConstatnts.ACCOUNTCREATED;
            }
        }
        public async Task<String> LogInProcess(String userName, String password)
        {
            var signUpCol = this._studentsCartRepo.GetCollectionIMongo<SignUp>();
            var categoryList = signUpCol.Aggregate().ToList();
            if (categoryList.Select(s => s.UserName).Contains(userName))
            {
                if (categoryList.Where(s => s.UserName == userName).Any(s => s.Password == password))
                    return "Authentication Successful";
                else
                    return "Incorrect UserName or Password";
            }
            else
            {
                return "Incorrect UserName or Password";
            }
        }
    }
}
