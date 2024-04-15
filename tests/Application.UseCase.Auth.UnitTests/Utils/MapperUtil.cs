using AutoMapper;

namespace Application.UseCase.Auth.UnitTests.Utils;

public class MapperUtil
{
    public static Mapper GetTypedMapper<T>() where T : Profile
    {
        var profile = Activator.CreateInstance<T>();
        var mappingConfiguration = new MapperConfiguration(cfg => 
            cfg.AddProfile(profile));

        return new Mapper(mappingConfiguration);
    }

    public static Mapper GetTypedMapper(params Type[] profileTypes)
    {
        var profiles = profileTypes.Select(x => (Profile)Activator.CreateInstance(x)!).ToList();
        var mappingConfiguration = new MapperConfiguration(cfg => 
            cfg.AddProfiles(profiles));

        return new Mapper(mappingConfiguration);
    }
}