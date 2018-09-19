var productController = function () {
    //var quantityManagement = new QuantityManagement();
    //var imageManagement = new ImageManagement();
    //var wholePriceManagement = new WholePriceManagement();

    this.initialize = function () {
        $.when(initTreeDropDownCategorySearch()).then(function () {
            loadData();
        });
        //loadCategories();
        //loadData();
        registerEvents();
        registerControls();
        //quantityManagement.initialize();
        //imageManagement.initialize();
        //wholePriceManagement.initialize();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                ddlCategoryIdM: { required: true },
                txtPriceM: {
                    required: true,
                    number: true
                },
                txtOriginalPriceM: {
                    required: true,
                    number: true
                }
            }
        });

        //todo: binding events to controls
        $('#ddlShowPage').on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        // Nhấn nút Tìm kiếm
        $('#btn-search').on('click', function () {
            loadData();
        });

        // Nếu gõ Enter trên txtKeyword
        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
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
                    tedu.notify('Đã tải ảnh lên thành công!', 'success');
                },
                error: function () {
                    tedu.notify('Đã xảy ra lỗi khi tải file lên!', 'error');
                }
            });
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            //$.when(loadManufacturers(),
            //    loadVendors())
            //    .done(function () {
            //        loadDetails(that);
            //    });
            loadDetails(that);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteProduct(that);
        });

        $('#btnSave').on('click', function (e) {
            saveProduct(e);
        });

        $('#btn-import').on('click', function () {
            initTreeDropDownCategory();
            $('#modal-import-excel').modal('show');
        });

        $('#btnImportExcel').on('click', function () {
            var fileUpload = $("#fileInputExcel").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append("files", files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('categoryId', $('#ddlCategoryIdImportExcel').combotree('getValue'));
            $.ajax({
                url: '/Admin/Product/ImportExcel',
                type: 'POST',
                data: fileData,
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                success: function (data) {
                    $('#modal-import-excel').modal('hide');
                    loadData();
                }
            });
            return false;
        });

        $('#btn-export').on('click', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Product/ExportExcel",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    window.location.href = response;
                    tedu.stopLoading();
                },
                error: function () {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });
        });
    }

    function registerControls() {
        CKEDITOR.replace('txtContent', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
    }

    function saveProduct(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var categoryId = $('#ddlCategoryIdM').combotree('getValue');
            var description = $('#txtDescM').val();
            var unit = $('#txtUnitM').val();

            var price = $('#txtPriceM').val();
            var originalPrice = $('#txtOriginalPriceM').val();
            var promotionPrice = $('#txtPromotionPriceM').val();

            var image = $('#txtImage').val();

            var tags = $('#txtTagM').val();
            var metaKeyword = $('#txtMetakeywordM').val();
            var metaDescription = $('#txtMetaDescriptionM').val();
            var metaTitle = $('#txtSeoPageTitleM').val();
            var metaAlias = $('#txtSeoAliasM').val();

            var content = CKEDITOR.instances.txtContent.getData();
            var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
            var hot = $('#ckHotM').prop('checked');
            var showHome = $('#ckShowHomeM').prop('checked');
            var hotSales = $('#ckHotSalesM').prop('checked');
            var hotNew = $('#ckHotNewM').prop('checked');
            var alwaysOnTheHomePage = $('#ckAlwaysOnTheHomePageM').prop('checked');
            var alwaysOnTheMainMenu = $('#ckAlwaysOnTheMainMenuM').prop('checked');
            var allowAddedToCart = $('#ckAllowAddedToCartM').prop('checked');

            $.ajax({
                type: "POST",
                url: "/Admin/Product/SaveEntity",
                data: {
                    Id: id,
                    Name: name,
                    CategoryId: categoryId,
                    ThumbnailImage: image,
                    Price: price,
                    OriginalPrice: originalPrice,
                    PromotionPrice: promotionPrice,
                    Description: description,
                    Content: content,
                    HomeFlag: showHome,
                    HotFlag: hot,
                    HotSalesFlag: hotSales,
                    HotNewFlag: hotNew,
                    AlwaysOnTheHomePage: alwaysOnTheHomePage,
                    AlwaysOnTheMainMenu: alwaysOnTheMainMenu,
                    AllowAddedToCart: allowAddedToCart,    
                    Tags: tags,
                    Unit: unit,
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

                    // Mới thêm
                    $('#paginationUL').twbsPagination('destroy');
                },
                error: function () {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });
            return false;
        }
    }

    function deleteProduct(that) {
        tedu.confirm('Bạn có chắc chắn muốn xoá?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Product/Delete",
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

    function loadDetails(that) {        
        $.ajax({
            type: "GET",
            url: "/Admin/Product/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                initTreeDropDownCategory(data.CategoryId);

                $('#txtDescM').val(data.Description);
                $('#txtUnitM').val(data.Unit);

                $('#txtPriceM').val(data.Price);
                $('#txtOriginalPriceM').val(data.OriginalPrice);
                $('#txtPromotionPriceM').val(data.PromotionPrice);

                $('#txtImage').val(data.ThumbnailImage);

                $('#txtTagM').val(data.Tags);
                $('#txtMetakeywordM').val(data.MetaKeywords);
                $('#txtMetaDescriptionM').val(data.MetaDescription);
                $('#txtSeoPageTitleM').val(data.MetaTitle);
                $('#txtSeoAliasM').val(data.MetaAlias);

                CKEDITOR.instances.txtContent.setData(data.Content);
                $('#ckStatusM').prop('checked', data.Status == 1);
                $('#ckHotM').prop('checked', data.HotFlag);
                $('#ckShowHomeM').prop('checked', data.HomeFlag);
                $('#ckHotSalesM').prop('checked', data.HotSalesFlag);
                $('#ckHotNewM').prop('checked', data.HotNewFlag);
                $('#ckAlwaysOnTheHomePageM').prop('checked', data.AlwaysOnTheHomePage);
                $('#ckAlwaysOnTheMainMenuM').prop('checked', data.AlwaysOnTheMainMenu);
                $('#ckAllowAddedToCartM').prop('checked', data.AllowAddedToCart);

                $('#modal-add-edit').modal('show');
                tedu.stopLoading();
            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        initTreeDropDownCategory('');

        $('#txtDescM').val('');
        $('#txtUnitM').val('');

        $('#txtPriceM').val('0');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionPriceM').val('');

        $('#txtImageM').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        CKEDITOR.instances.txtContent.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);
        $('#ckHotSalesM').prop('checked', false);
        $('#ckHotNewM').prop('checked', false);
        $('#ckAlwaysOnTheHomePageM').prop('checked', false);
        $('#ckAlwaysOnTheMainMenuM').prop('checked', false);
        $('#ckAllowAddedToCartM').prop('checked', true);
    }

    function loadCategories() {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllCategories',
            dataType: 'json',
            success: function (response) {
                var render = "<option value=''>--Chọn nhóm--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlCategorySearch').html(render);
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Không thể tải dữ liệu nhóm sản phẩm', 'error');
            }
        });
    }

    function loadManufacturers(selectedId) {
        return $.ajax({
            type: "GET",
            url: "/admin/manufacturer/GetAll",
            dataType: "json",
            success: function (response) {
                var render = "<option value=''>--Chọn nhà sản xuất--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlManufacturerM').html(render);
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Không thể tải dữ liệu nhà sản xuất', 'error');
            }
        });
    }

    function loadVendors(selectedId) {
        return $.ajax({
            type: "GET",
            url: "/admin/vendor/GetAll",
            dataType: "json",
            success: function (response) {
                var render = "<option value=''>--Chọn đại lý--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlVendorM').html(render);
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Không thể tải dữ liệu đại lý', 'error');
            }
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                categoryId: $('#ddlCategoryIdSearch').combotree('getValue'),
                keyword: $('#txtKeyword').val(),
                status: -1,
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            url: '/admin/product/GetAllPagingAsync',
            dataType: 'json',
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        CategoryName: item.ProductCategory.Name,
                        ThumbnailImage: item.ThumbnailImage == null ? '<img src="/admin-side/img/avatars/1.jpg" width=25' : '<img src="' + item.ThumbnailImage + '" width=25 />',
                        OriginalPrice: tedu.formatNumber(item.OriginalPrice, 0),
                        Price: tedu.formatNumber(item.Price, 0),
                        PromotionPrice: tedu.formatNumber(item.PromotionPrice, 0),
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

    function initTreeDropDownCategorySearch(selectedId) {
        $.ajax({
            url: '/admin/product/GetAllCategories',
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
                $('#ddlCategoryIdSearch').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlCategoryIdSearch').combotree('setValue', selectedId);
                }
            }
        });
    }

    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: '/admin/product/GetAllCategories',
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

                $('#ddlCategoryIdImportExcel').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }
}