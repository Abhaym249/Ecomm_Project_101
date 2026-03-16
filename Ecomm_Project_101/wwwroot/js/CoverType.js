import { data } from "jquery";

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
            "columns": [{ "data": "name", "Width": "70%" },
                {
                    data: "id", "render": function (data) {
                        return `
                        <div class="text-center">
                        <a href="/Admin/CoverType/Upsert/${data}" 
                        class="btn btn-info text-white ">
                        <i class=" fas fa-trash-alt"></i>
                        </a>
                        </div>`
                    }
                }
            ],
         })
}
function Delete(url) {
    alert(url);
}