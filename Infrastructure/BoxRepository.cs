using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class BoxRepository : IBoxRepository
{
    private readonly BoxDbContext _context;

    public BoxRepository(BoxDbContext context)
    {
        _context = context;
    }
    
    public List<Box> GetAllBoxes()
    {
        return _context.BoxTable.ToList();
    }

    public Box CreateNewBox(Box box)
    {
        _context.BoxTable.Add(box);
        _context.SaveChanges();
        return box;
    }

    public Box GetBoxById(int id)
    {
        return _context.BoxTable.Find(id) ?? throw new KeyNotFoundException();
    }

    public Box UpdateBox(Box box)
    {
        _context.BoxTable.Update(box);
        _context.SaveChanges();
        return box;
    }

    public Box DeleteBox(int id)
    {
        var boxToDelete = _context.BoxTable.Find(id) ?? throw new KeyNotFoundException();
        _context.BoxTable.Remove(boxToDelete);
        _context.SaveChanges();
        return boxToDelete;
    }

    public void RebuildDb()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}