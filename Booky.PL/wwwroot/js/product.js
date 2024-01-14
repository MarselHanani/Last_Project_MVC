function loadDataTable() {
     dataTable = $('#tblData').dataTable({

        "ajax": {
            url: '/Admin/Product/getAll',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json'
        },
        "columns": [
            {data: 'isbn'},
            {data: 'title'},
            {data: 'description'},
            {data: 'author'},
            {data: 'price'},
            {data: 'category.name'},
            {data: 'listPrice'},
            {data: 'price'},
            {data: 'price50'},
            {data: 'price100'},
            {data: null,
            render: function (row){
                return `
                    <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Product/Edit?id=${row.id}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit</a>
                        <a href="/Admin/Product/Delete?id=${row.id}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete</a>
                    </div>
                `
            }}
        ]


    });
}