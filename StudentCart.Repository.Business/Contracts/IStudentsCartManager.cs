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
    }
}
