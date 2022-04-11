using Buriti_Store.Core.DomainObjects;
using System.Collections.Generic;

namespace Buriti_store.Catalog.Domain
{
    public class Category : Entity
    {
        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            Validations.ValidateIfEmpty(Name, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidateEquals(Code, 0, "O campo Codigo não pode ser 0");
        }
    }
}