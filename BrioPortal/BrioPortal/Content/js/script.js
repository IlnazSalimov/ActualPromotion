function infoCardPanel(app) {
    var self = this;
    var createForm = $(".content-right-bar.account-create-form");
    var mappingForm = $(".content-right-bar.info-card");

    self.show = function (infoCard) {
        mappingForm.find(".names").html(infoCard.Surname + " " + infoCard.Name + " " + infoCard.Patronymic);
        mappingForm.find(".company span").html(infoCard.CompanyName);
        mappingForm.find(".phone span").html(infoCard.Phone);
        mappingForm.find(".email span").html(infoCard.Email);

        /*_app.controller("infoCardController", function ($scope) {
            $scope.Adress = infocard.Adress;
            $scope.Email = infocard.Email;
            $scope.Name = infocard.Name;
            $scope.Patronymic = infocard.Patronymic;
            $scope.Phone = infocard.Phone;
            $scope.Post = infocard.Post;
            $scope.Surname = infocard.Surname;
            $scope.CompanyName = infocard.CompanyName;
            console.log($scope);
        });*/
        createForm.hide();
        mappingForm.show();
    };

    self.getInfoCard = function (infocardId, callBack) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/InfoCard/GetInfoCard",
            data: "id=" + infocardId,
            success: function (infocard) {
                callBack(infocard);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
            },
            processData: false,
            async: false
        });
    };

    self.init = function () {
        $(".accaunt-page ul ul li, .news-page ul ul li").bind("click", function () {
            var infoCardId = $(this).attr("data-infocard-id");
            self.getInfoCard(infoCardId, function (infocard) {
                self.show(infocard);
            });
        });

        $("ul.acc-open li").bind("click", function () {
            var that = this;
            $("ul.acc-open li").removeClass("active");
            $(that).addClass("active");
            $("input[name='RoleId']").val($(that).attr("data-role-id"));

            createForm.show();
            mappingForm.hide();
        });

        /*$(".news-page ul ul li").bind("click", function () {
            var infoCardId = $(this).attr("data-infocard-id");
            self.getInfoCard(infoCardId, function (infocard) {
                self.show(infocard);
            });
        });*/
    }
}

Function.prototype.method = function (name, func) {
    if (!this.prototype[name]) {
        this.prototype[name] = func;
        return this;
    }
};

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

// Project object

function Project() {
    var projectLi = $(".progect-page p.proj_title");
    var projectStepLi = $(".progect-page li[data-step-id]:not(.add)");
    var projectStepAddButton = $(".progect-page li.add");
    var projectStepForm = $(".project-step-form");
    var projectStepsContainer = $(".project-steps");
    var projectViewingForm = $(".project");
    var projectCreateForm = $(".project-crate-form");
    var self = this;

    function renderDocuments(docs) {
        projectViewingForm.find(".docs").empty();

        var documentLi = $("<li>");
        for (var i = 0; i < docs.length; i++) {
            var documentLink = $("<a>").attr("href", "#").text(docs[i].DocumentTitle);
            projectViewingForm.find(".docs").append(documentLi.append(documentLink));
        }
    }

    self.showProject = function (project) {
        projectViewingForm.find(".title").html(project.Tile);
        projectViewingForm.find(".description span").html(project.Description);
        projectViewingForm.find(".responsible span").html(project.ResponsibleUserFullName);
        projectViewingForm.find(".start span").html(project.StartDate);
        projectViewingForm.find(".end span").html(project.EndDate);

        renderDocuments(project.Documents);

        showRightBlock(projectViewingForm);
    },

    self.showProjectSteps = function (project) {
        projectStepsContainer.find(".title").html(project.Title);
        projectStepsContainer.find(".description span").html(project.Description);

        showRightBlock(projectStepsContainer);
    },

    self.getProject = function (id, callBack) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/Project/GetProject",
            data: "id=" + id,
            success: function (response) {
                if (response.IsSuccess) {
                    var project = response.Object;
                    project.StartDate = ToJavaScriptDate(project.StartDate);
                    project.EndDate = ToJavaScriptDate(project.EndDate);
                    project.CreateDate = ToJavaScriptDate(project.CreateDate);
                    callBack(project);

                }
                else {
                    var beforElement = $("form[name='serch']");
                    showResultMessage(beforElement, response.Message, false);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
            },
            processData: false,
            async: false
        });
    },

    self.getProjectSteps = function (id, callBack) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/Project/GetProjectStep",
            data: "id=" + id,
            success: function (response) {
                if (response.IsSuccess) {
                    var projectSteps = response.Object;
                    callBack(projectSteps);
                }
                else {
                    var beforElement = $("form[name='serch']");
                    showResultMessage(beforElement, response.Message, false);
                }
                callBack(response);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
            },
            processData: false,
            async: false
        });
    },

    self.init = function () {
        projectLi.bind("click", function () {
            var that = this;
            self.getProject($(that).parents("li[data-project-id]").attr("data-project-id"), function (project) {
                self.showProject(project);
            });
        });

        projectStepLi.bind("click", function () {
            var that = this;
            self.getProjectSteps($(that).attr("data-step-id"), function (projectStep) {
                self.showProjectSteps(projectStep);
            });
        });

        projectStepAddButton.bind("click", function () {
            var that = this;
            projectStepForm.find("input[name='ProjectId']").val($(that).parents("li[data-project-id]").attr("data-project-id"));
            showRightBlock(projectStepForm);
        });


    }

    function showRightBlock(block) {
        if ($(block).hasClass("content-right-bar")) {
            $(".content-right-bar").hide();
            $(block).show();
        }
    }
}

function showResultMessage(element, message, isSuccess) { //element - элемент после которого будет добавлено сообщение
    var alert = $("div").addClass("alert").attr("role", "alert");
    if (message && message != "") {
        alert.text(message);
        if (isSuccess) {
            alert.addClass("alert-success");
        }
        else {
            alert.addClass("alert-danger");
        }
        element.after(alert);
    }
}

$(document).ready(function () {
    //Календарь
    $("#datepicker").datepicker();

    //Тултипы календаря
    $('.ui-state-default').hover(function () {
        $(this).append('<div class="tooltip">Добавить заметку</div>')
    }, function () {
        $('.tooltip').remove()
    });

    $('.dropdown').bind("click", function () {
        var that = this;
        $(that).addClass('select');
        var parrentLi = $(that).parents("li");

        parrentLi.children('ul').slideToggle(600, function () {
            if ($(this).css('display') == 'none') {
                $(that).removeClass('select');
            }
        });
        return false;
    });
});