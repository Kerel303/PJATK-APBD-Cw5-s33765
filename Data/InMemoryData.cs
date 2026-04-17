namespace Cw5.Data;

using Cw5.Models;

public static class InMemoryData
{
    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "A101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "B204", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "C303", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "A102", BuildingCode = "A", Floor = 1, Capacity = 25, HasProjector = true, IsActive = false }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 2,
            OrganizerName = "Anna Kociubińska",
            Topic = "REST API",
            Date = DateTime.Parse("2026-05-10"),
            StartTime = TimeSpan.Parse("10:00"),
            EndTime = TimeSpan.Parse("12:00"),
            Status = "confirmed"
        }
    };
}