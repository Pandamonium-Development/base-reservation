namespace BaseReservation.Infrastructure.Models;
public partial class ReservaServicio
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public byte IdServicio { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
