
using AlturaTerminales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;

namespace AlturaTerminales.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QainspeccionContext _context;

        public HomeController(ILogger<HomeController> logger, QainspeccionContext context)
        {
            _logger = logger;
            _context = context;
        }
          
        /// <summary>
        /// Obtiene el listado de las terminales del listado
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            IEnumerable<AlturasList> oListadoAlturas;
            oListadoAlturas = await _context.AlturasList.FromSqlRaw("sel_ListadoAlturas").AsNoTracking().ToListAsync();

            return View(oListadoAlturas);
        }

        /// <summary>
        /// Muestra el detalle de alturas de una terminal seleccionada
        /// </summary>
        /// <param name="oTerminalId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PartialViewResult> DetalleTerminal(int oTerminalId)
        {
            ViewBag.TituloModal = "Mensaje";

            IEnumerable<DetalleAlturasTerminal> oListadoAlturas;

            var oIDTerminal = new SqlParameter("IDTerminal", oTerminalId);
            var oList = await _context.DetalleAlturasTerminal.FromSqlRaw("sel_DetalleAlturaByIDTerm @IDTerminal", oIDTerminal).AsNoTracking().ToListAsync();

            return PartialView(oList.ToList());
        } 

        /// <summary>
        /// Obtiene el listado de aplicadores para una combinacion de GA en especifico
        /// </summary>
        /// <param name="AlturaID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PartialViewResult> DetalleAplicadores(int AlturaID)
        {
            IEnumerable<DetalleAplicadores> oList;

            var oidAltura = new SqlParameter("idAltura", AlturaID);
            oList = await _context.DetalleAplicadores.FromSqlRaw("sel_AplicadoresByAlturaID @idAltura", oidAltura).AsNoTracking().ToArrayAsync();
             
            return PartialView(oList.ToList());
        }

        [HttpGet]
        public async Task<PartialViewResult> DetalleInsulacion(int AlturaID)
        {
            IEnumerable<DetalleAlturasInsulacion> oList;

            var oidAltura = new SqlParameter("idAltura", AlturaID);
            oList = await _context.DetalleAlturasInsulacion.FromSqlRaw("sel_AlturaInsulacionByAlturaId @idAltura", oidAltura).AsNoTracking().ToArrayAsync();
 
            return PartialView(oList.ToList());
        }

        /// <summary>
        /// Abre la especificacion de la terminal en PDF en una nueva ventana
        /// </summary>
        /// <param name="oFileName"></param>
        /// <returns></returns>
        [HttpPost]
        public string AbrirEspecificacionTerminal(string oFileName)
        {
            byte[] pdfByteArray = System.IO.File.ReadAllBytes(oFileName);
            string base64EncodedPDF = Convert.ToBase64String(pdfByteArray);
            return base64EncodedPDF;
        }
  
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}