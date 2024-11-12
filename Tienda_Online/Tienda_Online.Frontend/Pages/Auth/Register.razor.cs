using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Tienda_Online.Frontend.Repositories;
using Tienda_Online.Frontend.Services;
using Tienda_Online.Shared.DTOs;
using Tienda_Online.Shared.Enums;

namespace Tienda_Online.Frontend.Pages.Auth
{
    public partial class Register
    {
        private UserDTO userDTO = new();

        [Inject] NavigationManager NavigationManager { get; set; } = null!;
        [Inject] SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] IRepository Repository { get; set; } = null!;
        [Inject] ILoginService LoginService { get; set; } = null!;

        private async Task CreateUserAsync()
        {
            userDTO.UserName = userDTO.Email;
            userDTO.userType = UserType.Cliente;
            var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("/api/cuentas/CreateUser", userDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }
    }
}