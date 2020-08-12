using BLL;
using Consola.Helpers;
using Consola.Tools;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class CrearUsuarioController : Controller
    {
        [HttpGet]
        public ActionResult CrearUsuario()
        {
            clsUsuario ObjUsuario = new clsUsuario();
            ViewBag.Lista = ObjUsuario.ConsultarRoles().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(string txtNombreUsuario, int listIdRol, string txtContrasena, string txtConfirmarContrasena)
        {
            try
            {
                if (txtContrasena.Equals(txtConfirmarContrasena))
                {
                    if (!txtNombreUsuario.Equals("") && !txtContrasena.Equals("") && !txtConfirmarContrasena.Equals(""))
                    {
                        if (!ModelState.IsValid)
                        {
                            ModelState.AddModelError("", "Usuario o Password Incorrectos");
                            return View();
                        }
                        else
                        {
                            clsUsuario ObjUsuario = new clsUsuario();

                            var nuevoUsuario = ObjUsuario.ConsultarUsuario1(txtNombreUsuario, Seguridad.Encriptar(txtConfirmarContrasena)).Where(x => x.estado == true);

                            bool Resultado = ObjUsuario.AgregarUsuario(listIdRol,
                                txtNombreUsuario, Seguridad.Encriptar(txtConfirmarContrasena), true);

                            if (Resultado)
                            {
                                TempData["exitoMensaje"] = "El usuario se ha creado exitosamente.";
                                return RedirectToAction("CrearUsuario");
                            }
                            else
                            {
                                return View("CrearUsuario");
                            }
                        }
                        //}
                        //else
                        //{
                        //    return View("CrearUsuario");
                        //}
                    }
                    else
                    {
                        TempData["errorMensaje"] = "Inserte un nombre de usuario.";
                        return RedirectToAction("CrearUsuario");
                    }
                }
                else
                {
                    TempData["errorMensaje"] = "Las contraseñas ingresadas en los campos no coinciden.";
                    return RedirectToAction("CrearUsuario");
                }
            }
            catch (Exception)
            {
                TempData["errorMensaje"] = "Todos los campos son obligatorios.";
                return CrearUsuario();
            }
        }

        // GET: CrearUsuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: CrearUsuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CrearUsuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CrearUsuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CrearUsuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CrearUsuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CrearUsuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CrearUsuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
