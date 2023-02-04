using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AnswerOption> AnswerOptions { get; set; } = default!;
    public DbSet<BankAccount> BankAccounts { get; set; } = default!;
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<EventNavigation> EventNavigations { get; set; } = default!;
    public DbSet<EventUser> EventUsers { get; set; } = default!;
    public DbSet<Meeting> Meetings { get; set; } = default!;
    public DbSet<MeetingUser> MeetingUsers { get; set; } = default!;
    public DbSet<MeetingInvite> MeetingInvites { get; set; } = default!;
    public DbSet<MoneyTransfer> MoneyTransfers { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;
    public DbSet<Questionnaire> Questionnaires { get; set; } = default!;
    public DbSet<QuestionnaireRelation> QuestionnaireRelations { get; set; } = default!;
    public DbSet<Requirement> Requirements { get; set; } = default!;
    public DbSet<RequirementParameter> RequirementParameters { get; set; } = default!;
    public DbSet<RequirementParameterInOption> RequirementsParameterInOptions { get; set; } = default!;
    public DbSet<RequirementOption> RequirementOptions { get; set; } = default!;
    public DbSet<RequirementUser> RequirementUsers { get; set; } = default!;

    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EventNavigation>()
            .HasOne(x => x.NextEvent)
            .WithMany(x => x.PreviousEventNavigations)
            .HasForeignKey(x => x.NextEventId);

        builder.Entity<EventNavigation>()
            .HasOne(x => x.PreviousEvent)
            .WithMany(x => x.NextEventNavigations)
            .HasForeignKey(x => x.PreviousEventId);
        
        builder.Entity<MoneyTransfer>()
            .HasOne(x => x.Sender)
            .WithMany(x => x.SendTransfers)
            .HasForeignKey(x => x.SenderId);

        builder.Entity<MoneyTransfer>()
            .HasOne(x => x.Receiver)
            .WithMany(x => x.ReceiveTransfers)
            .HasForeignKey(x => x.ReceiverId);
    }

    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void FixEntities(DbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                if (prop.GetValue(entity) is not DateTime originalValue)
                    continue;

                prop.SetValue(entity, originalValue.ToUniversalTime());
            }
        }
    }
}