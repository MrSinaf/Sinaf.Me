using Microsoft.EntityFrameworkCore;

namespace Sinaf.Me.Data;

public static class DbContextUtils
{
	public static void OnConfiguring(DbContextOptionsBuilder optionsBuilder, string db)
	{
		var dbString = Environment.GetEnvironmentVariable(db);
		if (dbString == null)
			throw new Exception("Missing 'DB' environment variable (╯▔皿▔)╯");
		
		optionsBuilder.UseMySql(dbString, ServerVersion.Parse("11.8.3-MariaDB"));
	}
}

public partial class WarhammerDbContext
{
	public WarhammerDbContext() { }
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> DbContextUtils.OnConfiguring(optionsBuilder, "DB_WARHAMMER");
}

public partial class WebDbContext
{
	public WebDbContext() { }
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> DbContextUtils.OnConfiguring(optionsBuilder, "DB_WEB");
}