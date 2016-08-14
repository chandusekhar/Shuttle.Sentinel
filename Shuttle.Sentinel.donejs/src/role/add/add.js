import Component from 'can/component/';
import Map from 'can/map/';
import 'can/map/define/';
import './add.less!';
import template from './add.stache!';
import resources from 'sentinel/resources';
import Permissions from 'sentinel/permissions';
import api from 'sentinel/api';
import state from 'sentinel/state';

resources.add('role', { action: 'add', permission: Permissions.Add.Role});

export const ViewModel = Map.extend({
  define: {
    message: {
      value: 'This is the sentinel-role-add component'
    }
  }
});

export default Component.extend({
  tag: 'sentinel-role-add',
  viewModel: ViewModel,
  template
});