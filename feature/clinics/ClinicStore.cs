namespace CareCommerece.feature.clinics;

public class ClinicStore
{
    private readonly List<Clinic> _clinics =
    [
        new Clinic(Guid.NewGuid(), "Teeth Surgical Clinic", "Green Road, Farmgate, Dhaka 1212", "01736780752"),
        new Clinic(Guid.NewGuid(), "Baby care clinic", "Pantho path, Farmgate, Dhaka 1212", "01736345652")
    ];
    
    public IReadOnlyList<Clinic> All() => _clinics;
    public Clinic? ById(Guid id) => _clinics.FirstOrDefault(x => x.Id == id);
    public Clinic Add(Clinic clinic)
    {
        _clinics.Add(clinic);
        return clinic;
    }
    
    public bool NameExists(string name) => _clinics.Any(x => x.Name == name);
}