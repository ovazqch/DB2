function itemDetails(prodCode, ) {
    var invoiceId = document.getElementById('InvoiceID').value;
    var prodCode = document.getElementById('ProductCode').value;

    debugger;
    var lineItemEdit = {
        InvoiceID: invoiceId,
        ProductCode: prodCode
    }

    $.get('/Invoices/ItemDetails/' + lineItemEdit, function (data) {
        $('#exampleModal').modal('show');
        $(".modal-body").html(data);
    });
    
}