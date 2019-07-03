

alertConfirm = {
    position: "center",
    icon: "fa fa-question-circle",
    show: function (title, message, position, icon) {
        if (position === undefined) { position = alertConfirm.position; }
        if (icon === undefined) { icon = alertConfirm.icon; }
        iziToast.question({
            color: '#ffffff',
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
    success: function (title, msg) {
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
    },
    custom: function (title, msg, position) {
        iziToast.show({
            id: 'sebastian',
            theme: 'dark',
            icon: '',
            title: title,
            displayMode: 2,
            message: msg,
            position: position || 'bottomCenter',
            transitionIn: 'flipInX',
            transitionOut: 'flipOutX',
            progressBarColor: 'rgb(0, 255, 184)',
            image: $("#_img_check_msg").data("url"),
            imageWidth: 90,
            layout: 2,
            overlay: true,
            overlayClose: true,
            onClosing: function () {
            },
            onClosed: function (instance, toast, closedBy) {
            },
            iconColor: 'rgb(0, 255, 184)'
        });
    }
}

function init_file_input() {


var fileInput = document.querySelector(".input-file"),
    button = document.querySelector(".input-file-trigger"),
    the_return = document.querySelector(".file-return");

// Show image
$("body").off("change", ".input-file").on("change",".input-file",function (e) {
    handleImage(e);
});

var canvas = document.getElementById('imageCanvas');
var ctx = canvas.getContext('2d');



button.addEventListener("keydown", function (event) {
    if (event.keyCode == 13 || event.keyCode == 32) {
        fileInput.focus();
    }
});
button.addEventListener("click", function (event) {
    fileInput.focus();
    //return false;
});
fileInput.addEventListener("change", function (event) {
    the_return.innerHTML = this.value.replace(/C:\\fakepath\\/i, '');
    canvas.style.display = "block";
    //button.innerHTML =  this.value.replace(/C:\\fakepath\\/i, '');
});


function handleImage(e) {
    var reader = new FileReader();
    reader.onload = function (event) {
        var img = new Image();
        img.onload = function () {
            canvas.width = img.width;
            canvas.height = img.height;
            ctx.drawImage(img, 0, 0);
        }
        img.src = event.target.result;
        canvas.height = img.height;
    }
    reader.readAsDataURL(e.target.files[0]);
}
}
