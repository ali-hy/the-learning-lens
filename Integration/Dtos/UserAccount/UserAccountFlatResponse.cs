namespace Integration.Dtos.UserAccount
{
    public class UserAccountFlatResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
