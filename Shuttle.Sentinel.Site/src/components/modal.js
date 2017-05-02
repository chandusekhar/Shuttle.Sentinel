import Component from 'can-component';
import DefineMap from 'can-define/map/';
import view from './modal.stache!';
import localisation from '~/localisation';

export const ViewModel = DefineMap.extend({
    modalType: {
        get: function(value) {
            return value || 'fade';
        }
    },

    dismissText: {
        get: function(value) {
            return value || localisation.value(value);
        }
    },

    textType: {
        get: function(value) {
            return value || 'primary';
        }
    },

    message: { type: 'string', value: '' },

    hasMessage: {
        get: function() {
            return !!this.message;
        }
    },

    _primaryClick: function() {
        const modalElement = $('#' + this.modalId);

        if (modalElement) {
            modalElement.modal('hide');
        }

        this.primaryClick(arguments);
    }
});

export default Component.extend({
    tag: 'sentinel-modal',
    view,
    ViewModel
});