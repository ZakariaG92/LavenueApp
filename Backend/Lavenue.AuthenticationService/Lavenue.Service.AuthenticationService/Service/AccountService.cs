using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Lavenue.AuthenticationService.Model;
using Lavenue.AuthenticationService.Model.Dto;
using Lavenue.AuthenticationService.Service.Interfaces;
using Lavenue.Service.Common.Model;
using Lavenue.Services.Entities.Model;
using Microsoft.IdentityModel.Tokens;

namespace Lavenue.AuthenticationService.Service;

public class AccountService : IAccountService
{

    private readonly IJwtSettings _jwtSettings;

    public AccountService( IJwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
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
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(nameof(user.FirstName), user.FirstName),
                new Claim(nameof(user.LastName), user.LastName),
                new Claim(nameof(user.Email), user.Email),
                new Claim(nameof(user.Id), user.Id),
                new Claim(nameof(user.PhoneNumber), user.PhoneNumber)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwToken { Token = tokenHandler.WriteToken(token)} ;
        
    }

    #endregion
  
}