using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
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

        public List<CarritoDeCompra>? CarritosDeCompra { get; set; }

        public List<CarritoConProductoDTO>? CarritoConProducto { get; set; }

        public Producto? producto = new();

        protected async override Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<List<CarritoConProductoDTO>>("/api/carritoCompra/ObtenerCarritos");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }


            CarritoConProducto = responseHttp.Response;
            Console.WriteLine($"CarritoConProducto: {CarritoConProducto?.Count}");

        }

    }
}