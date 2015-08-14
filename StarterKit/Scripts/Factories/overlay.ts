module StarterKit.Factories {
    import fact = StarterKit.Factories.Notif;

    export class Overlay {
        show() {
            $('#overlay').show();
        }
        hide() {
            $('#overlay').hide();
        }
    }

    angular.module("starterKit").service('overlay', Overlay);
}