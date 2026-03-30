var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "pageLength": 2, // default selected value
        "lengthMenu": [[2, 4, 6, 8], [2, 4, 6, 8]],
        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Category/Upsert/${data}" class="btn btn-info ">
                        <i class="fas fa-edit"></i>
                    </a>`;
                },
                "width": "10%"
            },
            {
                "data": "name",
                "width": "70%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a onClick=Delete('/Admin/Category/Delete/${data}') 
                        class="btn btn-danger btn-sm">
                        <i class="fas fa-trash"></i>
                    </a>`;
                },
                "width": "10%"
            }
        ]
    });
}

function Delete(url) {

    swal({
        title: "Want to Delete data?",
        text: "Delete information !!!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {

        if (willDelete) {

            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {

                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }

                }
            });

        }

    });

}