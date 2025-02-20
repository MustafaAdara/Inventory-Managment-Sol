// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function ($) {
    $('.modal').on('show.bs.modal', function (event) {
        var idx = $('.modal:visible').length;
        $(this).css('z-index', 1050 + (10 * idx));
        $(this).css('overflow-y', 'auto');
    });
    $('.modal').on('shown.bs.modal', function (event) {
        var idx = ($('.modal:visible').length) - 1; // raise backdrop after animation.
        $('.modal-backdrop').not('.stacked').css('z-index', 1049 + (10 * idx));
        $('.modal-backdrop').not('.stacked').addClass('stacked');
    });

    //$('select').select2();



    $(".modal:not('.rowTable')").on("submit", "form", function (e) {
        e.preventDefault();
        var form = $(this);
        if (form[0].id === 'filter')
            return;
        if (form[0].id === 'LookUp')
            return;
        var btn = form.find(':submit');
        showLoadingBtn(btn);
        var modal = form.closest('.modal');
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize(),
            success: function (partialResult) {
                if (partialResult.indexOf('<html') !== -1) {
                    window.location.href = url;
                    return;
                }
                if (partialResult.match("^list")) {
                    initList('#' + partialResult);
                    $(modal).modal('hide');
                } else if (partialResult.match("^/")) {
                    //if (partialResult.indexOf('_') !== -1)
                    window.location.href = partialResult;
                    //else
                    //    $(modal).html(partialResult);
                }
                else {
                    var tabContent = $(modal).find(".tab-content");
                    if (tabContent.length > 0)
                        $(tabContent[0]).html(partialResult);
                    else
                        $(modal).html(partialResult);
                    if (typeof registerChngDep !== "undefined") {
                        registerChngDep();
                    }
                    var forms = $(modal).find("form");
                    if (forms.length > 0)
                        $.validator.unobtrusive.parse($(forms[0]));
                }
                hideLoading();
            }
        });
    });
    $(".modal.rowTable").on("submit", "form", function (e) {
        e.preventDefault();
        showLoading();
        var form = $(this);
        if (form[0].id === 'filter')
            return;
        if (form[0].id === 'LookUp')
            return;
        var modal = form.closest('.modal');
        var tableId = $(form.find('#PrntId')[0]).val();
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize(),
            success: function (partialResult) {
                if (partialResult.indexOf('<html') !== -1) {
                    window.location.href = url;
                    return;
                }
                $('#' + tableId).closest('table').children('tbody').append(partialResult);
                $(modal).modal('hide');
                hideLoading();
            }
        });
    });


    $('#filter').on('submit', FilterOnSubmit);


    $('#LookUp').on('submit', LookUpOnSubmit);
});
function LookUpOnSubmit(e) {
    e.preventDefault();
    var form = $(this);
    var lookup = form.closest('.modal');
    var result = JSON.parse(lookup.data('result'));
    $(lookup.data('resulttable')).html('<tbody></tbody>');
    result.forEach(function (value, i) {
        $.ajax({
            url: lookup.data('rowurl'),
            method: "GET",
            data: { serial: i, prnt: lookup.data('resultprnt'), id: value.i, text: value.t },
            success: function (partialResult) {
                if (partialResult.indexOf('<html') !== -1) {
                    window.location.href = url;
                    return;
                }
                $(lookup.data('resulttable')).append(partialResult);

                hideLoading();
            }
        });
    });
    $(lookup).modal('hide');
}

function FilterOnSubmit(e) {
    e.preventDefault();
    var form = $(this);
    var modal = form.closest('.modal');
    if (modal[0].id === "searchModal")
        $(modal).modal('hide');
    filterList(form.data('target'), form.serialize());
}

function filterList(listHolderId, filter) {
    var url = $(listHolderId).data('target');

    if (url.indexOf('?') !== -1)
        url = url.slice(0, url.indexOf('?'));
    url = url + '?' + filter;

    var selected = $(listHolderId).data('selected');
    if (selected != undefined)
        url = url + '&selected=' + selected;

    //if (url.indexOf('?') !== -1)
    //    url = url + '&' + filter;
    //else
    //    url = url + '?' + filter;

    loadList(listHolderId, url);
}
function initList(listHolderId) {
    loadList(listHolderId, $(listHolderId).data('target'));
}
function loadList(listHolderId, url) {
    var listHolder = $(listHolderId);
    var querystring = '';
    if (window.location.href.indexOf('?') !== -1)
        querystring = window.location.href.slice(window.location.href.indexOf('?') + 1);
    if (querystring !== '') {
        if (url.indexOf('?') !== -1)
            url = url + '&' + querystring;
        else
            url = url + '?' + querystring;
    }
    showLoadingIn(listHolder);
    $.ajax({
        url: url,
        method: "GET",
        success: function (partialResult) {
            if (partialResult.indexOf('<html') !== -1) {
                window.location.href = url;
                return;
            }
            listHolder.html(partialResult);
            listHolder.data('target', url);
            var forms = listHolder.find("form");
            if (forms.length > 0) {
                $.validator.unobtrusive.parse($(forms[0]));
            }

            if (typeof registerChngDep !== "undefined") {
                registerChngDep();
            }
            $('ul.pagination a').click(
                function (e) {
                    e.preventDefault();
                    if (e.target.href !== "")
                        loadList(listHolderId, e.target.href)
                });
            hideLoading();
        }
    });
}

//function getModalAction(e, url)
function getModalAction(e, url, target) {
    //forward querystring if exists
    var querystring = '';
    if (window.location.href.indexOf('?') !== -1)
        querystring = window.location.href.slice(window.location.href.indexOf('?') + 1);
    if (querystring !== '') {
        if (url.indexOf('?') !== -1)
            url = url + '&' + querystring;
        else
            url = url + '?' + querystring;
    }

    e.preventDefault();

    // convert target (e.g. the button) to jquery object
    var $target = (target === undefined) ? $(e.target) : $(target);
    // modal targeted by the button
    var modalSelector;
    if ($target.data('target') !== undefined)
        modalSelector = $target.data('target');
    else
        modalSelector = $target.parent().data('target');

    showLoading();
    $.ajax({
        url: url,
        method: "GET",
        success: function (partialResult) {
            if (partialResult.indexOf('<html') !== -1) {
                window.location.href = url;
                return;
            }
            $(modalSelector).html(partialResult);
            var forms = $(modalSelector).find("form");
            if (forms.length > 0) {
                $.validator.unobtrusive.parse($(forms[0]));
            }

            if (typeof registerChngDep !== "undefined") {
                registerChngDep();
            }
            $(modalSelector).modal('show');
            hideLoading();
        }
    });
}

function postModalForm(e) {
    var modal = $(e.target).closest('.modal');
    var form = $(modal).find('form')[0];
    $(form).submit();
    return false;
}

function attachActionRowToTable(e, url, prntKey) {
    e.preventDefault();
    var lastvalue = 1 + parseInt($(e.target).closest('table').children('tbody').children('tr:last').children('td:first').text());
    lastvalue = lastvalue || 1;
    lastvalue = lastvalue - 1;
    showLoading();
    $.ajax({
        url: url,
        method: "GET",
        data: { serial: lastvalue, prnt: $(e.target).closest('table')[0].id, prntKey: prntKey },
        success: function (partialResult) {
            if (partialResult.indexOf('<html') !== -1) {
                window.location.href = url;
                return;
            }
            $(e.target).closest('table').children('tbody').append(partialResult);

            var $currForm = $(e.target).closest("form");
            $currForm.removeData("validator");
            $currForm.removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($currForm);
            $currForm.validate();

            hideLoading();
        }
    });
}
function attachActionRowPopUpToTable(e, url, prntKey) {
    e.preventDefault();
    // convert target (e.g. the button) to jquery object
    var $target = $(e.target);
    // modal targeted by the button
    var modalSelector;
    if ($target.data('target') !== undefined)
        modalSelector = $target.data('target');
    else
        modalSelector = $target.parent().data('target');
    var lastvalue = 1 + parseInt($(e.target).closest('table').children('tbody').children('tr:last').children('td:first').text());
    lastvalue = lastvalue || 1;
    lastvalue = lastvalue - 1;
    showLoading();
    $.ajax({
        url: url,
        method: "GET",
        data: { serial: lastvalue, prntId: $(e.target).closest('table')[0].id, prntKey: prntKey, mode: 2 },
        success: function (partialResult) {
            if (partialResult.indexOf('<html') !== -1) {
                window.location.href = url;
                return;
            }
            $(modalSelector).html(partialResult);
            var forms = $(modalSelector).find("form");
            if (forms.length > 0) {
                $.validator.unobtrusive.parse($(forms[0]));
            }

            if (typeof registerChngDep !== "undefined") {
                registerChngDep();
            }

            $(modalSelector).modal('show');
            hideLoading();
        }
    });
}


function selectLookUpRow(e) {
    e.preventDefault();
    var btn = $(e.target.parentNode)
    if (e.target.tagName === 'A')
        btn = $(e.target)
    var modal = btn.closest('.modal');
    if (modal.data('result') === undefined)
        modal.data('result', JSON.stringify([]));
    var result = JSON.parse(modal.data('result'));

    var isSelected = btn.data('selected');
    if (isSelected)//remove
    {
        var i = result.map(e => e.i).indexOf(btn.data('id'));
        result.splice(i, 1);
        btn.html("<i class='fa-2x far fa-circle'></i>");
    }
    else {
        result.push({ i: btn.data('id'), t: btn.data('text') });
        btn.html("<i class='fa-2x fas fa-circle'></i>");
    }
    modal.data('result', JSON.stringify(result))
    btn.data('selected', !isSelected);

    $(modal.data('listholder')).data('selected', JSON.stringify(result.map(e => e.i)));

    $('ul.pagination a').off('click');
    $('ul.pagination a').click(
        function (e) {
            e.preventDefault();
            if (e.target.href !== "") {
                url = e.target.href;
                url = url.slice(0, url.lastIndexOf('&'));
                loadList(modal.data('listholder'), url + '&selected=' + JSON.stringify(result.map(e => e.i)));
            }
        });
}
function deleteRow(e) {
    e.preventDefault();
    var table = $(e.target).closest('table');
    $(e.target).closest('tr').remove();
    renumberRows(table);

    var $currForm = $(e.target).closest("form");
    $currForm.removeData("validator");
    $currForm.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse($currForm);
    $currForm.validate();
}

function renumberRows(table) {
    var prntprefix = table[0].id;
    var prntprefixName = prntprefix.replace(/_/g, '.');
    $(table).children('tbody').children('tr').each(function (index) {
        var serial = index;
        var prevSerial = parseInt($(this).children('td:first').text()) - 1;
        $(this).children('td:not(:first):not(:last)').each(function (index) {
            $(this).find("[id]").each(function (index) {
                this.id = this.id.replace(prntprefix + '[' + prevSerial + ']', prntprefix + '[' + serial + ']');
            });
            $(this).find("[for]").each(function (index) {
                $(this).attr('for', $(this).attr('for').replace(prntprefix + '[' + prevSerial + ']', prntprefix + '[' + serial + ']'));
            });
            $(this).find("[aria-describedby]").each(function (index) {
                $(this).attr('aria-describedby', $(this).attr('aria-describedby').replace(prntprefix + '[' + prevSerial + ']', prntprefix + '[' + serial + ']'));
            });
            $(this).find("[name]").each(function (index) {
                this.name = this.name.replace(prntprefixName + '[' + prevSerial + ']', prntprefixName + '[' + serial + ']');
            });
            $(this).find("[data-valmsg-for]").each(function (index) {
                $(this).attr('data-valmsg-for', $(this).attr('data-valmsg-for').replace(prntprefixName + '[' + prevSerial + ']', prntprefixName + '[' + serial + ']'));
            });
            $(this).find("[data-target]").each(function (index) {
                $(this).attr('data-target', $(this).attr('data-target').replace(prntprefix + '[' + prevSerial + ']', prntprefix + '[' + serial + ']'));
            });
        });
        $(this).children('td:first').text(index + 1);
    });
}

function showLoading() {
}
function showLoadingIn(container) {
    container.html("<div class=\"text-center\"><div class=\"spinner-border text-primary me-2\" style=\"\" role=\"status\"><span class=\"sr-only\">Loading...</span></div></div>");
}
function showLoadingBtn(btn) {
    btn.html("<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"></span>");
    btn.prop('disabled', true);
}
function hideLoading() {
}
function hideLoadingBtn(btn) {
    btn.prop('disabled', false);
}
