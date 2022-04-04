using Buriti_Store.Core.DomainObjects;
using System;

namespace Buriti_store.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(
            string name,
            string description, 
            bool isActive,
            decimal value, 
            Guid categoryId,
            DateTime dateRegister, 
            string image)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Value = value;
            DateRegister = dateRegister;
            Image = image;
            CategoryId = categoryId;

            Validate();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public decimal Value { get; private set; }
        public DateTime DateRegister { get; private set; }
        public string Image { get; private set; }
        public int QuantityStock { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        public void Activate() => IsActive = true;
        public void Disable() => IsActive = false;

        public void UpdateCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void UpdateDescription(string description)
        {
            Validations.ValidateIfEmpty(description, "O campo Descricao do produto não pode estar vazio");
            Description = description;
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HaveStock(quantity)) throw new DomainException("Estoque insuficiente");
            QuantityStock -= quantity;
        }

        public void ReplenishStock(int quantity)
        {
            QuantityStock += quantity;
        }

        public bool HaveStock(int quantity)
        {
            return QuantityStock >= quantity;
        }

        public void Validate()
        {
            Validations.ValidateIfEmpty(Name, "O campo Nome do produto não pode estar vazio");
            Validations.ValidateIfEmpty(Description, "O campo Descricao do produto não pode estar vazio");
            Validations.ValidateEquals(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
            Validations.ValidateIfLessThan(Value, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validations.ValidateIfEmpty(Image, "O campo Imagem do produto não pode estar vazio");
        }
    }
}