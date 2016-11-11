(function () {

    var checkBoxes;
    var mainCheckBox;
    var checks = [];

    $(document).ready(initialize());

    function initialize() {
        checkBoxes = $("input[type='checkbox'][role='select']:not([id='selectAll']):not([readonly]):not([disabled])");
        mainCheckBox = $("#selectAll");

        bindCreateButtons();
        initializeCheckboxes();
        bindRenameButton();
        bindDeleteButton();
        initializeDragAndDrop();
    }

    function initializeAsync() {
        checkBoxes = $("input[type='checkbox'][role='select']:not([id='selectAll']):not([readonly]):not([disabled])");
        mainCheckBox = $("#selectAll");

        checks = [];
        initializeCheckboxes();
        initializeDragAndDrop();
        showActionsPanel(checks.length);
    }

    function bindCreateButtons() {
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
                bindSendDialogAsync(dialog);
                dialog.modal("show");
            });
        });
    }

    function initializeCheckboxes() {
        
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

            showActionsPanel(checks.length);
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

            showActionsPanel(checks.length);
        });
    }

    function showActionsPanel(length) {
        if (length > 0) {
            $("#actionsPanel").removeClass("hidden");
            if (length == 1) {
                $("#renameFolderButton").removeClass("hidden");
                prepareRenameDialog();
            } else {
                $("#renameFolderButton").addClass("hidden");
            }
        } else {
            $("#actionsPanel").addClass("hidden");
        }
    }

    function prepareRenameDialog() {
        $("input[name='OldName']").val(checks[0]);
        $("input[name='NewName']").val(checks[0]);
        $("#oldName").text(checks[0]);
    }

    function bindRenameButton() {
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
                bindSendDialogAsync(dialog);
                $(".modal").modal("show");
            });
        });
    }

    function bindDeleteButton() {
        $("#delete").bind("click", function (e) {
            e.preventDefault();
            var url = $(this).attr("href");
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                url: url,
                data: JSON.stringify({ 'folders': checks }),
                statusCode: {
                    200: function (response, status, xhr) {
                        proceedResponse(response, status, xhr, "#mainContainer");
                    }
                }
            });
        });
    }

    function initializeDragAndDrop() {
        $(".draggable").draggable({
            drag: function (event, ui) { },
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

    function proceedResponse(response, status, xhr, container, shouldCloseDialog) {
        if ($(response).filter("#validationContent").length > 0) {
            var $container = $(container);
            $("#validationResult", $container).html(response);
        } else {
            $("#foldersResults").html(response);
            initializeAsync();
            if (shouldCloseDialog) {
                $(".modal").modal("hide");
            }
        }
    }

    function bindSendDialogAsync(dialog) {
        dialog.find("[type='submit']").bind("click", function (e) {
            e.preventDefault();
            var form = $("form", dialog);
            $.ajax({
                type: 'POST',
                url: form.attr("action"),
                data: form.serializeArray(),
                statusCode: {
                    200: function (response, status, xhr) {
                        proceedResponse(response, status, xhr, dialog, true);
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