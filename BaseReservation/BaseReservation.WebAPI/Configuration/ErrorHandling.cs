using System.Text;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Error handling configuration class
/// </summary>
public static class ErrorHandling
{
    /// <summary>
    /// Get message exception
    /// </summary>
    /// <param name="excepcion">Exception handled</param>
    /// <returns>string</returns>
    public static string? GetMessageException(Exception excepcion)
    {
        if (excepcion == null) return null;

        var errorMessage = new StringBuilder();
        errorMessage.Append(excepcion.Message);
        return excepcion.InnerException != null ? GetInnerMessageExceptions(excepcion) : errorMessage.ToString();
    }

    /// <summary>
    /// Get the inner message exception
    /// </summary>
    /// <param name="excepcion">Exception handled</param>
    /// <returns>string</returns>
    private static string GetInnerMessageExceptions(Exception excepcion)
    {
        if (excepcion.InnerException == null) return excepcion.Message;

        return $"{excepcion.Message} : {GetInnerMessageExceptions(excepcion.InnerException)}";
    }
}