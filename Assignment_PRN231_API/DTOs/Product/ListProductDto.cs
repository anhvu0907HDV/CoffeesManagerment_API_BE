namespace Assignment_PRN231_API.DTOs.Product
{
    public class ListProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? Discount { get; set; }

        public int Size { get; set; }

        public int Quantity { get; set; }

        public int IsActive { get; set; }

        public string Image { get; set; } = null!; // Đường dẫn ảnh sản phẩm

        public string CategoryName { get; set; } = null!; // Tên danh mục

        public string? RecipeDescription { get; set; } // Mô tả công thức
    }

}
