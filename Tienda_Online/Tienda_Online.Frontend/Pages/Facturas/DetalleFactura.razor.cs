using Microsoft.AspNetCore.Components;
using System.Text;

namespace Tienda_Online.Frontend.Pages.Facturas
{
    public partial class DetalleFactura
    {
        [Parameter]
        public string EncodedHtml { get; set; } = null!;

        private MarkupString HtmlContent => new MarkupString(Encoding.UTF8.GetString(Convert.FromBase64String(EncodedHtml)));
    }


}
