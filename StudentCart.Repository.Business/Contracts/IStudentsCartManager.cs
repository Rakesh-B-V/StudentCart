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
    }
}
