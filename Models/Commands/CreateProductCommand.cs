using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.CQRS.Commands;
using Tools.Database;

namespace Models.Commands
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public CreateProductCommand(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IDbConnection _dbConnection;

        public CreateProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(CreateProductCommand command)
        {
            try
            {
                _dbConnection.ExecuteNonQuery("INSERT INTO Produit (Nom, Prix) OUTPUT inserted.Id VALUES (@Nom, @Prix)", parameters: new { Nom = command.Name, Prix = command.Price });
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
            
        }
    }
}
