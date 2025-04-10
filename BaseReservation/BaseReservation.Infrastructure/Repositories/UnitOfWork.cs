using Dapper;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using BaseReservation.Common.Extensions;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Domain.Core.Models;
using BaseReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using BaseReservation.Application.Core.Interfaces;

namespace BaseReservation.Infrastructure.Repositories;

public class UnitOfWork(ILoggerFactory loggerFactory, BaseReservationContext dbContext) : IUnitOfWork
{
    private readonly ILoggerFactory _loggerFactory = loggerFactory;

    private readonly BaseReservationContext _dbContext = dbContext;

    private readonly Dictionary<Type, dynamic> _repositories = [];

    public IDbConnection SqlConnection => new SqlConnection(_dbContext.Connection.ConnectionString);

    public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();

    public void ClearChangeTracking() => _dbContext.ChangeTracker.Clear();

    public IExecutionStrategy CreateExecutionStrategy() => _dbContext.Database.CreateExecutionStrategy();

    public async Task<IList<T>?> FromSqlAsync<T>(FormattableString sql, params Expression<Func<T, object>>[]? includes)
    {
        using var connection = new SqlConnection(_dbContext.Database.GetConnectionString());
        await connection.OpenAsync();
        var result = await connection.QueryAsync<T>(sql.GetSQL(), new { });
        return result.ToList();
    }

    public async Task<IList<string>> GetTableColumnsAsync(string tableName) => await GetTableColumnsAsync(tableName, excludeColumnsList: null!);

    public async Task<IList<string>> GetTableColumnsAsync(string tableName, string excludeColumn) => await GetTableColumnsAsync(tableName, new List<string>() { excludeColumn });

    public async Task<IList<string>> GetTableColumnsAsync(string tableName, List<string> excludeColumnsList)
    {
        FormattableString sqlExclude;
        sqlExclude = $"1=1";
        if (excludeColumnsList != null && excludeColumnsList.HasItems())
        {
            sqlExclude = FormattableStringFactory.Create("Name NOT IN({0})", string.Join(".", excludeColumnsList.Select(m => "'" + m + "'")));
        }

        var sql = FormattableStringFactory.Create(
            "SELECT Name FROM sys.columns WHERE OBJECT_NAME(OBJECT_ID) = '{0}s' AND is_hidden = 0 AND {1}", tableName, sqlExclude.GetSQL()
        );

        var columns = await FromSqlAsync<string>(sql);
        return columns!;
    }

    public async Task<int> GetTotalRowCountAsync(FormattableString sql)
    {
        using var connection = new SqlConnection(_dbContext.Database.GetConnectionString());
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<int>(sql.GetSQL(), new { });
        return result;
    }

    public IBaseRepositoryAsync<T> Repository<T>() where T : BaseSimpleDto
    {
        var entityType = typeof(T);
        dynamic repositoryExisting;
        if (_repositories.TryGetValue(entityType, out repositoryExisting!))
        {
            return repositoryExisting;
        }

        var repositoryType = typeof(BaseRepositoryAsync<>);
        var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _loggerFactory, _dbContext);
        _repositories.Add(entityType, repository!);

        return (IBaseRepositoryAsync<T>)repository!;
    }

    public async Task RollbackChangesAsync() => await _dbContext.Database.RollbackTransactionAsync();

    public async Task<int> SaveChangesAsync()
    {
        int rowsAffected = await _dbContext.SaveChangesAsync();
        if (rowsAffected == 0) throw new BaseReservationException("No rows were affected");
        return rowsAffected;
    }
}