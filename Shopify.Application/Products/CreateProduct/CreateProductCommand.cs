using Shopify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopify.Application.Products.CreateProduct
{
    public sealed record CreateProductCommand(
         string Name,
         string Description,
         decimal Price,
         string Merchant,
         byte[]? Image) : ICommand<Guid>;
}
