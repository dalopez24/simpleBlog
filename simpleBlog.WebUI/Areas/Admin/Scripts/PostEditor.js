$(document).ready(function () {

    var $tagEdior = $(".post-tag-editor");

    $tagEdior
        .find(".tag-select")
        .on("click", "> li > a", function(e) {
            e.preventDefault();

            var $this = $(this);
            var $tagParent = $this.closest("li");
            $tagParent.toggleClass("selected");

           
            var selected = $tagParent.hasClass("selected");
            $tagParent.find(".selected-input").val(selected);
        });

    var $addTagButton = $tagEdior.find(".add-tag-button");
    var $newTagName = $tagEdior.find(".new-tag-name");
    $addTagButton.click(function (e) {
        e.preventDefault();
        addTag($newTagName.val());
    });

    $newTagName
        .keyup(function () {
            if ($newTagName.val().trim().length > 0)
                $addTagButton.prop("disabled", false);
            else
                $addTagButton.prop("disabled", true);
        })
        .keydown(function (e) {
            if (e.which != 13)
                return;
            e.preventDefault();
            addTag($newTagName.val());

        });

    function addTag(name) {

        var newIndex = $tagEdior.find(".tag-select > li").size() - 1;
        
        $tagEdior
            .find(".tag-select > li.template")
            .clone()
            .removeClass("template")
            .addClass("selected")
            .find(".name").text(name).end()
            .find(".name-input").val(name).attr("name", "Tags[" + newIndex + "].Name").end()
            .find(".selected-input").attr("name", "Tags[" + newIndex + "].isChecked").val(true).end()
            .appendTo($tagEdior.find(".tag-select"));

        $newTagName.val("");
        $addTagButton.prop("disbaled", true);
    }
});