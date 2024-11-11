using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using Tienda_Online.Frontend.Repositories;
using Tienda_Online.Shared.DTOs;
using Tienda_Online.Shared.Entidades;

namespace Tienda_Online.Frontend.Pages.CarritosDeCompra
{
    public partial class CarritoDeCompraIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;


        public List<CarritoConProductoDTO>? CarritoConProducto { get; set; }

        public int? TotalCompra = 0;
        protected async override Task OnInitializedAsync()
        {
            await CargarDatos();
        }


        private void IncrementarTotalAPagar()
        {
            TotalCompra = 0;
            foreach (var carrito in CarritoConProducto!)
            {
                TotalCompra += Convert.ToInt32(carrito.PrecioTotal);
            }
        }



        private async Task CargarDatos()
        {
            var responseHttp = await Repository.GetAsync<List<CarritoConProductoDTO>>("/api/carritoCompra/ObtenerCarritos");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }


            CarritoConProducto = responseHttp.Response;
            IncrementarTotalAPagar();
        }

        private async Task IncrementarCantidad(CarritoConProductoDTO carrito)
        {
            var responseHttp = await Repository.GetAsync<CarritoDeCompra>($"/api/carritoCompra/{carrito.IdCarrito}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                return;
            }
            var producto = responseHttp.Response;
            producto!.CantidadProducto = producto.CantidadProducto + 1;

            var HttpResponse = await Repository.PutAsync($"/api/carritoCompra/Actualizar", producto);
            if (HttpResponse.Error)
            {
                var meesageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", meesageError, SweetAlertIcon.Error);
                return;
            }
            await CargarDatos();
            IncrementarTotalAPagar();
        }

        private async Task DecrementarCantidad(CarritoConProductoDTO carrito)
        {
            var responseHttp = await Repository.GetAsync<CarritoDeCompra>($"/api/carritoCompra/{carrito.IdCarrito}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                return;
            }
            var producto = responseHttp.Response;

            if(carrito.CantidadProductos > 1)
            {
                producto!.CantidadProducto = producto.CantidadProducto - 1;

                var HttpResponse = await Repository.PutAsync($"/api/carritoCompra/Actualizar", producto);
                if (HttpResponse.Error)
                {
                    var meesageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", meesageError, SweetAlertIcon.Error);
                    return;
                }
                await CargarDatos();
                IncrementarTotalAPagar();
            }
            
        }

        private async Task DeleteAsync(CarritoConProductoDTO carrito)
        {

            var responseHttp = await Repository.DeleteAsync($"/api/carritoCompra/{carrito.IdCarrito}");
            if (responseHttp.Error)
            {
                if(responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                return;
            }

            await CargarDatos();
            IncrementarTotalAPagar();
        }

    }
}