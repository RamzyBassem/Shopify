using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));

        builder.Property(user => user.LastName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new LastName(value));

        builder.Property(user => user.UserName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new UserName(value));

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value)); ;

        builder.Property(user => user.Phone)
            .HasMaxLength(20)
            .HasConversion(phone => phone.phoneNumber, value => new Phone(value));

        builder.Property(user => user.PasswordHash)
           .HasMaxLength(200)
           .HasConversion(passwordHash => passwordHash.Value, value => new PasswordHash(value));

        builder.Property(user => user.Role)
            .HasMaxLength(20) 
            .HasConversion(
                role => role.ToString(),       
                value => (Role)Enum.Parse(typeof(Role), value)
            );

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
