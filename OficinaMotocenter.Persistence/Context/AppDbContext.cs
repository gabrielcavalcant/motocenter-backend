using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Persistence.Mapping;

namespace OficinaMotocenter.Persistence.Context
{
    /// <summary>
    /// Represents the application's database context, providing access to the database entities.
    /// Inherits from <see cref="DbContext"/>.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="Motorcycle"/> entity.
        /// </summary>
        public DbSet<Motorcycle> Motorcycles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="Customer"/> entity.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="User"/> entity.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="Role"/> entity.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="Permission"/> entity.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="Team"/> entity.
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="TeamMember"/> entity.
        /// </summary>
        public DbSet<TeamMember> TeamMembers { get; set; }

        /// <summary>
        /// Configures the Entity Framework model when creating the entities in the database.
        /// This method is called when the context is used for the first time.
        /// </summary>
        /// <param name="builder">A <see cref="ModelBuilder"/> used to build the model for the entities.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MotorcycleConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new TokensConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new TeamMemberConfiguration());

            base.OnModelCreating(builder);
        }
    }
}