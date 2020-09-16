var calendar = null;
function calendario() {
    let calendario = document.getElementById("calendar");
    calendar = new FullCalendar.Calendar(calendario, {
        locale: 'es',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        buttonIcons: true,
        weekNumbers: true,
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        dayMaxEvents: true,
        loading: function (bool) {
            document.getElementById('loading').style.display =
                bool ? 'block' : 'none';
        },
        dateClick: function (info) {
            console.log("hola");
            $("#Fecha").val(info.dateStr);
            $("#modalProgramacion").modal('show');
        },
        eventClick: function (info) {
            console.log(info.event);
        }
    });
    calendar.render();
}

function guardar() {
    let formularioProgrmacion = $("#formularioProgrmacion").serialize();
    $.ajax({
        url: '/Programacion/ProgramacionCurso/Guardar',
        type: 'post',
        data: formularioProgrmacion,
        dataType: 'json'
    }).done(function (respuesta) {
        if (respuesta.status) {
            listar();
            alert("Se guardo");
        }
    })
}

function listar() {
    $.ajax({
        url: '/Programacion/ProgramacionCurso/Listar',
        type: 'get',
        dataType: 'json'
    }).done(function (respuesta) {
        if (respuesta.status) {
            let data = [];
            respuesta.data.map(function (e) {
                data.push({
                    id: e.programacionId,
                    title: e.nombreCurso,
                    start: moment(e.fecha).format("YYYY-MM-DD") + " " + e.horaInicio,
                    end: moment(e.fecha).format("YYYY-MM-DD") + " " + e.horaFinal,
                    descripcion: e.descripcion
                });
            });
            refrescarCalendario(data)
        }
    })
}

function refrescarCalendario(data) {
    calendar.addEventSource(data);
    calendar.refetchEvents();
}