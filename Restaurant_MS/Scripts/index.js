var data_Table_Language = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    select: {
        rows: {
            _: " | %d filas seleccionadas",
            0: "",
            1: ""
        }
    }
}

const button_save = '<span class="fa fa-save fa-lg cicon-save cfirst-dt-button"></span>';
const button_save_tooltip = 'Grabar Datos (shift + 2)';

var url_content;
var callback_modal;
function modal_ajax(open, url, callback, title) {
    if (!open) {
        $('#modal-search').iziModal('close');
        return false;
    }
    url_content = url;
    callback_modal = callback;
    $("#modal-search").iziModal({
        title: title || 'Búsqueda',
        subtitle: '',
        history: false,
        icon: 'fa fa-search',
        top: 10,
        // bottom: 50,
        // timeout: 5000,
        timeoutProgressbar: false,
        timeoutProgressbarColor: 'white',
        arrowKeys: true,
        width: 900,
        padding: 20,
        restoreDefaultContent: true,
        headerColor: '#2f353a',
        group: 'grupo1',
        loop: true,
        fullscreen: true,
        overlayClose: false,
        onResize: function (modal) {
        },
        afterRender: function (modal) {
        },
        onOpening: function (modal) {
            modal.startLoading();
            $.get(url_content, function (data) {
                $("#modal-search .iziModal-content").html(data);
                modal.stopLoading();
            });
        },
        onClosing: function () {
            callback_modal() || false;
        },
        /*    transitionIn: false,
            transitionOut: false,
            transitionInOverlay: false,
            transitionOutOverlay: false*/
    });

    $('#modal-search').iziModal('open');
}

RMS = {
    common: {
        init: function () {
            // application-wide code
            $('input.form-control').keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            setTimeout(() => {
                initFormatters();
            }, 200)

            function initFormatters() {
                $('._valid-number-l-11').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        blocks: [11],
                        numeral: true,
                        numeralThousandsGroupStyle: 'none',
                        numeralPositiveOnly: true
                    });
                });
                $('._valid-date').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        date: true,
                        datePattern: ['d', 'm', 'Y']
                    });
                });
                $('._valid-int-l-2').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        blocks: [2],
                        numericOnly: true,
                        numeralThousandsGroupStyle: 'none',
                        numeralDecimalScale: 0
                    });
                });
                $('._valid-int-l-3').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        blocks: [3],
                        numericOnly: true,
                        numeralThousandsGroupStyle: 'none',
                        numeralDecimalScale: 0
                    });
                });
                $('._valid-int-l-4').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        blocks: [4],
                        numericOnly: true,
                        numeralThousandsGroupStyle: 'none',
                        numeralDecimalScale: 0
                    });
                });
                $('._valid-string-l-3-u').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        uppercase: true,
                        blocks: [3]
                    });
                });
                $('._valid-string-l-10').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        blocks: [10]
                    });
                });
                $('._valid-string-l-4-u').toArray().forEach(function (campo) {
                    new Cleave(campo, {
                        uppercase: true,
                        blocks: [4]
                    });
                });
            }
            console.log("js comun para toda la app");
        }
    },
    Usuario: {
        init: function () {
            $('body').off("click", "#saveUserData").on('click', '#saveUserData', function () {
                $("#Frm_Datos_Usuario").submit();
            });
            init_file_input();
            $("body").off("submit", "#Frm_Datos_Usuario").on("submit", "#Frm_Datos_Usuario", function () {
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            if (res.response) {
                                msg.success("Aviso", `Datos actualizados`);

                                setTimeout(function () { location.reload(); }, 1500);
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                return false;
            });
        }
    },
    Home: {
        init: function () {
            $('body').off("click", "._fill_table").on('click', '._fill_table', function () {
                let url = $("#_gestion_mesa").data("request-url");
                const id = $(this).data("id");
                url = `${url}?FS_COD_MESA=${id}`;
                modal_ajax(true, url, load_mesa, "Ocupar mesa");
            });

            $('body').off("click", "._agregar_mesa").on('click', '._agregar_mesa', function () {
                let url = $("#_registrar_mesa").data("request-url");
                modal_ajax(true, url, load_mesa, "Registrar mesa");
            });

            $('body').off("click", "._delete_table").on('click', '._delete_table', function () {
                var prm = {
                    FS_COD_MESA: $(this).data("id"),
                }
                $.post($("#_eliminar_mesa").data("request-url"), prm,
                    function (res, textStatus, jqXHR) {
                        if (res.response) {
                            msg.success("Aviso", `Mesa ${res.result} eliminada`);
                            $.get($("#_lista_mesas").data("request-url"),
                                function (data, textStatus, jqXHR) {
                                    $("#lista_mesas").html(data);
                                },
                                "html"
                            );
                        }
                        else {
                            msg.error("Aviso", res.error);
                        }
                        return false;
                    },
                    "json"
                );
            });

            $('body').off("click", "._open_table").on('click', '._open_table', function () {
                var prm = {
                    FS_COD_MESA: $(this).data("id"),
                    FS_STA_OCUP: 'N'
                }
                $.post($("#_liberar_mesa").data("request-url"), prm,
                    function (res, textStatus, jqXHR) {
                        if (res.response) {
                            msg.success("Aviso", `Mesa ${res.result} liberada`);
                            $.get($("#_lista_mesas").data("request-url"),
                                function (data, textStatus, jqXHR) {
                                    $("#lista_mesas").html(data);
                                },
                                "html"
                            );
                        }
                        else {
                            msg.error("Aviso", res.error);
                        }
                        return false;
                    },
                    "json"
                );
            });

            function load_mesa() {
                $.get($("#_lista_mesas").data("request-url"),
                    function (data, textStatus, jqXHR) {
                        $("#lista_mesas").html(data);
                    },
                    "html"
                );
            }
            $('body').off("click", "#btnRegistrarReservacion").on('click', '#btnRegistrarReservacion', function () {
                $("#Frm_Reservacion_Mesa").submit();
            });

            $("body").off("submit", "#Frm_Reservacion_Mesa").on("submit", "#Frm_Reservacion_Mesa", function () {
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            if (res.response) {
                                msg.success("Aviso", `Mesa ${res.result} reservada`);
                                modal_ajax(false);
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                return false;
            });

            $('body').off("click", "#btnRegistrarMesa").on('click', '#btnRegistrarMesa', function () {
                $("#Frm_Registrar_Mesa").submit();
            });
            $("body").off("submit", "#Frm_Registrar_Mesa").on("submit", "#Frm_Registrar_Mesa", function () {
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            if (res.response) {
                                msg.success("Aviso", `Mesa ${res.result} registrada`);
                                modal_ajax(false);
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                return false;
            });
        }
    },

    Articulo: {
        init: function () {
            moment.locale('es');
            init_file_input();
            $("body").off("input", "#TXT_FS_COD_ARTI").on("input", "#TXT_FS_COD_ARTI", function (e) {
                $("#status").val("AGREGAR");
            });
            var dTable = $('#TB_ARTI').DataTable({
                "scrollX": true,
                ajax: {
                    "url": $("#_listar_articulos").data('request-url'),
                    "dataSrc": ""
                },
                "columnDefs": [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": `<div style="inline-block"><button type='button' class='btn  btn-outline-success _edit'><i class="fa fa-pencil"></i></button>
                        <button type='button' class='btn  btn-outline-danger _delete'><i class="fa fa-trash"></i></button></div>`
                }],
                columns: [
                    { data: 'FS_COD_ARTI' },
                    { data: 'FS_NOM_ARTI' },
                    { data: 'FS_COD_CLAS' },
                    { data: 'FS_DES_CLAS' },
                    { data: 'FN_IMP_VENT' },
                    { data: 'FS_TIP_SITU' },
                    { data: 'FS_DES_OBSE' },
                    {
                        data: 'FS_RUT_FOTO',
                        "defaultContent": "",
                        "render": function (data, type, row) {
                            return `<img style="max-height:40px" src="${data}">`;
                        }
                    },
                    { data: null }
                ],
                dom: 'lfrtip',
                select: true,
                "rowId": function (a) {
                    return 'id_' + a.FS_COD_ARTI;
                },
                "processing": true,

                "lengthMenu": [[5, 10, -1], [5, 10, "Todos"]],
                "language": data_Table_Language,
                "fnInitComplete": function () {
                },
            });

            dTable.on('user-select', function (e, dt, type, cell, originalEvent) {
                if ($(cell.node()).parent().hasClass('selected')) {
                    e.preventDefault();
                }
            });

            $('body').off("click", "#submitButton").on("click", "#submitButton", function (e) {
                if ($("#status").val() === "AGREGAR") {
                    alertConfirm.show("¿Desea registrar el artículo?", "");
                } else {
                    alertConfirm.show("¿Desea actualizar el artículo?", "");
                }
                alertConfirm.yes = function () {
                    $("#Frm_Articulo_Registro").submit();
                };
            });
            $('#TB_ARTI tbody').on('click', '._edit', function () {
                const data = dTable.row($(this).parents('tr')).data();
                $.post($("#_cargar_articulo").data('request-url'), { FS_COD_ARTI: data.FS_COD_ARTI },
                    function (data, textStatus, jqXHR) {
                        $("#registro_articulo_card").html(data);
                        init_file_input();
                    },
                    "html"
                );
            });

            $('#TB_ARTI tbody').on('click', '._delete', function () {
                alertConfirm.show("¿Desea eliminar el artículo?", "");
                const data = dTable.row($(this).parents('tr')).data();

                alertConfirm.yes = function () {
                    $.post($("#_eliminar_articulo").data('request-url'), { FS_COD_ARTI: data.FS_COD_ARTI },
                        function (res, textStatus, jqXHR) {
                            if (res.response) {
                                msg.success("Aviso", `Artículo ${data.FS_COD_ARTI} eliminado`);
                                dTable.ajax.reload();
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                          
                        },
                        "json"
                    );
                };
            });
            $("body").off("submit", "#Frm_Articulo_Registro").on("submit", "#Frm_Articulo_Registro", function () {
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            if (res.response) {
                                if ($("#status").val() === "AGREGAR") {
                                    msg.custom("Aviso", `Artículo <strong> ${res.result}  </strong> registrado`);
                                } else {
                                    msg.custom("Aviso", `Artículo <strong> ${res.result}  </strong> actualizado`);
                                }
                                dTable.ajax.reload();
                                load_articulo(res.result);
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                return false;
            });
        }
    },
    Cliente: {
        init: function () {
            moment.locale('es');
            $("body").off("input", "#TXT_FS_COD_CLIE").on("input", "#TXT_FS_COD_CLIE", function (e) {
                $("#status").val("AGREGAR");
            });

            var dTable = $('#TB_CLIE').DataTable({
                "scrollX": true,
                ajax: {
                    "url": $("#_listar_clientes").data('request-url'),
                    "dataSrc": ""
                },
                "columnDefs": [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": `<div style="inline-block"><button type='button' class='btn  btn-outline-success _edit'><i class="fa fa-pencil"></i></button>
                        <button type='button' class='btn  btn-outline-danger _delete'><i class="fa fa-trash"></i></button></div>`
                }],
                columns: [
                    { data: 'FS_COD_CLIE' },
                    { data: 'FS_NOM_CLIE' },
                    { data: 'FS_NOM_RAZO_SOCI' },
                    { data: 'FS_NUM_RUCS' },
                    { data: 'FS_DES_OBSE' },
                    { data: 'FS_TIP_SITU' },
                    { data: 'FS_DES_TIPO_CLIE' },
                    { data: null }
                ],
                dom: 'lfrtip',
                select: true,
                "rowId": function (a) {
                    return 'id_' + a.FS_COD_CLIE;
                },
                "processing": true,

                "lengthMenu": [[5, 10, -1], [5, 10, "Todos"]],
                "language": data_Table_Language,
                "fnInitComplete": function () {
                },
            });

            dTable.on('user-select', function (e, dt, type, cell, originalEvent) {
                if ($(cell.node()).parent().hasClass('selected')) {
                    e.preventDefault();
                }
            });
            $('#TB_CLIE tbody').on('click', '._edit', function () {
                const data = dTable.row($(this).parents('tr')).data();
                $.post($("#_cargar_cliente").data('request-url'), { FS_COD_CLIE: data.FS_COD_CLIE },
                    function (data, textStatus, jqXHR) {
                        $("#registro_cliente_card").html(data);
                    },
                    "html"
                );
            });

            $('#TB_CLIE tbody').on('click', '._delete', function () {
                alertConfirm.show("¿Desea eliminar el cliente?", "");
                const data = dTable.row($(this).parents('tr')).data();

                alertConfirm.yes = function () {
                    $.post($("#_eliminar_cliente").data('request-url'), { FS_COD_CLIE: data.FS_COD_CLIE },
                        function (res, textStatus, jqXHR) {
                            if (res.response) {
                                msg.success("Aviso", `Cliente ${data.FS_COD_CLIE} eliminado`);
                                dTable.ajax.reload();
                            }
                        },
                        "json"
                    );
                };
            });
            $('body').off("click", "#submitButton").on("click", "#submitButton", function (e) {
                if ($("#status").val() === "AGREGAR") {
                    alertConfirm.show("¿Desea registrar el cliente?", "");
                } else {
                    alertConfirm.show("¿Desea actualizar el cliente?", "");
                }

                alertConfirm.yes = function () {
                    $("#Frm_Cliente_Registro").submit();
                };
            });
            function load_cliente(FS_COD_CLIE) {
                if (FS_COD_CLIE === null) {
                    return false;
                }
                $.get($("#_cargar_cliente").data("request-url"), { FS_COD_CLIE: FS_COD_CLIE },
                    function (data, textStatus, jqXHR) {
                        $("#registro_cliente_card").html(data);
                    },
                    "html"
                );
            }
            $("body").off("submit", "#Frm_Cliente_Registro").on("submit", "#Frm_Cliente_Registro", function () {
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            if (res.response) {
                                if ($("#status").val() === "AGREGAR") {
                                    msg.custom("Aviso", `Cliente <strong> ${res.result}  </strong> registrado`);
                                } else {
                                    msg.custom("Aviso", `Cliente <strong> ${res.result}  </strong> actualizado`);
                                }
                                load_cliente(res.result);
                                dTable.ajax.reload();
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                return false;
            });

            $("body").off("click", "#busqueda_ruc").on("click", "#busqueda_ruc", function (e) {
                var ruc = $("#consulta_ruc").val();
                $(".dot_loader").removeClass("invisible");
                $.get($("#_consulta_ruc").data("request-url"), { codigo: ruc },
                    function (res, textStatus, jqXHR) {
                        $("#TXT_FS_NOM_RAZO_SOCI").val(res.result.razon_social);
                        $("#TXT_FS_COD_CLIE").val(res.result.ruc);
                        $("#TXT_FS_NUM_RUCS").val(res.result.ruc);
                        $("#TXT_FS_DES_OBSE").val(res.result.condicion);
                        $("#TXT_FS_DES_DIRE").val(res.result.direccion);
                        if (ruc.substring(0, 2) == "20") {
                            $("#TXT_FS_COD_TIPE_SUNA").val("02");
                            $("#TXT_FS_DES_TIPE_SUNA").val("Jurídica");
                        }
                        if (ruc.substring(0, 2) == "10") {
                            $("#TXT_FS_COD_TIPE_SUNA").val("01");
                            $("#TXT_FS_DES_TIPE_SUNA").val("Natural");
                        }
                        $(".dot_loader").addClass("invisible");
                    },
                    "json"
                );
            });
        },
    },
    Venta: {
        init: function () {
            moment.locale('es');

            console.log("js para  Venta");

            $("#TXT_FD_FEC_DOCU").flatpickr({
                dateFormat: "d/m/Y",
                minDate: "today",
                "locale": "es",
                maxDate: new Date().fp_incr(360)
            });
            var dTable = $('#TB_DETA_DOCU_VENT').DataTable({
                "scrollX": true,
                ajax: {
                    "url": $("#_detalle_data_loader_venta").data('request-url'),
                    "dataSrc": ""
                },
                columns: [
                    { data: 'FI_NUM_SECU' },
                    {
                        data: 'FS_NOM_ARTI'
                    },
                    {
                        data: 'FN_PRE_VENT',
                        "defaultContent": "0.0",
                        "render": function (data, type, row) {
                            // return moment(date).format('HH:mm a, D MMM , YY');
                            return "S/. " + data;
                        }
                    },
                    { data: 'FN_CAN_ARTI' }

                ],
                dom: 'lrtip',
                select: true,
                "rowId": function (a) {
                    return 'id_' + a.FI_NUM_SECU;
                },
                "processing": true,

                "lengthMenu": [[5, 10, -1], [5, 10, "Todos"]],
                "language": data_Table_Language,
                "fnInitComplete": function () {
                },
            });

            dTable.on('user-select', function (e, dt, type, cell, originalEvent) {
                if ($(cell.node()).parent().hasClass('selected')) {
                    e.preventDefault();
                }
            });

            $("body").off("click", "#busqueda_ordenes").on("click", "#busqueda_ordenes", function (e) {
                modal_ajax(true, $("#_busqueda_ordenes").data("request-url"), load_orden, "Búsqueda de Órdenes");
            });

            $("body").off("click", "#busqueda_ventas").on("click", "#busqueda_ventas", function (e) {
                modal_ajax(true, $("#_busqueda_ventas").data("request-url"), load_venta, "Búsqueda de Ventas");
            });
            $("body").off("change", "#TXT_FS_TIP_DOCU").on("change", "#TXT_FS_TIP_DOCU", function (e) {
                if ($(this).val() === "FAC") {
                    $.get($("#_obtener_nuevo_documento_correlativo").data("request-url"), { FS_TIP_DOCU: "FAC" },
                        function (data, textStatus, jqXHR) {
                            $("#TXT_FS_NUM_DOCU").val(data);
                        },
                        "json"
                    );
                }
                else {
                    $.get($("#_obtener_nuevo_documento_correlativo").data("request-url"), { FS_TIP_DOCU: "BOL" },
                        function (data, textStatus, jqXHR) {
                            $("#TXT_FS_NUM_DOCU").val(data);
                        },
                        "json"
                    );
                }
            });

            function load_venta() {
                const FN_IDE_DOCU = localStorage.getItem("FN_IDE_DOCU");
                if (FN_IDE_DOCU === null) {
                    return false;
                }

                $.get($("#_detalle_data_loader_venta2").data("request-url"), { FN_IDE_DOCU: FN_IDE_DOCU },
                    function (data, textStatus, jqXHR) {
                        dTable.clear();
                        dTable.rows.add(data);
                        dTable.draw(false);
                    },
                    "json"
                );
                $.get($("#_cargar_venta").data("request-url"), { FN_IDE_DOCU: FN_IDE_DOCU },
                    function (data, textStatus, jqXHR) {
                        $("#registro_venta_form_card").html(data);
                        $("#TXT_FD_FEC_DOCU").flatpickr({
                            dateFormat: "d/m/Y",
                            minDate: "today",
                            "locale": "es",
                            maxDate: new Date().fp_incr(360)
                        });
                    },
                    "html"
                );
            }

            function load_orden() {
                const FN_IDE_ORDE = localStorage.getItem("FN_IDE_ORDE");
                if (FN_IDE_ORDE === null) {
                    return false;
                }

                $.get($("#_detalle_data_loader_orden").data("request-url"), { FN_IDE_ORDE: FN_IDE_ORDE },
                    function (data, textStatus, jqXHR) {
                        dTable.clear();
                        dTable.rows.add(data);
                        dTable.draw(false);
                    },
                    "json"
                );
                $.get($("#_cargar_orden").data("request-url"), { FN_IDE_ORDE: FN_IDE_ORDE },
                    function (data, textStatus, jqXHR) {
                        $("#registro_venta_form_card").html(data);
                        $("#TXT_FD_FEC_DOCU").flatpickr({
                            dateFormat: "d/m/Y",
                            minDate: "today",
                            "locale": "es",
                            maxDate: new Date().fp_incr(360)
                        });
                    },
                    "html"
                );
            }
            $('body').off("click", "#submitSaleButton").on("click", "#submitSaleButton", function (e) {
                const totalRecords = dTable.rows().count();
                if (totalRecords === 0) { msg.warning("Aviso", "Se debe tener al menos un detalle"); return false; }

                alertConfirm.show("¿Desea registrar la venta?", "");
                alertConfirm.yes = function () {
                    $(this).attr("disabled", "disabled");
                    $("#Frm_Venta_Registro").submit();
                };
            });
            $('body').off("click", "#printSaleButton").on("click", "#printSaleButton", function (e) {
                var rows = dTable.rows().data().toArray();
                console.table(rows);
                var doc = new jsPDF();

                function headRows() {
                    return [{ FI_NUM_SECU: 'Sec.', FS_COD_ARTI: 'Código', FS_NOM_ARTI: 'Artículo', FN_CAN_ARTI: 'Cantidad', FN_PRE_VENT: 'Precio' }];
                }

                var total = 0;

                var omfg = rows[0].FS_TIP_DOCU;
                rows.forEach(function (v) { delete v.FD_FEC_DOCU; delete v.FN_IDE_ORDE; delete v.FN_IMP_TOTA;delete v.FI_COD_EMPR; delete v.FN_IDE_DOCU; delete v.FS_TIP_DOCU; total = total + v.FN_PRE_VENT * v.FN_CAN_ARTI });

                function footerRows() {
                    return [{
                        FI_NUM_SECU: '', FS_COD_ARTI: '', FS_NOM_ARTI: '', FN_CAN_ARTI: '', FN_PRE_VENT: `Total: ${total} `
                    }];
                }
                doc.autoTable({
                    head: headRows(),
                    body: rows,
                    foot: footerRows(),
                    startY: 42,
                    tableLineColor: [231, 76, 60],
                    tableLineWidth: 1,
                    styles: {
                        lineColor: [44, 62, 80],
                        lineWidth: 1
                    },
                    headStyles: {
                        fillColor: [47, 53, 58],
                        fontSize: 15
                    },
                    footStyles: {
                        fontSize: 15
                    },
                    bodyStyles: {
                        fillColor: [52, 73, 94],
                        textColor: 240
                    },
                    alternateRowStyles: {
                        fillColor: [74, 96, 117]
                    },

                    columnStyles: {
                        FN_PRE_VENT: {
                            fontStyle: 'bold'
                        },

                        FN_CAN_ARTI: {
                            halign: 'right'
                        }
                    },
                    allSectionHooks: true,
                    didDrawPage: function (data) {
                        doc.setFontSize(18);
                        doc.text("Documento N° " + $("#TXT_FS_NUM_DOCU").val(), data.settings.margin.left, 22);
                        if (omfg === 'BOL') {
                            doc.text("Boleta", data.settings.margin.left, 30);
                        } else {
                            doc.text("Factura", data.settings.margin.left, 30);
                        }

                        var date = new Date();
                        date = moment(date).format('HH:mm a, D MMM , YYYY');
                        doc.setFontSize(11);
                        doc.text("Fecha de impresión: " + date, data.settings.margin.left, 38)
                    },
                });
                doc.save('a4.pdf')
            });
            $("body").off("submit", "#Frm_Venta_Registro").on("submit", "#Frm_Venta_Registro", function () {
                debugger;
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            debugger;
                            if (res.response) {
                                msg.custom("Aviso", `Venta nro. <strong> ${res.result}  </strong> registrada`);
                                load_venta(res.result);
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                $("#submitSaleButton").removeAttr("disabled");
                return false;
            });
        }
    },
    Orden: {
        init: function () {
            //debugger;
            // controller-wide code  - general de cada vista
            moment.locale('es');

            console.log("js para  Orden");

            $("#TXT_FD_FEC_ORDE").flatpickr({
                dateFormat: "d/m/Y",
                minDate: "today",
                "locale": "es",
                maxDate: new Date().fp_incr(360)
            });

            $('#FD_FEC_ORDE').removeAttr("data-val-date");

            const url_detail_add = $("#_detalle_data_add").data('request-url');

            var dTable = $('#TB_DETA_ORDE').DataTable({
                "scrollX": true,
                ajax: {
                    "url": $("#_detalle_data_loader").data('request-url'),
                    "dataSrc": ""
                },
                "columnDefs": [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": `<div style="inline-block">
                        <button type='button' class='btn  btn-outline-danger _delete'><i class="fa fa-trash"></i></button></div>`
                }],
                columns: [
                    { data: 'FI_NUM_SECU' },
                    {
                        data: 'FS_NOM_ARTI'
                    },
                    {
                        data: 'FN_PRE_VENT',
                        "defaultContent": "0.0",
                        "render": function (data, type, row) {
                            // return moment(date).format('HH:mm a, D MMM , YY');
                            return "S/. " + data;
                        }
                    },
                    { data: 'FN_CAN_ARTI' },
                    { data: null }
                ],
                dom: 'lrtip',
                select: true,
                "rowId": function (a) {
                    return 'id_' + a.FI_NUM_SECU;
                },
                "processing": true,

                "lengthMenu": [[5, 10, -1], [5, 10, "Todos"]],
                "language": data_Table_Language,
                "fnInitComplete": function () {
                },
            });

            dTable.on('user-select', function (e, dt, type, cell, originalEvent) {
                if ($(cell.node()).parent().hasClass('selected')) {
                    e.preventDefault();
                }
            });

            /* Agregamos producto a una lista de sesión y añadimos a la tabla */
            $('body').off("click", "[data-product]").on("click", "[data-product]", function (e) {
                let $this = $(this);
                $.post(url_detail_add, { FS_COD_ARTI: $this.data("product") },
                    function (res, textStatus, jqXHR) {
                        if (res.response) {
                            dTable.clear();
                            dTable.rows.add(res.data);
                            dTable.draw(false);
                            msg.success("Aviso", res.result);

                            $("#TXT_FN_IMP_TOTA").val(res.total);
                            $(dTable.column(3).footer()).html(
                                'S/ ' + res.total
                            );
                        }
                        else {
                            msg.error("Aviso", res.error);
                        }
                        return false;
                    },
                    "json"
                ).fail(function () {
                    msg.error("Aviso", "Error de conexión");
                });
            });

            $('body').off("click", "#submitButton").on("click", "#submitButton", function (e) {
                const totalRecords = dTable.rows().count();
                if (totalRecords === 0) { msg.warning("Aviso", "Ingrese al menos un detalle"); return false; }

                if ($("#status").val() === "AGREGAR") {
                    alertConfirm.show("¿Desea registrar la orden?", "");
                }
                else {
                    alertConfirm.show("¿Desea actualizar la orden?", "");
                }
                alertConfirm.yes = function () {
                    $(this).attr("disabled", "disabled");
                    $("#Frm_Orden_Registro").submit();
                };
            });

            $("body").off("submit", "#Frm_Orden_Registro").on("submit", "#Frm_Orden_Registro", function () {
                debugger;
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (res) {
                            debugger;
                            if (res.response) {
                                load_order(res.result);

                                if ($("#status").val() === "AGREGAR") {
                                    msg.custom("Aviso", `Orden nro. <strong> ${res.result}  </strong> registrada`);
                                }
                                else {
                                    msg.custom("Aviso", `Orden nro. <strong> ${res.result}  </strong> actualizada`);
                                }
                            }
                            else {
                                msg.error("Aviso", res.error);
                            }
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso", "Error de conexión");
                        }
                    });
                }
                $("#submitButton").removeAttr("disabled");
                return false;
            });

            function load_order(FN_IDE_ORDE) {
                $.get($("#_busqueda_orden_registrada").data("request-url"), { FN_IDE_ORDE: FN_IDE_ORDE },
                    function (data, textStatus, jqXHR) {
                        debugger;
                        $("#registro_form_card").html(data);

                        $('._valid-date').toArray().forEach(function (campo) {
                            new Cleave(campo, {
                                date: true,
                                datePattern: ['d', 'm', 'Y']
                            });
                        });
                        $("#TXT_FD_FEC_ORDE").flatpickr({
                            dateFormat: "d/m/Y",
                            minDate: "today",
                            "locale": "es",
                            maxDate: new Date().fp_incr(360)
                        });
                        $.get($("#_detalle_data_loader").data('request-url'),
                            function (data, textStatus, jqXHR) {
                                dTable.clear();
                                dTable.rows.add(data);
                                dTable.draw(false);
                            },
                            "json"
                        );
                    },
                    "html"
                );
            }

            function set_generic_client(FI_STA_DEFE) {
                $.get($('#_buscar_cliente_defecto').data('request-url'), { FI_STA_DEFE: FI_STA_DEFE },
                    function (data, textStatus, jqXHR) {
                        $("#TXT_FS_COD_CLIE").val(data.FS_COD_CLIE);
                        $("#TXT_FS_NUM_RUCS").val(data.FS_NUM_RUCS);
                        $("#TXT_FS_NOM_CLIE").val(data.FS_NOM_CLIE);
                        $("#TXT_FS_TIP_CLIE").val(data.FS_TIP_CLIE);
                        $("#TXT_FS_DES_TIPO_CLIE").val(data.FS_DES_TIPO_CLIE);
                        $("#TXT_FS_NUM_DOCU_IDEN").val(data.FS_NUM_DOCU_IDEN);
                    },
                    "json"
                );
            }

            $("body").off("change", "#CHK_isGeneric").on("change", "#CHK_isGeneric", function (e) {
                if ($(this).prop("checked")) {
                    set_generic_client(1);
                }
                else {
                    $("#TXT_FS_COD_CLIE").val('');
                    $("#TXT_FS_NUM_RUCS").val('');
                    $("#TXT_FS_NOM_CLIE").val('');
                    $("#TXT_FS_DES_TIPO_CLIE").val('');
                    $("#TXT_FS_NUM_DOCU_IDEN").val('');
                }
            });

            $("body").off("click", "#busqueda_cliente").on("click", "#busqueda_cliente", function (e) {
                modal_ajax(true, $("#_busqueda_clientes").data("request-url"), set_cliente, "Búsqueda de clientes");
            });
            function set_cliente() {
                const FS_COD_CLIE = localStorage.getItem("FS_COD_CLIE");
                if (FS_COD_CLIE === null) {
                    return false;
                }
                console.log(FS_COD_CLIE);
                $.get($("#_busqueda_cliente_por_codigo").data("request-url"), { FS_COD_CLIE: FS_COD_CLIE },
                    function (data, textStatus, jqXHR) {
                        $("#CHK_isGeneric").prop("checked", false);
                        $("#TXT_FS_COD_CLIE").val(data.FS_COD_CLIE);
                        $("#TXT_FS_NUM_RUCS").val(data.FS_NUM_RUCS);
                        $("#TXT_FS_NOM_CLIE").val(`${data.FS_NOM_CLIE} ${data.FS_NOM_RAZO_SOCI}`);
                        $("#TXT_FS_TIP_CLIE").val(data.FS_TIP_CLIE);
                        $("#TXT_FS_DES_TIPO_CLIE").val(data.FS_DES_TIPO_CLIE);
                        $("#TXT_FS_NUM_DOCU_IDEN").val(data.FS_NUM_DOCU_IDEN);
                    },
                    "json"
                );
            }

            $('#TB_DETA_ORDE tbody').on('click', '._delete', function () {
                const data = dTable.row($(this).parents('tr')).data();

                $.post($("#_eliminar_detalle").data('request-url'), { FS_COD_ARTI: data.FS_COD_ARTI },
                    function (res, textStatus, jqXHR) {
                        if (res.response) {
                            dTable.clear();
                            dTable.rows.add(res.data);
                            dTable.draw(false);
                            msg.success("Aviso", res.result);

                            $("#TXT_FN_IMP_TOTA").val(res.total);
                            $(dTable.column(3).footer()).html(
                                'S/ ' + res.total
                            );
                        }
                        else {
                            msg.error("Aviso", res.error);
                        }
                        return false;
                    },
                    "json"
                );
            });

            $("body").off("click", "#busqueda_ordenes").on("click", "#busqueda_ordenes", function (e) {
                modal_ajax(true, $("#_busqueda_ordenes").data("request-url"), load_orden, "Búsqueda de Órdenes");
            });

            function load_orden() {
                const FN_IDE_ORDE = localStorage.getItem("FN_IDE_ORDE");
                if (FN_IDE_ORDE === null) {
                    return false;
                }

                load_order(FN_IDE_ORDE);
            }
        }
    }
    /* ------------------------------------------------------------------------- */
};
/* Principal objeto javascript que inicializa todos los scripts específicos de página */
UTIL = {
    exec: function (controller, action) {
        /* Definimos nuestro objeto que contendrá las funciones*/
        var ns = RMS,
            action = (action === undefined) ? "init" : action;
        if (controller !== "" && ns[controller] && typeof ns[controller][action] == "function") {
            ns[controller][action]();
        }
    },
    init: function () {
        /* html tag que controlará la inicialización del objeto respectivo */
        var body = $("._load"),
            controller = body.data("load"),
            action = body.data("load-action");
        UTIL.exec("common");
        UTIL.exec(controller);
        if (action !== undefined && action !== "") {
            UTIL.exec(controller, action);
        }
    }
};

function myFunctions() {
    /* Funciones variadas no específicas de página*/
    console.log("myfunc activated");

    $(".btn").on("mouseup", function () {
        $(this).blur();
    });

    /* Activar urls extras para smoothState - necesario para urls fuera del main container*/
    $('#main-navBar a , #main-sideBar a, a.smooth-link').not(".no-smoothState , .nav-dropdown-toggle").off("click").on("click", function (e) {
        e.preventDefault();
        var content = $('#main_content').smoothState().data('smoothState');
        var href = $(this).attr('href');
        console.log("smooth-State activated");
        content.load(href);
    });
};

$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
    }
});

/* smoothState Init*/

$(function () {
    var options = {
        onPopState: function (e) {
            if (e.state !== null || typeof e.state !== undefined) {
                var url = window.location.href;
                var $page = $('#' + e.state.id);
                var page = $page.data('smoothState');

                if (typeof (page.cache[page.href]) !== 'undefined') {
                    var diffUrl = (page.href !== url && !utility.isHash(url, page.href));
                    var diffState = (e.state !== page.cache[page.href].state);

                    if (diffUrl || diffState) {
                        if (diffState) {
                            page.clear(page.href);
                        }
                        page.load(url, false);
                    }
                }
                else {
                    //reload the page if page.cache[page.href] is undefined
                    location.reload();
                }
            }
        },
        prefetch: false,
        anchors: 'a.smoothState',
        forms: 'form.smoothState',
        cacheLength: 2,
        onStart: {
            duration: 150, // Duration of our animation
            render: function ($container) {
                // Add your CSS animation reversing class
                $container.addClass('is-exiting');

                setTimeout(function () { $(".bg-loader").removeClass("_loaded"); }, 200);

                // Restart your animation
                smoothState.restartCSSAnimations();
            }
        },

        onReady: {
            duration: 270,
            render: function ($container, $newContent) {
                // Remove your CSS animation reversing class
                $container.removeClass('is-exiting');
                $container.html($newContent);
                // Inject the new content
            }
        },
        onAfter: function () {
            UTIL.init();
            $(".bg-loader").addClass("_loaded");

            myFunctions();
            //$(document).ready();
        }
    },
        smoothState = $('#main_content').smoothState(options).data('smoothState'); /* container => #main_content*/
});

myFunctions();
UTIL.init();