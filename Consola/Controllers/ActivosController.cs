using BLL;
using Consola.Helpers;
using Consola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class ActivosController : Controller
    {
        // GET: Activos
        public ActionResult Index()
        {
            try
            {
                List<Activos> listaActivos = new List<Activos>();
                clsActivos activo = new clsActivos();
                var data = activo.ConsultarActivo().ToList();

                foreach (var item in data)
                {
                    Activos modelo = new Activos();
                    modelo.IdActivo = item.IdActivo;
                    modelo.codigoActivo = item.codigoActivo;
                    modelo.nombreActivo = item.nombreActivo;
                    modelo.costoActivo = item.costoActivo;
                    modelo.anno = item.anno;
                    modelo.meses = item.meses;
                    modelo.costoAnual = item.costoAnual;
                    modelo.costoMensual = item.costoMensual;
                    modelo.fechaActivo = item.fechaActivo;
                    modelo.estadoActivo = item.estadoActivo;

                    listaActivos.Add(modelo);
                }
                return View(listaActivos);
            }
            catch
            {
                string descMsg = "Error consultando la informacion del CLiente.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Activos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Activos/Create
        public ActionResult Crear(Activos activos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsActivos ObjActivo = new clsActivos();
                    bool Resultado = ObjActivo.AgregarActivo(activos.codigoActivo, activos.nombreActivo, activos.costoActivo, activos.anno,
                        activos.meses, activos.fechaActivo, true);

                    if (Resultado)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Crear");
                    }
                }
                else
                {
                    return View("Crear");
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            try
            {
                clsActivos activos = new clsActivos();

                var dato = activos.ConsultaActivo(id);

                Activos modelo = new Activos();
                modelo.IdActivo = dato[0].IdActivo;
                modelo.codigoActivo = dato[0].codigoActivo;
                modelo.nombreActivo = dato[0].nombreActivo;
                modelo.costoActivo = dato[0].costoActivo;
                modelo.anno = dato[0].anno;
                modelo.meses = dato[0].meses;
                modelo.fechaActivo = dato[0].fechaActivo;
                return View(modelo);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // POST: Activos/Edit/5

        [HttpPost]
        public ActionResult Editar(int id, Activos activos)
        {
            try
            {

                clsActivos Objactivos = new clsActivos();
                bool Resultado = Objactivos.ActualizarActivo(activos.IdActivo, activos.codigoActivo, activos.nombreActivo,
                    activos.costoActivo, activos.anno, activos.meses, activos.fechaActivo, true);

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Deshabilitar/5
        public ActionResult Deshabilitar(int id)
        {
            if (Session["ROLES"].Equals("Admin"))
            {
                clsActivos ObjActivo = new clsActivos();
                ObjActivo.DeshabilitarActivo(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }
    }
}
