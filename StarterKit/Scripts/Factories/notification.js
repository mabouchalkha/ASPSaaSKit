var StarterKit;
(function (StarterKit) {
    var Factories;
    (function (Factories) {
        var DomNotif = StarterKit.Architectures.Factories.DOM.Notif;
        var Notif = (function () {
            function Notif() {
                this.overlay = new StarterKit.Factories.Overlay();
                this._options = {
                    "allowHtml": true,
                    "positionClass": "toast-top-right",
                    "stimeOut": "3600",
                    "extendedTimeOut": "3600",
                    "closeButton": true
                };
            }
            Notif.prototype.logIt = function (title, message, type) {
                if (type == DomNotif.NotifType.WAIT) {
                    this.overlay.show();
                    return null;
                }
                return this.toastr[type](message, title, this.toastr.options);
            };
            Notif.prototype.print = function (message) {
                if (message === void 0) { message = null; }
                return this.logIt(message.text, message.title, message.type);
            };
            Notif.prototype.wait = function () {
                return this.logIt(null, null, DomNotif.NotifType.WAIT);
            };
            Notif.prototype.clear = function () {
                this.overlay.hide();
            };
            Notif.$inject = ['toastr', '$rootScope', 'overlay'];
            return Notif;
        })();
        Factories.Notif = Notif;
        angular.module("starterKit").service("notif", Notif);
    })(Factories = StarterKit.Factories || (StarterKit.Factories = {}));
})(StarterKit || (StarterKit = {}));
//# sourceMappingURL=notification.js.map