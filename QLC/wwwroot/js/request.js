(function ($) {
    function Requests() {
        var $this = this;
        function initlizeModel() {
            $('#modal-action-request').on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () {
            initlizeModel();
        }
    }
    $(function () {
        var self = new Requests();
        self.init();
    })
}(jQuery))
