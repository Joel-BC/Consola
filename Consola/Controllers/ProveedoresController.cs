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
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public ActionResult Index()
        {
            try
            {
                List<Proveedores> listaProveedores = new List<Proveedores>();
                clsProveedores proveedor = new clsProveedores();
                var data = proveedor.ConsultarProveedor().ToList();

                foreach (var item in data)
                {
                    Proveedores modelo = new Proveedores();
                    modelo.idProveedor = item.idProveedor;
                    modelo.nombreProveedor = item.nombreProveedor;
                    modelo.telefono = item.telefono;
                    modelo.direccion = item.direccion;
                    modelo.correoElectronico = item.correoElectronico;
                    modelo.nombreContacto = item.nombreContacto;
                    modelo.estadoProveedor = item.estadoProveedor;

                    listaProveedores.Add(modelo);
                }
                return View(listaProveedores);
            }
            catch
            {
                string descMsg = "Error consultando la informacion del CLiente.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proveedores/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        public ActionResult Crear(Proveedores proveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsProveedores objproveedor = new clsProveedores();
                    bool Resultado = objproveedor.AgregarProveedor(proveedor.nombreProveedor, proveedor.telefono, proveedor.direccion,
                        proveedor.correoElectronico, proveedor.nombreContacto, true);

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
                clsProveedores proveedores = new clsProveedores();
                var dato = proveedores.ConsultaProveedor(id);

                Proveedores modelo = new Proveedores();
                modelo.idProveedor = dato[0].idProveedor;
                modelo.nombreProveedor = dato[0].nombreProveedor;
                modelo.telefono = dato[0].telefono;
                modelo.direccion = dato[0].direccion;
                modelo.correoElectronico = dato[0].correoElectronico;
                modelo.nombreContacto = dato[0].nombreContacto;
                modelo.estadoProveedor = dato[0].estadoProveedor;
                return View(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Activos/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, Proveedores proveedores)
        {
            try
            {
                clsProveedores Objproveedores = new clsProveedores();
                bool Resultado = Objproveedores.ActualizarProveedor(proveedores.idProveedor, proveedores.nombreProveedor, proveedores.telefono,
                    proveedores.direccion, proveedores.correoElectronico, proveedores.nombreContacto, true);
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
                clsProveedores ObjProveedor = new clsProveedores();
                ObjProveedor.DeshabilitarProveedor(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }
    }
}
