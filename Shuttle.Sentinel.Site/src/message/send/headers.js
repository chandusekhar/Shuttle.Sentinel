import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './headers.stache!';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import guard from 'shuttle-guard';

export const MessageHeaderMap = DefineMap.extend({
    id: {
        type: 'string',
        default: ''
    },
    key: {
        type: 'string',
        default: ''
    },
    value: {
        type: 'string',
        default: ''
    },
    description: {
        get() {
            return this.key + ': ' + this.value;
        }
    },
    saved: {
        type: 'boolean',
        default: false,
        serialize: false
    },
    edit() {
        this.viewModel.edit(this);
    },
    remove() {
        this.viewModel.remove(this);
    },
    viewModel: {
        type: '*',
        serialize: false
    },
    toggle(){
        var self = this;

        if (!this.saved){
            api.headers.post({
                key: this.key,
                value: this.value,
            })
                .then(function(id){
                    self.id = id;
                    self.saved = true;
                });
        } else{
            api.headers.delete({
                id: this.id
            })
                .then(function(){
                    self.saved = false;
                });
        }
    }
});

export const MessageHeaderList = DefineList.extend({
    '#': MessageHeaderMap
});

var api = {
    search: new Api({
        endpoint: 'messageheaders/{search}',
        Map: MessageHeaderMap
    }),
    headers: new Api({
        endpoint: 'messageheaders/{id}'
    })
}

export const ViewModel = DefineMap.extend({
    get savedHeaders() {
        return api.search.list({search: encodeURIComponent(this.searchValue)});
    },

    searchValue: {
        type: 'string',
        default: ''
    },

    searchHeader: function (el) {
        this.searchValue = el.value;

        $(el).dropdown();
    },

    select: function (item) {
        const header = this.find(item.key);

        if (!!header) {
            header.id = item.id;
            header.value = item.value;
            header.saved = true;
        }
        else {
            this.headers.push({
                id: item.id,
                key: item.key,
                value: item.value,
                saved: true,
                viewModel: this
            });
        }
    },

    columns: {
        Default: DefineList
    },

    edit(header) {
        guard.againstUndefined(header, 'header');

        this.headerKey = header.key || '';
        this.headerValue = header.value || '';
    },

    remove(header) {
        guard.againstUndefined(header, 'header');

        var match = header.key.toLowerCase();

        this.headers = this.headers.filter(function (header) {
            return header.key.toLowerCase() !== match;
        });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'key',
                columnClass: 'col-5',
                attributeName: 'key'
            });

            columns.push({
                columnTitle: 'value',
                columnClass: 'col-5',
                attributeName: 'value'
            });

            columns.push({
                columnTitle: 'saved',
                columnClass: 'col-1',
                stache: '<cs-checkbox click:from="toggle" checked:bind="saved" checkedClass:from="\'fa-toggle-on\'" uncheckedClass:from="\'fa-toggle-off\'"/>'
            });

            columns.push({
                columnTitle: 'actions',
                columnClass: 'col-1',
                stache: '<cs-button click:from="edit" iconNameClass:from="\'fa-pencil\'" elementClass:from="\'btn-sm\'"/><cs-button click:from="remove" iconNameClass:from="\'fa-times\'"  elementClass:from="\'btn-sm\'"/>'
            });
        }
    },

    headerKey: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    headerValue: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    headers: {
        Default: MessageHeaderList
    },

    find(key) {
        guard.againstUndefined(key, 'key');

        var match = key.toLowerCase();

        var result = this.headers.filter(function (header) {
            return header.key.toLowerCase() === match;
        });

        return result.length > 0 ? result[0] : null;
    },

    add() {
        const self = this;

        if (!!this.errors()) {
            return false;
        }

        const header = this.find(this.headerKey);

        if (!!header) {
            header.value = this.headerValue;
            header.saved = false;
        }
        else {
            this.headers.push({
                key: this.headerKey,
                value: this.headerValue,
                viewModel: this
            });
        }
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'sentinel-message-send-headers',
    ViewModel,
    view
});