

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
 

    Orden: {
        init: function () {
            //debugger;
            // controller-wide code  - general de cada vista
            console.log("js para  Orden");

            $('#FD_FEC_ORDE').removeAttr("data-val-date");

            const url_detail_add = $("#_detalle_data_add").data('request-url');

            let dTable = $('#TB_DETA_ORDE').DataTable({
                "scrollX": true,  
                ajax: {
                    "url": $("#_detalle_data_loader").data('request-url'),
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
                dom: 'lBrtip',
                select: true,
                "rowId": function (a) {
                    return 'id_' + a.FI_NUM_SECU;
                },
                "processing": true,
                buttons: [

                    {
                        text: button_save,
                        key: {
                            shiftKey: true,
                            key: '2',
                        },
                        className: 'btn btn-lg btn-transparent',
                        titleAttr: "Procesar Orden (shift + 2)",
                        action: function (e, dt, node, config) {
                            msg.success("test", "test");
                        },
                        init: function (api, node, config) {
                            $(node).removeClass('btn-default')
                        }
                    }
                ],
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

        /*    $('body').off("click", ".collapse .food-card").on("click", ".collapse .food-card", function (e) {
                $this = $(this);

                $.post(url_detail_add, { FS_COD_ARTI: $this.data("id") },
                    function (data, textStatus, jqXHR) {
                        dTable.ajax.reload(null, false);
                    },
                    "json"
                );
            });*/

            $('body').off("click", "[data-product]").on("click", "[data-product]", function (e) {
                let $this = $(this);
                $.post(url_detail_add, { FS_COD_ARTI: $this.data("product") },
                    function (res, textStatus, jqXHR) {
                        if (res.response) {
                            dTable.clear();
                            dTable.rows.add(res.data);
                            dTable.draw(false);
                            msg.success("Aviso", res.result);

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

            $('body').off("click", "#submitButton").on("click", "#submitButton", function (e) {
                $(this).attr("disabled", "disabled");
                $("#Frm_Orden_Registro").submit();
            });

            $("#Frm_Orden_Registro").off("submit").on("submit", function () {
                debugger;
                const $form = $(this);
                $.validator.unobtrusive.parse($form);
                const result = $form.valid();
                if (result) {
                    $form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: $form.attr('action'),
                        success: function (data) {                    

                            dTable.clear();
                            dTable.rows.add(data);
                            dTable.draw(false);
                            return false;
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                            msg.error("Aviso","Error de conexión");
                        }
                    });
                }
                $("#submitButton").removeAttr("disabled");
                return false;
            });

            $.get($('#_tipo_cliente_data_loader').data('request-url'), function (data) {
                $('._FS_DES_TIPO_CLIE').search({
                    source: data,
                    fields: {
                        description: 'FS_TIP_CLIE',
                        title: 'FS_DES_TIPO_CLIE'
                    },
                    searchFields: [
                        'FS_TIP_CLIE',
                        'FS_DES_TIPO_CLIE'
                    ],
                    fullTextSearch: true,
                    onSelect: function (result, response) {
                        $("#TXT_FS_TIP_CLIE").val(result.FS_TIP_CLIE);
                        debugger;
                        if (result.FS_TIP_CLIE === "9999")
                            set_generic_client(result.FS_TIP_CLIE);
                        else
                            clean_fields();
                    },
                    error: {
                        noResults: "No hay resultados"
                    },
                    maxResults: 15,
                    minCharacters: 0
                });
            }).fail(function () {
                console.error("error at get in s-ui search");
            });

            function set_generic_client(searchTerm) {
                $.get($('#_tipo_cliente_data_search_by_code').data('request-url'), { FS_TIP_CLIE: searchTerm },
                    function (data, textStatus, jqXHR) {
                        $("#TXT_FS_COD_CLIE").val(data.FS_COD_CLIE).attr("readonly", true);
                        $("#TXT_FS_NUM_RUCS").val(data.FS_NUM_RUCS).attr("readonly", true);
                        $("#TXT_FS_NOM_CLIE").val(data.FS_NOM_CLIE).attr("readonly", true);
                    },
                    "json"
                );
            }
            function clean_fields() {       
                $("#TXT_FS_COD_CLIE").val('').removeAttr("readonly");
                $("#TXT_FS_NUM_RUCS").val('').removeAttr("readonly");
                $("#TXT_FS_NOM_CLIE").val('').removeAttr("readonly"); 
               
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
        if (action !== undefined && action !== "")
        {
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
    onPopState : function(e) {
        if(e.state !== null || typeof e.state !== undefined) {
            var url = window.location.href;
            var $page = $('#' + e.state.id);	
            var page = $page.data('smoothState');
					
            if (typeof(page.cache[page.href]) !== 'undefined') {
                var diffUrl = (page.href !== url && !utility.isHash(url, page.href));
                var diffState = (e.state !== page.cache[page.href].state); 

                if(diffUrl || diffState) {
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

            setTimeout(function () { $(".bg-loader").removeClass("_loaded");}, 200);
           
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