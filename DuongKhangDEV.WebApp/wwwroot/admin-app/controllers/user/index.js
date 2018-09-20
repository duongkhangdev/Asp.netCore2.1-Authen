﻿var UserController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtFullName: { required: true },
                txtUserName: { required: true },
                txtPassword: {
                    required: true,
                    minlength: 6
                },
                txtConfirmPassword: {
                    equalTo: "#txtPassword"
                },
                txtEmail: {
                    required: true,
                    email: true
                }
            }
        });

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });

        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            initRoleList();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('#btnSave').on('click', function (e) {
            saveUser(e);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteUser(that);
        });
    };

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/User/GetByIdAsync",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidId').val(data.Id);
                $('#txtFullName').val(data.FullName);
                $('#txtUserName').val(data.UserName);
                $('#txtEmail').val(data.Email);
                $('#txtPhoneNumber').val(data.PhoneNumber);
                $('#txtAddress').val(data.Address);
                $('#txtCity').val(data.City);
                $('#ckStatus').prop('checked', data.Status === 1);

                initRoleList(data.Roles);

                disableFieldEdit(true);
                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function () {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function saveUser(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();

            var id = $('#hidId').val();
            var fullName = $('#txtFullName').val();
            var userName = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            var email = $('#txtEmail').val();
            var phoneNumber = $('#txtPhoneNumber').val();
            var address = $('#txtAddress').val();
            var city = $('#txtCity').val();
            var roles = [];
            $.each($('input[name="ckRoles"]'), function (i, item) {
                if ($(item).prop('checked') === true)
                    roles.push($(item).prop('value'));
            });
            var status = $('#ckStatus').prop('checked') == true ? 1 : 0;

            $.ajax({
                type: "POST",
                url: "/Admin/User/SaveEntityAsync",
                data: {
                    Id: id,
                    FullName: fullName,
                    UserName: userName,
                    Password: password,
                    Email: email,
                    PhoneNumber: phoneNumber,
                    Address: address,
                    City: city,
                    Status: status,
                    Roles: roles
                },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function () {
                    tedu.notify('Lưu dữ liệu thành công', 'success');
                    $('#modal-add-edit').modal('hide');
                    resetFormMaintainance();

                    tedu.stopLoading();
                    loadData(true);
                },
                error: function () {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });
        }
        return false;
    }

    function deleteUser(that) {
        tedu.confirm('Bạn có chắc chắn muốn xoá?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/DeleteAsync",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify('Xoá thành công', 'success');
                    tedu.stopLoading();
                    loadData();

                    // Mới thêm
                    $('#paginationUL').twbsPagination('destroy');
                },
                error: function (status) {
                    tedu.notify('Xoá không thành công', 'error');
                    tedu.stopLoading();
                }
            });
        });
    }

    function disableFieldEdit(disabled) {
        $('#txtUserName').prop('disabled', disabled);
        $('#txtPassword').prop('disabled', disabled);
        $('#txtConfirmPassword').prop('disabled', disabled);
    }

    function resetFormMaintainance() {
        disableFieldEdit(false);
        $('#hidId').val('');
        initRoleList();
        $('#txtFullName').val('');
        $('#txtUserName').val('');
        $('#txtPassword').val('');
        $('#txtConfirmPassword').val('');
        $('input[name="ckRoles"]').removeAttr('checked');
        $('#txtEmail').val('');
        $('#txtPhoneNumber').val('');
        $('#txtAddress').val('');
        $('#txtCity').val('');
        $('#ckStatus').prop('checked', true);
    }

    function initRoleList(selectedRoles) {
        $.ajax({
            url: "/Admin/Role/GetAllAsync",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var template = $('#role-template').html();
                var data = response;
                var render = '';
                $.each(data, function (i, item) {
                    var checked = '';
                    if (selectedRoles !== undefined && selectedRoles.indexOf(item.Name) !== -1)
                        checked = 'checked';
                    render += Mustache.render(template,
                        {
                            Name: item.Name,
                            Description: item.Description,
                            Checked: checked
                        });
                });
                $('#list-roles').html(render);
            }
        });
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/user/GetAllPagingAsync",
            data: {
                categoryId: '',
                keyword: $('#txtKeyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Id,
                            FullName: item.FullName,                            
                            UserName: item.UserName,
                            Avatar: item.Avatar == undefined ? '<img src="/admin-side/img/avatars/1.jpg" width=25 />' : '<img src="' + item.Avatar + '" width=25 />',
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            Status: tedu.getStatus(item.Status)
                        });
                    });

                    $("#lbl-total-records").text(response.RowCount);
                    if (render !== undefined) {
                        $('#tbl-content').html(render);

                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                }
                else {
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                if (tedu.configs.pageIndex != p) {
                    tedu.configs.pageIndex = p;
                    setTimeout(callBack(), 200);
                }
            }
        });
    }
}