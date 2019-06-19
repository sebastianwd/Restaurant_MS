

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
