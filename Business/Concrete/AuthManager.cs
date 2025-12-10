using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helper;
using Core.Helper.Mail;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthDal _authDal;
        private readonly IMapper _mapper;

        private readonly string _jwtKey = "this_is_a_very_long_secret_key_12345";

        public AuthManager(IAuthDal authDal, IMapper mapper)
        {
            _authDal = authDal;
            _mapper = mapper;
        }

        public IDataResult<AuthLoginDTO> loginAuth(AuthLoginDTO authLoginDTO)
        {
            try
            {
                // 1. Find user by username (or email)
                Auth user = _authDal.Get(u => u.Username == authLoginDTO.Username && u.Status == 1);

                if (user == null)
                {
                    return new ErrorDataResult<AuthLoginDTO>(authLoginDTO, "İstifadəçi mövcud və ya aktiv deyil.");
                }

                // 2. Verify password
                bool isPasswordValid = PasswordHelper.VerifyPassword(authLoginDTO.Password, user.Password);
                if (!isPasswordValid)
                {
                    return new ErrorDataResult<AuthLoginDTO>(authLoginDTO, "Yanlış şifrə.");
                }

                return new SuccessDataResult<AuthLoginDTO>(authLoginDTO, "Uğurlu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return new ErrorDataResult<AuthLoginDTO>(authLoginDTO, $"Login failed. Error: {ex.Message}");
            }
        }

        public IDataResult<AuthLoginResponseDTO> loginAuth2(AuthLoginDTO authLoginDTO)
        {
            try
            {
                if (authLoginDTO == null || string.IsNullOrEmpty(authLoginDTO.Username) || string.IsNullOrEmpty(authLoginDTO.Password))
                {
                    return new ErrorDataResult<AuthLoginResponseDTO>(_mapper.Map<AuthLoginResponseDTO>(authLoginDTO), "Username and password are required.");
                }
                var user = _authDal.Get(u => u.Username == authLoginDTO.Username);

                if (user == null)
                    return new ErrorDataResult<AuthLoginResponseDTO>(_mapper.Map<AuthLoginResponseDTO>(authLoginDTO), "Username and password are required.");

                // 2. Verify password
                bool isPasswordValid = PasswordHelper.VerifyPassword(authLoginDTO.Password, user.Password);
                if (!isPasswordValid)
                {
                    return new ErrorDataResult<AuthLoginResponseDTO>(_mapper.Map<AuthLoginResponseDTO>(authLoginDTO), "Yanlış şifrə.");
                }

                // Create JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username)
                    }),

                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                AuthLoginResponseDTO authLoginResponseDTO = _mapper.Map<AuthLoginResponseDTO>(authLoginDTO);
                authLoginResponseDTO.Token = jwtToken;
                return new SuccessDataResult<AuthLoginResponseDTO>(authLoginResponseDTO);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use ILogger here)
                return new ErrorDataResult<AuthLoginResponseDTO>(_mapper.Map<AuthLoginResponseDTO>(authLoginDTO), "Yanlış.");
            }
        }

        public IDataResult<AuthRegisterDTO> registerAuth(AuthRegisterDTO authRegisterDTO)
        {
            try
            {
                string token = TokenHelper.GenerateVerificationToken();
                authRegisterDTO.Password = PasswordHelper.HashPassword(authRegisterDTO.Password);

                // Map DTO to entity
                Auth authEntity = _mapper.Map<Auth>(authRegisterDTO);
                authEntity.MailToken = token;

                // Add to database
                _authDal.Add(authEntity);

                // Create verification link
                string verificationLink = $"https://localhost:7176/api/auth/verify-email?token={Uri.EscapeDataString(token)}";

                // Send email
                MailHelper emailHelper = new MailHelper();
                emailHelper.SendVerificationEmail(authEntity.Email,
                    "Emailinizi təsdiqləyin",
                    $"Zəhmət olmazsa, <a href='{verificationLink}'>link</a>ə daxil olaraq mailinizi təsdiqləyin.");

                // Return success result
                return new SuccessDataResult<AuthRegisterDTO>(
                    authRegisterDTO,
                    "User registered successfully."
                );
            }
            catch (Exception ex)
            {
                // Log exception (you can use ILogger here if available)
                Console.WriteLine($"Error while registering user: {ex.Message}");

                // Return failure result
                return new ErrorDataResult<AuthRegisterDTO>(
                    authRegisterDTO,
                    $"User registration failed. Error: {ex.Message}"
                );
            }
        }

        public IResult verifyEmail(string token)
        {
            Auth user = _authDal.Get(u => u.MailToken == token);

            if (user == null)
                return new ErrorResult("Yanlış və ya vaxtı keçmiş token.");

            user.Status = 1; // Activate user
            user.MailToken = null; // Clear token
            _authDal.Update(user);

            return new SuccessResult("Email uğurla təsdiqləndi.");
        }
    }
}
