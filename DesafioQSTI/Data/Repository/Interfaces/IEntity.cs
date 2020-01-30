using Microsoft.EntityFrameworkCore;

namespace DesafioQSTI.Data.Repositories.Interfaces
{
    public interface IEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        TPrimaryKey EntityId { get; }
    }
}