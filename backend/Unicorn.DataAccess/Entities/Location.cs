using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Location : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string PostIndex { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
