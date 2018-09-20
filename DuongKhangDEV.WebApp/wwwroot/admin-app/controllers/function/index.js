var FunctionController = function () {
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
                txtNameM: { required: true },
                txtAliasM: { required: true }
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
            initTreeDropDownCategory();
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('#btnSaveM').on('click', function (e) {
            saveFunction(e);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteFunction(that);
        });
    };

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Function/GetByIdAsync",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                initTreeDropDownCategory(data.ParentId);
                $('#txtAliasM').val(data.URL);
                $('#txtIconCssM').val(data.IconCss);
                $('#txtParentM').val(data.ParentId);
                $('#txtOrderM').val(data.SortOrder);
                $('#ckStatusM').prop('checked', data.Status === 1);

                $('#modal-add-edit').modal('show');
                tedu.stopLoading();
            },
            error: function () {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function saveFunction(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var url = $('#txtAliasM').val();
            var parentId = $('#ddlCategoryIdM').combotree('getValue');
            var iconCss = $('#txtIconCssM').val();
            var sortOrder = $('#txtOrderM').val();
            var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;

            $.ajax({
                type: "POST",
                url: "/Admin/Function/SaveEntityAsync",
                data: {
                    Id: id,
                    Name: name,
                    URL: url,
                    Status: status,
                    ParentId: parentId,
                    IconCss: iconCss,
                    SortOrder: sortOrder
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
            return false;
        }
        return false;
    }

    function deleteFunction(that){
        tedu.confirm('Bạn chắc chắn muốn xoá?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Function/DeleteAsync",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function () {
                    tedu.notify('Xoá thành công !', 'success');
                    tedu.stopLoading();
                    loadData();
                },
                error: function () {
                    tedu.notify('Xoá không thành công', 'error');
                    tedu.stopLoading();
                }
            });
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        initTreeDropDownCategory('');
        $('#txtAliasM').val('');
        $('#ckStatusM').prop('checked', true);
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/Function/GetAllPagingAsync",
            data: {
                keyword: $('#txt-search-keyword').val(),
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
                            Name: item.Name,
                            URL: item.URL,
                            ParentId: item.ParentId,
                            IconCss: item.IconCss,
                            Status: tedu.getStatus(item.Status)
                        });
                    });

                    $("#lbl-total-records").text(response.RowCount);
                    if (render != undefined) {
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
                tedu.stopLoading();
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

    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/Function/GetAllAsync",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = tedu.unflattern(data);
                $('#ddlCategoryIdM').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }
}