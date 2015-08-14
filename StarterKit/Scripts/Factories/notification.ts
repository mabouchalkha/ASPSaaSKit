module StarterKit.Factories {
    import DomNotif = StarterKit.Architectures.Factories.DOM.Notif;

    export class Notif {
        static $inject = ['toastr', '$rootScope', 'overlay'];
        private _options: Object;
        private toastr: any;
        private $rootScope: ng.IRootScopeService;
        private overlay: StarterKit.Factories.Overlay = new StarterKit.Factories.Overlay();
        constructor() {
            this._options = {
                "allowHtml": true,
                "positionClass": "toast-top-right",
                "stimeOut": "3600",
                "extendedTimeOut": "3600",
                "closeButton": true
            };
        }

        private logIt(title: string, message: string, type: DomNotif.NotifType): any {
            if (type == DomNotif.NotifType.WAIT) {
                this.overlay.show();
                return null;
            }

            return this.toastr[type](message, title, this.toastr.options);
        }

        public print(message: DomNotif.Message = null): any {
            return this.logIt(message.text, message.title, message.type);
        }

        public wait(): any {
            return this.logIt(null, null, DomNotif.NotifType.WAIT);
        }

        public clear() {
            this.overlay.hide();
        }
    }

    angular.module("starterKit").service("notif", Notif);
}

