using Microsoft.JSInterop;

namespace BlazorCrudPersonasSql.Client.Helpers
{
    public static class JSextensions
    {
        public static async Task<object> MostrarMensaje(this IJSRuntime js, string mensaje)
        {
            return await js.InvokeAsync<object>("Swal.fire", mensaje);
        }

        public static async Task<object> MostrarMensaje(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensajeAlert)
        {
            return await js.InvokeAsync<object>("Swal.fire", titulo, mensaje, tipoMensajeAlert.ToString());
        }

        public enum TipoMensajeSweetAlert  
        {
            question, warning, error, success, info
        }

        public static async Task<bool> Confirmar(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensajeAlert)
        {
            return await js.InvokeAsync<bool>("CustomConfirm", titulo, mensaje, tipoMensajeAlert.ToString());
        }
    }
}
