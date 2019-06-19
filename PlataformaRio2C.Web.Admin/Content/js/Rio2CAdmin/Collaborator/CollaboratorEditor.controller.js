(function () {
	'use strict';

	angular
        .module('Collaborator')
        .controller('CollaboratorEditor', CollaboratorEditor);
     
	CollaboratorEditor.$inject = ['$scope'];

	function CollaboratorEditor($scope) {
		var vm = this;

		vm.players = [];		

		vm.loadPlayers = loadPlayers;
		vm.addPlayer = addPlayer;
		vm.removePlayer = removePlayer;

		function loadPlayers(value) {
		    vm.players = JSON.parse(value);		    
		}

		function addPlayer() {
		    vm.players.push({Uid : '00000000-0000-0000-0000-000000000000'});
		}

		function removePlayer(index) {			
			vm.players.splice(index, 1);
		}
	}

    
})();