var utils =
{
    htmlEncode: function (value) {
        return $('<div/>').text(value).html();
    },
    toHtmlString: function (elem) {
        return $('<div></div>').html($(elem).clone()).html();
    },
    htmlDecode: function (value) {
        return $('<div/>').html(value).text();
    },
    serialize: function ($form) {
        var result = {};
        $.each($form.serializeArray(), function () {
            if (result[this.name] !== undefined) {
                if (!result[this.name].push) { result[this.name] = [result[this.name]]; }
                result[this.name].push(this.value || '');
            } else {
                result[this.name] = this.value || '';
            }
        });
        return result;
    }
}
