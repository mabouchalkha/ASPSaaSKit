var StarterKit;
(function (StarterKit) {
    var Factories;
    (function (Factories) {
        var Overlay = (function () {
            function Overlay() {
            }
            Overlay.prototype.show = function () {
                $('#overlay').show();
            };
            Overlay.prototype.hide = function () {
                $('#overlay').hide();
            };
            return Overlay;
        })();
        Factories.Overlay = Overlay;
        angular.module("starterKit").service('overlay', Overlay);
    })(Factories = StarterKit.Factories || (StarterKit.Factories = {}));
})(StarterKit || (StarterKit = {}));
//# sourceMappingURL=overlay.js.map