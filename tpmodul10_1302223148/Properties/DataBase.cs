using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using tpmodul10_1302223148;
class DataBase : DbContext
{
    public DataBase(DbContextOptions<DataBase> options)
        : base(options) { }
    public DbSet<Mahasiswa> mhs => Set<Mahasiswa>();
}