using System.Data.Entity.ModelConfiguration;
using Couponat.Models;

namespace Couponat.Persistence.EntityConfigrations
{
    public class BannerConfiguration : EntityTypeConfiguration<Banner>
    {
        public BannerConfiguration()
        {
            HasKey(c => c.Id);
            
        }
    }
}