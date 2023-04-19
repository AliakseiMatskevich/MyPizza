namespace MyPizza.Web.Models.AbstractViewModel
{
    public abstract class NavLinkViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string ActiveLink { get; set; }

        public NavLinkViewModel()
        {
            ActiveLink = string.Empty;
        }
    }
}
