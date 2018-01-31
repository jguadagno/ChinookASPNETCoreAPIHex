using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class InvoiceLineCoverter
    {
        public static InvoiceLineViewModel Convert(InvoiceLine invoiceLine)
        {
            var invoiceLineViewModel = new InvoiceLineViewModel()
            {
                InvoiceLineId = invoiceLine.InvoiceLineId,
                InvoiceId = invoiceLine.InvoiceId,
                TrackId = invoiceLine.TrackId,
                UnitPrice = invoiceLine.UnitPrice,
                Quantity = invoiceLine.Quantity
            };

            return invoiceLineViewModel;
        }
        
        public static List<InvoiceLineViewModel> ConvertList(List<InvoiceLine> invoiceLines)
        {
            List<InvoiceLineViewModel> invoiceLineViewModels = new List<InvoiceLineViewModel>();
            foreach(var i in invoiceLines)
            {
                var invoiceLineViewModel = new InvoiceLineViewModel
                {
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceId = i.InvoiceId,
                    TrackId = i.TrackId,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                };
                invoiceLineViewModels.Add(invoiceLineViewModel);
            }

            return invoiceLineViewModels;
        }
    }
}