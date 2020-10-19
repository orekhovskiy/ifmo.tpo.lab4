namespace ChatApp.Services
{
    public static class UserService
    {
        private static string _login;

        public static string GetLogin() => _login;
        public static void SetLogin(string login) => _login = login;
    }
}
