using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Domain.Products
{
    public sealed record Description
    {
        public string Value { get; init; }
         
        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty", nameof(value));

            if (!IsAlphanumeric(value))
                throw new ArgumentException("Product description must be alphanumeric.");


            Value = value;
        }
        private bool IsAlphanumeric(string value)
        {
            return value.All(char.IsLetterOrDigit);
        }
    }
}
