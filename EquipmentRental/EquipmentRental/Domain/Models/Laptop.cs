namespace EquipmentRental.Domain.Models
{
    public class Laptop : Equipment //dziedziczenie
    {
        public int RamSizeGB { get; }
        public string ProcessorModel { get; }

        public Laptop(string name, string serialNumber, int ramSizeGB, string processorModel) : base(name, serialNumber)
        {
            RamSizeGB = ramSizeGB;
            ProcessorModel = processorModel;
        }
    }
}