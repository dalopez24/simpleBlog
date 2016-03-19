$(document).ready(function() {
    //var deleteObj;
    //$('.delete-user').click(function() {
    //    deleteObj = $(this);
    //    $('#delete-dialog').dialog('open');
    //    return false;
    //});

    //$('#delete-dialog').dialog({
    //    autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
    //    buttons: {
    //        "Continue": function () {
    //            $.post(deleteObj[0].href, function (data) {
    //                if (data == '<%= Boolean.TrueString %>') {
    //                    deleteLinkObj.closest("tr").hide('fast'); 
                        
    //                }
    //            });
    //            $(this).dialog("close");
    //        },
    //        "Cancel": function () {
    //            $(this).dialog("close");
    //        }
    //    }
    //});

    $("[data-slug]").each(function () {
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        $sendSlugFrom.keyup(function(){
            var slug = $sendSlugFrom.val();
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, "");
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-");
            if (slug.charAt(slug.length - 1) == "-")
                slug = slug.substr(0, slug.length - 1);

            $this.val(slug);
        });
    });
});