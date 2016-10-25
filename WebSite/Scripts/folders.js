(function() {
    $(document).ready(initialize());

    function initialize() {
        $("#createFolderButton").bind("click", function (e) {
            if ($(".modal").length > 0) {
                //$(".modal").modal("show");
                return;
            }
            var url = $(this).attr("href");
            $.get(url, function (response) {
                $("body").append(response);
                $(".modal").modal("show");
            });
        });

        var checkBoxes = $("input[type='checkbox'][role='select']:not([id='selectAll'])");
        var mainCheckBox = $("#selectAll");
        var checks = [];
        checkBoxes.bind("click", function (e) {
            if (e.target.checked) {
                checks.push(e.target.getAttribute("id"));
            } else {
                mainCheckBox.prop("checked", false);
                var index = checks.indexOf(e.target.getAttribute("id"));
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
                    checks.push(element.getAttribute("id"));
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
                dataType: 'json',
                type: 'POST',
                url: url,
                data: JSON.stringify({ 'folders': checks }),
                statusCode: {
                    200: function() {
                        location.reload();
                    }
                }
            });
        });

        $("#renameFolderButton").bind("click", function (e) {
            if ($(".modal").length > 0) {
                //$(".modal").modal("show");
                return;
            }
            var url = $(this).attr("href") + "?folder=" + checks[0];
            $.get(url, function (response) {
                $("body").append(response);
                $(".modal").modal("show");
            });
        });
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
            var target = $(this).attr("id");
            var source = ui.draggable.attr("id");
            var path = $("#pathInput").val();
            $.get("/Folders/CopyTo/" + path + "?source=" + source + "&target=" + target);
        },
        classes: {
            "ui-droppable-hover": "warning"
        }
    });
})();