using FluentNHibernateExamples.DataVisualization.SQLiteDatabase.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernateExamples.DataVisualization.SQLiteDatabase.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasManyToMany(x => x.Products)
                .Cascade.All()
                .Table("StoreProduct");
            HasMany(x => x.Staff)
                .Cascade.All()
                .Inverse();
        }
    }
}