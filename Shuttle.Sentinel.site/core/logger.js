Sentinel.Logger = can.Construct.extend({
    _displayMessage: function (message, options) {
        options = options ? options : {};

        if ((options.type === 'error' || options.type === 'warn') || Sentinel.DEBUG) {
            var logMessage = '[' + new Date().toTimeString() + '] : ' + message;

            switch (options.type) {
                case 'debug':
                    console.debug(logMessage);
                    break;
                case 'warn':
                    console.warn(logMessage);
                    break;
                case 'error':
                    console.error(logMessage);
                    break;
                default:
                    console.log(logMessage);
                    break;
            }
        }
    },

    debug: function (message) {
        this._displayMessage(message, { type: 'debug' });
    },

    info: function (message) {
        this._displayMessage(message, { type: 'info' });
    },

    warn: function (message) {
        this._displayMessage(message, { type: 'warn' });
    },

    error: function (message) {
        this._displayMessage(message, { type: 'error' });
    }
});

Sentinel.logger = new Sentinel.Logger();
