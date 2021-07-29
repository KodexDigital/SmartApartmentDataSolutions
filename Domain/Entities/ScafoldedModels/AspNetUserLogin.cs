namespace Domain.Entities.ScafoldedModels
{
    public partial class AspNetUserLogin
    {
        public string Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
