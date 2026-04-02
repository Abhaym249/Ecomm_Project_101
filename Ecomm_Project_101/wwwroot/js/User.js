var datatable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    datatable = $("#tblData").DataTable({
        "ajax": {
            "url":"/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": { id: "id", "lockoutEnd": "Lockout End" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        //user locked
                        return `
                        <div class ="text-center">
                        <a class="btn btn-danger" onclick="lockUnlock('${data.id}')">Unlock
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
    alert(id)
};