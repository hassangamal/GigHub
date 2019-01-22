
var GigDetailsController = function (followingServices) {
    var button;

    var init = function (container) {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-user-id");
        if (button.hasClass("btn-default"))
            followingServices.createFollowing(followeeId, done, fail);
        else
            followingServices.deleteFollowing(followeeId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Follow") ? "Following" : "Follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Somthing Faild!");
    };

    return {
        init: init
    }

}(FollowingServices);
