using Fleet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Repository.Configuration
{
    public class ListasItensConfiguration : BaseConfiguration, IEntityTypeConfiguration<ListasItens>
    {
        public void Configure(EntityTypeBuilder<ListasItens> builder)
        {

        }
    }
}
