using Fleet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Repository.Configuration
{
    public class ListasConfiguration : BaseConfiguration, IEntityTypeConfiguration<Listas>
    {
        public void Configure(EntityTypeBuilder<Listas> builder)
        {

        }
    }
}
