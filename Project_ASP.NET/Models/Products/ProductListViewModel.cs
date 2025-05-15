namespace Project_ASP.NET.Models.Products
{
    public class ProductListViewModel
    {
        public ProductSearchViewModel Search { get; set; } = new();
        public PaginationViewModel<ProductItemViewModel> Page { get; set; } = new();
    }
}
