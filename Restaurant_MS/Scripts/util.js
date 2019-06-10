/*-Range Datepicker Custom Component -------------------------*/

function dateRangePickerComponent(min, max) {

    $(min).datepicker({
        changeMonth: true, changeYear: true, language: 'es', clearBtn: true,

    }).on('changeDate', function (selected) {
        if (selected.date != null) {
            var minDate = new Date(selected.date.valueOf());
            $(max).datepicker('setStartDate', minDate);
        }
    });

    $(max).datepicker({
        changeMonth: true, changeYear: true, language: 'es', clearBtn: true,
    }).on('changeDate', function (selected) {
        if (selected.date != null) {
            var maxDate = new Date(selected.date.valueOf());
            $(min).datepicker('setEndDate', maxDate);

        }
    });

    var start = moment().subtract(0, 'days');
    var end = moment().add(1, 'days');

    function cb(start, end) {
        $(min).datepicker('setEndDate', end.format('DD/MM/YYYY'));
        $(max).datepicker('setStartDate', start.format('DD/MM/YYYY'));

        $(max).datepicker("update", end.format('DD/MM/YYYY'));
        $(min).datepicker("update", start.format('DD/MM/YYYY'));

    }

    cb(start, end);

    $(max).change(function () {
        if ($(this).val() == '') {
            $(min).datepicker('setEndDate', '');

        }
    });

    $(min).change(function () {
        if ($(this).val() == '') {
            $(max).datepicker('setStartDate', '');

        }
    });

    $(max).datepicker('setStartDate', (new Date()).fromStringFullYear($(min).val()));
    $(min).datepicker('setEndDate', (new Date()).fromStringFullYear($(max).val()));
};


alertConfirm = {
    position: "center",
    icon: "fa fa-question-circle",
    show: function (title, message, position, icon) {
        if (position === undefined) { position = alertConfirm.position; }
        if (icon === undefined) { icon = alertConfirm.icon; }
        iziToast.question({
            color: 'dark',
            timeout: false,
            class: 'pv-confirmation',
            close: true,
            overlay: true,
            toastOnce: true,
            overlayClose: true,
            id: 'question',
            zindex: 7999,
            title: title,
            message: message,
            position: position,
            transitionIn: 'fadeIn',
            buttons: [
                [
                    '<button class="primary" >Aceptar</button>',
                    function (instance, toast) {
                        debugger;
                        alertConfirm.yes();
                        instance.hide({
                            transitionOut: 'fadeOutUp'
                        }, toast);
                    }
                ],
                [
                    '<button  class="secondary" >Cancelar</button>',
                    function (instance, toast) {
                        instance.hide({
                            transitionOut: 'fadeOutDown'
                        }, toast);
                        alertConfirm.no();
                    }
                ]
            ]
        });
    },
    yes: function () {


    },
    no: function () {
        return false;

    }

};

msg = {
    success: function (title,msg ) {
        iziToast.success({
            title: title,
            message: msg,
        });
    },
    error: function (title, msg) {
        iziToast.error({
            title: title,
            message: msg,
        });

    },
    warning: function (title, msg) {
          iziToast.warning({
            title: title,
            message: msg,
        });

    }
     
}
