using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUsuarioSucursal(BaseReservationContext context) : IRepositoryUsuarioSucursal
{
    /// <inheritdoc />
    public async Task<bool> AssignUsuariosAsync(byte idSucursal, IEnumerable<UsuarioSucursal> usuariosSucursal)
    {
        var result = true;
        var usuariosSucursalExistentes = await ListAllBySucursalAsync(idSucursal);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.UsuarioSucursals.RemoveRange(usuariosSucursalExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && usuariosSucursalExistentes.Any())
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.UsuarioSucursals.AddRange(usuariosSucursal);
                    rowsAffected = await context.SaveChangesAsync();

                    if (rowsAffected == 0)
                    {
                        await transaccion.RollbackAsync();
                        result = false;
                    }
                    else
                    {
                        await transaccion.CommitAsync();
                    }
                }
            }
            catch (Exception exc)
            {
                await transaccion.RollbackAsync();
                throw new RequestFailedException("Error al guardar usuarios", exc);
            }
        });

        return result;
    }

    /// <inheritdoc />    
    public async Task<ICollection<UsuarioSucursal>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<UsuarioSucursal>()
           .AsNoTracking()
           .Where(m => m.IdSucursal == idSucursal)
           .ToListAsync();
        return collection;
    }
}