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
    public class DeleteProductCommand : ICommand
    {
        public int Id { get; init; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IDbConnection _dbConnection;
        public DeleteProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(DeleteProductCommand command)
        {
            try
            {
                _dbConnection.ExecuteNonQuery("DELETE From Produit WHERE Id = @Id;", parameters: command);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
