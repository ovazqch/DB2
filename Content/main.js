
function newInvoice() {
    $.get('/Invoices/CreateInvoice/', function (data) {
        $('#modalInvoicesList').modal('show');
        $(".modal-bodyInvoicesList").html(data);
    });
}

function newProduct() {
    $.get('/Home/CreateProduct/', function (data) {
        $('#modalCreateProduct').modal('show');
        $(".modal-bodyNewProduct").html(data);
    });
}

function editProduct(id) {
    $.get('/Home/EditProduct/'+ id, function (data) {
        $('#modalEditProduct').modal('show');
        $(".modal-EditProduct").html(data);
    });
}

