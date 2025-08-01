using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using StockManager.Models;
using System.IO;
using System.Threading.Tasks;

public class OrdenCompraPrintService
{
    public async Task<byte[]> GenerarOrdenCompraPdfAsync(OrdenCompra orden)
    {
        return await Task.Run(() =>
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.Header().Text($"Orden de Compra #{orden.NumeroOrden}").FontSize(20).Bold();
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Proveedor: {orden.Proveedor?.Nombre}");
                        col.Item().Text($"Fecha de creación: {orden.FechaCreacion:dd/MM/yyyy}");
                        col.Item().Text($"Usuario: {orden.Usuario?.Nombre}");
                        col.Item().Text($"Entrega estimada: {orden.FechaEntregaEstimada:dd/MM/yyyy}");
                        col.Item().Text($"Notas: {orden.Notas}");

                        col.Item().LineHorizontal(1);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Producto").Bold();
                                header.Cell().Text("Cantidad").Bold();
                                header.Cell().Text("Precio Unitario").Bold();
                                header.Cell().Text("Subtotal").Bold();
                            });

                            foreach (var det in orden.Detalles)
                            {
                                table.Cell().Text(det.Producto?.Nombre ?? "");
                                table.Cell().Text(det.Cantidad.ToString());
                                table.Cell().Text($"RD${det.PrecioCompraUnitario:N2}");
                                table.Cell().Text($"RD${det.Subtotal:N2}");
                            }
                        });

                        col.Item().LineHorizontal(1);

                        col.Item().AlignRight().Text($"Subtotal: RD${orden.Subtotal:N2}");
                        col.Item().AlignRight().Text($"Impuestos: RD${orden.Impuestos:N2}");
                        col.Item().AlignRight().Text($"Total: RD${orden.Total:N2}").Bold();
                    });
                    page.Footer().AlignCenter().Text("Generado por el sistema de compras");
                });
            });

            using var ms = new MemoryStream();
            pdf.GeneratePdf(ms);
            return ms.ToArray();
        });
    }
}