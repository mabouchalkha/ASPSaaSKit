module StarterKit.Architectures.Factories.DOM.Notif {
    export class Message {
        constructor(public text: string, public title: string, public type: NotifType) {
            if (type == null) {
                throw new RangeError("Cannot create a message without a type");
            }
        };
    }

    export enum NotifType {
        WAIT, ERROR, SUCCESS, WARNING, INFO
    }
}