var datatable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    datatable = $("#tblData").DataTable({
        "ajax": {
            "url":"/Admin/User/GetAll"
        }, "pageLength": 2, // default selected value
        "lengthMenu": [[2, 4, 6, 8], [2, 4, 6, 8]],
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": { id: "id", "LockoutEnd": "LockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        //user locked
                        return `
                        <div class ="text-center">
                        <a class="btn btn-info" onclick="lockUnlock('${data.id}')">Unlock
                        </a>
                        </div>
                        `
                    }
                    else {
                        return `
                        <div class ="text-center">
                        <a class="btn btn-danger" onclick="lockUnlock('${data.id}')">lock
                        </a>
                        </div>
                        `
                    }
                }
            }
        ]
    })
}
function lockUnlock(id) {
    // alert(id)
    $.ajax({
        url: "/Admin/User/LockUnlock",
        type: "Post",
        data: JSON.stringify(id),
        contentType: "application/JSON",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                datatable.ajax.reload();
            }
            else {
                toastr.error(data.message);
                datatable.ajax.reload();
            }
        }
    })
};