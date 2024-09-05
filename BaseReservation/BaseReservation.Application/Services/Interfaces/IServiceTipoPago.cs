using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTipoPago
{
    Task<ICollection<ResponseTipoPagoDto>> ListAllAsync();
}