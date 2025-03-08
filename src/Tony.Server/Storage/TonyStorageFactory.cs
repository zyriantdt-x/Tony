using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Tony.Server.Storage;
internal class TonyStorageFactory : IDesignTimeDbContextFactory<TonyStorage>, IDbContextFactory<TonyStorage> {
    public TonyStorage CreateDbContext( string[] args = null ) {
        DbContextOptionsBuilder<TonyStorage> options_builder = new DbContextOptionsBuilder<TonyStorage>();
        options_builder.UseSqlite( "Data Source=C:\\etc\\tony.db" ); // todo: move this into options

        return new TonyStorage( options_builder.Options );
    }
}