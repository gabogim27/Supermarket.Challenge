using Supermarket.Challenge.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Data
{
    public interface IDataSource
    {
        List<Product> GetProducts();
    }
}
