using Amazon.Runtime;
using GetAwaitService.DB.Models;
using GetAwaitService.Services.GetAwaitService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Auth.JWT.Service
{
    public class AuthServiceJWT : IAuthServiceJWT
    {
        private readonly IConfiguration _configuration;
        private readonly IUserTelegramInformationService _userTelegramInformationService;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthServiceJWT(IConfiguration configuration, 
            IUserTelegramInformationService userTelegramInformationService,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _userTelegramInformationService = userTelegramInformationService;
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
        }

        public async Task<string> Login(ulong userTelegramId)
        {
            var userTelegramResultSearch = await SearchUser(userTelegramId);
            if (userTelegramResultSearch == null)
            {
                var userInstanceCreateDTO = CreateInstanceUserCreteDTO(userTelegramId);

                var userResultCreate = await CreateUserAsync(userInstanceCreateDTO);

                var userTG = await CreateUserTG(userTelegramId, userResultCreate);

                return await GenerateJWT(userResultCreate.Id, userTelegramId);
            }

            return await GenerateJWT(userTelegramResultSearch.UserId, userTelegramId);
        }

        private UserCreateDTO CreateInstanceUserCreteDTO(ulong userTelegramId)
        {
            var userDTO = new UserCreateDTO
            {
                Name = "UserDefaultName",
                DateBirth = null,   
                Login = "UserDefaultLogin_" + userTelegramId.ToString() ,
                ImageType = "jpg",
                Image = DefaultImgAndCont.GetImg(),
                Content = "Default"
            };
            return userDTO;
        }


        public async Task<UserTelegramInformation> SearchUser(ulong userTelegramId)
        {
            var userTG = _userTelegramInformationService.GetUserTgInfoFromTgId(userTelegramId);

            return await userTG;
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO userDto)
        {
            var jsonContent = JsonSerializer.Serialize(userDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/User/CreateUser", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserDTO>(responseBody, _jsonOptions);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка при создании пользователя. StatusCode: {response.StatusCode}, Response: {errorResponse}");
        }

        public async Task<UserTelegramInformation> CreateUserTG(ulong userTelegramId, UserDTO userDTO)
        {
            // Создаем экземпляр класса UserTelegramInformation
            var userTelegramInfo = new UserTelegramInformation
            {
                UserId = userDTO.Id,
                TelegramId = userTelegramId, 
                DateCreate = DateTime.UtcNow,
                IsDeleted = false,
                DateDeleted = null
            };

            await _userTelegramInformationService.AddAsync(userTelegramInfo);

            return userTelegramInfo;
        }

        public async Task<string> GenerateJWT(Guid userId, ulong userTelegramId)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

            var claims = new List<Claim>
            {
                new Claim("userId", userId.ToString()),
                new Claim("userTelegramId", userTelegramId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, "user"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
