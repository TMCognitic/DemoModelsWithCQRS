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
    public class UpdateProductCommand : ICommand
    {
        public UpdateProductCommand(int id, double price)
        {
            Id = id;
            Price = price;
        }

        public int Id { get; init; }
        public double Price { get; init; }
    }

    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IDbConnection _dbConnection;

        public UpdateProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(UpdateProductCommand command)
        {
            try
            {
                _dbConnection.ExecuteNonQuery("UPDATE Produit SET Prix = @Price WHERE Id = @Id", parameters: command);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }

}
