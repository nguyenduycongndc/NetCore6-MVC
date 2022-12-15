﻿function openView(type, value) {
    var index = $("#view");
    var create = $("#create");
    var edit = $("#edit");
    var detail = $("#detail");
    if (type === 0) {
        index.show();
        create.hide();
        edit.hide();
        setTimeout(function () {
            onSearch();
        }, 100);
    }
    else if (type === 1) {
        clearMsgInvalid();
        $('#emailAddressCreate').val(""),
        $('#CCCreate').val(""),
        localStorage.setItem("type", "1");
        index.hide();
        detail.hide();
        create.show();
        //document.getElementById("userNameCreate").focus();
        edit.hide();
        //document.getElementById("formCreate").reset();
        //$("#frmHeaderCreate").val(frmHeaderCreate);
    }
    else if (type === 2) {
        index.hide();
        create.hide();
        edit.hide();
        detail.show();

        fnGetDetail(type, value);
    }
    else if (type === 3) {
        clearMsgInvalid();
        index.hide();
        create.hide();
        detail.hide();
        edit.show();
        //document.getElementById("userNameEdit").focus();
        fnGetDetail(type, value);
    }
}

function fnSearchDataSuccess(rspn) {
    showLoading();
    if (rspn !== undefined && rspn !== null && rspn.data.length > 0) {
        var tbBody = $('#emailTypeTable tbody');
        $("#emailTypeTable").dataTable().fnDestroy();
        tbBody.html('');
        for (var i = 0; i < rspn.data.length; i++) {
            var obj = rspn.data[i];
            var CC = obj.cc != null ? obj.cc : "";
            var CA = obj.create_at != null ? obj.create_at : "";
            var html = '<tr>' +
                '<td class="text-center"></td>' +
                '<td>' + obj.email_address + '</td>' +
                '<td>' + CC + '</td>' +
                '<td>' + CA + '</td>' +
                '<td class="text-center">' +
                '<a type="button" class="btn icon-default btn-action-custom" onclick="openView(2,' + obj.id + ')" style="color:green"><i data-toggle="tooltip" title="Chi tiết" class="fa fa-eye" aria-hidden="true"></i></a>' +
                '<a type="button" class="btn icon-default btn-action-custom" onclick="openView(3,' + obj.id + ')" style="color:blue"><i data-toggle="tooltip" title="Cập nhật" class="micon dw dw-edit2" aria-hidden="true"></i></a>'+
                '<a type="button" class="btn icon-delete btn-action-custom" onclick="Delete(' + obj.id + ')" style="color:red"><i data-toggle="tooltip" title="Xóa" class="fa fa-trash" aria-hidden="true"></i></a>'+
                '</td>' +
                '</tr>';
            tbBody.append(html);
        }
        var page_size = (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
        var t = $("#emailTypeTable").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": [0, 3, 4],
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustom(rspn.count);
        viewBtnActionPage();
        hideLoading();
    } else if (rspn.data == "" || rspn.data == null || rspn.data == undefined) {
        var tbBody = $('#emailTypeTable tbody');
        $("#emailTypeTable").dataTable().fnDestroy();
        tbBody.html('');

        var page_size = (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
        var t = $("#emailTypeTable").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": [0, 3, 4],
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustomNull();
        hideLoading();
    }
}
function onSearch() {
    var obj = {
        'email_address': $('#EmailAddress').val(),
        'page_size': parseInt($("#cbPageSize").val()),
        'start_number': (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
    }
    callApi_userservice(
        apiConfig.api.sendmail.controller,
        apiConfig.api.sendmail.action.searchemail.path,
        apiConfig.api.sendmail.action.searchemail.method,
        obj, 'fnSearchDataSuccess', 'msgError');
}
//function fnDeleteSuccess(rspn) {
//    swal({
//        title: "Thông báo",
//        text: 'Bạn có chắc chắn muốn xoá bản ghi' + ' ' + '"' + rspn.data.fullName + '"',
//        type: 'warning',
//        showCancelButton: !0,
//    }).then((isConfirm) => {
//        if (isConfirm.value == true) {
//            fnDeleteUser(rspn.data.id);
//        }
//        return false;
//    });
//}
//function Delete(id) {
//    fnGetDetail(null, id);
//}

//function UnitTypeActive(id, input) {
//    var status = 1;
//    if ($(input).prop("checked") == false) {
//        status = 0;
//    }
//    fnActive(id, status);
//}
//function updateUserSuccess(data) {
//    if (data != false) {
//        toastr.success("Thêm mới thành công");
//        setTimeout(function () {
//            openView(0, 0)
//        }, 2000);
//    }
//    else {
//        toastr.error("Thêm mới thất bại");
//        //setTimeout(function () { toastr.error(getStatusCode(data.code), 'Error', { progressBar: true }) }, 70);
//    }
//}
function createEmailSuccess(data) {
    if (data.data !== null) {
        toastr.success("Thêm mới thành công");
        //localStorage.removeItem("id");
        //localStorage.removeItem("type");
        setTimeout(function () {
            openView(0, 0)
        }, 2000);
    }
    else {
        toastr.error(data.message)
        //setTimeout(function () { toastr.error(getStatusCode(data.code), 'Error', { progressBar: true }) }, 50);
    }
}

//function fnDeleteUserSuccess(rspn) {
//    if (rspn.data === true) {
//        toastr.success("Xóa dữ liệu thành công");
//        onSearch();
//    }
//    else {
//        toastr.error("Xóa dữ liệu thất bại");
//    }

//}



//function fnDeleteUser(id) {
//    callApi_userservice(
//        apiConfig.api.user.controller,
//        apiConfig.api.user.action.delete.path + "?id=" + id,
//        apiConfig.api.user.action.delete.method,
//        null, 'fnDeleteUserSuccess', 'msgError');
//}
function submitCreate() {
    var obj = {
        'email_address': $('#emailAddressCreate').val().trim(),
        'cc': $('#CCCreate').val().trim(),
    }
    if (validateRequired('#formCreate')) {
        callApi_userservice(
            apiConfig.api.sendmail.controller,
            apiConfig.api.sendmail.action.addEmail.path,
            apiConfig.api.sendmail.action.addEmail.method,
            obj, 'createEmailSuccess', 'msgError');
    }
}

//function submitEdit() {
//    var obj = {
//        'Id': parseInt($('#IdEdit').val()),
//        'IsActive': $('#isActiveEdit').val(),
//        'FullName': $('#NameEdit').val().trim(),
//        'Email': $('#emailEdit').val() != "" ? $('#emailEdit').val().trim() : "",
//    }
//    if (validateRequired('#formEdit')) {
//        callApi_userservice(
//            apiConfig.api.user.controller,
//            apiConfig.api.user.action.update.path,
//            apiConfig.api.user.action.update.method,
//            obj, 'updateUserSuccess', 'msgError');
//    }
//}
function fnGetDetail(type, param) {
    var call_back = '';
    if (type === 3) {
        call_back = 'fnEditSuccess';
    }
    else {
        call_back = 'fnDeleteSuccess';
    }
    localStorage.removeItem("id");
    localStorage.removeItem("type");
    callApi_userservice(
        apiConfig.api.sendmail.controller,
        apiConfig.api.sendmail.action.getItem.path + "?id=" + param,
        apiConfig.api.sendmail.action.getItem.method,
        null, call_back, 'msgError');
}

//function fnGetDetailSuccess(rspn) {
//    var frmModify = $("#formDetail");
//    if (rspn !== undefined && rspn !== null) {

//        frmModify.find("#IdDetail").val(rspn.data.id);
//        frmModify.find("#userNameDetail").val(rspn.data.userName);

//        frmModify.find("#NameDetail").val(rspn.data.fullName);
//        frmModify.find("#isActiveDetail").val(rspn.data.isActive);

//        frmModify.find("#roleDetail").val(rspn.data.roleId);
//        frmModify.find("#emailDetail").val(rspn.data.email);
//    }
//}
function fnEditSuccess(rspn) {
    localStorage.removeItem("id");
    localStorage.removeItem("type");
    var frmModify = $("#formEdit");

    if (rspn !== undefined && rspn !== null) {
        frmModify.find("#IdEdit").val(rspn.data.id);
        frmModify.find("#emailAddressEdit").val(rspn.data.userName);
        frmModify.find("#CCEdit").val(rspn.data.roleId);
    }
}
function downloadSample() {
    showLoading();
    var obj = {
        'email_address': $('#EmailAddress').val().trim(),
        'page_size': parseInt($("#cbPageSize").val()),
        'start_number': (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
    };
    var jsonData = JSON.stringify(obj);
    var request = new XMLHttpRequest();
    request.responseType = "blob";
    request.open("GET", apiConfig.api.host_user_service + apiConfig.api.sendmail.controller +
        apiConfig.api.sendmail.action.exportexcel.path + "?jsonData=" + jsonData);
    request.setRequestHeader('Authorization', getSessionToken());
    request.setRequestHeader('Accept-Language', 'vi-VN');
    request.onload = function () {
        hideLoading();
        if (this.status == 200) {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(this.response);
            link.download = "Danh sách email.xlsx";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
        if (this.status == 404) {
            toastr.error("Không tìm thấy dữ liệu!", "Lỗi!", { progressBar: true });
        }
        if (this.status == 400) {
            toastr.error("Có lỗi xảy ra!", "Lỗi!", { progressBar: true });
        }
    }
    request.send();
}
