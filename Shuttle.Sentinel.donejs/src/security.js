import can from 'can';
import localisation from 'sentinel/localisation';
import AnonymousPermissions from 'sentinel/models/anonymous-permissions';
import RegisterSession from 'sentinel/models/register-session';
import state from 'sentinel/application-state';

var security = {
    hasSession: function () {
        return state.attr('token') != undefined;
    },

    hasPermission: function (permission) {
        var result = false;
        var permissionCompare = permission.toLowerCase();

        state.attr('permissions').each(function (item) {
            if (result) {
                return;
            }

            result = item.permission === '*' || item.permission.toLowerCase() === permissionCompare;
        });

        return result;
    },

    removePermission: function(permission) {
        state.attr('permissions', state.attr('permissions').filter(function(item) {
            return item.permission !== permission;
        }));
    },

    fetchAnonymousPermissions: function () {
        var self = this;
        var deferred = can.Deferred();

        AnonymousPermissions.findAll()
			.done(function (data) {
			    can.each(data, function (item) {
			        self._addPermission('anonymous', item.permission);
			    });

			    deferred.resolve();
			})
			.fail(function () {
			    deferred.reject(localisation.value('exceptions.anonymous-permissions'));
			});

        return deferred;
    },

    _addPermission: function (type, permission) {
        state.attr('permissions').push({ type: type, permission: permission });
    },

    login: function (username, password) {
        var deferred = can.Deferred();

        new RegisterSession({
            username: username,
            password: password
        }).save()
			.done(function (response) {
			    if (response.ok) {
			        deferred.resolve();
			    } else {
			        deferred.reject();
			    }
			})
			.fail(function () {
			    deferred.reject();
			});

        return deferred;
    },

    logout: function() {
    },

    accessDenied: function (permission) {
        alert(localisation.value('security.access-denied', { hash: window.location.hash, permission: permission, interpolation: { escape: false } }));
	    
        window.location.hash = !this.hasSession() ? '#!user/login' : '#!dashboard';
    }
};

export default security;