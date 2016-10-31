(function() {
    $(document).ready(initialize());

    function initialize() {
        $("#createFolderButton, #createFileButton").bind("click", function (e) {
            var button = $(e.target);
            var dialogSelector = button.data("target");
            var dialog = $(dialogSelector);
            if (dialog.length > 0) {
                return;
            }
            var url = $(this).attr("href");
            $.get(url, function (response) {
                $("body").append(response);
                dialog = $(dialogSelector);
                BindSendDialogAsync(dialog);
                dialog.modal("show");
            });
        });

        var checkBoxes = $("input[type='checkbox'][role='select']:not([id='selectAll'])");
        var mainCheckBox = $("#selectAll");
        var checks = [];
        checkBoxes.bind("click", function (e) {
            if (e.target.checked) {
                checks.push(getNameForCheckBox(e.target));
            } else {
                mainCheckBox.prop("checked", false);
                var index = checks.indexOf(getNameForCheckBox(e.target));
                if (index > -1) {
                    checks.splice(index, 1);
                }
            }

            showActionsPanel(checks.length, false);
        });

        mainCheckBox.bind("click", function (e) {
            if (mainCheckBox.prop("checked")) {
                checkBoxes.each(function (index, element) {
                    element.checked = true;
                    checks.push(getNameForCheckBox(element));
                });
            } else {
                checkBoxes.each(function (index, element) {
                    element.checked = false;
                });
                checks = [];
            }

            showActionsPanel(checks.length, true);
        });

        $("#delete").bind("click", function (e) {
            e.preventDefault();
            var url = $(this).attr("href");
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                url: url,
                data: JSON.stringify({ 'folders': checks }),
                statusCode: {
                    200: function(response, status, xhr) {
                        proceedResponse(response, status, xhr, "#mainContainer");
                    }
                }
            });
        });

        $("#renameFolderButton").bind("click", function (e) {
            var button = $(e.target);
            var dialogSelector = button.data("target");
            var dialog = $(dialogSelector);
            if (dialog.length > 0) {
                //$(".modal").modal("show");
                return;
            }
            var url = $(this).attr("href") + "?folder=" + checks[0];
            $.get(url, function (response) {
                $("body").append(response);
                var dialog = $(dialogSelector);
                BindSendDialogAsync(dialog);
                $(".modal").modal("show");
            });
        });
    }

    function getNameForCheckBox(checkBox) {
        var $checkBox = $(checkBox);
        var row = $checkBox.parent().parent();
        return getNameForRow(row);
    }

    function getNameForRow(row) {
        var $row = $(row);
        var name = $row.find("[name='Name']").val();
        return name;
    }

    function showActionsPanel(length, main) {
        if (length > 0) {
            $("#actionsPanel").removeClass("hidden");
            if (length == 1) {
                $("#renameFolderButton").removeClass("hidden");
            } else {
                $("#renameFolderButton").addClass("hidden");
            }
        } else {
            $("#actionsPanel").addClass("hidden");
        }
    }

    $(".draggable").draggable({
        drag: function(event, ui) { },
        helper: 'clone'
    });
    $(".droppable").droppable({
        drop: function (event, ui) {
            var target = getNameForRow(this);
            var source = getNameForRow(ui.draggable);
            var path = $("#pathInput").val();
            $.get("/Folders/CopyTo/" + path + "?source=" + source + "&target=" + target, function (response, status, xhr) {
                proceedResponse(response, status, xhr, "#mainContainer");
            });
        },
        classes: {
            "ui-droppable-hover": "warning"
        }
    });

    function proceedResponse(response, status, xhr, container) {
        var contentType = xhr.getResponseHeader("Content-Type");
        if ("url; charset=utf-8" === contentType) {
            window.location = "/Folders/" + response;
        } else {
            var $container = $(container);
            $("#validationResult", $container).html(response);
        }
    }

    function BindSendDialogAsync(dialog) {
        dialog.find("[type='submit']").bind("click", function (e) {
            e.preventDefault();
            var form = $("form", dialog);
            $.ajax({
                type: 'POST',
                url: form.attr("action"),
                data: form.serializeArray(),
                statusCode: {
                    200: function (response, status, xhr) {
                        proceedResponse(response, status, xhr, dialog);
                    },
                    414: function (response, status, xhr) {
                        $("#validationResult", dialog).html('<div class="alert alert-danger alert-dismissible" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                           'Sorry, your uri is to long for us' + '</div>');
                    } 
                }
            });
        });
    }
})();