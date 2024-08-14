using Fleet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Repository.Configuration
{
    public class EstabeleciomentosConfiguration : BaseConfiguration, IEntityTypeConfiguration<Estabelecimentos>
    {
        public void Configure(EntityTypeBuilder<Estabelecimentos> builder)
        {

        }
    }
}
