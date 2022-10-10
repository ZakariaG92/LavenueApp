using Lavenue.AuthenticationService.Model;
using Lavenue.AuthenticationService.Model.Dto;
using Lavenue.Services.Entities.Model;

namespace Lavenue.AuthenticationService.Service.Interfaces;

public interface IAccountService
{
     Task<bool> SignUp(UserDto user, string connectionType = Auth0ConnectionTypes.Credentials);
     JwToken? SignIn(User user);
}