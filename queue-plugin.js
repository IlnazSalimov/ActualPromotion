/*!
 * EQueuePlugin (http://crmsensor.ru)
 * Copyright 2016 CRM-Sensor.
 */
EQueuePlugin = (function () {
    function EQueuePlugin(option) {
        var that = this;
        this.connection = $.connection;
        this.dashboardHub = $.connection.dashboardHub;
        this.hub = $.connection.hub;

        this.selectors = {
            console: "#message",
            screenplayPanel: "#screenplayPanel",
            surveyButtons: "#surveyButtons",
            testButtons: "#testButtons",
            b_exit: "#exit",
            b_errorExit: ".error_exit",
            b_errorReconnect: "#error_reconnect",
            dashboardName: "dashboardName",
            header: "#header",
            survey: {
                b_stopSurvey: "#stopSurvey",
                b_volumeDown: "#volumeDown",
                b_volumeUp: "#volumeUp"
            },
            queue: {
                b_redirect: "#redirect",
                b_delay: "#delay",
                b_break: "#break",
                b_callByNumber: "#call_by_number",
                b_specReg: "#spec_reg",
                b_next: "#next_ticket",
                b_complete: "#complete_ticket",
                b_resume: "#resume",
                b_cancel: "#cancel",
                b_confirm_cancel: "#confirm_cancel",
                b_stopCall: "#stop_calling",
                b_repeatCall: "#repeat_calling",
                b_equeue_close: "#m_equeue .close",
                b_print: "#print",
                b_open_stat: "#open-stat",
                p_tactic_color: "#tactic-color",
                p_last_tickets: "#last_tickets",
                p_ticket_info: "#ticket_info_panel",
                p_func_action: "#func_actions",
                p_conversation_actions: "#conversation_actions",
                p_service_duration: "#service-duration",
                p_service_duration_plan: "#service-duration-plan",
                p_date: "#date",
                p_break_duration: "#break-duration",
                p_progress_container: "#progress-container",
                p_dashboard_name: "#dashboardName h3 small",
                services: "#services",
                windows: "#windows"
            },
            modal: {
                error: "#m_error",
                serviceError: "#m_service_error",
                lastTickets: "#m_last_tickets",
                redirect: "#m_equeue_redirect",
                info: "#m_info",
                equeue: "#m_equeue",
                surveyError: "#m_survey_error",
                specialRegister: "#m_special_reg",
                cancel_reason: "#m_cancel_reason",
                _break: "#m_break",
                stat: "#m_stat",
                adminNotification: "#m_admin_notification"
            },
            chat: {
                b_open: "#open-chat"
            },
            templates: {
                notification_item: "#notification-item"
            }
        }
        //option = {
        //      redirectEnabled: true,
        //      duplexChannelCheckInterval: 1,
        //      deviceId: 1,
        //      companyCode: 1,
        //      employeeCardNumber: '',
        //      employeeName: '',
        //      deviceType: '',
        //      style: {
        //          mainColor: ''
        //      }
        //      debugMode: true,
        //      equeueEnabled: true,
        //      serviceAddress: ''
        //}
        this.option = option;
        this.chat = null;
        this.queueSetting = {
            IsAutomaticCall: false,
            IsPerformServiceWithTactics: false,
            IsAccessDelayTickets: true,
            EnableSpecialRegTicket: true,
            EnableCallingByNumber: true,
            EnableRedirectToDevice: true,
            EnableAudioNotify: false,
            EnableCompletionWithoutCalling: false,
            CallAnnouncementType: 2,
            MinCallingInterval: 0,
            UnitId: -999,
            ConnectionInterval: 120,
            TicketShowingTypeInOperatorBoard: 1,
            IsOperatorPanelRequired: false
        };
        this.isMSIE = window.navigator.userAgent.indexOf("MSIE ") > 0;
        this.deviceConnected = false;
        this.surveyLaunched = false;
        this.isWindowInFocus = true;
        this.dashboardClosed = false;
        /**
         * Подключен ли SignalR к хабу
         */
        this.connectionStarted = false;

        this.checkAliveTimer = null;
        this.stopwatch = null;
        this.serviceDurationTimer = null;
        this.audio = document.createElement("AUDIO");

        this.currentTicket = {
            Name: "",
            ServiceId: null
        }
        this.lastTicketCount = 0;

        /**
         * ---------- Свойства для вывода сообщений ----------
         */
        this.messages = []; // Стек сообщений
        this.tempDuration = 0; // Текущий интервал
        this.INTERVAL_BETWEEN_MESSAGES = 1800; // Интервал между сообщениями
        this.isMessageViewerBusy = false;
        this.durationTimeoutObj = {};

        /* --------------------------------------------------*/

        this.queueTicketState = {
            Cancelled: "Cancelled",
            Delayed: "Delayed",
            Waiting: "Waiting"
        }

        this.MODAL_STATES = {
            HIDE: "hide",
            SHOW: "show"
        }
        this.DEVICE_TYPES = {
            WEB_DASHBOARD: "WebDashboard"
        }

        this.TICKET_SHOWING_TYPE = {
            Manually: 1,
            AllServiceTime: 2
        }

        this.HTML_PROP = {
            DISABLED: "disabled",
            TITLE: "title",
            ROLE: "role",
            ID: "id"
        }

        this.HTML_COLORS = {
            RED: "#d9534f",
            YELLOW: "#f0ad4e",
            GREEN: "#5cb85c",
            GRAY: "#888"
        }

        this.HELPER_CLASSES = {
            HAS_ERROR: "has-error"
        }

        /**
         * ---------- Свойства для прогрес бара ----------
         */
        this.progressBar = undefined;

        /* --------------------------------------------------*/

        /* ------------------- Нормативы ---------------------*/

        this.QUEUE_SERVICE_TACTIC = {
            NONE: 0,
            GREEN: 1,
            YELLOW: 2,
            RED: 3
        }

        this.rule = {
            ServiceTimeString: "00:00:00",
            ServiceTimeDuration: moment.duration("00:00:00"),
            CurrentTactics: that.QUEUE_SERVICE_TACTIC.NONE,
            isServiceTimeGreatThanZero: false,
            UseQueueServiceTactics: false
        }

        /* --------------------------------------------------*/

        this.LASTTICKETS_REQUEST_TIMEOUT = 1000;
        this.RECONNECT_INTERVAL = 3000;
        this.SURVEY_LAUNCH_TIMEOUT = 20000;
        this.DEVICE_DISCONNECT_TIMEOUT = 20000;
        this.STATUSBAR_CHANGE_TIMEOUT = 120000;
    }

    function createTimer(option) {
        var tick = 0;
        var hour = 0;
        var minute = 0;
        var second = 0;

        var timer = window.setInterval(function () {
            ++tick;
            hour = Math.floor(tick / 3600);
            minute = Math.floor((tick - hour * 3600) / 60);
            second = tick - hour * 3600 - minute * 60;

            if (option.renderMode !== "h") {
                if (hour > 0) option.renderMode = "h";
                if (minute > 0) option.renderMode = "m";
            }

            if (hour < 10) hour = '0' + hour;
            if (minute < 10) minute = '0' + minute;
            if (second < 10) second = '0' + second;

            switch (option.renderMode) {
                case "h":
                    $(option.container).html(hour + ':' + minute + ':' + second);
                    break;
                case "m":
                    $(option.container).html(minute + ':' + second);
                    break;
                case "s":
                    $(option.container).html(second);
                    break;

                default:
                    $(option.container).html(hour + ':' + minute + ':' + second);
            }
            if (option.onEachTicks) {
                option.onEachTicks(moment.duration({
                    seconds: second,
                    minutes: minute,
                    hours: hour
                }));
            }
        }, 1000);

        option.callback(timer);
    }

    function createButton(that, option) {
        var button = $("<button>").addClass("btn btn-default btn-md").
                css({
                    "color": that.option.style.mainColor,
                    "border-color": that.option.style.mainColor
                }).
                prop(that.HTML_PROP.ID, option.id).
                prop(that.HTML_PROP.TITLE, option.title).
                text(option.text).
                click(option.clickTrigger).
                html(option.innerHtml);
        return button;
    }

    function startServiceDurationTimer(that) {
        clearTimeout(that.serviceDurationTimer);

        createTimer({
            renderMode: "h",
            container: that.selectors.queue.p_service_duration,
            callback: function (timer) {
                $(that.selectors.queue.p_service_duration).removeClass("hide");

                if (that.rule.isServiceTimeGreatThanZero && that.rule.UseQueueServiceTactics) {
                    $(that.selectors.queue.p_service_duration_plan).text(that.rule.ServiceTimeString);
                    $(that.selectors.queue.p_service_duration_plan).removeClass("hide");
                    $(that.selectors.queue.p_service_duration_plan).attr("title", "Норматив обслуживания");
                }

                that.serviceDurationTimer = timer;
            },
            onEachTicks: function (timerDuration) {
                $(that.selectors.queue.p_service_duration).attr(that.HTML_PROP.TITLE,
                    "Длительность обслуживания: " + timerDuration.humanize());

                if (!that.rule.isServiceTimeGreatThanZero || !that.rule.UseQueueServiceTactics) {
                    return;
                }

                if (timerDuration.asMilliseconds() >= that.rule.ServiceTimeDuration.asMilliseconds() &&
                    $(that.selectors.queue.p_service_duration).hasClass("color") !== that.HTML_COLORS.RED) {
                    $(that.selectors.queue.p_service_duration).css("color", that.HTML_COLORS.RED);
                }
            }
        });
    }

    function disableServiceDurationTimer(that) {
        clearTimeout(that.serviceDurationTimer);
        $(that.selectors.queue.p_service_duration).addClass("hide");
        $(that.selectors.queue.p_service_duration_plan).addClass("hide");
        $(that.selectors.queue.p_service_duration_plan).removeAttr("title");
        $(that.selectors.queue.p_service_duration).html("00:00:00").css("color", that.option.style.mainColor);
    }

    function onModalHidden(that, modal, callBack) {
        if (that && modal) {
            modal.on("hidden.bs.modal", callBack);
        }
    }

    function startRefreshActivity(that) {
        if (that.option.deviceType === that.DEVICE_TYPES.WEB_DASHBOARD) {
            setInterval(function () {
                if (that.deviceConnected) {
                    that.dashboardHub.server.refreshActivity();
                    that.logger.debug("Обновлена активность окна");
                }
            }, that.queueSetting.ConnectionInterval * 1000);
        }
    }

    function startDeviceAliveChecker(that) {
        if (that.option.duplexChannelCheckInterval > 0) {
            that.checkAliveTimer = setInterval(function () {
                that.dashboardHub.server.checkDeviceAlive();
            }, that.option.duplexChannelCheckInterval * 1000);
        }
    }

    function endDeviceAliveChecker(that) {
        if (that.checkAliveTimer != null) {
            clearInterval(that.checkAliveTimer);
        }
    }

    /**
     * Устанавливает поддерживаемый формат оповещения.
     * @param {} that Текущий объект EQueuePlugin
     * @returns {} 
     */
    function setSupportedAudioNotify(that) {
        if ((detectIE() <= 10 && detectIE() >= 7)) {
            that.audio.src = "/Media/equeue_notification.mp3";
            return;
        }

        if (that.audio.canPlayType("audio/aac")) {
            that.audio.src = "/Media/equeue_notification.aac";
        } else if (that.audio.canPlayType("audio/ogg")) {
            that.audio.src = "/Media/equeue_notification.ogg";
        } else {
            that.audio.src = "/Media/equeue_notification.mp3";
        }
    }

    function refreshLastTicketsList(that, tickets) {
        $(that.selectors.queue.b_callByNumber).prop(that.HTML_PROP.DISABLED, true);
        $(that.selectors.queue.b_callByNumber).prop(that.HTML_PROP.DISABLED, false);
        that.logger.debug('Обновление списка последних талонов...');
        $(that.selectors.queue.p_last_tickets).empty();
        $(that.selectors.modal.lastTickets).modal();

        if (tickets.length === 0) {
            var emptyMsg = $("<div>").addClass("text-center").text("Список пуст");
            $(that.selectors.queue.p_last_tickets).append(emptyMsg);
            return;
        }
        $.each(tickets, function (index, val) {
            var ticketName = $('<strong>').append($('<mark>').addClass('ticket-name').text(val.Name)),
                listGroupItem = $('<button>').addClass('btn btn-default').val(val.Name).append(ticketName),
                label = $('<span>').addClass('label').css('margin-left', '5px'),
                registerDate = $('<div>').text("Открыт: " + val.RegisterDate + " | Последний вызов: " + val.CallDate),
                serviceName = $('<div>').addClass('mute service-name').text(val.ServiceName);

            listGroupItem.click(function () {
                var callTicketNum = $(this).val();
                if (callTicketNum) {
                    that.dashboardHub.server.callTicketByName(callTicketNum);
                    $(that.selectors.modal.lastTickets).modal(that.MODAL_STATES.HIDE);
                } else {
                    that.logger.error('Не корректный номер талона.');
                }
            });

            switch (val.State) {
                case that.queueTicketState.Cancelled:
                    listGroupItem.append(label.addClass('label-default').text('Отменен'));
                    break;
                case that.queueTicketState.Waiting:
                    listGroupItem.append(label.addClass('label-danger').text('Ожидание'));
                    break;
                case that.queueTicketState.Delayed:
                    listGroupItem.append(label.addClass('label-primary').text('Отложен'));
                    break;
                default:
            }
            listGroupItem.append(registerDate);
            listGroupItem.append(serviceName);

            $(that.selectors.queue.p_last_tickets).append(listGroupItem);
        });
    }

    function refreshQueueSetting(that, setting, callBack) {
        if ($.isEmptyObject(setting)) {
            that.logger.debug('Настроек эл.очереди не обнаружен. Используются стандартные настройки.');
        } else {
            that.queueSetting = setting;
        }
        that.applySettings();
        if (callBack !== undefined) {
            callBack();
        }
    }

    function refreshOnlineDevices(that, devices) {
        $(that.selectors.queue.b_redirect).prop(that.HTML_PROP.DISABLED, true);
        $(that.selectors.queue.b_redirect).prop(that.HTML_PROP.DISABLED, false);
        that.logger.debug('Обновление списка онлайн устройств');
        $(that.selectors.queue.windows).empty();

        $.each(devices, function (index, val) {
            var windowButton = $("<button>").addClass("btn btn-primary q").val(val.Id).text(val.Name);
            // Перенаправление в окно
            windowButton.click(function () {
                var reason = $(that.selectors.modal.redirect).find(".reason input").val();
                var deviceId = $(this).val();
                $(that.selectors.modal.redirect).modal(that.MODAL_STATES.HIDE);
                that.dashboardHub.server.redirectTicketToDevice({
                    ticketName: that.currentTicket.Name,
                    deviceId: deviceId,
                    reason: reason
                });
            });

            $(that.selectors.queue.windows).append(windowButton);
        });
        if (devices.length === 0) {
            $(that.selectors.queue.windows).text("Нет активных устройств");
        }

        $(that.selectors.modal.redirect).modal();
    }

    function renderDateTimeToPage(that) {
        var date = new Date,
        year = date.getFullYear(),
        month = date.getMonth(),
        months = new Array('Января', 'Февраля', 'Марта', 'Апреля', 'Мая', 'Июня', 'Июля', 'Августа', 'Сентября', 'Октября', 'Ноября', 'Декабря'),
        day = date.getDate(),
        h = date.getHours();
        if (h < 10) {
            h = "0" + h;
        }
        var m = date.getMinutes();
        if (m < 10) {
            m = "0" + m;
        }
        var s = date.getSeconds();
        if (s < 10) {
            s = "0" + s;
        }
        var result = '' + day + ' ' + months[month] + ' ' + ' ' + year + ' ' + h + ':' + m;
        $(that.selectors.queue.p_date).html(result);
        setTimeout(function () {
            renderDateTimeToPage(that);
        }, 1000);
        return true;
    }

    function buildAndAnimateProgressBar(that) {
        that.progressBar = new ProgressBar.Line(that.selectors.queue.p_progress_container, {
            strokeWidth: .7,
            duration: moment.duration(that.rule.ServiceTimeString).asMilliseconds(),
            color: that.option.style.mainColor,
            trailColor: '#fff',
            trailWidth: 1,
            svgStyle: { width: '100%', height: '100%', 'border-radius': '5px 5px 0 0' },
            step: function (state, bar) {
                if (state.offset < 15) {
                    bar.path.setAttribute('stroke', state.color);
                }
            }
        });
        that.progressBar.animate(1.0, {
            from: { color: that.option.style.mainColor },
            to: { color: that.HTML_COLORS.RED }
        });
    }

    /**
     * Метод который показывает все сообщения которые находятся в стеке с определенным интервалом
     * @param {} that 
     * @returns {} 
     */
    function startMessageViewer(that) {
        if (!that.isMessageViewerBusy && that.messages.length > 0) {
            that.isMessageViewerBusy = true;
            setTimeout(function () {
                that.tempDuration = that.INTERVAL_BETWEEN_MESSAGES;
                clearTimeout(that.durationTimeoutObj);
                that.durationTimeoutObj = setTimeout(function () { // Если после последнего сообщения длительное время не было сообщений, то следующее сообщение будем показывать без задержки
                    that.tempDuration = 0;
                }, that.INTERVAL_BETWEEN_MESSAGES + 100);

                var currentMessage = that.messages.shift();
                $(that.selectors.console).fadeToggle(function () {
                    that.logger.debugFormat("Viewed message: {0}", [currentMessage]);
                    $(that.selectors.console).text(currentMessage);
                    $(that.selectors.console).fadeToggle();
                });
                that.isMessageViewerBusy = false;
                startMessageViewer(that);
            }, that.tempDuration);
        }
    }

    function onDeviceConnected(that) {
        that.logger.debug("Dashboard device connected");
        that.registerMessage('Табло оператора подключено');
        $(that.selectors.queue.p_dashboard_name).css("color", that.HTML_COLORS.GREEN);
        $(that.selectors.queue.p_dashboard_name).attr("title", "Табло оператора подключено");
        that.deviceConnected = true;
        $(that.selectors.modal.error).modal(that.MODAL_STATES.HIDE);
        $(that.selectors.screenplayPanel).fadeIn();
    }

    function onDeviceDisconnected(that) {
        that.logger.debug("Dashboard device disconnected");
        that.registerMessage('Табло оператора отключено');
        $(that.selectors.queue.p_dashboard_name).css("color", that.HTML_COLORS.RED);
        $(that.selectors.queue.p_dashboard_name).attr("title", "Табло оператора отключено");
        $(that.selectors.screenplayPanel).fadeOut();
        that.deviceConnected = false;
    }

    /**
     * Срабатывает после отключения SignalR к хабу
     * @param {} that 
     * @returns {} 
     */
    function onHubDisconnected(that, msg) {
        that.connectionStarted = false;
        that.endBreakStopwatch();
        that.hideInterface(true);
        that.disableInterface(true);
        that.logger.debug(msg);
    }

    /**
     * Срабатывает до подключения SignalR к хабу
     * @param {} that 
     * @returns {} 
     */
    function onBeforeHubConnected(that) {
        that.hideInterface();
        moment.locale('ru');
        renderDateTimeToPage(that);
        // Сразу подписываемя на событие выхода, чтобы даже если при подключении возникла ошибка была возможность выйти
        $(that.selectors.b_exit).click(function () {
            that.exitDashboard();
        });
        $(that.selectors.b_errorExit).click(function () {
            $(that.selectors.modal.error).modal(that.MODAL_STATES.HIDE);
            that.exitDashboard();
        });

        that.hub.logging = true;
        that.hub.url = that.option.serviceAddress;
        that.hub.transportConnectTimeout = 5000;

        if (that.dashboardHub !== undefined) {
            that.initHubMethods();
            that.initDashboardHubMethods();
            initChat(that);
        }
    }

    /**
     * Срабатывает после подключения SignalR к хабу
     * @param {} that 
     * @returns {} 
     */
    function onHubConnected(that) {
        that.connectionStarted = true;
        that.enableInterface();
        startDeviceAliveChecker(that);
        that.registerObservers();
        that.dashboardHub.server.getQueueSetting().done(function (setting) {
            refreshQueueSetting(that, setting, function () {
                if (that.queueSetting.EnableAudioNotify) {
                    setSupportedAudioNotify(that); // Устанавливаем путь для звуковых оповещений для браузеров PC. Оповещение для планшетов начинается со второго талона. 
                    // См. событие нажатия на кнопку that.selectors.queue.b_next
                }
            });
        });
        that.dashboardHub.server.getDeviceConnectedStatus().done(function (isDeviceConnected) {
            that.deviceConnected = isDeviceConnected;
            if (that.deviceConnected) {
                onDeviceConnected(that);
            }
        });
    }

    /**
     * Проверка доступности Service Host`a, в частности скрипты хабов SignalR 
     * @param {} that Текущий объект EQueuePlugin
     * @param {} callback Функция обратного вызова, с параметрами. Первый параметр - результат доступности(bool), второй - сообщение об ошибке
     * @returns {} 
     */
    function isServiceHostAvailible(that, callback) {
        try {
            $.post(that.option.serviceAddress + '/hubs', {}, function (res) {
                callback(true);
            }).fail(function (xhr, status, error) {
                callback(false, "Service host unavailible");
            });
        } catch (e) {
            callback(false, "Service host unavailible");
        }
    }

    /**
     * Инициализация чата
     * @param {} that 
     * @returns {} 
     */
    function initChat(that) {
        if (that.option.deviceType !== that.DEVICE_TYPES.WEB_DASHBOARD) {
            $(that.selectors.chat.b_open).removeClass('hide');
            that.chat = new ChatPlugin({
                employeeName: that.option.employeeName,
                eQueuePlugin: that
            });
            that.chat.init();
        }
    }

    EQueuePlugin.prototype.init = function () {
        var that = this;
        that.logger.debug("Starting queue dashboard plugin");
        
        onBeforeHubConnected(that);
        isServiceHostAvailible(that, function(isAvailible, error) {
            if (!isAvailible) {
                $(that.selectors.modal.serviceError).modal(that.MODAL_STATES.SHOW);
                that.logger.error(error);
            } else {
                that.connect();
            }
        });
    }

    EQueuePlugin.prototype.connect = function () {
        var that = this;

        //if (that.option.debugMode) {
        //    that.logger.debug("DEBUG mode is ON");
        //    that.hub.logging = true;
        //}

        var useJsonp = false;
        if (detectIE() && (detectIE() <= 9 && detectIE() > 7)) {
            useJsonp = true;
        }

        try {
            that.logger.debug("Connecting to host " + that.hub.url);
            that.hub.start({ jsonp: useJsonp }).done(function () {
                //this.hub.start({ transport: 'foreverFrame' }).done(function () {
                that.logger.debug("Connection established to " + that.hub.url);
                onHubConnected(that);
            }).fail(function (e) {
                that.logger.error(e);
            });
        }
        catch (e) {
            that.logger.error(e);
        }
    }

    EQueuePlugin.prototype.reconnect = function () {
        var that = this;
        that.registerMessage("Инициализация...");
        that.logger.debug("Soft reconnect");

        that.deviceConnected = false;
        if (that.option.debugMode) {
            that.hub.logging = true;
        }

        isServiceHostAvailible(that, function (isAvailible, error) {
            if (!isAvailible) {
                $(that.selectors.modal.serviceError).modal(that.MODAL_STATES.SHOW);
                that.logger.error(error);
            } else {
                that.connect();
            }
        });
    }

    EQueuePlugin.prototype.hideInterface = function (isHideNextButton) {
        var that = this;
        that.logger.debug("Hiding queue action buttons");

        $(that.selectors.modal.equeue).modal(that.MODAL_STATES.HIDE);
        $(that.selectors.queue.p_conversation_actions).hide();
        $(that.selectors.queue.p_ticket_info).hide();
        $(that.selectors.queue.b_complete).hide();
        if (isHideNextButton) {
            $(that.selectors.queue.b_next).hide();
        }

        disableServiceDurationTimer(that);

        if (that.progressBar) {
            that.progressBar.destroy();
            that.progressBar = null;
        }
        $(that.selectors.queue.p_tactic_color).addClass("hide");
    }

    EQueuePlugin.prototype.showInterface = function () {
        var that = this;
        $(that.selectors.queue.p_ticket_info).show();
        $(that.selectors.queue.p_conversation_actions).show();
        $(that.selectors.queue.b_next).show();

        startServiceDurationTimer(that);
        if (that.rule.isServiceTimeGreatThanZero && that.rule.UseQueueServiceTactics) {
            $(that.selectors.queue.p_tactic_color).removeClass("hide");
            switch (that.rule.CurrentTactics) {
                case that.QUEUE_SERVICE_TACTIC.GREEN:
                    $(that.selectors.queue.p_tactic_color).css("background-color", that.HTML_COLORS.GREEN);
                    $(that.selectors.queue.p_tactic_color).attr("title", "Зеленая тактика");
                    break;
                case that.QUEUE_SERVICE_TACTIC.YELLOW:
                    $(that.selectors.queue.p_tactic_color).css("background-color", that.HTML_COLORS.YELLOW);
                    $(that.selectors.queue.p_tactic_color).attr("title", "Желтая тактика");
                    break;
                case that.QUEUE_SERVICE_TACTIC.RED:
                    $(that.selectors.queue.p_tactic_color).css("background-color", that.HTML_COLORS.RED);
                    $(that.selectors.queue.p_tactic_color).attr("title", "Красная тактика");
                    break;
            }

            //if (!detectIE() || detectIE() > 8) {
            //    buildAndAnimateProgressBar(that);
            //}
        }
    }

    EQueuePlugin.prototype.disableInterface = function (isExtensionDisable) {
        var that = this;
        $(that.selectors.queue.b_delay).prop(that.HTML_PROP.DISABLED, true);
        $(that.selectors.queue.b_redirect).prop(that.HTML_PROP.DISABLED, true);
        $(that.selectors.queue.b_print).prop(that.HTML_PROP.DISABLED, true);

        $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, true);

        if (isExtensionDisable) {
            $(that.selectors.chat.b_open).prop(that.HTML_PROP.DISABLED, true);
            $(that.selectors.queue.b_open_stat).prop(that.HTML_PROP.DISABLED, true);
            $(that.selectors.queue.b_break).prop(that.HTML_PROP.DISABLED, true);
            $(that.selectors.queue.b_callByNumber).prop(that.HTML_PROP.DISABLED, true);
            $(that.selectors.queue.b_specReg).prop(that.HTML_PROP.DISABLED, true);
        }

        that.logger.debug('Call interface disabled');

        //if (that.queueSetting.MinCallingInterval > 0) {
        //    that.disableNextButtonForInterval(that.queueSetting.MinCallingInterval);
        //} else {
        //    $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, true);
        //}
    }

    EQueuePlugin.prototype.enableInterface = function (isEnableNextButton) {
        var that = this;
        $(that.selectors.queue.b_delay).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_redirect).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_print).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.chat.b_open).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_open_stat).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_break).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_callByNumber).prop(that.HTML_PROP.DISABLED, false);
        $(that.selectors.queue.b_specReg).prop(that.HTML_PROP.DISABLED, false);

        $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, false);
        that.logger.debug('Call interface enabled');

        //if (isEnableNextButton) {
        //    $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, false);
        //}
    }

    EQueuePlugin.prototype.registerMessage = function (message) {
        var that = this;
        that.messages.push(message);
        startMessageViewer(that);
    }

    EQueuePlugin.prototype.showBrowserNotification = function (title, body) {
        if (typeof Notification !== "undefined" && Notification.permission === "granted") {
            var notification = new Notification(title, {
                tag: "equeue",
                body: body,
                icon: "/Styles/images/crm_logo.png"
            });

            notification.onclick = function () {
                window.focus();
            };
        }
    }
    /**
     * Метод выхода из веб-панели оператор на страницу авторизации
     * @param {} message Сообщение отображаемое после выхода на странице авторизации
     * @returns {} 
     */
    EQueuePlugin.prototype.exitDashboard = function (message) {
        var that = this;

        if (that.dashboardClosed) {
            return;
        }

        that.dashboardClosed = true;

        try {
            that.logger.debug("Closing dashboard");
            that.hub.stop();
            that.dashboardHub.server.closeSession();
        } catch (err) {
            that.logger.error(err);
        }

        location.href = "/dashboard/" + that.option.companyCode + (message ? "?msg=" + message : '');
    }

    EQueuePlugin.prototype.setTicketInfoPanel = function (ticketName, serviceName) {
        var that = this;
        $(that.selectors.queue.p_ticket_info).find(".service-name").css("color", that.option.style.mainColor).text(ticketName + " - " + serviceName);
    }

    EQueuePlugin.prototype.openEqueueModal = function (ticketName, serviceName, dynamicPriorityReason, regDate) {
        var that = this;
        that.registerMessage("Вызван клиент: " + ticketName);
        $(that.selectors.modal.equeue).find(".modal-body h1").text(ticketName);
        $(that.selectors.modal.equeue).find(".service-name").text(serviceName);
        if (dynamicPriorityReason) {
            $(that.selectors.modal.equeue).find(".benefit").text(dynamicPriorityReason).css("color", that.option.style.mainColor);
        } else {
            $(that.selectors.modal.equeue).find(".benefit").text("");
        }
        $(that.selectors.modal.equeue).find(".reg-date").text(regDate);
        // Задержка сделана для того, что модальное окно нормально отрабатывало событие
        // закрытие/открытие. Иногда черный задний фон остаётся, а модальное окно закрывается нормально.
        setTimeout(function () {
            $(that.selectors.modal.equeue).modal(that.MODAL_STATES.SHOW);
            that.logger.debug("Modal window is opening");

            $(that.selectors.modal.equeue).on('shown.bs.modal', function (e) {
                that.logger.debug("Modal window was opened");
            });

            that.setTicketInfoPanel(ticketName, serviceName);
        }, 200);
    }

    EQueuePlugin.prototype.startBreakStopwatch = function () {
        var that = this;
        that.logger.debug("Start break stopwatch");
        createTimer({
            renderMode: "h",
            container: that.selectors.queue.p_break_duration,
            callback: function (timer) {
                that.stopwatch = timer;
            }
        });
        $(that.selectors.modal._break).modal();
    }

    EQueuePlugin.prototype.endBreakStopwatch = function () {
        var that = this;
        that.logger.debug("Clear break stopwatch");
        clearInterval(that.stopwatch);
        $(that.selectors.modal._break).find("h1").html("00:00:00");
        $(that.selectors.modal._break).modal(that.MODAL_STATES.HIDE);
    }

    EQueuePlugin.prototype.disableNextButtonForInterval = function (interval) {
        var that = this;
        $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, true);
        $(that.selectors.queue.b_next).prop(that.HTML_PROP.TITLE, "Минимальный интервал вызова " + interval + " сек.");
        setTimeout(function () {
            $(that.selectors.queue.b_next).prop(that.HTML_PROP.DISABLED, false);
            $(that.selectors.queue.b_next).prop(that.HTML_PROP.TITLE, "");
        }, interval * 1000);
    }

    EQueuePlugin.prototype.applySettings = function () {
        var that = this;
        that.logger.debug("Applying queue settings ...");

        var delayGroup = createButton(that, {
            id: that.selectors.queue.b_delay.substr(1, that.selectors.queue.b_delay.length),
            text: "Отложить",
            clickTrigger: function () {
                //that.disableInterface();
                that.dashboardHub.server.delayTicket(that.currentTicket.Name);
            }
        }),
            redirGroup = createButton(that, {
                id: that.selectors.queue.b_redirect.substr(1, that.selectors.queue.b_redirect.length),
                text: "Перенаправить",
                clickTrigger: function () {
                    that.dashboardHub.server.getOnlineDevices().done(function (devices) {
                        that.logger.debugFormat('Get {0} online devices', [devices.length]);
                        refreshOnlineDevices(that, devices);
                    });
                }
            }),
            callByNumberGroup = createButton(that, {
                id: that.selectors.queue.b_callByNumber.substr(1, that.selectors.queue.b_callByNumber.length),
                text: "Вызвать по номеру",
                clickTrigger: function () {
                    that.dashboardHub.server.getLastTickets().done(function (tickets) {
                        that.logger.debugFormat('Get {0} last tickets', [tickets.length]);
                        refreshLastTicketsList(that, tickets);
                    });
                }
            }),
            specRegGroup = createButton(that, {
                id: that.selectors.queue.b_specReg.substr(1, that.selectors.queue.b_specReg.length),
                text: "Спец.регистрация",
                clickTrigger: function () {
                    $(that.selectors.modal.specialRegister).modal();
                }
            });

        var funcActionPanel = $(that.selectors.queue.p_func_action);
        var conversatioActionPanel = $(that.selectors.queue.p_conversation_actions);

        if (that.queueSetting.IsAccessDelayTickets) {
            if (conversatioActionPanel.find($(that.selectors.queue.b_delay)).length === 0) {
                conversatioActionPanel.append(delayGroup);
            }
        } else {
            delayGroup.remove();
        }

        if (that.option.redirectEnabled) {
            if (conversatioActionPanel.find($(that.selectors.queue.b_redirect)).length === 0) {
                conversatioActionPanel.append(redirGroup);
            }
        } else {
            redirGroup.remove();
        }

        if (that.queueSetting.EnableCallingByNumber && that.option.equeueEnabled) {
            if (funcActionPanel.find($(that.selectors.queue.b_callByNumber)).length === 0) {
                funcActionPanel.append(callByNumberGroup);
            }
        } else {
            callByNumberGroup.remove();
        }

        if (that.queueSetting.EnableSpecialRegTicket && that.option.equeueEnabled) {
            if (funcActionPanel.find($(that.selectors.queue.b_specReg)).length === 0) {
                funcActionPanel.append(specRegGroup);
            }
        } else {
            specRegGroup.remove();
        }

        if (that.queueSetting.EnableRedirectToDevice) {
            $(that.selectors.queue.windows).show();
            $(that.selectors.queue.windows).next(".redirect-disabled").hide();
        } else {
            $(that.selectors.queue.windows).hide();
            $(that.selectors.queue.windows).next(".redirect-disabled").show();
        }
    }

    EQueuePlugin.prototype.registerObservers = function () {
        var that = this;
        that.logger.debug("Registering UI event handlers ...");
        $(that.selectors.surveyButtons).find("button").click(function () {
            that.surveyLaunched = false,
            that.currentSurveyName = this.innerText;

            // Проверить факт запуска опроса не возможно потому что андроид не может послать событие о запуске напрямую на сервис.
            // Он посылает его на хост(метод SurveyPointServiceImpl.Notify())
            //setTimeout(function () {
            //    if (!that.surveyLaunched) {
            //        that.logger.error("Survey launch timeout occured");
            //        that.registerMessage("Ошибка запуска");
            //        $(that.selectors.modal.surveyError).find("h3").text("Ошибка запуска: '" + that.currentSurveyName + "'");
            //        $(that.selectors.modal.surveyError).modal();
            //    }
            //}, that.SURVEY_LAUNCH_TIMEOUT);

            // Фиктивное оповещение, так как проверить не можем.
            that.registerMessage("Запущен: '" + that.currentSurveyName + "'");

            that.dashboardHub.server.startSurvey($(this).val());
        });

        $(that.selectors.survey.b_stopSurvey).click(function () {
            that.dashboardHub.server.stopSurvey();
        });

        $(that.selectors.testButtons).find("button").click(function () {
            that.dashboardHub.server.startTest();
            $(that.selectors.testButtons).hide();
        });

        $(that.selectors.survey.b_volumeDown).click(function () {
            that.dashboardHub.server.volumeDown();
        });

        $(that.selectors.survey.b_volumeUp).click(function () {
            that.dashboardHub.server.volumeUp();
        });

        $(that.selectors.queue.b_next).click(function () {
            that.disableInterface();
            that.dashboardHub.server.callNextTicket();

            // Этот код для планшетов. Потому что аудио на Android м iOS автоматически не воспроизводится. 
            // Для этого нужно привязать его к эвенту (в данном случае к кнопке)
            // -- {
            if (!detectIE() || (detectIE() && detectIE() > 9)) {
                var isSrcChanged = false;
                if (!isSrcChanged && that.queueSetting.EnableAudioNotify) { // Меняем путь оповещения. Он устанавливается при инициализации и на данный момент он есть, 
                    that.audio.src = "";                                    // но если его не поменять воспроизводиться на планшетах не будет.

                    // Internet Explorer 6-11
                    var isIE = /*@cc_on!@*/false || !!document.documentMode;
                    // Edge 20+
                    var isEdge = !isIE && !!window.StyleMedia;

                    if (isIE || isEdge) {
                        that.audio.pause(); // Ставим на паузу чтобы не срабатывало после каждого нажатия. Так происходит только у IE
                    } else {
                        that.audio.play();
                    }

                    setSupportedAudioNotify(that);
                    isSrcChanged = true;
                }
            }
            // } --
        });

        $(that.selectors.queue.b_complete).click(function () {
            that.dashboardHub.server.completeTicket();
        });

        $(that.selectors.queue.b_cancel).click(function () {
            $(that.selectors.modal.cancel_reason).modal();
        });

        $(that.selectors.queue.b_confirm_cancel).click(function () {
            var formGroup = $(that.selectors.modal.cancel_reason).find(".form-group.reason");
            var reason = formGroup.find("textarea").val();

            if (reason !== "") {
                that.dashboardHub.server.cancelTicket(reason);
                $(that.selectors.modal.cancel_reason).modal(that.MODAL_STATES.HIDE);
                $(that.selectors.modal.equeue).modal(that.MODAL_STATES.HIDE);
            } else {
                $(that.selectors.modal.cancel_reason).find(".alert.not-reason").show();
                formGroup.addClass(that.HELPER_CLASSES.HAS_ERROR);
            }
        });

        $(that.selectors.queue.b_repeatCall).click(function () {
            that.dashboardHub.server.repeatTicketCallNotification(that.currentTicket.Name);
            that.logger.debug("Repeat call ticket");
        });
        /**
         * Перенаправление на услугу
         */
        $(that.selectors.queue.services).find("button").click(function () {
            var reason = $(that.selectors.modal.redirect).find(".reason input").val();
            var serviceId = $(this).val();
            $(that.selectors.modal.redirect).modal(that.MODAL_STATES.HIDE);
            that.dashboardHub.server.redirectTicketToService({
                ticketName: that.currentTicket.Name,
                serviceId: serviceId,
                reason: reason
            });
        });

        $(that.selectors.b_errorReconnect).click(function () {
            $(that.selectors.modal.error).modal(that.MODAL_STATES.HIDE);
            $('.modal-backdrop').remove();
            that.reconnect();
        });

        $(that.selectors.modal.specialRegister).find("button").click(function () {
            var serviceId = $(this).val();
            that.dashboardHub.server.registerVirtualTicket(serviceId);
            $(that.selectors.modal.specialRegister).modal(that.MODAL_STATES.HIDE);
        });

        $(that.selectors.queue.b_break).click(function () {
            that.startBreakStopwatch();
            that.dashboardHub.server.beginBreak();
        });

        $(that.selectors.queue.b_resume).click(function () {
            that.endBreakStopwatch();
            that.dashboardHub.server.endBreak();
        });

        $(that.selectors.queue.b_stopCall).click(function () {
            that.dashboardHub.server.stopTicketCalling();

            $(that.selectors.modal.equeue).modal(that.MODAL_STATES.HIDE)
            that.logger.debug("Modal window is closing");
        });

        $(that.selectors.queue.b_equeue_close).click(function (e) {
            that.dashboardHub.server.stopTicketCalling();
        });

        $(that.selectors.queue.b_open_stat).click(function () {
            that.logger.debug("Get stats");
            that.dashboardHub.server.getStats().done(function (stats) {
                that.logger.debugFormat("Ticket total tount: {0}, Waiting time avg: {1}, Service time avg: {2}", [stats.TicketTotalCount, stats.WaitingTimeAvg, stats.ServiceTimeAvg]);
                $(that.selectors.modal.stat).find(".ticket-count .value").text(stats.TicketTotalCount);
                $(that.selectors.modal.stat).find(".waiting-avg .value").text(stats.WaitingTimeAvg);
                $(that.selectors.modal.stat).find(".service-avg .value").text(stats.ServiceTimeAvg);
            });
            $(that.selectors.modal.stat).modal();
        });

        if (that.queueSetting.IsOperatorPanelRequired) {
            setTimeout(function () {
                if (!that.deviceConnected) {
                    that.logger.debug("Device disconnected");
                    $(that.selectors.modal.error).modal({
                        backdrop: "static",
                        keyborad: false
                    });
                }
            }, that.DEVICE_DISCONNECT_TIMEOUT);
        }

        $(window).focus(function () {
            that.isWindowInFocus = true;
        }).blur(function () {
            that.isWindowInFocus = false;
        });

        window.onbeforeunload = function (evt) {
            that.exitDashboard();
        }

        onModalHidden(that, $(that.selectors.modal.redirect), function () {
            $(that.selectors.modal.redirect).find(".reason input").val("");
        });

        onModalHidden(that, $(that.selectors.modal.cancel_reason), function () {
            var modal = $(that.selectors.modal.cancel_reason);
            modal.find(".alert.not-reason").hide();
            modal.find(".form-group.reason").removeClass(that.HELPER_CLASSES.HAS_ERROR);
            modal.find("textarea").val("");
        });
    }

    EQueuePlugin.prototype.initDashboardHubMethods = function () {
        var that = this;
        that.logger.debug("Subscribing to hub callback methods");
        that.dashboardHub.client.notifyConnectionEstablished = function () {
            that.logger.debug("Connection established");
            that.registerMessage("Соединение установлено");

            if (that.queueSetting.IsOperatorPanelRequired == false) {
                $(that.selectors.screenplayPanel).fadeIn();
            }
        };
        that.dashboardHub.client.notifyConnectionLost = function () {
            that.logger.debug("Connection lost");
            that.hub.stop();
            endDeviceAliveChecker(that);
            $(that.selectors.modal.error).modal(that.MODAL_STATES.SHOW);
        };
        that.dashboardHub.client.notifyDeviceConnected = function () {
            if (that.queueSetting.IsOperatorPanelRequired) {
                that.enableInterface();
            }
            //if (that.option.deviceType === that.DEVICE_TYPES.WEB_DASHBOARD) {
            //    startRefreshActivity(that);
            //}
            onDeviceConnected(that);
        };
        that.dashboardHub.client.notifyDeviceDisconnected = function () {
            if (that.queueSetting.IsOperatorPanelRequired) {
                that.disableInterface();
            }
            onDeviceDisconnected(that);
        };
        that.dashboardHub.client.notifyServerError = function (message) {
            that.logger.debug("Server error occurred: " + message);
        };
        that.dashboardHub.client.notifyScreenplayUpdated = function () {
            location.href = "/dashboard/" + that.option.companyCode + "/reload?deviceId=" + that.option.deviceId + "&employeeNumber=" + that.option.employeeCardNumber;
        };
        that.dashboardHub.client.notifySurveyLaunched = function (surveyName) {
            that.surveyLaunched = true;
            that.registerMessage("Запущен: '" + surveyName + "'");
        };
        that.dashboardHub.client.notifyMessage = function (message) {
            that.registerMessage(message);
        };
        that.dashboardHub.client.notifyTicketWaiting = function (waitingTicketCount, serviceTicketCount) {
            that.logger.debug('Waiting tickets in queue: ' + waitingTicketCount);
            that.logger.debug('Service tickets in queue: ' + serviceTicketCount);

            var isEnableNextButton = that.queueSetting.MinCallingInterval === 0;
            that.enableInterface(isEnableNextButton);
            //that.refreshQueueSetting();

            var ticketCount = waitingTicketCount + serviceTicketCount;
            if (ticketCount === 0) {
                $(that.selectors.queue.b_next).text("Завершить");
                $(that.selectors.queue.b_next).hide();
                that.lastTicketCount = 0;
                return;
            }

            var isExistNewTicket = ticketCount > that.lastTicketCount;

            if (waitingTicketCount > 0) {
                //TODO: из-за того, что вызов талона происходит первее получения настроек,
                // условие не выполняется и кнопка "Завершить" не отображается
                if (serviceTicketCount > 0 && that.queueSetting.EnableCompletionWithoutCalling) {
                    $(that.selectors.queue.b_complete).show();
                }

                $(that.selectors.queue.b_next).text("Следующий (" + waitingTicketCount + ")");
            }
            else {
                $(that.selectors.queue.b_complete).hide();
                $(that.selectors.queue.b_next).text("Завершить");
            }

            $(that.selectors.queue.b_next).slideDown("fast", function () {
                if (waitingTicketCount === 1 && isExistNewTicket) {
                    //blinkButton($(that.selectors.queue.b_next));
                }
                if (waitingTicketCount > 0 && isExistNewTicket) {
                    that.registerMessage('Новый клиент в очереди');
                }
            });

            that.lastTicketCount = waitingTicketCount + serviceTicketCount;

            if (isExistNewTicket) {
                if (waitingTicketCount === 1 && serviceTicketCount === 0 || waitingTicketCount === 0 && serviceTicketCount === 1) {

                    if (!detectIE() || (detectIE() && detectIE() > 9)) {
                        that.audio.play();
                    }

                    if (window.external.IsWinOperator) {
                        window.external.NewTicket();
                    }
                }

                if (!that.isMSIE && !that.isWindowInFocus) {
                    that.showBrowserNotification("Электронная очередь", "Новый клиент!");
                }
            }
        };
        that.dashboardHub.client.notifyTicketCalling = function (ticketName, serviceId, serviceName, dynamicPriorityReason, regDate) {
            that.currentTicket.Name = ticketName;
            that.currentTicket.ServiceId = serviceId;
            that.logger.debug("Current ticket: " + ticketName);

            that.openEqueueModal(ticketName, serviceName, dynamicPriorityReason, regDate);
            that.showInterface();
            that.enableInterface(true);

            //            that.lastTicketCount--;
            //            if (that.lastTicketCount > 0) {
            //                $(that.selectors.queue.b_next).text("Следующий (" + that.lastTicketCount + ")");
            //            } else {
            //                $(that.selectors.queue.b_next).text("Завершить");
            //            }

            if (that.chat) {
                that.chat.clear();
                that.chat.close();
            }
        };
        that.dashboardHub.client.notifyAdmin = function (notifications) {
            if (window.external.IsWinOperator) {
                if (notifications.length > 0) {
                    window.external.showAlertMessage(JSON.stringify(notifications));
                } else {
                    window.external.hideAlert();
                }
            } else {
                if (notifications.length > 0) {
                    var tmpl = $(that.selectors.templates.notification_item).html();

                    var html = "";
                    $.each(notifications, function (index, val) {
                        val.Description = val.Description.replace(new RegExp("\r\n", 'g'), "<br>");
                        html += Mustache.to_html(tmpl, val);
                    });
                    $(that.selectors.modal.adminNotification).find(".list-group.notifications").html(html);
                    $(that.selectors.modal.adminNotification).modal(that.MODAL_STATES.SHOW);

                } else {
                    $(that.selectors.modal.adminNotification).modal(that.MODAL_STATES.HIDE);
                }


            }
        }
        that.dashboardHub.client.notifyCompleteConversation = function (isQueueEmpty) {
            that.hideInterface(isQueueEmpty);
            if (that.chat) {
                that.chat.clear();
                that.chat.close();
            }
        }
        that.dashboardHub.client.notifyResponseSelected = function (responseData, responseTag) {
            var text = responseTag + " " + responseData;
            $(that.selectors.modal.info).find(".modal-body h3").text(text);
            $(that.selectors.modal.info).modal();
        }
        that.dashboardHub.client.notifySpecTicketReg = function (ticketName) {
            that.logger.debug('=> notifySpecTicketReg(\'' + ticketName + '\')');
            that.currentTicket.Name = ticketName;
            that.registerMessage('Зарегистрирован талон: ' + ticketName);
        };
        that.dashboardHub.client.updateRule = function (rule) {

            that.rule = rule;
            that.rule.ServiceTimeDuration = moment.duration(that.rule.ServiceTimeString);
            that.rule.isServiceTimeGreatThanZero = that.rule.ServiceTimeDuration.asMilliseconds() > 0;
        };
        //that.dashboardHub.client.refreshLastTicketsList = function () {
        //    // При обновлении без задержки последний талон не успевает 
        //    // изменить состояние в БД (Напрмер: с "Обслуживается" на "Отложен")
        //    setTimeout(function () {
        //        that.refreshLastTicketsList();
        //    }, that.LASTTICKETS_REQUEST_TIMEOUT);
        //}
        //that.dashboardHub.client.refreshOnlineDeviceList = function () {
        //    //that.refreshOnlineDevices();
        //}
        //that.dashboardHub.client.lastTicketInfo = function(tickets) {
        //    refreshLastTicketsList(that, tickets);
        //}
        //that.dashboardHub.client.queueSettingInfo = function (setting) {
        //    refreshQueueSetting(that, setting, function() {
        //        if (that.queueSetting.EnableAudioNotify) {
        //            setSupportedAudioNotify(that); // Устанавливаем путь для звуковых оповещений для браузеров PC. Оповещение для планшетов начинается со второго талона. 
        //            // См. событие нажатия на кнопку that.selectors.queue.b_next
        //        }
        //    });
        //}
        //that.dashboardHub.client.onlineDeviceInfo = function(devices) {
        //    refreshOnlineDevices(that, devices);
        //}
    }

    EQueuePlugin.prototype.initHubMethods = function () {
        var that = this;

        that.logger.debug("Initalizing hub connection event handlers");

        that.hub.qs = {
            deviceId: that.option.deviceId,
            employeeNumber: that.option.employeeCardNumber,
            serverDate: that.option.serverDate
        }

        that.hub.connectionSlow(function () {
            that.registerMessage("Ого! Возможны проблемы с подключением :-(");
        });
        that.hub.reconnected(function () {
            that.registerMessage("Подключение восстановлено");
            that.enableInterface();
            that.dashboardHub.server.checkWaitingTickets();
        });
        that.hub.disconnected(function () {
            onHubDisconnected(that, "Hub disconnected");
        });

        that.hub.error(function (error) {
            that.logger.error(error);
            onHubDisconnected(that);
            that.exitDashboard("Работа веб-панели была прекращена. Некоторые службы электронной очереди стали недоступны.");

            //setTimeout(function () {
            //    that.reconnect();
            //}, that.RECONNECT_INTERVAL);
        });

        that.hub.stateChanged(function (state) {
            var stateConversion = { 0: "Connecting", 1: "Connected", 2: "Reconnecting", 4: "Disconnected" };
            var newState = stateConversion[state.newState];
            var oldState = stateConversion[state.oldState];
            that.logger.debug(oldState + " -> " + newState);
        });
    }

    EQueuePlugin.prototype.logger = {
        debug: function (debugText) {
            var date = new Date();
            var options = {
                hour: 'numeric',
                minute: 'numeric',
                second: 'numeric'
            };

            if (!detectIE() || detectIE() > 8) {
                console.log(date.toLocaleString("ru", options) + "  DEBUG: " + debugText);
            }
        },
        debugFormat: function (formatText, arg) {
            var that = this;
            for (var i = 0; i < arg.length; i++) {
                formatText = formatText.replace("{" + i + "}", arg[i]);
            }
            that.debug(formatText);
        },
        error: function (debugText) {
            var date = new Date();
            var options = {
                hour: 'numeric',
                minute: 'numeric',
                second: 'numeric'
            };

            if (!detectIE() || detectIE() > 8) {
                console.log(date.toLocaleString("ru", options) + "  ERROR: " + debugText);
            }
        }
    }

    return EQueuePlugin;
})();

ChatPlugin = (function () {
    function ChatPlugin(option) {
        this.chatHub = $.connection.dashboardHub;
        this.selectors = {
            b_submit: "#btn-submit",
            t_input: "#text-input",
            my_msg_tmpl: "#my-message-tmpl",
            you_msg_tmpl: "#you-message-tmpl",
            p_body: "#chat-body",
            p_chat: "#chat",
            chat_container: ".chat-container",
            b_open: "#open-chat",
            b_close: "#close-chat"
        };

        //option = {
        //      employeeName: ''
        //      eQueuePlugin: {}
        //}
        this.option = option;
    }

    function showMessageInChat(that, tmpl, message) {
        var html = Mustache.to_html(tmpl, message);
        $(that.selectors.p_chat).append(html);

        var body = $(that.selectors.p_body);
        body.scrollTop(body[0].scrollHeight);
    }

    function initHubMethods(that) {
        that.chatHub.client.receiveChatMessage = function (message) {
            var tmpl = $(that.selectors.you_msg_tmpl).html();
            showMessageInChat(that, tmpl, message);
        };
    }

    function sendMessage(that, message) {
        that.chatHub.server.sendChatMessage(message);
        $(that.selectors.t_input).val("");

        var tmpl = $(that.selectors.my_msg_tmpl).html();
        var options = {
            hour: 'numeric',
            minute: 'numeric'
        };
        var sendTime = new Date().toLocaleString("ru", options);
        showMessageInChat(that, tmpl, {
            Message: message,
            Name: that.option.employeeName,
            Date: sendTime
        });
    }

    function registerObservers(that) {
        $(that.selectors.b_submit).click(function () {
            var message = $(that.selectors.t_input).val();
            if (message !== "") {
                sendMessage(that, message);
            }
        });

        $(that.selectors.t_input).keypress(function (e) {
            var message = $(that.selectors.t_input).val();
            if (e.which === 13) {
                if (message !== "") {
                    sendMessage(that, message);
                }
            }
        });

        $(that.selectors.b_open).click(function () {
            that.open();
        });

        $(that.selectors.b_close).click(function () {
            that.close();
        });
    }

    ChatPlugin.prototype.init = function () {
        var that = this;
        registerObservers(that);
        initHubMethods(that);
    }

    ChatPlugin.prototype.open = function () {
        var that = this;
        console.log(that);
        var isClosed = $(that.selectors.chat_container).is('.fade');
        if (!that.option.eQueuePlugin.deviceConnected) {
            that.option.eQueuePlugin.registerMessage("Устройство отключено. Чат не может работать без устройства.");
            return;
        }

        if (isClosed) {
            that.chatHub.server.beginChat();
            $(that.selectors.chat_container).removeClass("fade");
        }
    }

    ChatPlugin.prototype.close = function () {
        var that = this;
        var isClosed = $(that.selectors.chat_container).is('.fade');
        if (!isClosed) {
            that.chatHub.server.endChat();
            $(that.selectors.chat_container).addClass("fade");
        }
    }

    ChatPlugin.prototype.clear = function () {
        var that = this;
        $(that.selectors.p_chat).empty();
    }

    return ChatPlugin;
})();
