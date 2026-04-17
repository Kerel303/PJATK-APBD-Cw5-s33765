namespace Cw5.Controllers;

using Cw5.Models;
using Cw5.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(InMemoryData.Rooms);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var rooms = InMemoryData.Rooms
            .Where(r => r.BuildingCode == buildingCode)
            .ToList();

        return Ok(rooms);
    }

    [HttpGet("filter")]
    public IActionResult Filter(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = InMemoryData.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        room.Id = InMemoryData.Rooms.Any()
            ? InMemoryData.Rooms.Max(r => r.Id) + 1
            : 1;
        InMemoryData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updated)
    {
        var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        room.Name = updated.Name;
        room.BuildingCode = updated.BuildingCode;
        room.Floor = updated.Floor;
        room.Capacity = updated.Capacity;
        room.HasProjector = updated.HasProjector;
        room.IsActive = updated.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        if (InMemoryData.Reservations.Any(r => r.RoomId == id))
            return Conflict("Room has reservations");

        InMemoryData.Rooms.Remove(room);
        return NoContent();
    }
}