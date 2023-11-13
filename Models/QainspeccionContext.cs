using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlturaTerminales.Models;

public partial class QainspeccionContext : DbContext
{
    public QainspeccionContext()
    {
    }

    public QainspeccionContext(DbContextOptions<QainspeccionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlturasList>AlturasList { get; set; }  
    public virtual DbSet<DetalleAlturasTerminal> DetalleAlturasTerminal { get; set; }   
    public virtual DbSet<DetalleAlturasInsulacion> DetalleAlturasInsulacion { get; set; }
    public virtual DbSet<DetalleAplicadores> DetalleAplicadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
