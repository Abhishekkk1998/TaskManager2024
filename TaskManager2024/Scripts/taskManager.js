$(document).ready(function () {
    $("#taskTable").sortable({
        update: function (event, ui) {
            var taskOrder = $(this).sortable('toArray', { attribute: 'data-id' }).toString();

            // AJAX call to update task priorities in the backend
            $.ajax({
                url: '/Task/UpdateTaskPriorities',
                type: 'POST',
                data: {
                    order: taskOrder
                },
                success: function (response) {
                    if (response.success) {
                        alert('Task priorities updated successfully.');
                    } else {
                        alert('Failed to update task priorities.');
                    }
                },
                error: function () {
                    alert('An error occurred while updating task priorities.');
                }
            });
        }
    });

    
    $("#taskTable tr").each(function (index, element) {
        $(element).attr('data-id', $(element).find('.task-id').text());
    });
});
