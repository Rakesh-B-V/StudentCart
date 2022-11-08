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
            var usersList = signUpCol.Aggregate().ToList();
            if (usersList.Select(s => s.UserName).Contains(userName))
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
        /// <summary>
        /// This method is used to Authenticate the user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<String> LogInProcess(String userName, String password)
        {
            var signUpCol = this._studentsCartRepo.GetCollectionIMongo<SignUp>();
            var usersList = signUpCol.Aggregate().ToList();
            if (usersList.Select(s => s.UserName).Contains(userName))
            {
                if (usersList.Where(s => s.UserName == userName).Any(s => s.Password == password))
                    return AppConstatnts.AUTHENTICATIONSUCCESSFUL;
                else
                    return AppConstatnts.AUTHENTICATIONFAILURE;
            }
            else
            {
                return AppConstatnts.AUTHENTICATIONFAILURE;
            }
        }
        /// <summary>
        /// This method is used to return Bicycle Details
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<List<Bicycles>> BicycleDetails(String productName)
        {
            var bicyclesCol = this._studentsCartRepo.GetCollectionIMongo<Bicycles>();
            var bicyclesList = bicyclesCol.Aggregate().ToList();
            return bicyclesList;
        }
        /// <summary>
        /// This method is used to return HouseItem Details
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<List<HouseHoldItems>> HouseItemDetails(String productName)
        {
            var houseHoldCol = this._studentsCartRepo.GetCollectionIMongo<HouseHoldItems>();
            var houseHoldList = houseHoldCol.Aggregate().ToList();
            return houseHoldList;
        }
        /// <summary>
        /// This method is used to return Accomodation Details
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<List<AccomodationServices>> AccomodationDetails(String productName)
        {
            var accomodationCol = this._studentsCartRepo.GetCollectionIMongo<AccomodationServices>();
            var accomodationsList = accomodationCol.Aggregate().ToList();
            return accomodationsList;
        }
        /// <summary>
        /// This method is used to return Books Details
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<List<Books>> BooksDetails(String productName)
        {
            var booksCol = this._studentsCartRepo.GetCollectionIMongo<Books>();
            var bookssList = booksCol.Aggregate().ToList();
            return bookssList;
        }
    }
}
