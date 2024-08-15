using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Repository.Configuration
{
    public class ChecklistConfiguration : BaseConfiguration, IEntityTypeConfiguration<Checklist> 
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {

        }

    }
}
