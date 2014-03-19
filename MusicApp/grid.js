$(document).ready(function () {
    var baseUrl = "http://musicapp.jwendl.net/odata/Albums",
        expandStatement = "?$expand=Artist";

    $("#albumGrid").kendoGrid({
        dataSource: {
            type: "odata",
            transport: {
                read: {
                    url: baseUrl + expandStatement,
                    dataType: "json"
                },
                create: {
                    url: baseUrl + expandStatement,
                    dataType: "json"
                },
                update: {
                    url: function (data) {
                        return baseUrl + "(" + data.Id + ")";
                    },
                    dataType: "json"
                },
                destroy: {
                    url: function (data) {
                        return baseUrl + "(" + data.Id + ")";
                    },
                    dataType: "json"
                }
            },
            schema: {
                data: function (data) {
                    if (data.value) {
                        return data.value;
                    }

                    delete data["odata.metadata"];
                    return [data];
                },
                total: function (data) {
                    return data["odata.count"];
                },
                model: {
                    id: "Id",
                    fields: {
                        Id: { type: "number", nullable: false },
                        Name: { type: "string", validation: { required: true } },
                        ArtistId: { type: "number", defaultValue: 1, validation: { required: true } },
			ImageUrl: { type: "string" }
                    }
                }
            },
            requestEnd: function (e) {
                if (e.type === 'update' || e.type === 'create') {
                    this.read();
                }
            },
            error: function (e) {
                var message = e.xhr.responseJSON["odata.error"].message.value;
                var innerMessage = e.xhr.responseJSON["odata.error"].innererror.message;
                alert(message + "\n\n" + innerMessage);
            },
            batch: false,
            pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        },
        save: function (e) {
            var dropDownList = $("#ArtistIdDropDownList").data("kendoDropDownList");
            e.model.ArtistId = dropDownList.value();
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        height: 430,
        toolbar: ["create"],
        editable: "inline",
        sortable: true,
        pageable: true,
        columns: [{
            field: "Name",
            filterable: true
        },
        {
            field: "ArtistId",
            editor: artistIdEditor,
            template: "#if (data.Artist !== undefined) {# #= Artist.Name # #} else {# #= '' # #}#",
            title: "Artist"
        },
	{
            field: "ImageUrl",
            filterable: true
        },
        {
            command: ["edit", "destroy"],
            title: "&nbsp;",
            width: "182px"
        }],
    });
});

function artistIdEditor(container, options) {
    var foreignKeyUrl = "http://musicapp.jwendl.net/odata/Artists";

    $('<input required id="ArtistIdDropDownList" data-text-field="Name" data-value-field="Id" data-bind="value:' + options.field + '" />')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            dataSource: {
                type: "odata",
                transport: {
                    read: {
                        url: foreignKeyUrl,
                        dataType: "json"
                    }
                },
                schema: {
                    data: function (data) {
                        return data.value;
                    },
                    total: function (data) {
                        return data["odata.count"];
                    }
                }
            }
        });
}

