(function () {
    var $ = jQuery;

    var PhotoPanel = (function () {
        function PhotoPanel() {
            this.init();
        }

        PhotoPanel.prototype.init = function () {
            this.enable();
            this.build();
        };

        PhotoPanel.prototype.enable = function () {
            var self = this;
            $('body').on('click', 'a[rel^=photopanel], area[rel^=photopanel], a[data-photopanel], area[data-photopanel]', function (event) {
                self.start($(event.currentTarget));
                return false;
            });
        };

        PhotoPanel.prototype.start = function ($link) {
            var self = this;
            this.album = [];

            function addToAlbum($link) {
                self.album.push({
                    link: $link.attr('href'),
                    title: $link.attr('data-title') || $link.attr('title')
                });
            }
        };
    })();
}).call(this);