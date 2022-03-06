﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewYourMovie.Server.Context;
using ReviewYourMovie.Server.Models;
using ReviewYourMovie.Server.Managers;
using System.Security.Claims;

namespace ReviewYourMovie.Server.Services
{
    public class UserService
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }

        private static User userToDb;

        public List<User> Get() =>
            _context.Users.Where(user => true).ToList();

        public User Get(int id) =>
            _context.Users.FirstOrDefault<User>(user => user.UserId == id);

        public async Task<string> GetUsername(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync<User>(user => user.UserId == id);
            //var user = await _context.Users.Where(user => user.UserId == id).FirstOrDefaultAsync();
            return user.Username;

        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public async Task<ActionResult<string>> Register(UserLoginDto login)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(login.Password);

            userToDb = new()
            {
                Username = login.Username,
                Password = passwordHash,
                LastLogonTime = DateTime.Now,
                UserRoleId = 3,
                RegisterTime = DateTime.Now,
                Token = String.Empty,
                FirstName = String.Empty,
                LastName = String.Empty,
                EmailAddress = String.Empty,
            };

            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Username == login.Username);

            if (userFromDb != null)
            {
                throw new System.Exception("Username is taken");
            }

            userToDb.RegisterComplete = true;

            await _context.Users.AddAsync(userToDb);

            await _context.SaveChangesAsync();

            return "Registered";

        }

        public async Task<ActionResult<string>> Login(UserLoginDto authentication)
        {
            var user = await _context.Users.FirstOrDefaultAsync<User>(u => u.Username == authentication.Username);

            bool validPassword = BCrypt.Net.BCrypt.Verify(authentication.Password, user.Password);

            if (validPassword)
            {
                var refreshToken = TokenManager.GenerateRefreshToken(user);

                user.Token.Remove(0);

                user.Token = refreshToken.refreshToken;

                await _context.SaveChangesAsync();

                var token = TokenManager.GenerateAccessToken(user);

                return token;

                //return new Tokens
                //{
                //    AccessToken = TokenManager.GenerateAccessToken(user),
                //    RefreshToken = refreshToken.jwt
                //};
            }
            else
            {
                throw new System.Exception("Username or password incorrect");
            }
        }

        public async Task<ActionResult<Tokens>> Refresh(Claim userClaim, Claim refreshClaim)
        {
            User user = await _context.Users.FirstOrDefaultAsync<User>(x => x.Username == userClaim.Value);

            if (user == null)
                throw new System.Exception("User doesn't exist");

            string token = user.Token = refreshClaim.Value;

            if (token != null)
            {
                var refreshToken = TokenManager.GenerateRefreshToken(user);

                user.Token.Remove(0);

                user.Token = refreshToken.refreshToken;

                await _context.SaveChangesAsync();

                return new Tokens
                {
                    AccessToken = TokenManager.GenerateAccessToken(user),
                    RefreshToken = refreshToken.jwt
                };
            }
            else
            {
                throw new System.Exception("Refresh token incorrect");
            }
        }

        //public void Update(string id, User userIn) =>
        //    _context.Users.ReplaceOne(user => user.Id == id, userIn);

        public void Update(int id, User userIn)
        {
            var user = _context.Users.Where(user => user.UserId == Convert.ToInt32(id));
            //_context.Users.Update(userIn);
            _context.Remove(user);
            _context.Add(userIn);
        }

        public void Remove(User userIn)
        {
            var user = _context.Users.Where(user => user.UserId == userIn.UserId);
            _context.Users.Remove((User)user);
        }

        public void Remove(int id)
        {
            var user = _context.Users.Where(user => user.UserId == id);
            _context.Users.Remove((User)user);
        }
    }
}

