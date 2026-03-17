var dataTable;
$(document).ready(function ()
{
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable(
        {
            "ajax":
            {
                "url": "/Admin/CoverType/GetAll"
            },
            "columns": [{ "data": "name", "width": "70%" },
                {
                    data: "id", "render": function (data) {
                        return `
                        <div class="text-center">
                        <a href="/Admin/CoverType/Upsert/${data}" 
                        class="btn btn-info">
                        <i class="fas fa-edit"></i>
                        </a>
                         <a class="btn btn-danger"
                        onclick="Delete('/Admin/CoverType/Delete/${data}')">
                        <i class="fas fa-trash-alt"></i>
                        </a>
                        </div>`
                    }
                }
            ],
         })
}
function Delete(url) {
    //alert(url);
    swal({
        title: "Want to delete data?",
        icon: "warning",
        text: "Delete Information",
        buttons: true,
        dangerMode: true
    }).then((willdelete) => {
            if (willdelete) {
                $.ajax({
                    url: url,
                    type: "DELETE",
                    success: function (data) {
                        if (data.success) {
                            toastr.success(data.message);
                            dataTable.ajax.reload();//if deleted
                        }
                        else {
                            toastr.error(data.message);//not deleted
                        }
                    }
                })
            }
        })
}