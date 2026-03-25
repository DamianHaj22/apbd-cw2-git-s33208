using System;

namespace EquipmentRental.Domain.Models
{
    public class Equipment
    {
        public Guid Id { get; }
        public string Name { get; }
        public string SerialNumber { get; }
        public EquipmentStatus Status { get; private set; }

        protected Equipment(string name, string serialNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            SerialNumber = serialNumber;
            Status = EquipmentStatus.Available;
        }

        public void ChangeStatus(EquipmentStatus newStatus)
        {
            Status = newStatus;
        }
        
    }
}