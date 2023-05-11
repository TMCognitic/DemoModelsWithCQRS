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
    public class GetProductQuery : IQuery<Product?>
    {
        public int Id { get; init; }

        public GetProductQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, Product?>
    {
        private readonly IDbConnection _dbConnection;

        public GetProductQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Product? Execute(GetProductQuery query)
        {
            return _dbConnection.ExecuteReader("Select Id, Nom, Prix FROM Produit WHERE Id = @Id", dr => dr.ToProduct(), parameters: query).SingleOrDefault();
        }
    }
}
