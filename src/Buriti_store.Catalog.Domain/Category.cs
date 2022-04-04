using Buriti_Store.Core.DomainObjects;

namespace Buriti_store.Catalog.Domain
{
    public class Category : Entity
    {
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; private set; }
        public int Code { get; private set; }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}