using StudentCart.Repository.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCart.Repository.Business.Contracts
{
    public interface IStudentsCartManager
    {
        Task<List<String>> GetCategoriesList();
        Task<String> SignUpProcess(String userName, String password);
        Task<String> LogInProcess(String userName, String password);
        Task<List<Bicycles>> BicycleDetails(String product);
        Task<List<HouseHoldItems>> HouseItemDetails(String product);
        Task<List<AccomodationServices>> AccomodationDetails(String product);
        Task<List<Books>> BooksDetails(String product);

        Task<String> AddBook(Dictionary<String, String> book);
        Task<String> AddBicycle(Dictionary<String, String> bicycle);
        Task<String> AddHouseHoldItems(Dictionary<String, String> houseHoldItem);
        Task<String> AddAccomodationService(Dictionary<String, String> accomodationServices);


        Task<String> EditAccomodationService(String ownerNo, String itemType, String price, String category, String newContactNo);
        Task<String> EditHouseHoldItems(String ownerNo, String itemType, String price, String category, String newContactNo);
        Task<String> EditBicycle(String ownerNo, String itemType, String price, String category, String newContactNo);
        Task<String> EditBook(String ownerNo, String itemType, String price, String category, String newContactNo);


        Task<String> DeleteAccomodationService(String ownerNo, String itemType, String category);
        Task<String> DeleteHouseHoldItems(String ownerNo, String itemType, String category);
        Task<String> DeleteBicycle(String ownerNo, String category);
        Task<String> DeleteBook(String ownerNo, String itemType, String category);
        Task<String> LogOutProcess(String userName, String password);


    }
}
