using Buriti_Store.Core.DomainObjects;
using System;

namespace Buriti_store.Catalog.Domain
{
    public class Dimensions
    {
        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Validations.ValidateIfLessThan(height, 1, "O campo Altura não pode ser menor ou igual a 0");
            Validations.ValidateIfLessThan(width, 1, "O campo Largura não pode ser menor ou igual a 0");
            Validations.ValidateIfLessThan(depth, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public string DescriptionFormated()
        {
            return $"LxAxP: {Width} x {Height} x {Depth}";
        }

        public override string ToString()
        {
            return DescriptionFormated();
        }
    }
}
