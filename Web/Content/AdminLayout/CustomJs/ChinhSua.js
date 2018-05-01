/// <reference path="../Scripts/jquery-3.0.0.min.js" />
/// <reference path="https://code.jquery.com/ui/1.12.1/jquery-ui.js" />
/// <reference path="../../js/plugin/ckeditor/config.js" />
$(function () {
    $("#datepicker").datepicker();
    CKEDITOR.replace('CauHinh');
    CKEDITOR.replace('MoTa');

});
$(document).ready(function(){
        $("#btnchonanh").click(function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (URL) {
                //lay ten ảnh
                imgname = URL.substring(URL.lastIndexOf("/") + 1);

                $("#HinhAnh").val(imgname);
            };
            finder.popup();
        });
});
