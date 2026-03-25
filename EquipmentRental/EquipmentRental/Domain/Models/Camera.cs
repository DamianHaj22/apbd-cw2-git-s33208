namespace EquipmentRental.Domain.Models
{
    public class Camera : Equipment
    {
        public bool HasInterchangeableLens { get; }
        public double MegaPixels { get; }

        public Camera(string name, string serialNumber, bool hasInterchangeableLens, double megaPixels) : base(name,
            serialNumber)
        {
            HasInterchangeableLens = hasInterchangeableLens;
            MegaPixels = megaPixels;
        }
    }
}