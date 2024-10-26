namespace Auth.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<User> Users =>
        new List<User>
        {
            new User
            {
                Id = Guid.Parse("f7a9e4b4-c3a5-4e44-bf5f-476f5d7b8cb4"),
                Username = "alagtari",
                Email = "ala@gtari.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Ala@123"),
                Role = "User"
            },
            new User
            {
                Id = Guid.Parse("4e02d5b1-7529-4388-9133-76e8e6e3d96f"),
                Username = "makremtaieb",
                Email = "makrem@taieb.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Makrem@123"),
                Role = "User"
            },
            new User
            {
                Id = Guid.Parse("ce4789b5-6fe3-4399-b6a6-20e2dc91419d"),
                Username = "faycelkhloun",
                Email = "faycel@kahloun.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Faycel@123"),
                Role = "User"
            }
        };
}