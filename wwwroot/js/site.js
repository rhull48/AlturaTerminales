
//Ruta de las spec de las terminales
let FullPath = '\\\\172.16.129.21\\Specialty Harness\\CUSTOMER PRINTS\\DOWNLEVELS\\';

//Allow open multiple modals at same time
$(document).on('show.bs.modal', '.modal', function (event) {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});

//Close anuy modal
function CierraModal(oModalName) {
    $('#' + oModalName).modal('hide')
}
 
//Search in table
$("#TextBuscar").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#TableBody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}); 

//Export list of terminals to Excel
$("#btnExport").click(function () {

    $("#MainTable").table2excel({
        // exclude CSS class
        exclude: ".noExl",
        name: "Worksheet Name",
        filename: "ListadoTerminales", //do not include extension
        fileext: ".xlsx", // file extension
        preserveColors: true

    });
});

//Display terminal crimp values
function VerDetalleAlturas(oIDTerminal, oURL) {

    axios.get(oURL, { params: { oTerminalId: oIDTerminal } })
        .then(respose => {
            $('#DetalleModalLabel').html('<p>Detalle Alturas</p>');
            $('#modalBody').html(respose.data);
            $('#DetalleModal').modal('show');
        })
        .catch(error => {

            $('#ModalInfoTitle').html('<p>Mensaje</p>');
            $('#modalBodyInfo').html('<p>No hay informacion de alturas para esta terminal...</p>');
            $('#ModalInfo').modal('show');

            console.log(error);
        })
}

//Display tooling
function VerDetalleAplicadores(oAlturaID, oURL) {

    axios.get(oURL, { params: { AlturaID: oAlturaID } })
        .then(response => {
            $('#ModalInfoTitle').html('<p>Aplicadores y Tooling Adicional</p>');
            $('#modalBodyInfo').html(response.data);
            $('#ModalInfo').modal('show');
        })
        .catch(error => {
            $('#ModalInfoTitle').html('<p>Mensaje</p>');
            $('#modalBodyInfo').html('<p>No hay informacion de aplicadores para esta terminal...</p>');
            $('#ModalInfo').modal('show');

            console.log(error);
        })    
}

//Display insulation detail
function VerDetalleInsulaciones(oAlturaID, oURL) {

    axios.get(oURL, { params: { AlturaID: oAlturaID }})
        .then(response => {
            $('#ModalInfoTitle').html('<p>Detalle Insulaciones</p>');
            $('#modalBodyInfo').html(response.data);
            $('#ModalInfo').modal('show');
        })
}

//Open a PDF spec for terminal from network path
function AbrirEspecificacion(oTerminal) {

    let specPath = FullPath + oTerminal + '.pdf';

    axios.post('Home/AbrirEspecificacionTerminal', null, { params: { oFileName: specPath } })
        .then(response => {
            let pdfWindow = window.open("")
            pdfWindow.document.write(
                "<iframe width='100%' height='100%' src='data:application/pdf;base64, " +
                encodeURI(response.data) + "'></iframe>"
            )
        })
        .catch(error => {
            $('#ModalInfoTitle').html('<p>Mensaje</p>');
            $('#modalBodyInfo').html('<p>No hay informacion de la especificacion de la terminal en ' + specPath + '</p>');
            $('#ModalInfo').modal('show');

            console.log(error);
        })
}
 