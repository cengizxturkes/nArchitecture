using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<LoginCommand, LoginDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                //await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForLoginDto.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForLoginDto.Password, out passwordHash, out passwordSalt);




                User? loginedUser = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);


                if (loginedUser == null) throw new Exception("Maile ait kullanıcı yok");

                if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, loginedUser.PasswordHash, loginedUser.PasswordSalt))
                    throw new Exception("Hatalı kullanıcı adı veya şifre ");



                AccessToken createdAccessToken = await _authService.CreateAccessToken(loginedUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(loginedUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoginDto loginDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };
                return loginDto;

            }
        }
    }

}
