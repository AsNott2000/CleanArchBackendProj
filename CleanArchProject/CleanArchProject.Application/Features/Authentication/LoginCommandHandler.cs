using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchProject.Application.Authentication;

public class LoginCommandHandler(IConfiguration config) : ICommandQueryHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        var isValidUser = false;

        await Task.Run(() =>
            {
                isValidUser = IsValidUser(request);
            }, cancellationToken);

        if (!isValidUser)
        {
            throw new NotFoundException(BuildingBlocks.Resources.Messages.NotFound);
        }

        var token = string.Empty;
        await Task.Run(() =>
        {
            token = GenerateToken(request.userName);
        }, cancellationToken);

        result.AddValue(token);
        result.OK();
        return result;
    }

    private static bool IsValidUser(LoginCommand request)
    {
        //girilen user ve password ile eşleşen bir kullanıcı kontrolü yapılabilir.
        //buraya db den kontrol etme eklenmeli
        //user tablosundan kontrol yapacak
        if (request.userName == "MyUsername" && request.password == "MyPassword")
        {
            return true;
        }
        return false;
    }

    private string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "AlternativeKey"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // SADECE gerekli claim’i ekle (ör. kullanıcı adı)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username)
        };

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
