namespace Catalog.Data
{
    public class AccountAuthDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class AccountGetDTO
    {
        public string Login { get; set; }
        public string RoleName { get; set; }
    }
}