﻿namespace API.Interfaces;
public interface ITokenService
{
    string CreateToken(UserEntity user);
}
