using System;

namespace EquipmentRental.Domain.Models
{
    public class Rental
    {
        public Guid Id { get; }
        public User Borrower { get; }
        public Equipment RentedEquipment { get; }
        public DateTime RentalDate { get; }
        public DateTime ExpectedReturnDate { get; }
        
        public DateTime? ActualReturnDate { get; private set; }
        public decimal PenaltyFee { get; private set; } //kara

        public Rental(User borrower, Equipment equipment, int rentDurationDays)
        {
            Id = Guid.NewGuid();
            Borrower = borrower;
            RentedEquipment = equipment;
            RentalDate = DateTime.Now;
            ExpectedReturnDate = RentalDate.AddDays(rentDurationDays);
            PenaltyFee = 0; // na poczatku brak kar
        }
        
        public void MarkAsReturned(DateTime returnDate, decimal penalty)
        {
            ActualReturnDate = returnDate;
            PenaltyFee = penalty;
        }

        public bool IsOverDue(DateTime checkDate) // sprawdzenie czy wypozyczenie jest przeterminowane
        {
            return ActualReturnDate == null && checkDate > ExpectedReturnDate;
            
        }
    }
}