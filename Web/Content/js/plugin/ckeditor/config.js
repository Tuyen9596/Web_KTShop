/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
     config.uiColor = '#AADC6E';
	 config.filebrowserBrowseUrl =      '/ckfinder/ckfinder.html';
	 config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?type=Images';
	 config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?type=flash';
	 config.filebrowserUploadUrl =      '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=files';
	 config.filebrowserImageUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
	 config.filebrowserFlashUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=flash';

    //config.filebrowserBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Images';
    //config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Flash';
    //config.filebrowserUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = '/Data';
    //config.filebrowserFlashUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Content/js/plugin/ckfinder/');
};
