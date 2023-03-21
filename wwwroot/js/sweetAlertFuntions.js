function blogDelete(event) {
    event.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Deleted!',
                text: 'Your file has been deleted.',
                icon: 'success',
                showConfirmButton: false
            })
        }
        setTimeout(function () {
            window.location.href = event.target.href;
        }, 1500);
    })
}

function blogSave(event) {
    event.preventDefault();
    Swal.fire({
        title: 'Do you want to save the changes?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Save',
        denyButtonText: `Don't save`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Saved!',
                icon: 'success',
                preConfirm: function () {
                    var form = document.querySelector('#formEdit');
                    form.submit();
                }
            })
        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    })
}

document.getElementById('formCreate').addEventListener('submit', function (event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    // Perform additional validation or display a confirmation message here
    Swal.fire({
        title: 'Do you want to create this blog?',
        showCancelButton: true,
        confirmButtonText: 'Create',
        cancelButtonText: `Change`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Created!',
                icon: 'success',
                preConfirm: function () {
                    var form = document.querySelector('#formCreate');
                    form.submit();
                }
            })
        }
    });
});