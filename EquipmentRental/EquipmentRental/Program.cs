using System;
using System.Linq;
using EquipmentRental.Application.Policies;
using EquipmentRental.Application.Services;
using EquipmentRental.Domain.Models;

namespace EquipmentRental
{
    class Program
    {
        public static void Main(string[] args)
        {
            var limitPolicy = new DefaultRentalLimitPolicy();
            var penaltyCalculator = new DailyPenaltyCalculator(10m); // 10 PLN za każdy dzień opóźnienia
            var rentalService = new RentalService(limitPolicy, penaltyCalculator);
            
            Console.WriteLine("WYPOŻYCZALNIA SPRZĘTU UCZELNIANEGO\n");
            
            // dodanie sprzetu
            var laptop = new Laptop("Dell XPS 15", "SN_1001", 16, "Intel i7");
            var projector = new Projector("Epson Rzutnik", "PRJ-2002", 1920, 3000);
            var camera = new Camera("Sony Alpha", "CAM-3003", true, 24.2);
            
            rentalService.AddEquipment(laptop);
            rentalService.AddEquipment(projector);
            rentalService.AddEquipment(camera);
            
            // dodanie użytkowników
            var student = new Student("Damian", "Hajduk", "s33208", "123456789");
            var employee = new Employee("Jan", "Naczelny", "IT Department");
            
            rentalService.AddUser(student);
            rentalService.AddUser(employee);
            
            Console.WriteLine("Stan początkowy: Dostępny sprzęt");
            foreach (var eq in rentalService.GetAllEquipment().Where(e => e.Status == EquipmentStatus.Available))
            {
                Console.WriteLine($"- {eq.Name} (SN: {eq.SerialNumber})");
            }
            
            // pomyślne wypożyczenie sprzętu
            Console.WriteLine("\nPomyślne wypożyczenie sprzętu");
            var rental1 = rentalService.RentEquipment(student.Id, laptop.Id, 7); // wypożyczenie na 7 dni
            Console.WriteLine($"Student {student.FirstName} wypożyczył {laptop.Name}.");
            
            // zwrot sprzetu w terminie
            Console.WriteLine("\nZwrot w terminie");
            // test zwrotu po 5 dniach
            rentalService.ReturnEquipment(rental1.Id, DateTime.Now.AddDays(5));
            Console.WriteLine($"Zwrócono {laptop.Name}. Naliczona kara: {rental1.PenaltyFee} PLN.");
            
            // zwrot opozniony skutkujący naliczeniem kary
            Console.WriteLine("\n--- Wypożyczenie i opóźniony zwrot ---");
            var rental2 = rentalService.RentEquipment(employee.Id, projector.Id, 2); // wypożyczenie na 2 dni
            
            // zwrot po 5 dniach (3 dni spóżnienia)
            rentalService.ReturnEquipment(rental2.Id, DateTime.Now.AddDays(5));
            Console.WriteLine($"Pracownik {employee.FirstName} zwrócił {projector.Name} z opóźnieniem.");
            Console.WriteLine($"Naliczona kara: {rental2.PenaltyFee} PLN (3 dni x 10 PLN).");
            
            // raport
            Console.WriteLine("\nRAPORT KOŃCOWY");
            var allEquipment = rentalService.GetAllEquipment().ToList();
            
            Console.WriteLine($"Liczba zarejestrowanych użytkowników: {rentalService.GetAllUsers().Count()}");
            Console.WriteLine($"Całkowita liczba sprzętu: {allEquipment.Count}");
            Console.WriteLine($"Sprzęt dostępny: {allEquipment.Count(e => e.Status == EquipmentStatus.Available)}");
            Console.WriteLine($"Sprzęt wypożyczony: {allEquipment.Count(e => e.Status == EquipmentStatus.Rented)}");
            
            var totalPenalties = rentalService.GetAllRentals().Sum(r => r.PenaltyFee);
            Console.WriteLine($"Łączna kwota naliczonych kar w systemie: {totalPenalties} PLN");
            
            // wyjscie
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}