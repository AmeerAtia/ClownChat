using Microsoft.EntityFrameworkCore;

namespace ClownChat.Api.Database;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
}
