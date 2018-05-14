/// <reference path="../Scripts/jquery-3.0.0.min.js" />
/// <reference path="https://code.jquery.com/ui/1.12.1/jquery-ui.js" />
/// <reference path="../../js/plugin/ckeditor/config.js" />
$(function () {
    $("#datepicker").datepicker();
    CKEDITOR.replace('CauHinh');
    CKEDITOR.replace('MoTa');

});
$(document).ready(function(){
                        $('#Delete').click(function () {
                            var id=$(this).data('id');
                            $.ajax({
                                url: '/QuanLySanPham/Xoa',
                                
                                data: id,
                                type: 'post',
                                dataType:'json',
                                success: function (result) {
                                    if (result==true) {
                                        bootbox.alert('Xóa Bản Ghi Thành Công');
                                    } else {
                                        bootbox.alert('Xóa Bản Ghi Lỗi');
                                        return false;
                                    }
                                }
                            })
                        })

        $("#btnchonanh").click(function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (URL) {
                //lay ten ảnh
                imgname = URL.substring(URL.lastIndexOf("/") + 1);

                $("#HinhAnh").val(imgname);
            };
            finder.popup();
        });
         $("#btnchonanh2").click(function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (URL) {
                //lay ten ảnh
                imgname = URL.substring(URL.lastIndexOf("/") + 1);

                $("#HinhAnh2").val(imgname);
            };
            finder.popup();
         });
         $("#btnchonanh3").click(function () {
             var finder = new CKFinder();
             finder.selectActionFunction = function (URL) {
                 //lay ten ảnh
                 imgname = URL.substring(URL.lastIndexOf("/") + 1);

                 $("#HinhAnh3").val(imgname);
             };
             finder.popup();
         });
         $("#btnchonanh4").click(function () {
             var finder = new CKFinder();
             finder.selectActionFunction = function (URL) {
                 //lay ten ảnh
                 imgname = URL.substring(URL.lastIndexOf("/") + 1);

                 $("#HinhAnh4").val(imgname);
             };
             finder.popup();
         });
});
