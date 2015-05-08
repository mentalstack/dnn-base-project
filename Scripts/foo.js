var foo =
{
    init: function () { },

    getJn: function () {
        $.get(
           "/DesktopModules/DNNBase/API/Web/GetAll",
           function (result) {
               $.each(result, function () {
                   $("#result").append(this.KeyId + " " + this.Name + " " + this.Description);
               })
           });
    },

    add: function () {       
        var name = $("#" + txtN).val();
        var desc = $("#" + txtD).val();

            var obj = {
                Name:name,
                Description:desc
            };

            $.ajax({
                url: "/DesktopModules/DNNBase/API/Web/Add",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(obj)
            });       
    },

    update: function () {
        var name = $("#" + txtN).val();
        var desc = $("#" + txtD).val();
        var hdfld = $("#" + hdField).val();

        var obj = {
            FooId: hdfld,
            Name: name,
            Description: desc
        };

        $.ajax({
            url: "/DesktopModules/DNNBase/API/Web/Update",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj)
        });
    }
}