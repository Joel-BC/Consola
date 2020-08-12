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
    public class SancionController : Controller
    {
        // GET: Sancion
        public ActionResult Index()
        {
            try
            {
                List<Sancion> listaSancion = new List<Sancion>();
                clsSancion activo = new clsSancion();
                var data = activo.ConsultarSancion().ToList();

                foreach (var item in data)
                {
                    Sancion modelo = new Sancion();
                    modelo.idSanciones = item.idSanciones;
                    modelo.idEmpleado = item.IdEmpleado;
                    modelo.idDepartamento = item.idDepartamento;
                    modelo.fechaSancion = (DateTime)item.fechaSancion;
                    modelo.codigo = item.codigo;
                    modelo.nombre = item.nombre;
                    modelo.departamento = item.departamento;
                    modelo.detalle = item.detalle;
                    modelo.estadoSancion = item.estadoSancion;

                    listaSancion.Add(modelo);
                }
                return View(listaSancion);
            }
            catch
            {
                string descMsg = "Error consultando la informacion del CLiente.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Sancion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sancion/Create
        public ActionResult Crear(Sancion sancion, string txtDetalleSancion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsSancion ObjSancion = new clsSancion();
                    bool Resultado = ObjSancion.AgregarSancion(sancion.idEmpleado, sancion.idDepartamento, sancion.fechaSancion, sancion.codigo,
                        sancion.nombre, sancion.departamento, txtDetalleSancion, true);

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
        public ActionResult Editar(int id, string txtDetalleSancion)
        {
            try
            {
                clsSancion sancion = new clsSancion();

                var dato = sancion.ConsultaSancion(id);

                Sancion modelo = new Sancion();
                modelo.idSanciones = dato[0].idSanciones;
                modelo.idEmpleado = dato[0].IdEmpleado;
                modelo.idDepartamento = dato[0].idDepartamento;
                modelo.fechaSancion = (DateTime)dato[0].fechaSancion;
                modelo.codigo = dato[0].codigo;
                modelo.nombre = dato[0].nombre;
                modelo.departamento = dato[0].departamento;
                modelo.detalle = txtDetalleSancion;
                modelo.estadoSancion = dato[0].estadoSancion;
                return View(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Sancion/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, Sancion sancion)
        {
            try
            {
                clsSancion Objsancion = new clsSancion();
                bool Resultado = Objsancion.ActualizarSancion(sancion.idSanciones, sancion.idEmpleado, sancion.idDepartamento, sancion.fechaSancion, sancion.codigo,
                        sancion.nombre, sancion.departamento, sancion.detalle, true);

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
                clsSancion ObjSancion = new clsSancion();
                ObjSancion.DeshabilitarSancion(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }
    }
}
