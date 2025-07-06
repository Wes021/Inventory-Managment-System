using Inventory.DTO_S.Account;

namespace Inventory.Models.IRepositories
{
    public interface IAccountRepository
    {
        Task<bool> RegesterUser(NewUserRegestrationDTO newUserRegestrationDTO/*, string role*/);


        Task<AuthResponseDTO> LogIn(LoginDTO loginDTO);
    }
}
