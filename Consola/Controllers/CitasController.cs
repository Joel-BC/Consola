using BLL;
using Consola.Helpers;
using Consola.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class CitasController : Controller
    {
        clsCita Informacion = new clsCita();
        // GET: Citas
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Citas()
        {
            return View();
        }
        public JsonResult ConsultarCitas()
        {
            List<ConsultarCitaResult> citas = Informacion.ConsultarCita();
            return Json(citas, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public JsonResult CambiarCita(int IdCita, string Asunto, string Descripcion, DateTime Inicio, DateTime Fin, string ColorFondo, bool DiaCompleto)
        {
            var status = false;
            try
            {
                bool Resultado = Informacion.ActualizarCita(IdCita, Asunto, Descripcion, Inicio, Fin, ColorFondo, DiaCompleto);

                if (Resultado)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch
            {
                status = false;
            }
            return Json(new { Estado = status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarCita(string Asunto, string Descripcion, DateTime Inicio, DateTime Fin, string ColorFondo, bool DiaCompleto)
        {
            var status = false;
            try
            {
                bool Resultado = Informacion.AgregarCita(Asunto, Descripcion, Inicio, Fin, ColorFondo, DiaCompleto);

                if (Resultado)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

            }
            catch
            {
                status = false;
            }
            return Json(new { Estado = status }, JsonRequestBehavior.AllowGet);
        }


    }
}