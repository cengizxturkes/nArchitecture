namespace WebUi.Models.InventoryControllerModel
{
	public class InventoryAddViewModel
	{
        public int UserId { get; set; }
        public string Name { get; set; } 
        public string AsinCode { get; set; } 
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Desi { get; set; }
        public decimal TotalPrice { get; set; }
        public double Length { get; set; }
    }
}
