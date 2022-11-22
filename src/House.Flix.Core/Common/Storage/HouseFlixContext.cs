using Microsoft.EntityFrameworkCore;

namespace House.Flix.Core.Common.Storage;

public class HouseFlixContext : DbContext
{
    public HouseFlixContext(DbContextOptions<HouseFlixContext> options) : base(options) { }
}
