//using ReviewYourMovie.Server.Context;
//using

//namespace ReviewYourMovie.Server.Services
//{
//    public class UserService
//    {
//        private readonly UserContext _context;

//        public UserService(UserContext context)
//        {
//            _context = context;
//        }

//        public List<User> Get() =>
//            _context.Users.Find(user => true).ToList();

//        public User Get(string id) =>
//            _users.Find<User>(user => user.Id == id).FirstOrDefault();

//        public User Create(User user)
//        {
//            _users.InsertOne(user);
//            return user;
//        }

//        public Tokens Login(Authentication authentication)
//        {
//            User user = _users.Find<User>(u => u.Username == authentication.Username).FirstOrDefault();

//            bool validPassword = user.Password == authentication.Password;

//            if (validPassword)
//            {
//                var refreshToken = TokenManager.GenerateRefreshToken(user);

//                if (user.RefreshTokens == null)
//                    user.RefreshTokens = new List<string>();

//                user.RefreshTokens.Add(refreshToken.refreshToken);

//                _users.ReplaceOne(u => u.Id == user.Id, user);

//                return new Tokens
//                {
//                    AccessToken = TokenManager.GenerateAccessToken(user),
//                    RefreshToken = refreshToken.jwt
//                };
//            }
//            else
//            {
//                throw new System.Exception("Username or password incorrect");
//            }
//        }

//        public Tokens Refresh(Claim userClaim, Claim refreshClaim)
//        {
//            User user = _users.Find<User>(x => x.Username == userClaim.Value).FirstOrDefault();

//            if (user == null)
//                throw new System.Exception("User doesn't exist");

//            if (user.RefreshTokens == null)
//                user.RefreshTokens = new List<string>();

//            string token = user.RefreshTokens.FirstOrDefault(x => x == refreshClaim.Value);

//            if (token != null)
//            {
//                var refreshToken = TokenManager.GenerateRefreshToken(user);

//                user.RefreshTokens.Add(refreshToken.refreshToken);

//                user.RefreshTokens.Remove(token);

//                _users.ReplaceOne(u => u.Id == user.Id, user);

//                return new Tokens
//                {
//                    AccessToken = TokenManager.GenerateAccessToken(user),
//                    RefreshToken = refreshToken.jwt
//                };
//            }
//            else
//            {
//                throw new System.Exception("Refresh token incorrect");
//            }
//        }

//        public void Update(string id, User userIn) =>
//            _users.ReplaceOne(user => user.Id == id, userIn);

//        public void Remove(User userIn) =>
//            _users.DeleteOne(user => user.Id == userIn.Id);

//        public void Remove(string id) =>
//            _users.DeleteOne(user => user.Id == id);
//    }
//}
//}
