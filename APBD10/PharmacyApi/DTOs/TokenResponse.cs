using System.IdentityModel.Tokens.Jwt;

namespace PharmacyApi.DTOs;

public record TokenResponse(string accessToken, string refreshToken);