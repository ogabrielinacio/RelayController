using RelayController.Domain.Common;

namespace RelayController.Domain.ValueObjects;

public class Role : ValueObject
{
    public int Id { get; }
    public string Name { get; }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static readonly Role Owner = new(1, "Owner");
    public static readonly Role Manager = new(2, "Manager");

    private static readonly List<Role> _all = new() { Owner, Manager };

    public static bool IsValid(int id) => _all.Any(r => r.Id == id);
    public static Role FromId(int id) => _all.FirstOrDefault(r => r.Id == id)
                                         ?? throw new ArgumentException($"Invalid RoleId: {id}");

    public static Role FromName(string name) => _all.FirstOrDefault(r => r.Name == name)
                                                ?? throw new ArgumentException($"Invalid RoleName: {name}"); 
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
    }
}