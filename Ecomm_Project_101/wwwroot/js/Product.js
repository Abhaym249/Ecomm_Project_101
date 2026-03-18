var dataTable;
$(document).ready(function(){
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "author", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            {
                "data": "id", "render": function (data) {
                    return `
                    <div class="text-center">
                    <a href="/Admin/Product/Upsert/${data}" class="btn btn-info">
                    <i class="fas fa-edit"></i>
                    </a>
                        <a class="btn btn-danger" onclick="Delete('/Admin/Product/Delete/${data}')">
                        <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                    `
                }
            }
        ]
    })

}
function Delete(url) {
    alert(url);
}