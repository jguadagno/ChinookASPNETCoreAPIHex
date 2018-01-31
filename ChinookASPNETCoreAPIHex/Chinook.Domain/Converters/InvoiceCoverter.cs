using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class InvoiceCoverter
    {
        public static InvoiceViewModel Convert(Invoice invoice)
        {
            var invoiceViewModel = new InvoiceViewModel()
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                InvoiceDate = invoice.InvoiceDate,
                BillingAddress = invoice.BillingAddress,
                BillingCity = invoice.BillingCity,
                BillingState = invoice.BillingState,
                BillingCountry = invoice.BillingCountry,
                BillingPostalCode = invoice.BillingPostalCode,
                Total = invoice.Total
            };

            return invoiceViewModel;
        }
        
        public static List<InvoiceViewModel> ConvertList(List<Invoice> invoices)
        {
            List<InvoiceViewModel> invoiceViewModels = new List<InvoiceViewModel>();
            foreach(var i in invoices)
            {
                var invoiceViewModel = new InvoiceViewModel
                {
                    InvoiceId = i.InvoiceId,
                    CustomerId = i.CustomerId,
                    InvoiceDate = i.InvoiceDate,
                    BillingAddress = i.BillingAddress,
                    BillingCity = i.BillingCity,
                    BillingState = i.BillingState,
                    BillingCountry = i.BillingCountry,
                    BillingPostalCode = i.BillingPostalCode,
                    Total = i.Total
                };
                invoiceViewModels.Add(invoiceViewModel);
            }

            return invoiceViewModels;
        }
    }
}