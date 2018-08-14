(function ($) {
    function Approver() {
        var $this = this;
        function initlizeModel() {
            $('#modal-action-approver').on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
                });          
        }
        $this.init = function () {
            initlizeModel();
        }  
    }
    $(function () {
        var self = new Approver();
        self.init();
    })
}(jQuery))