namespace WebUi.Models.InventoryControllerModel
{
	public class InventoryResponse
	{
        public string Name { get; set; } = "";
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Volume { get; set; }
        public double Capacity { get; set; }
        public string ImageUrl { get; set; }
        public double Lenght { get; set; }
        public int UserId { get; set; }
    }
}
