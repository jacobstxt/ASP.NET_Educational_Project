namespace Project_ASP.NET.Models.Products
{
    public class ProductListViewModel
    {
        //відображення списку
        public List<ProductItemViewModel> Products { get; set; } = new();
        //Модель для пошуку
        public ProductSearchViewModel Search { get; set; } = new();
        //кількість елементів під час пошуку
        public int Count {  get; set; }
    }
}
