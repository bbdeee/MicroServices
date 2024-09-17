using MicroServicesProject.Data;
using MicroServicesProject.Models;

public class ProfileService
{
    private readonly ProfileContext context;

    public ProfileService(ProfileContext context)
    {
        this.context = context;
    }

    public void SaveProfile(Profile profile)
    {
        this.context.Profiles.Add(profile);
        this.context.SaveChanges();
    }
}


