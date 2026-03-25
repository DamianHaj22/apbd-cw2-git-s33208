using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentRental.Domain.Models;
using EquipmentRental.Application.Policies;

namespace EquipmentRental.Application.Services
{
    public class RentalService
    {
        private readonly List<Equipment> _equipment = new List<Equipment>();
        private readonly List<User> _users = new List<User>();
        private readonly List<Rental> _rentals = new List<Rental>();

        private readonly IRentalLimitPolicy _limitPolicy;
        private readonly IPenaltyCalculator _penaltyCalculator;

        // Konstruktor
        public RentalService(IRentalLimitPolicy limitPolicy, IPenaltyCalculator penaltyCalculator)
        {
            _limitPolicy = limitPolicy;
            _penaltyCalculator = penaltyCalculator;
        }

        // dodawanie do systemu
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void AddEquipment(Equipment equipment)
        {
            _equipment.Add(equipment);
        }

        // odczyt danych
        public IEnumerable<Equipment> GetAllEquipment() => _equipment;
        public IEnumerable<User> GetAllUsers() => _users;
        public IEnumerable<Rental> GetAllRentals() => _rentals;
        
        public void MarkEquipmentAsUnavailable(Guid equipmentId)
        {
            var equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null) throw new ArgumentException("Nie znaleziono sprzętu.");
            
            equipment.ChangeStatus(EquipmentStatus.Unavailable);
        }

        public Rental RentEquipment(Guid userId, Guid equipmentId, int rentDurationDays)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new ArgumentException("Nie znaleziono użytkownika.");

            var equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null) throw new ArgumentException("Nie znaleziono sprzętu.");

            // Sprawdzenie dostępności
            if (equipment.Status != EquipmentStatus.Available)
                throw new InvalidOperationException("Sprzęt nie jest dostępny do wypożyczenia.");

            // Liczymy ile użytkownik ma aktualnie wypożyczonych i niezwróconych sprzętów
            int activeRentalsCount = _rentals.Count(r => r.Borrower.Id == userId && r.ActualReturnDate == null);
            int maxAllowed = _limitPolicy.GetMaxActiveRentals(user.Type);

            // Sprawdzenie limitu
            if (activeRentalsCount >= maxAllowed)
                throw new InvalidOperationException($"Użytkownik przekroczył limit wypożyczeń (max {maxAllowed}).");

            // Tworzymy nowe wypożyczenie i zmieniamy status sprzętu
            var rental = new Rental(user, equipment, rentDurationDays);
            equipment.ChangeStatus(EquipmentStatus.Rented);
            _rentals.Add(rental);

            return rental;
        }

        public void ReturnEquipment(Guid rentalId, DateTime returnDate)
        {
            var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null) throw new ArgumentException("Nie znaleziono wypożyczenia.");

            if (rental.ActualReturnDate != null)
                throw new InvalidOperationException("To wypożyczenie zostało już wcześniej zwrócone.");

            // Liczymy karę
            decimal penalty = _penaltyCalculator.CalculatePenalty(rental.ExpectedReturnDate, returnDate);
            
            // Oznaczamy wypożyczenie jako zwrócone i zwalniamy sprzęt
            rental.MarkAsReturned(returnDate, penalty);
            rental.RentedEquipment.ChangeStatus(EquipmentStatus.Available);
        }
    }
}