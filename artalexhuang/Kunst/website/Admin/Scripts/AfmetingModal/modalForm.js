

function initializeLoad() {
    $(document).ready(function () {





                    $(function () {
                        $.ajaxSetup({ cache: false });

                        $(".PopUp").on("click", function (e) {
                            $('#myModalContent').load(this.href, function () {
                                $('#myModal').modal({
                                    /*backdrop: 'static',*/
                                    keyboard: true
                                }, 'show');
                                bindForm(this);
                            });
                            return false;
                        });                      
                    });



                    function bindForm(dialog) {

                        $('form', dialog).submit(function () {
                            $.ajax({
                                url: this.action,
                                type: this.method,
                                data: $(this).serialize(),
                                dataType: 'json',
                                success: function (result) {
                                    if (result.success) {
                                        $('#myModal').modal('hide');
                                        $('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                                    }
                                    else {
                                        $('#myModalContent').html(result);
                                        bindForm();
                                    }
                                }
                            });
                            return false;
                        });

                    }





    });
}

initializeLoad();
Sys.WebForms.PageRequestManager.getInstance().add_endrequest(EndRequestHandler);
function EndRequestHandler(sender, args) {
    if(args.get_error()== undefined)
    {
        initializeLoad();
    }
}