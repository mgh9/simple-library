altairApp.filter('ellipsed10chars', function ($filter, $sce, $sanitize) {
    return function (value, size) {
        var result = "<span class=\"ellipsed-10-chars\" title=\"".concat(_.escape(value), "\">").concat(_.escape(value), "</span>");
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('ellipsed20chars', function ($filter, $sce, $sanitize) {
    return function (value, size) {
        var result = "<span class=\"ellipsed-20-chars\" title=\"".concat(_.escape(value), "\">").concat(_.escape(value), "</span>");
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('ellipsed30chars', function ($filter, $sce, $sanitize) {
    return function (value, size) {
        var result = "<span class=\"ellipsed-30-chars\" title=\"".concat(_.escape(value), "\">").concat(_.escape(value), "</span>");
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('ellipsed40chars', function ($filter, $sce, $sanitize) {
    return function (value, size) {
        var result = "<span class=\"ellipsed-40-chars\" title=\"".concat(_.escape(value), "\">").concat(_.escape(value), "</span>");
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('nullToNoTitleString', function ($sanitize) {
    return function (value) {
        return (value || "بدون عنوان");
    };
});
altairApp.filter('toPersianDate', function ($filter) {
    return function (value) {
        if (value == undefined)
            return "";
        return $filter('persianDate')(value, 'yyyy/MM/dd');
    };
});
altairApp.filter('toPersianDateTime', function ($filter) {
    return function (value) {
        if (value == undefined)
            return "";
        return $filter('persianDate')(value, 'yyyy/MM/dd HH:mm');
    };
});
altairApp.filter('toPersianDateTimeSeconds', function ($filter) {
    return function (value) {
        if (value == undefined)
            return "";
        return $filter('persianDate')(value, 'yyyy/MM/dd HH:mm:ss');
    };
});
altairApp.filter('toFarsiDigits', function ($filter) {
    return function (value) {
        if (value == undefined)
            return "";
        value = value.replace(/0/g, '۰');
        value = value.replace(/1/g, '۱');
        value = value.replace(/2/g, '۲');
        value = value.replace(/3/g, '۳');
        value = value.replace(/4/g, '۴');
        value = value.replace(/5/g, '۵');
        value = value.replace(/6/g, '۶');
        value = value.replace(/7/g, '۷');
        value = value.replace(/8/g, '۸');
        value = value.replace(/9/g, '۹');
        return value;
    };
});
altairApp.filter('gridFilter', function ($filter, $sce, $sanitize) {
    return function (value, filterName, row) {
        if (!filterName) {
            value = _.escape(value);
            return value;
        }
        var result = undefined;
        var isEnumString = (filterName.indexOf('toEnumString') == 0);
        if (isEnumString) {
            var enumName = filterName.split(',')[1], enumNames = window["".concat(enumName, "TitleValues")];
            var enumItem = enumNames.filter(function (item) {
                return item.value == value;
            });
            result = (enumItem && enumItem[0].title) || '[Unknown]';
        }
        else {
            switch (filterName) {
                case 'dateTime':
                    result = new Date(value).toLocaleString();
                    break;
                case 'persianDate':
                    result = $filter(filterName)(value, 'yyyy/MM/dd');
                    break;
                case 'persianDateTime':
                    value = value || '';
                    result = "<span class=\"ltr uk-display-inline-block md-color-grey-500\">".concat($filter('persianDate')(value, 'yyyy/MM/dd HH:mm'), "</span>");
                    result = $sce.trustAsHtml(result);
                    break;
                case 'persianDateTimeSeconds':
                    value = value || '';
                    result = "<span class=\"ltr uk-display-inline-block md-color-grey-500\">".concat($filter('persianDate')(value, 'yyyy/MM/dd HH:mm:ss'), "</span>");
                    result = $sce.trustAsHtml(result);
                    break;
                default:
                    if (filterName.indexOf('|') != -1) {
                        var filterNames = filterName.split('|');
                        result = value;
                        for (var _i = 0, filterNames_1 = filterNames; _i < filterNames_1.length; _i++) {
                            var filter = filterNames_1[_i];
                            result = $filter(filter)(result, row);
                        }
                    }
                    else
                        result = $filter(filterName)(value, row);
                    break;
            }
        }
        return result;
    };
});
altairApp.filter('escapeHtml', function () {
    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': '&quot;',
        "'": '&#39;',
        "/": '&#x2F;'
    };
    return function (str) {
        return String(str).replace(/[&<>"'\/]/g, function (s) {
            var sss = entityMap[s];
            return sss;
        });
    };
});
altairApp.filter('toYesNo', function ($filter) {
    return function (value) {
        if (value == true)
            return 'Yes';
        else if (value == false)
            return 'No';
        else
            return 'Unknown';
    };
});
altairApp.filter('toPrice', function ($filter) {
    return function (value) {
        return (value || '').toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    };
});
altairApp.filter('toActiveDeactive', function ($filter, $sce) {
    return function (value) {
        var result = '';
        if (value == true)
            result = '<span class="uk-badge uk-badge-success">Active</span>';
        else if (value == false)
            return '<span class="uk-badge uk-badge-danger">Deactive</span>';
        else
            return '<span class="uk-badge uk-badge-warning">Unknown</span>';
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('toYesNo', function ($filter, $sce) {
    return function (value) {
        var result = '';
        if (value == true)
            result = '<span class="uk-badge uk-badge-success">Yes</span>';
        else if (value == false)
            return '<span class="uk-badge uk-badge-danger">No</span>';
        else
            return '<span class="uk-badge uk-badge-warning">Unknown</span>';
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('toUserLockStatus', function ($filter, $sce) {
    return function (value) {
        var result = '';
        if (value)
            result = "<span class=\"uk-badge uk-badge-danger\">Deactive</span>";
        else
            result = "<span class=\"uk-badge uk-badge-success\">Active</span>";
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('toAcceptedString', function ($filter, $sce) {
    return function (value) {
        var result = '';
        if (value == true)
            result = '<span class="uk-badge uk-badge-success">Accepted</span>';
        else if (value == false)
            return '<span class="uk-badge uk-badge-danger">Rejected</span>';
        else
            return '<span class="uk-badge uk-badge-warning">Pending</span>';
        return $sce.trustAsHtml(result);
    };
});
altairApp.filter('persianDate', ['$locale', 'PersianDateService', function ($locale, PersianDateService) {
        function int(str) {
            return parseInt(str, 10);
        }
        var R_ISO8601_STR = /^(\d{4})-?(\d\d)-?(\d\d)(?:T(\d\d)(?::?(\d\d)(?::?(\d\d)(?:\.(\d+))?)?)?(Z|([+-])(\d\d):?(\d\d))?)?$/;
        function padNumber(num, digits, trim) {
            var neg = '';
            if (num < 0) {
                neg = '-';
                num = -num;
            }
            num = '' + num;
            while (num.length < digits)
                num = '0' + num;
            if (trim)
                num = num.substr(num.length - digits);
            return neg + num;
        }
        function dateGetter(name, size, offset, trim) {
            offset = offset || 0;
            return function (date) {
                var value = PersianDateService['get' + name](date);
                if (offset > 0 || value > -offset)
                    value += offset;
                if (value === 0 && offset == -12)
                    value = 12;
                return padNumber(value, size, trim);
            };
        }
        function dateStrGetter(name, shortForm) {
            return function (date, formats) {
                var value = PersianDateService['get' + name](date);
                var get = angular.uppercase(shortForm ? ('SHORT' + name) : name);
                return formats[get][value];
            };
        }
        function timeZoneGetter(date) {
            var zone = -1 * date.getTimezoneOffset();
            var paddedZone = (zone >= 0) ? "+" : "";
            paddedZone += padNumber(Math[zone > 0 ? 'floor' : 'ceil'](zone / 60), 2) +
                padNumber(Math.abs(zone % 60), 2);
            return paddedZone;
        }
        function ampmGetter(date, formats) {
            return date.getHours() < 12 ? formats.AMPMS[0] : formats.AMPMS[1];
        }
        function concat(array1, array2, index) {
            var slice = [].slice;
            return array1.concat(slice.call(array2, index));
        }
        var DATE_FORMATS = {
            yyyy: dateGetter('FullYear', 4),
            yy: dateGetter('FullYear', 2, 0, true),
            y: dateGetter('FullYear', 1),
            MMMM: dateStrGetter('Month'),
            MMM: dateStrGetter('Month', true),
            MM: dateGetter('Month', 2, 1),
            M: dateGetter('Month', 1, 1),
            dd: dateGetter('Date', 2),
            d: dateGetter('Date', 1),
            HH: dateGetter('Hours', 2),
            H: dateGetter('Hours', 1),
            hh: dateGetter('Hours', 2, -12),
            h: dateGetter('Hours', 1, -12),
            mm: dateGetter('Minutes', 2),
            m: dateGetter('Minutes', 1),
            ss: dateGetter('Seconds', 2),
            s: dateGetter('Seconds', 1),
            sss: dateGetter('Milliseconds', 3),
            EEEE: dateStrGetter('Day'),
            EEE: dateStrGetter('Day', true),
            a: ampmGetter,
            Z: timeZoneGetter
        };
        var DATE_FORMATS_SPLIT = /((?:[^yMdHhmsaZE']+)|(?:'(?:[^']|'')*')|(?:E+|y+|M+|d+|H+|h+|m+|s+|a|Z))(.*)/, NUMBER_STRING = /^\-?\d+$/;
        function jsonStringToDate(string) {
            var match;
            if (match = string.match(R_ISO8601_STR)) {
                var date = new Date(0), tzHour = 0, tzMin = 0, dateSetter = match[8] ? date.setUTCFullYear : date.setFullYear, timeSetter = match[8] ? date.setUTCHours : date.setHours;
                if (match[9]) {
                    tzHour = int(match[9] + match[10]);
                    tzMin = int(match[9] + match[11]);
                }
                dateSetter.call(date, int(match[1]), int(match[2]) - 1, int(match[3]));
                var h = int(match[4] || 0) - tzHour;
                var m = int(match[5] || 0) - tzMin;
                var s = int(match[6] || 0);
                var ms = Math.round(parseFloat('0.' + (match[7] || 0)) * 1000);
                timeSetter.call(date, h, m, s, ms);
                return date;
            }
            return string;
        }
        return function (date, format) {
            var text = '', parts = [], fn, match;
            format = format || 'mediumDate';
            format = $locale.DATETIME_FORMATS[format] || format;
            if (angular.isString(date)) {
                if (NUMBER_STRING.test(date)) {
                    date = int(date);
                }
                else {
                    date = jsonStringToDate(date);
                }
            }
            if (angular.isNumber(date)) {
                date = new Date(date);
            }
            if (!angular.isDate(date)) {
                return date;
            }
            while (format) {
                match = DATE_FORMATS_SPLIT.exec(format);
                if (match) {
                    parts = concat(parts, match, 1);
                    format = parts.pop();
                }
                else {
                    parts.push(format);
                    format = null;
                }
            }
            angular.forEach(parts, function (value) {
                fn = DATE_FORMATS[value];
                text += fn ? fn(date, {
                    MONTH: 'فروردین,اردیبهشت,خرداد,تیر,مرداد,شهریور,مهر,آبان,آذر,دی,بهمن,اسفند'
                        .split(','),
                    SHORTMONTH: 'Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec'.split(','),
                    DAY: 'یک شنبه,دوشنبه,سه شنبه,چهارشنبه,پنج شنبه,جمعه,شنبه'.split(','),
                    SHORTDAY: 'ی,د,س,چ,پ,ج,ش'.split(','),
                    AMPMS: ['AM', 'PM'],
                    medium: 'MMM d, y h:mm:ss a',
                    short: 'M/d/yy h:mm a',
                    fullDate: 'EEEE, MMMM d, y',
                    longDate: 'MMMM d, y',
                    mediumDate: 'MMM d, y',
                    shortDate: 'M/d/yy',
                    mediumTime: 'h:mm:ss a',
                    shortTime: 'h:mm a'
                }) : value.replace(/(^'|'$)/g, '').replace(/''/g, "'");
            });
            return text;
        };
    }]).service('PersianDateService', function () {
    if (typeof fdef !== 'function') {
        var fdef = function (f) {
            return (typeof f === 'function');
        };
    }
    this.isPersianDate = function (input) {
        var parts, tmp;
        if (input.search(/^\d+\-\d+\-\d+$/) == 0) {
            parts = input.split("-", 3);
        }
        else if (input.search(/^\d+\/\d+\/\d+$/) == 0) {
            parts = input.split('/', 3);
        }
        else if (input.search(/^\d{6}|\d{8}$/) == 0) {
            parts = [
                input.substr(0, input.length - 4),
                input.substr(input.length - 4, 2),
                input.substr(input.length - 2, 2)
            ];
        }
        else if ((parts = input.match(/^(\d{2})(\d{2})$/)) ||
            (parts = input.match(/^(\d{1,2})[/\-](\d{1,2})$/)) ||
            (parts = input.match(/^(\d{1,2})$/))) {
            var curPDate = new Date();
            curPDate = gregorian_to_jd(curPDate.getFullYear(), curPDate.getMonth() + 1, curPDate.getDate());
            curPDate = jd_to_persian(curPDate);
            parts[0] = curPDate[0];
            if (typeof parts[2] == "undefined")
                parts[2] = curPDate[1];
            if (parseInt(parts[2], 10) <= 12) {
                tmp = parts[1];
                parts[1] = parts[2];
                parts[2] = tmp;
            }
        }
        else {
            return false;
        }
        for (var i = 0; i <= 2; ++i)
            parts[i] = parseInt(parts[i], 10);
        if (parts[1] > 12 || parts[1] <= 0)
            return false;
        if (parts[2] > persianMonthDays(longYear(parts[0]), parts[1] - 1)) {
            tmp = parts[2];
            parts[2] = parts[0];
            parts[0] = tmp;
        }
        if (parts[0] > 9999)
            return false;
        if (parts[0] < 100)
            parts[0] = longYear(parts[0]);
        if (parts[2] === 0 || parts[2] > persianMonthDays(parts[0], parts[1] - 1))
            return false;
        return parts;
    };
    var longYear = function (yr) {
        var curPDate, curCentury;
        if (yr >= 100)
            return yr;
        curPDate = new Date();
        curPDate = gregorian_to_jd(curPDate.getFullYear(), curPDate.getMonth() + 1, curPDate.getDate());
        curPDate = jd_to_persian(curPDate);
        curCentury = Math.floor(curPDate[0] / 100) * 100;
        if (Math.abs(curPDate[0] - curCentury - yr) > 70)
            curCentury = curCentury + 100;
        return curCentury + yr;
    };
    var GREGORIAN_EPOCH = 1721425.5, PERSIAN_EPOCH = 1948320.5;
    if (!fdef(mod)) {
        var mod = function (a, b) {
            return a - (b * Math.floor(a / b));
        };
    }
    ;
    var leap_persian = function (year) {
        return ((((((year - ((year > 0) ? 474 : 473)) % 2820) + 474) + 38) * 682) % 2816) < 682;
    };
    var jd_to_persian = function (jd) {
        var year, month, day, depoch, cycle, cyear, ycycle, aux1, aux2, yday;
        jd = Math.floor(jd) + 0.5;
        depoch = jd - persian_to_jd(475, 1, 1);
        cycle = Math.floor(depoch / 1029983);
        cyear = mod(depoch, 1029983);
        if (cyear == 1029982) {
            ycycle = 2820;
        }
        else {
            aux1 = Math.floor(cyear / 366);
            aux2 = mod(cyear, 366);
            ycycle = Math.floor(((2134 * aux1) + (2816 * aux2) + 2815) / 1028522) + aux1 + 1;
        }
        year = ycycle + (2820 * cycle) + 474;
        if (year <= 0) {
            year--;
        }
        yday = (jd - persian_to_jd(year, 1, 1)) + 1;
        month = (yday <= 186) ? Math.ceil(yday / 31) : Math.ceil((yday - 6) / 30);
        day = (jd - persian_to_jd(year, month, 1)) + 1;
        return new Array(year, month, day);
    };
    var persian_to_jd = function (year, month, day) {
        var epbase, epyear;
        epbase = year - ((year >= 0) ? 474 : 473);
        epyear = 474 + mod(epbase, 2820);
        return day + ((month <= 7) ? ((month - 1) * 31) : (((month - 1) * 30) + 6)) + Math.floor(((epyear * 682) - 110) / 2816) + (epyear - 1) * 365 + Math.floor(epbase / 2820) * 1029983 + (PERSIAN_EPOCH - 1);
    };
    var leap_gregorian = function (year) {
        return ((year % 4) == 0) && (!(((year % 100) == 0) && ((year % 400) != 0)));
    };
    var jd_to_gregorian = function (jd) {
        var wjd, depoch, quadricent, dqc, cent, dcent, quad, dquad, yindex, year, month, day, yearday, leapadj;
        wjd = Math.floor(jd - 0.5) + 0.5;
        depoch = wjd - GREGORIAN_EPOCH;
        quadricent = Math.floor(depoch / 146097);
        dqc = mod(depoch, 146097);
        cent = Math.floor(dqc / 36524);
        dcent = mod(dqc, 36524);
        quad = Math.floor(dcent / 1461);
        dquad = mod(dcent, 1461);
        yindex = Math.floor(dquad / 365);
        year = (quadricent * 400) + (cent * 100) + (quad * 4) + yindex;
        if (!((cent == 4) || (yindex == 4))) {
            year++;
        }
        yearday = wjd - gregorian_to_jd(year, 1, 1);
        leapadj = ((wjd < gregorian_to_jd(year, 3, 1)) ? 0 : (leap_gregorian(year) ? 1 : 2));
        month = Math.floor((((yearday + leapadj) * 12) + 373) / 367);
        day = (wjd - gregorian_to_jd(year, month, 1)) + 1;
        return new Array(year, month, day);
    };
    var gregorian_to_jd = function (year, month, day) {
        return (GREGORIAN_EPOCH - 1) + (365 * (year - 1)) + Math.floor((year - 1) / 4) + (-Math.floor((year - 1) / 100)) + Math.floor((year - 1) / 400) + Math.floor((((367 * month) - 362) / 12) + ((month <= 2) ? 0 : (leap_gregorian(year) ? -1 : -2)) + day);
    };
    var gregorian_to_persian = function (year, month, day) {
        return jd_to_persian(gregorian_to_jd(year, month, day));
    };
    this.getFullYear = function (date) {
        var persianDate = this.gregorianDate_to_persianDateArray(date);
        return persianDate[0];
    };
    this.getMonth = function (date) {
        var persianDate = this.gregorianDate_to_persianDateArray(date);
        return persianDate[1];
    };
    this.getDay = function (date) {
        return date.getDay();
    };
    this.getHours = function (date) {
        return date.getHours();
    };
    this.getMinutes = function (date) {
        return date.getMinutes();
    };
    this.getSeconds = function (date) {
        return date.getSeconds();
    };
    this.getDate = function (date) {
        var persianDate = this.gregorianDate_to_persianDateArray(date);
        return persianDate[2];
    };
    this.gregorianDate_to_persianDateArray = function (date) {
        var persianDate = gregorian_to_persian(date.getFullYear(), date.getMonth() + 1, date.getDate());
        --persianDate[1];
        return persianDate;
    };
    var persian_to_gregorian = function (year, month, day) {
        return jd_to_gregorian(persian_to_jd(year, month, day));
    };
    this.persian_to_gregorian_Date = function (year, month, day) {
        month = month + 1;
        if (month > 12) {
            year += Math.floor(month / 12);
            month = month % 12 || 12;
        }
        else if (month < 1 && month > -12) {
            if (month === 0) {
                year -= 1;
            }
            else {
                year += Math.floor((month / 12));
            }
            month += 12;
        }
        var greg = jd_to_gregorian(persian_to_jd(year, month, day));
        return new Date(greg[0], greg[1] - 1, greg[2]);
    };
    function persianMonthDays(year, month) {
        return (month <= 5 ? 31 : (month <= 10 ? 30 : (month == 11 ? (leap_persian(year) ? 30 : 29) : 0)));
    }
    this.persianMonthDays = persianMonthDays;
    if (!fdef(stopPropagation)) {
        var stopPropagation = function (evt) {
            if (evt.stopPropagation) {
                evt.stopPropagation();
            }
            else {
                evt.cancelBubble = true;
            }
        };
    }
    ;
    if (!fdef(preventDefault)) {
        var preventDefault = function (evt) {
            if (evt.preventDefault) {
                evt.preventDefault();
            }
            else {
                evt.returnValue = false;
            }
        };
    }
    ;
    if (!fdef(faDigitsToEn)) {
        var faDigitsToEn = function (input) {
            var inputLength = input.length, strOutput = '', i = 0;
            for (i = 0; i < inputLength; ++i) {
                strOutput += String.fromCharCode(input.charCodeAt(i) >= 1776 && input.charCodeAt(i) <= 1785 ? input.charCodeAt(i) - 1728 : input.charCodeAt(i));
            }
            return strOutput;
        };
    }
    ;
    if (!fdef(enDigitsToFa)) {
        var enDigitsToFa = function (input) {
            input = input.toString();
            var inputLength = input.length, strOutput = '', i = 0;
            for (i = 0; i < inputLength; ++i) {
                strOutput += String.fromCharCode(input.charCodeAt(i) >= 48 && input.charCodeAt(i) <= 57 ? input.charCodeAt(i) + 1728 : input.charCodeAt(i));
            }
            return strOutput;
        };
    }
    ;
    if (!('trim' in String.prototype)) {
        String.prototype.trim = function () {
            return this.replace(/^\s*/, '').replace(/\s*$/, '');
        };
    }
    ;
    if (!('map' in Array.prototype)) {
        Array.prototype.map = function (mapper, that) {
            var other = new Array(this.length);
            for (var i = 0, n = this.length; i < n; i++)
                if (i in this)
                    other[i] = mapper.call(that, this[i], i, this);
            return other;
        };
    }
    ;
});
//# sourceMappingURL=filters.js.map