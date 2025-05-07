namespace Project_ASP.NET.Areas.Admin.Models.Users
{
    public class UserItemViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Role { get; set; }= string.Empty;
        public string Image {  get; set; } = string.Empty;
    }
}
