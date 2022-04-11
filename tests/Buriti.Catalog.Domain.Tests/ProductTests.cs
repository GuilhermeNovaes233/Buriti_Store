using Buriti_store.Catalog.Domain;
using Buriti_Store.Core.DomainObjects;
using System;
using Xunit;

namespace Buriti.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Validate_If_The_Product_Name_Is_Equal_To_Null()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Product(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );

            Assert.Equal("O campo Nome do produto não pode estar vazio", ex.Message);
        }

        [Fact]
        public void Validate_If_The_Product_Description_Is_Equal_To_Null()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );

            Assert.Equal("O campo Descricao do produto não pode estar vazio", ex.Message);
        }

        [Fact]
        public void Validate_If_The_Product_Value_Is_Negative()
        {
            var ex = Assert.Throws<DomainException>(() =>
                            new Product("Nome", "Descricao", false, 0, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
                        );

            Assert.Equal("O campo Valor do produto não pode se menor igual a 0", ex.Message);
        }


        [Fact]
        public void Validate_If_Category_Id_Is_Null()
        {
            var ex = Assert.Throws<DomainException>(() =>
                           new Product("Nome", "Descricao", false, 100, Guid.Empty, DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
                       );

            Assert.Equal("O campo CategoriaId do produto não pode estar vazio", ex.Message);
        }

        [Fact]
        public void Validate_If_Product_Image_Is_Null()
        {
            var ex = Assert.Throws<DomainException>(() =>
                   new Product("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensions(1, 1, 1))
               );

            Assert.Equal("O campo Imagem do produto não pode estar vazio", ex.Message);
        }

        [Fact]
        public void Validate_If_Product_Height_Is_Negative_Or_Equal_To_Zero()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Product("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensions(0, 1, 1))
            );

            Assert.Equal("O campo Altura não pode ser menor ou igual a 0", ex.Message);
        }
    }
}
