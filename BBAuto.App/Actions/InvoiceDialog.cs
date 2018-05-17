using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.Logic.ForCar;

namespace BBAuto.App.Actions
{
  internal static class InvoiceDialog
  {
    internal static bool CreateNewInvoiceAndOpen(int carId)
    {
      if (carId == 0)
        return false;

      Invoice invoice = new Invoice(carId);

      return Open(invoice);
    }

    internal static bool Open(Invoice invoice)
    {
      var invoiceAe = new Invoice_AddEdit(invoice);

      return invoiceAe.ShowDialog() == System.Windows.Forms.DialogResult.OK;
    }
  }
}
