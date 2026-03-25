# Wypożyczalnie sprzętu (EquipmentRental)

Program realizuje system wypożyczalni sprzętu w formie aplikacji konsolowej.

## Proces uruchomienia
Aby odpalić program, musi być zainstalowane środowisko .NET
1. Sklonuj repozytorium
2. Otwórz terminal w katalogu głównym projektu (tam gdzie znajduje się plik `EquipmentRental.csproj`).
3. Wykonaj polecenie:
   ```bash
   dotnet run

## Struktura projektu (Podział na warstwy)
[cite_start]Kod podzieliłem na osobne części, żeby oddzielić logikę od wyświetlania w konsoli[cite: 52]:
* **Domain.Models**: Tylko dane i stan (Sprzęt, Użytkownik, Wypożyczenie).
* [cite_start]**Application.Policies**: Zasady biznesowe ukryte za interfejsami (limity, kary)[cite: 48, 56].
* [cite_start]**Application.Services**: `RentalService`, czyli klasa koordynująca wypożyczenia[cite: 23].
* [cite_start]**Program.cs (ConsoleUI)**: Służy tylko do wyświetlenia scenariusza, nie ma w nim logiki biznesowej[cite: 51].

## Decyzje projektowe (Kohezja i Coupling)

* [cite_start]**Wysoka kohezja (SRP):** Klasy mają sensowne, pojedyncze odpowiedzialności[cite: 11, 53]. [cite_start]Modele (np. `Equipment`) przechowują tylko dane, a wyliczeniami zajmują się osobne serwisy[cite: 16].
* [cite_start]**Niski coupling (Luźne powiązania):** Główny serwis `RentalService` używa interfejsów do sprawdzania limitów i kar[cite: 54, 56]. Gdy zmieni się regulamin, wystarczy dopisać nową klasę, bez ruszania głównego serwisu. Dodatkowo, chronię stan obiektów – statusu sprzętu nie da się zmienić "z zewnątrz" bez użycia dedykowanej metody.
* [cite_start]**Sensowne dziedziczenie:** Stworzyłem bazowe klasy `Equipment` i `User` (wynikające z modelu domeny)[cite: 19, 55]. [cite_start]Konkretny sprzęt (np. `Laptop`) dziedziczy wspólne cechy i dodaje swoje specyficzne parametry[cite: 27].
* [cite_start]**Obsługa błędów:** Gdy operacja jest niedozwolona (np. wypożyczenie niedostępnego sprzętu), program rzuca jawny wyjątek[cite: 45, 57].