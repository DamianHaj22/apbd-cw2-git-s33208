namespace EquipmentRental.Domain.Models
{
    public class Projector : Equipment
    {
        public int NativeResolutionWidth { get; }
        public int Lumens { get; }

        public Projector(string name, string serialNumber, int nativeResolutionWidth, int lumens) : base(name,
            serialNumber)
        {
            NativeResolutionWidth = nativeResolutionWidth;
            Lumens = lumens;
        }
    }
}