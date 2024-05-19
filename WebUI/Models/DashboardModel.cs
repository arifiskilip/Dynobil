namespace WebUI.Models
{
    public class DashboardModel
    {
        public int TotalCustomers { get; set; }
        public int TotalRoles { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public CategoryIdByProducts CategoryIdByProducts { get; set; }
    }

    public class CategoryIdByProducts
    {
        public int CategoryId { get; set; }
        public int Products { get; set; }
    }
}
