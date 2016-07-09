/*
 * author: TrungNDT
 * method: Convert number to money format (10000 -> 10.000đ)
 * params: {Number} val: money value
 */
var toMoney = function (val, splitter, unit) {
    var s = splitter || '.';
    var u = unit || '';
    return accounting.formatMoney(val, "", 0, s) + u;
}

/*
 * author: TrungNDT
 * method: Convert money to number (10.000đ -> 10000)
 * params: {String} str: money string
 */
var moneyToNumber = function (str) {
    return parseInt(str.replace('đ', '').replace(/\./g, ''));
}

function ShowMessage(message, type, time) {
    var mapper = {
        '1': ['error', 'Lỗi'],
        '2': ['info', 'Thành công'],
        '3': ['success', 'Thông tin']
    }

    $.gritter.add({
        title: mapper[type][1],
        text: message,
        time: time || 00,
        class_name: 'gritter-' + mapper[type][0] //+ ' gritter-light'
    });
    //  $.gritter.removeAll();
}

jQuery.expr[":"].Contains = jQuery.expr.createPseudo(function (arg) {
    return function (elem) {
        return jQuery(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
    };
});

var provinces = [
  "An Giang",
  "Bà Rịa - Vũng Tàu",
  "Bắc Giang",
  "Bắc Kạn",
  "Bạc Liêu",
  "Bắc Ninh",
  "Bến Tre",
  "Bình Định",
  "Bình Dương",
  "Bình Phước",
  "Bình Thuận",
  "Cà Mau",
  "Cao Bằng",
  "Đắk Lắk",
  "Đắk Nông",
  "Điện Biên",
  "Đồng Nai",
  "Đồng Tháp",
  "Gia Lai",
  "Hà Giang",
  "Hà Nam",
  "Hà Tĩnh",
  "Hải Dương",
  "Hậu Giang",
  "Hòa Bình",
  "Hưng Yên",
  "Khánh Hòa",
  "Kiên Giang",
  "Kon Tum",
  "Lai Châu",
  "Lâm Đồng",
  "Lạng Sơn",
  "Lào Cai",
  "Long An",
  "Nam Định",
  "Nghệ An",
  "Ninh Bình",
  "Ninh Thuận",
  "Phú Thọ",
  "Quảng Bình",
  "Quảng Nam",
  "Quảng Ngãi",
  "Quảng Ninh",
  "Quảng Trị",
  "Sóc Trăng",
  "Sơn La",
  "Tây Ninh",
  "Thái Bình",
  "Thái Nguyên",
  "Thanh Hóa",
  "Thừa Thiên Huế",
  "Tiền Giang",
  "Trà Vinh",
  "Tuyên Quang",
  "Vĩnh Long",
  "Vĩnh Phúc",
  "Yên Bái",
  "Phú Yên",
  "Cần Thơ",
  "Đà Nẵng",
  "Hải Phòng",
  "Hà Nội",
  "Hồ Chí Minh",
]


jQuery.fn.datatablevpn = function (options) {
    if (options && options.aoColumnDefs) {
        for (var i = 0; i < options.aoColumnDefs.length; i++) {
            var item = options.aoColumnDefs[i];

            if (item.fnRender) {
                item.mRender = function (a, b, c) {
                    return this.fnRender({ aData: c });
                }.bind(item);
            }
        }
    }

    jQuery.fn.dataTable.call(this, options);
}

function RefreshTable(tableId) {
    var oTable = $('#' + tableId).dataTable();
    oTable._fnPageChange(0);
    oTable._fnAjaxUpdate();
}