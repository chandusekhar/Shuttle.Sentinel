import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './add.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import each from 'can-util/js/each/';
import {OptionList} from 'shuttle-canstrap/select/';

resources.add('subscription', { action: 'add', permission: Permissions.Manage.Subscriptions });

var api = {
    dataStores : new Api({
        endpoint: 'datastores'
    }),
    subscriptions : new Api({
        endpoint: 'subscriptions/{id}'
    })
}

export const ViewModel = DefineMap.extend(
    'subscription',
    {
        dataStores: {
            Default: OptionList
        },

        init () {
            const self = this;

            state.title = 'subscription:list.title';

            self.dataStores.push({
                value: undefined,
                label: 'select'
            });

            api.dataStores.list().then((response) => {
                each(response, (store) => {
                    self.dataStores.push({
                        value: store.id,
                        label: store.name
                    });
                });
            });

            const result = state.stack.pop('subscription');

            if (!result) {
                return;
            }

            this.dataStoreId = result.dataStoreId;
            this.messageType = result.messageType;
            this.inboxWorkQueueUri = result.inboxWorkQueueUri;
        },

        dataStoreId: { 
            type: 'string',
            default: '',
            validate: {
                presence: true
            }
        },

        messageType: { 
            type: 'string',
            default: '',
            validate: {
                presence: true
            }
        },

        inboxWorkQueueUri: { 
            type: 'string',
            default: '',
            validate: {
                presence: true
            }
        },

        add: function() {
            if (!!this.errors()) {
                return false;
            }

            api.subscriptions.post({
                dataStoreId: this.dataStoreId,
                messageType: this.messageType,
                inboxWorkQueueUri: this.inboxWorkQueueUri
            });

            this.close();

            return false;
        },

        close: function() {
            router.goto({
                resource: 'subscription',
                action: 'list'
            });
        }
    }
);

validator(ViewModel);

export default Component.extend({
    tag: 'sentinel-subscription-add',
    ViewModel,
    view
});