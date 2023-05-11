using Models.Entities;
using System.Data;

namespace Models.Mappers
{
    internal static partial class Mapper
    {
        internal static Product ToProduct(this IDataRecord record)
        {
            return new Product((int)record["Id"], (string)record["Nom"], (double)record["Prix"]);
        }
    }
}
