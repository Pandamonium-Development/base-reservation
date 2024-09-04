using System.Data;
using BaseReservation.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaseReservation.Infrastructure.Data;

public partial class BaseReservationContext(DbContextOptions<BaseReservationContext> options) : DbContext(options)
{
    const string FECHACREACIONNAME = "FechaCreacion";
    const string FECHAMODIFICACIONNAME = "FechaModificacion";

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<DetalleFacturaProducto> DetalleFacturaProductos { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetallePedidoProducto> DetallePedidoProductos { get; set; }

    public virtual DbSet<DetalleReserva> DetalleReservas { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Feriado> Feriados { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Impuesto> Impuestos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<InventarioProducto> InventarioProductos { get; set; }

    public virtual DbSet<InventarioProductoMovimiento> InventarioProductoMovimientos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<ReservaPregunta> ReservaPregunta { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<SucursalFeriado> SucursalFeriados { get; set; }

    public virtual DbSet<SucursalHorario> SucursalHorarios { get; set; }

    public virtual DbSet<SucursalHorarioBloqueo> SucursalHorarioBloqueos { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<TokenMaster> TokenMasters { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioSucursal> UsuarioSucursals { get; set; }

    public IDbConnection Connection => Database.GetDbConnection();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canton>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Cantons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canton_Provincia");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.UsuarioCreacion).HasDefaultValue("");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Clientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Distrito");
        });

        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Contactos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contacto_Proveedor");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Factura");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleFacturas).HasConstraintName("FK_DetalleFactura_Producto");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleFacturas).HasConstraintName("FK_DetalleFactura_Servicio");
        });

        modelBuilder.Entity<DetalleFacturaProducto>(entity =>
        {
            entity.HasOne(d => d.IdDetalleFacturaNavigation).WithMany(p => p.DetalleFacturaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFacturaProducto_DetalleFactura");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleFacturaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFacturaProducto_Producto");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos).HasConstraintName("FK_DetallePedido_Producto");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetallePedidos).HasConstraintName("FK_DetallePedido_Servicio");
        });

        modelBuilder.Entity<DetallePedidoProducto>(entity =>
        {
            entity.HasOne(d => d.IdDetallePedidoNavigation).WithMany(p => p.DetallePedidoProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedidoProducto_DetallePedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidoProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedidoProducto_Producto");
        });

        modelBuilder.Entity<DetalleReserva>(entity =>
        {
            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleReservas).HasConstraintName("FK_DetalleReserva_Producto");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleReservas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleReserva_Reserva");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleReservas).HasConstraintName("FK_DetalleReserva_Servicio");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasOne(d => d.IdCantonNavigation).WithMany(p => p.Distritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Distrito_Canton");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Cliente");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Impuesto");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Facturas).HasConstraintName("FK_Factura_Pedido");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Sucursal");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_TipoPago");
        });

        modelBuilder.Entity<Feriado>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.Property(e => e.Nombre).HasDefaultValue("");
            entity.Property(e => e.TipoInventario).HasDefaultValue("");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Inventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Sucursal");
        });

        modelBuilder.Entity<InventarioProducto>(entity =>
        {
            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.InventarioProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventarioProducto_Inventario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.InventarioProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventarioProducto_Producto");
        });

        modelBuilder.Entity<InventarioProductoMovimiento>(entity =>
        {
            entity.HasOne(d => d.IdInventarioProductoNavigation).WithMany(p => p.InventarioProductoMovimientos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventarioProductoMovimiento_InventarioProducto");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Cliente");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_EstadoPedido");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Impuesto");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Reserva");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Sucursal");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_TipoPago");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_UnidadMedida");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Proveedors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor_Distrito");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Estado)
                .HasDefaultValue("P")
                .IsFixedLength();

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Cliente");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Reservas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Sucursal");
        });

        modelBuilder.Entity<ReservaPregunta>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.ReservaPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservaPregunta_Reserva");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.Servicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicio_TipoServicio");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Sucursals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursal_Distrito");
        });

        modelBuilder.Entity<SucursalFeriado>(entity =>
        {
            entity.HasOne(d => d.IdFeriadoNavigation).WithMany(p => p.SucursalFeriados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalFeriado_Feriado");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.SucursalFeriados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalFeriado_Sucursal");
        });

        modelBuilder.Entity<SucursalHorario>(entity =>
        {
            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.SucursalHorarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalHorario_Horario");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.SucursalHorarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalHorario_Sucursal");
        });

        modelBuilder.Entity<SucursalHorarioBloqueo>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdSucursalHorarioNavigation).WithMany(p => p.SucursalHorarioBloqueos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalHorarioBloqueo_SucursalHorario");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TokenMaster>(entity =>
        {
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TokenMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TokenMaster_Usuario");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Simbolo).IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Distrito");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Genero");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<UsuarioSucursal>(entity =>
        {
            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.UsuarioSucursals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioSucursal_Sucursal");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioSucursals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioSucursal_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        DefaultProperties();
    }

    private void DefaultProperties()
    {
        string usuarioCreacionName = "UsuarioCreacion";
        string usuarioModificacionName = "UsuarioModificacion";

        DateTime fechaCreacion = DateTime.Now;
        DateTime fechaModificacion = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries())
        {
            string usuarioCreacion = string.Empty;
            string usuarioModificacion = null!;
            if (entry.Entity.GetType().GetProperty(usuarioCreacionName) != null) usuarioCreacion = entry.Property(usuarioCreacionName).CurrentValue!.ToString()!;
            if (entry.Entity.GetType().GetProperty(usuarioModificacionName) != null)
            {
                var modificacion = entry.Property(usuarioModificacionName).CurrentValue;
                if (modificacion != null) usuarioModificacion = modificacion.ToString()!;
            }

            if (entry.State == EntityState.Added)
            {
                GenerateAdded(entry, usuarioCreacionName, usuarioCreacion, usuarioModificacionName, fechaCreacion);
            }
            else
            {
                GenerateModified(entry, usuarioCreacionName, usuarioModificacionName, usuarioModificacion, fechaModificacion);
            }
        }
    }

    private void GenerateAdded(EntityEntry entry, string usuarioCreacionName, string usuarioCreacion, string usuarioModificacionName, DateTime fechaCreacion)
    {
        string activoName = "Activo";

        if (entry.Entity.GetType().GetProperty(FECHACREACIONNAME) != null && entry.Property(FECHACREACIONNAME).CurrentValue != null) entry.Property(FECHACREACIONNAME).CurrentValue = fechaCreacion;
        if (entry.Entity.GetType().GetProperty(activoName) != null && !(bool)entry.Property(activoName).CurrentValue!) entry.Property(activoName).CurrentValue = true;

        if (entry.Entity.GetType().GetProperty(usuarioCreacionName) != null && entry.Property(usuarioModificacionName).CurrentValue != null)
        {
            entry.Property(usuarioCreacionName).CurrentValue = entry.Property(usuarioModificacionName).CurrentValue;
            entry.Property(usuarioModificacionName).CurrentValue = null;
        }

        if (entry.Entity.GetType().GetProperty(usuarioCreacionName) != null) entry.Property(usuarioCreacionName).CurrentValue = usuarioCreacion;
        if (entry.Entity.GetType().GetProperty(FECHAMODIFICACIONNAME) != null) entry.Property(FECHAMODIFICACIONNAME).IsModified = false;
        if (entry.Entity.GetType().GetProperty(usuarioModificacionName) != null) entry.Property(usuarioModificacionName).IsModified = false;
    }

    private void GenerateModified(EntityEntry entry, string usuarioCreacionName, string usuarioModificacionName, string usuarioModificacion, DateTime fechaModificacion)
    {
        if (entry.State == EntityState.Modified)
        {
            if (entry.Entity.GetType().GetProperty(FECHAMODIFICACIONNAME) != null) entry.Property(FECHAMODIFICACIONNAME).CurrentValue = fechaModificacion;

            if (entry.Entity.GetType().GetProperty(usuarioModificacionName) != null) entry.Property(usuarioModificacionName).CurrentValue = usuarioModificacion;
            if (entry.Entity.GetType().GetProperty(FECHACREACIONNAME) != null) entry.Property(FECHACREACIONNAME).IsModified = false;
            if (entry.Entity.GetType().GetProperty(usuarioCreacionName) != null) entry.Property(usuarioCreacionName).IsModified = false;
        }
    }
}
