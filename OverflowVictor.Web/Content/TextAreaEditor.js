function execCommand(e) {
    $(this).toggleClass("selected");
    var contentWindow = window.editor.contentWindow;
    contentWindow.focus();
    contentWindow.document.execCommand($(this).data("commandName"), false, "");
    contentWindow.focus();
    return false;
}
$.fn.myTextEditor = function (options) {
    // extend the option with the default ones
    var settings = $.extend({
        width: "200px",
        height: "200px"
    }, options);
    return this.each(function () {
        var $this = $(this).hide();
        // create a container div on the fly
        var containerDiv = $("<div/>", {
            css: {
                width: settings.width,
                height: settings.height,
                border: "1px solid #ccc"
            }
        });
        $this.after(containerDiv);
        var editor = $("<iframe/>", {
            frameborder: "0",
            css: {
                width: settings.width,
                height: settings.height
            }
        }).appendTo(containerDiv).get(0);
        var buttonPane = $("<div/>", {
            "class": "editor-btns",
            css: {
                width: settings.width,
                height: "20px"
            }
        }).prependTo(containerDiv);
        // the bold button 
        var btnBold = $("<a/>", {
            href: "#",
            text: "B",
            data: {
                commandName: "bold"
            },
            click: execCommand
        }).appendTo(buttonPane);
        // opening and closing the editor is a workaround to solve issue in Firefox
        editor.contentWindow.document.open();
        editor.contentWindow.document.close();
        editor.contentWindow.document.designMode = "on";
    });
  

    // you can go on add buttons

    
}