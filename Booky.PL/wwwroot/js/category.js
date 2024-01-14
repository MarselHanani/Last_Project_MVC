function LoadCategories(){
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: "/Admin/Category/getAll",
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json'
        },
        "columns": [
            {data: "id"},
            {data: "name"},
            {data: null,
                render: function (row){
                    return `
                <div class="w-75 btn-group" role="group">
                    <a href="/Admin/Category/Edit?id=${row.id}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a> 
                    <a href="/Admin/Category/Delete?id=${row.id}" class="btn btn-danger mx-2"><i class="bi bi-trash3"></i>Delete</a>
                </div>
                `
                }}
        ]
    });
}