namespace Administration.Host.Models.Account
{
    public class RegistrationUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public RegistrationUserViewModel() { }
        public RegistrationUserViewModel(string name, string surname, string email, string password, DateTime birthday)
        {
            Name = name;
            Email = email;
            Password = password;
            Surname = surname;
            Birthday = birthday;
        }
    }
}