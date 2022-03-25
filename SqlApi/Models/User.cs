namespace SqlApi.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserRole { get; set; }
        public bool Aktif { get; set; }
        public string UserMail { get; set; }
    }
}
