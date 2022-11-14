(function(factory) {
    if (typeof define === "function" && define.amd) {

        // AMD. Register as an anonymous module.
        define(["../widgets/datepicker"], factory);
    } else {

        // Browser globals
        factory(jQuery.datepicker);
    }
}(function(datepicker) {

    datepicker.regional.es = {
        closeText: "Cerrar",
        prevText: "&#x3C;Ant",
        nextText: "Sig&#x3E;",
        currentText: "Hoy",
        monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
            "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
        ],
        monthNamesShort: ["Ene", "Feb", "mar", "Abr", "May", "Jun",
            "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
        ],
        dayNames: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
        dayNamesShort: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"],
        dayNamesMin: ["D", "L", "M", "M", "J", "V", "S"],
        weekHeader: "Sm",
        dateFormat: "dd/mm/yy",
        firstDay: 1,
        isRTL: false, //si esta en true el calendario va para atras
        showMonthAfterYear: false,
        yearSuffix: ""
    };
    datepicker.setDefaults(datepicker.regional.es);

    return datepicker.regional.es;

}));
$("#date").datepicker({
    beforeShow: function(input, inst) {
        setTimeout(function() {
            inst.dpDiv.css({
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50% )'

            });
        }, 0);
        $('.modal-bg').show();
    },
    dateFormat: "yy-mm-dd",
    minDate: new Date(), //Crea la instancia de una fecha con el dia de hoy
    setDate: new Date(),
    onSelect: function() {
        $('.modal-bg').hide();
    },

}).datepicker("setDate", new Date());



$("#date").datepicker({
    dateFormat: "yy-mm-dd",
    minDate: new Date(), //Crea la instancia de una fecha con el dia de hoy
    setDate: new Date()
}).datepicker("setDate", new Date());




$("#time").timepicker({
    timeFormat: 'h:mm p',
    interval: 30,
    minTime: '7pm',
    maxTime: '8:30pm',
    // defaultTime: '11',
    // startTime: '10:00',
    dynamic: false,
    dropdown: true,
    scrollbar: true
});