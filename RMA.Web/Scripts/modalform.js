﻿//$(function () {

//    $.ajaxSetup({ cache: false });

//    $("a[data-modal]").on("click", function (e) {

//        // hide dropdown if any
//        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');


//        $('#myModalContent').load(this.href, function () {


//            $('#myModal').modal({
//                /*backdrop: 'static',*/
//                keyboard: true
//            }, 'show');

//            bindForm(this);
//        });

//        return false;
//    });


//});

//function bindForm(dialog) {

//    $('form', dialog).submit(function () {
//        $.ajax({
//            url: this.action,
//            type: this.method,
//            data: $(this).serialize(),
//            success: function (result) {
//                if (result.success) {
//                    $('#myModal').modal('hide');
//                    //Refresh
//                    location.reload();
//                } else {
//                    $('#myModalContent').html(result);
//                    bindForm();
//                }
//            }
//        });
//        return false;
//    });
//}

$(function () {
	
    $.ajaxSetup({ cache: false });



	$("a[data-modal]").on("click", function (e) {
		
        return OpenModalPopup(this, e);
        // hide dropdown if any

    });


});

function OpenModalPopup(thiso, e) {
    $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');
   

    $('#myModalContent').load(thiso.href, function () {    

        $('#myModal').modal({
            /*backdrop: 'static',*/
            keyboard: true
        }, 'show');

        bindForm(thiso);
    });

    return false;
}


function bindForm(dialog) {
    //location.reload();
    //$('form', dialog).submit(function () {
    //    $.ajax({
    //        url: this.action,
    //        type: this.method,
    //        data: $(this).serialize(),
    //        success: function (result) {
    //            if (result.success) {
    //                $('#myModal').modal('hide');
    //                //Refresh
    //                location.reload();
    //            } else {
    //                $('#myModalContent').html(result);
    //                bindForm();
    //            }
    //        }
    //    });
    //    return false;
    //});
}
$(function () {
	
    try {
        $("table.table").tablesorter({ widgets: ['zebra'], widthFixed: true }).tablesorterPager({
            container: $("#Custompager"), size: $(".pagesize option:selected").val()
        });
    } catch (e) {

    }

});