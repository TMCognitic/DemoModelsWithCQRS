using Models.Entities;
using Models.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.CQRS.Queries;
using Tools.Database;

namespace Models.Queries
{
    public class GetProductsQuery : IQuery<IEnumerable<Product>>
    {
    }

    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IDbConnection _dbConnection;

        public GetProductsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Product> Execute(GetProductsQuery query)
        {
            return _dbConnection.ExecuteReader("Select Id, Nom, Prix FROM Produit", dr => dr.ToProduct());
        }
    }
}
