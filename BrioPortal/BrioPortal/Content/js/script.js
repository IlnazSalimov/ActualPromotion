var news = function () {
    var self = this;
    var isBusy = false;
    var newsBlock = $('.news-block');
    var newsForm = $('.news-form');
    var newsPage = $('.news-page');
    var newsCont = $('.news-cont')
    var newsLi = newsPage.find('li[data-news-id]');
    var newsEditForm = $('.news-edit-form');

    self.getNews = function (newsId, callBack, errorCallBack) {
        if (isBusy) {
            return false;
        }
        isBusy = true;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/News/GetNews",
            data: "id=" + newsId,
            success: function (response) {
                if (response.IsSuccess) {
                    console.log(response.Object);
                    callBack(response.Object);
                }
                else {
                    showResultMessage($('form[name="serch"]'), response.Message, false);
                    if (errorCallBack) {
                        errorCallBack();
                    }
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
                showResultMessage($('form[name="serch"]'), 'Невозможно извлечь новость. Обновите страницу и повторите поытку.', false);
                if (errorCallBack) {
                    errorCallBack();
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            processData: false,
            async: false
        });
    };

    self.show = function (news) {
        newsCont.find('.edit_pen').attr('data-news-id', news.Id);
        newsCont.find(".title").html(news.Title);
        newsBlock.find(".news-text").html(news.Text);
        newsBlock.find(".news-author").html(news.AuthorUserFullName);
        newsBlock.find(".news-date").html(ToJavaScriptDate(news.CreateDate));
        newsBlock.find("img.news-photo").attr('src', news.PhotoPath);

        showRightBlock(newsCont);
    };

    self.init = function () {
        var callBack = function(news) {
            self.show(news);
            hideLoader();
        }

        var errorCallback = function(){
            hideLoader();
        }

        $('.plus_').bind('click', function () {
            showRightBlock(newsForm);
        });

        newsLi.bind('click', function () {
            var that = this;
            showLoader();

            var newsId = $(that).attr('data-news-id');
            self.getNews(newsId, callBack, errorCallback);
        });

        if (newsPage.find('ul li').length > 0) {
            var firstNewsId = newsPage.find('ul li').first().attr('data-news-id');
            self.getNews(firstNewsId, callBack, errorCallback);
        }

        newsCont.find('.edit_pen').bind('click', function () {
            console.log(newsEditForm);
            var that = this;

            var _callBack = function(news) {
                newsEditForm.find('input[name="Title"]').val(news.Title);
                newsEditForm.find('textarea[name="Text"]').val(news.Text);
                newsEditForm.find('input[name="Id"]').val(news.Id);
            }
            self.getNews($(that).attr('data-news-id'), _callBack, errorCallback);
            showRightBlock(newsEditForm);
        });
    }

    return self;
};


function infoCardPanel(app) {
    var self = this;
    var createForm = $(".content-right-bar.account-create-form");
    var mappingForm = $(".content-right-bar.info-card");
    var isBusy = false;

    self.show = function (infoCard) {
        mappingForm.find(".names").html(infoCard.Surname + " " + infoCard.Name + " " + infoCard.Patronymic);
        mappingForm.find(".company span").html(infoCard.CompanyName);
        mappingForm.find(".phone span").html(infoCard.Phone);
        mappingForm.find(".email span").html(infoCard.Email);

        createForm.hide();
        mappingForm.show();
    };

    self.getInfoCard = function (infocardId, callBack, errorCallBack) {
        if (isBusy) {
            return false;
        }
        isBusy = true;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/InfoCard/GetInfoCard",
            data: "id=" + infocardId,
            success: function (response) {
                if (response.IsSuccess) {
                    callBack(response.Object);
                }
                else {
                    showResultMessage($('form[name="serch"]'), response.Message, false);
                    if (errorCallBack) {
                        errorCallBack();
                    }
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
                
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("request failed");
                showResultMessage($('form[name="serch"]'), 'Невозможно извлечь карточку сотрудника. Обновите страницу и повторите поытку.', false);
                if (errorCallBack) {
                    errorCallBack();
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            processData: false,
            async: false
        });
    };

    self.init = function () {
        $(".accaunt-page ul ul li, .news-page ul ul li").bind("click", function () {
            var infoCardId = $(this).attr("data-infocard-id");

            //начинается загрузка данных, показываем лоадер
            showLoader();
            self.getInfoCard(infoCardId, function (infocard) {
                self.show(infocard);
                hideLoader();
            }, function () {
                hideLoader();
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

        if ($(".news-page ul ul li[data-infocard-id]").length > 0) {
            var firstInfoCardId = $(".news-page ul ul li[data-infocard-id]").first().attr("data-infocard-id");
            //начинается загрузка данных, показываем лоадер
            showLoader();
            self.getInfoCard(firstInfoCardId, function (infocard) {
                self.show(infocard);
                hideLoader();
            }, function () {
                hideLoader();
            });
        }

        if ($(".accaunt-page ul ul li").length > 0) {
            var firstInfoCardId = $(".accaunt-page ul ul li").first().attr("data-infocard-id");
            //начинается загрузка данных, показываем лоадер
            showLoader();
            self.getInfoCard(firstInfoCardId, function (infocard) {
                self.show(infocard);
                hideLoader();
            }, function () {
                hideLoader();
            });
        }
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
    var projectPage = $(".progect-page");
    var projectLi = $(".progect-page p.proj_title");
    var projectStepLi = $(".progect-page li[data-step-id]:not(.add)");
    var projectStepAddButton = $(".progect-page li.add");
    var projectStepForm = $(".project-step-form");
    var projectStepsContainer = $(".project-steps");
    var projectViewingForm = $(".project");
    var projectCreateForm = $(".project-crate-form");
    var isBusy = false;
    var self = this;

    function renderDocuments(docs) {
        projectViewingForm.find(".docs").empty();

        
        for (var i = 0; i < docs.length; i++) {
            var documentLi = $("<li>");
            var documentLink = $("<a>").attr("href", "/ProjectDocument/Download/" + docs[i].Id).text(docs[i].DocumentTitle);
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

    self.getProject = function (id, successCallBack, errorCallBack) {
        if (isBusy) {
            return false;
        }
        isBusy = true;
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
                    successCallBack(project);

                }
                else {
                    var beforElement = $("form[name='serch']");
                    showResultMessage(beforElement, response.Message, false);
                    if (errorCallBack) {
                        errorCallBack();
                    }
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                showResultMessage($('form[name="serch"]'), 'Произошла непредвиденная ситуация до отправки запроса. Проект не был получен', false);
                if (errorCallBack) {
                    errorCallBack();
                }
                console.log("request failed");
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            processData: false,
            async: false
        });
    },

    self.getProjectSteps = function (id, successCallBack, errorCallBack) {
        if (isBusy) {
            return false;
        }
        isBusy = true;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: "/Project/GetProjectStep",
            data: "id=" + id,
            success: function (response) {
                if (response.IsSuccess) {
                    successCallBack(response.Object);
                }
                else {
                    var beforElement = $("form[name='serch']");
                    showResultMessage(beforElement, response.Message, false);
                    if(errorCallBack){
                        errorCallBack();
                    }
                }
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if(errorCallBack){
                    errorCallBack();
                }
                showResultMessage($('form[name="serch"]'), 'Произошла непредвиденная ситуация до отправки запроса. Этап проект не был получен', false);
                console.log("request failed");
                setTimeout(function () {
                    isBusy = false;
                }, 1000);
            },
            processData: false,
            async: false
        });
    },

    self.init = function () {
        projectLi.bind("click", function () {
            console.log('projectLi');
            var that = this;
            //начинается загрузка данных, показываем лоадер
            showLoader();

            self.getProject($(that).parents("li[data-project-id]").attr("data-project-id"), function (project) {
                self.showProject(project);
                
                //Загрузка данных завершена, убираем лоадер
                hideLoader();
            }, function () {
                //Загрузка данных завершена, убираем лоадер
                hideLoader();
            });
        });

        projectStepLi.bind("click", function () {
            console.log('projectStepLi');
            var that = this;
            //начинается загрузка данных, показываем лоадер
            showLoader();

            self.getProjectSteps($(that).attr("data-step-id"), function (projectStep) {
                self.showProjectSteps(projectStep);

                //Загрузка данных завершена, убираем лоадер
                hideLoader();
            }, function () {
                //Загрузка данных завершена, убираем лоадер
                hideLoader();
            });
        });

        projectStepAddButton.bind("click", function () {
            var that = this;
            projectStepForm.find("input[name='ProjectId']").val($(that).parents("li[data-project-id]").attr("data-project-id"));
            showRightBlock(projectStepForm);
        });

        $('.plus_').bind('click', function () {
            showRightBlock(projectCreateForm);
            hideLoader();
        });

        if (projectPage.find("li[data-project-id]").length > 0) {
            var firstProjectId = projectPage.find("li[data-project-id]").first().attr("data-project-id");
            //начинается загрузка данных, показываем лоадер
            showLoader();
            self.getProject(firstProjectId, function (project) {
                self.showProject(project);
                hideLoader();
            }, function () {
                hideLoader();
            });
        }
    }
}

function showRightBlock(block) {
    if ($(block).hasClass("content-right-bar")) {
        $(".content-right-bar").removeClass('active').hide();
        $(block).addClass('active').show();
    }
}

function showResultMessage(element, message, isSuccess) { //element - элемент после которого будет добавлено сообщение
    var alert = $("<div>").addClass("alert").attr("role", "alert");
    if (message && message != "") {
        alert.text(message);
        if (isSuccess) {
            alert.addClass("alert-success");
        }
        else {
            alert.addClass("alert-danger");
        }
        if (element.parents().find('.alert') !== undefined) {
            element.parents().find('.alert').remove();
        }
        element.after(alert);
    }
}

function showLoader() {
    $(".content-right-bar.active").addClass('block').append('<div class="loader"></div>');
}

function hideLoader() {
    setTimeout(function () {
        $(".content-right-bar").removeClass('block').
            find('.loader').
            remove();
    }, 650);
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

        parrentLi.children('ul').slideToggle(400, function () {
            if ($(this).css('display') == 'none') {
                $(that).removeClass('select');
            }
        });
        return false;
    });
});