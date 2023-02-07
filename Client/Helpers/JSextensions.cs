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

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
   => js.InvokeAsync<object>(
       "localStorage.setItem",
       key, content
       );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);
    }
}
