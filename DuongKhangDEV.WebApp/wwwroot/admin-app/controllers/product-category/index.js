var ProductCategoryController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                txtOrderM: { number: true },
                txtHomeOrderM: { number: true }
            }
        });

        $('#btn-create').off('click').on('click', function () {
            initTreeDropDownCategory();
            $('#modal-add-edit').modal('show');
        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImage').val(path);
                    tedu.notify('Tải ảnh lên thành công!', 'success');
                },
                error: function () {
                    tedu.notify('Có lỗi khi tải file!', 'error');
                }
            });
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteProductCategory(that);
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = parseInt($('#hidIdM').val());
                var name = $('#txtNameM').val();
                var parentId = $('#ddlCategoryIdM').combotree('getValue');
                var description = $('#txtDescM').val();
                var uniqueCode = $('#txtAliasM').val();

                var image = $('#txtImage').val();
                var order = parseInt($('#txtOrderM').val());
                var homeOrder = $('#txtHomeOrderM').val();

                var metaKeyword = $('#txtSeoKeywordM').val();
                var metaDescription = $('#txtSeoDescriptionM').val();
                var metaTitle = $('#txtSeoPageTitleM').val();
                var metaAlias = $('#txtSeoAliasM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategory/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Description: description,
                        UniqueCode : uniqueCode,
                        ParentId: parentId,
                        HomeOrder: homeOrder,
                        SortOrder: order,
                        HomeFlag: showHome,
                        ThumbnailImage: image,
                        Status: status,
                        MetaTitle: metaTitle,
                        MetaAlias: metaAlias,
                        MetaKeywords: metaKeyword,
                        MetaDescription: metaDescription
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Lưu dữ liệu thành công', 'success');
                        $('#modal-add-edit').modal('hide');

                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Đã có lỗi xảy ra', 'error');
                        tedu.stopLoading();
                    }
                });
            }
            return false;
        });
    }

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/ProductCategory/GetById",
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

                $('#txtDescM').val(data.Description);
                $('#txtAliasM').val(data.UniqueCode);
                $('#txtImage').val(data.ThumbnailImage);

                $('#txtSeoKeywordM').val(data.MetaKeywords);
                $('#txtSeoDescriptionM').val(data.MetaDescription);
                $('#txtSeoPageTitleM').val(data.MetaTitle);
                $('#txtSeoAliasM').val(data.MetaAlias);

                $('#ckStatusM').prop('checked', data.Status == 1);
                $('#ckShowHomeM').prop('checked', data.HomeFlag);
                $('#txtOrderM').val(data.SortOrder);
                $('#txtHomeOrderM').val(data.HomeOrder);

                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function deleteProductCategory(that){
        tedu.confirm('Bạn chắc chắn muốn xoá?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/ProductCategory/Delete",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify('Xoá thành công', 'success');
                    tedu.stopLoading();
                    loadData();
                },
                error: function (status) {
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

        $('#txtDescM').val('');
        $('#txtAliasM').val('');
        $('#txtOrderM').val('');
        $('#txtHomeOrderM').val('');
        $('#txtImageM').val('');

        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        $('#ckStatusM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);
    }

    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/ProductCategory/GetAllAsync",
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

    function loadData1() {
        $.ajax({
            url: '/Admin/ProductCategory/GetAll',
            dataType: 'json',
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
                var treeArr = tedu.unflattern(data);
                treeArr.sort(function (a, b) {
                    return a.sortOrder - b.sortOrder;
                });
                //var $tree = $('#treeProductCategory');

                $('#treeProductCategory').tree({
                    data: treeArr,
                    dnd: true,
                    onContextMenu: function (e, node) {
                        e.preventDefault();
                        // select the node
                        //$('#tt').tree('select', node.target);
                        $('#hidIdM').val(node.id);
                        // display context menu
                        $('#contextMenu').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        });
                    },
                    onDrop: function (target, source, point) {
                        console.log(target);
                        console.log(source);
                        console.log(point);
                        var targetNode = $(this).tree('getNode', target);
                        if (point === 'append') {
                            var children = [];
                            $.each(targetNode.children, function (i, item) {
                                children.push({
                                    key: item.id,
                                    value: i
                                });
                            });

                            //Update to database
                            $.ajax({
                                url: '/Admin/ProductCategory/UpdateParentId',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id,
                                    items: children
                                },
                                success: function (res) {
                                    loadData();
                                }
                            });
                        }
                        else if (point === 'top' || point === 'bottom') {
                            $.ajax({
                                url: '/Admin/ProductCategory/ReOrder',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id
                                },
                                success: function (res) {
                                    loadData();
                                }
                            });
                        }
                    }
                });
            }
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                status: -1,
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            url: '/admin/ProductCategory/GetAllPagingAsync',
            dataType: 'json',
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Description: item.Description,
                        ThumbnailImage: item.ThumbnailImage == null ? '<img src="/admin-side/img/avatars/1.jpg" width=25' : '<img src="' + item.ThumbnailImage + '" width=25 />',                       
                        DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                        Status: tedu.getStatus(item.Status)
                    });
                });

                $('#lbl-total-records').text(response.RowCount);
                if (render != '') {
                    $('#tbl-content').html(render);
                }

                wrapPaging(response.RowCount, function () {
                    loadData();
                }, isPageChanged);
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Không thể tải được dữ liệu', 'error');
            }
        });
    }

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

    function initTreeGrid(selectedId) {
        $.ajax({
            url: "/Admin/ProductCategory/GetAllAsync",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        idField: item.Id,
                        treeField: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = tedu.unflattern(data);
                $('#TreeGrid').treegrid({
                    data: arr
                });
                if (selectedId != undefined) {
                    //$('#TreeGrid').treegrid('setValue', selectedId);
                }
            }
        });
    }
}