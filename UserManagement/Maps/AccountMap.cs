using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Models;

namespace UserManagement.Maps
{
    public class AccountMap
    {
        public AccountMap(EntityTypeBuilder<Account> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("accounts");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
        }
    }
}