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

        Profile user = new Profile
        {
            Id = profile.Id,
            Username = profile.Username,
            Email = profile.Email,
            FullName = profile.FullName,
            Address = profile.Address

        };

        context.Profiles.Add(user);
        context.SaveChanges();
    }
}
