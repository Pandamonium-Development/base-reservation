using System.ComponentModel;

namespace BaseReservation.Application.ResponseDTOs.Enums;

public enum Role
{
    [Description("Administrador")]
    ADMINISTRADOR = 1,

    [Description("Usuario")]
    USUARIO = 2,

    [Description("Moderador")]
    MODERADOR = 3,

    [Description("Invitado")]
    INVITADO = 4
}