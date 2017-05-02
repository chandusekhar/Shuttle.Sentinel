﻿import Component from 'can-component';
import DefineMap from 'can-define/map/';
import view from './page-title.stache!';
import localisation from '~/localisation';

export const ViewModel = DefineMap.extend({
    title: {
        get: function(value) {
            return localisation.value(value);
        }
    }
});

export default Component.extend({
    tag: 'sentinel-page-title',
    view: view,
    viewModel: ViewModel
});

