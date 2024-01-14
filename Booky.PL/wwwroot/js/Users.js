function loadDataTable() {
    dataTable = $('#tblData').dataTable({

        "ajax": {
            url: '/User/Users/getAll',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json'
        },
        "columns": [
            {data: 'id'},
            {data: 'fname'},
            {data: 'lname'},
            {data: 'email'},
            {data: 'roles'},
            {data: null,
                render: function (row){
                    return `
                    <div class="w-75 btn-group" role="group">
                        <a href="/User/Users/Edit?id=${row.id}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit</a>
                        <a href="/User/Users/Delete?id=${row.id}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete</a>
                    </div>
                `
                }}
        ]
    });
}