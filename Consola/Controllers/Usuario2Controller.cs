using BLL;
using Consola.Helpers;
using Consola.Models;
using Consola.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class Usuario2Controller : Controller
    {
        // GET: Usuario2
        public ActionResult Index()
        {
            try
            {
                List<Usuario> listaUsuario = new List<Usuario>();
                clsUsuario usuario = new clsUsuario();
                var data = usuario.ConsultarUsuarios().ToList();

                foreach (var item in data)
                {
                    Usuario modelo = new Usuario();
                    modelo.idUsuario = item.idUsuario;
                    modelo.idTipoRol = item.idTipoRol;
                    modelo.UserName = item.usuario;
                    modelo.Password = item.clave;
                    modelo.Estado = item.estado;

                    listaUsuario.Add(modelo);

                }
                return View(listaUsuario);
            }
            catch
            {
                string descMsg = "Error.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            clsUsuario ObjUsuario = new clsUsuario();
            ViewBag.Lista = ObjUsuario.ConsultarRoles().ToList();
            try
            {
                clsUsuario usuario = new clsUsuario();
                var dato = usuario.ConsultaUsuario(id);

                Usuario modelo = new Usuario();
                modelo.idUsuario = dato[0].idUsuario;
                modelo.idTipoRol = dato[0].idTipoRol;
                modelo.UserName = dato[0].usuario;
                modelo.Password = dato[0].clave;
                modelo.Estado = dato[0].estado;
                return View(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Activos/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, Usuario usuario)
        {
            try
            {
                clsUsuario ObjUsuario = new clsUsuario();

                bool Resultado = ObjUsuario.ActualizarEditarUsuario(usuario.idUsuario, usuario.idTipoRol, usuario.UserName,
                usuario.Password, true);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Proveedor/Deshabilitar/5
        public ActionResult Deshabilitar(int id)
        {
            if (Session["ROLES"].Equals("Admin"))
            {
                clsUsuario ObjUsuario = new clsUsuario();
                ObjUsuario.DeshabilitarUsuario(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Crear()
        {
            clsUsuario ObjUsuario = new clsUsuario();
            ViewBag.Lista = ObjUsuario.ConsultarRoles().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(string txtNombreUsuario, int listIdRol, string txtContrasena, string txtConfirmarContrasena)
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
                                return RedirectToAction("Crear");
                            }
                            else
                            {
                                return View("Crear");
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
                        return RedirectToAction("Crear");
                    }
                }
                else
                {
                    TempData["errorMensaje"] = "Las contraseñas ingresadas en los campos no coinciden.";
                    return RedirectToAction("Crear");
                }
            }
            catch (Exception)
            {
                TempData["errorMensaje"] = "Todos los campos son obligatorios.";
                return Crear();
            }
        }
    }
}