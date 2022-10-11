using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lavenue.AuthenticationService.Model;
using Lavenue.AuthenticationService.Model.Dto;
using Lavenue.AuthenticationService.Service.Interfaces;
using Lavenue.Service.Common.Model;
using Lavenue.Service.Entities.Model;
using Lavenue.Service.Infrastructure.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Lavenue.AuthenticationService.Service;

public class AccountService : IAccountService
{
    private readonly IJwtSettings _jwtSettings;
    private readonly IMongoDbRepository<User> _userDbRepository;

    public AccountService(IJwtSettings jwtSettings, IMongoDbRepository<User> userDbRepository)
    {
        _jwtSettings = jwtSettings;
        _userDbRepository = userDbRepository;
    }

    public async Task<bool> SignUp(UserDto user, string connectionType = Auth0ConnectionTypes.Credentials)
    {
        // user.Connection = connectionType;
        //  _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //
        // var stringData = JsonSerializer.Serialize(user);
        // var stringContent = new StringContent(stringData, Encoding.UTF8,MediaTypeNames.Application.Json);
        // var response = await _httpClient.PostAsync("dbconnections/signup", stringContent);
        // return response.IsSuccessStatusCode;
        return true;
    }

    public JwToken? SignIn(User user)
    {
        return GenerateToken(user);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var result = await _userDbRepository.GetAll();
        return result;
    }

    public async Task InsertUser(User user)
    {
        await _userDbRepository.InsertUser(user);
    }

    #region Private methods

    private static async Task<string> ParseResponseAsString(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode) return content;
        var error = $"Failed to register Api Response: {response}";
        throw new HttpRequestException(error, null, response.StatusCode);
    }

    private JwToken GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new(nameof(user.FirstName), user.FirstName),
                new Claim(nameof(user.LastName), user.LastName),
                new Claim(nameof(user.Email), user.Email),
                new Claim(nameof(user.Id), user.Id),
                new Claim(nameof(user.PhoneNumber), user.PhoneNumber)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwToken { Token = tokenHandler.WriteToken(token) };
    }

    #endregion
}