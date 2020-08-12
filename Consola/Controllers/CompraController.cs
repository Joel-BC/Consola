using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Consola.Helpers;
using Consola.Models;

namespace Consola.Controllers
{
    [SessionManage]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            try
            {
                List<Compra> listaCompra = new List<Compra>();
                clsCompra compra = new clsCompra();
                var data = compra.ConsultarCompra().ToList();

                foreach (var item in data)
                {
                    Compra modelo = new Compra();
                    modelo.idCompra = item.idCompra;
                    modelo.idColaborador = item.idColaborador;
                    modelo.nombreProducto = item.nombreProducto;
                    modelo.cantidadCompra = item.cantidadCompra;
                    modelo.detalleCompra = item.detalleCompra;
                    modelo.estadoCompra = item.estadoCompra;

                    listaCompra.Add(modelo);
                }
                return View(listaCompra);
            }
            catch
            {
                string descMsg = "Error consultando la informacion de la compra.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            clsCompra ObjCompra = new clsCompra();
            ViewBag.Lista = ObjCompra.ConsultaCorreoProveedor().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(string txtCorreoElectronico, string asunto, string mensaje
            , string txtIdColaborador, string txtNombreProducto, string txtCantidadProducto, string txtDetalleCompra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsCompra objcompra = new clsCompra();
                    bool Resultado = objcompra.AgregarCompra(Int32.Parse(txtIdColaborador), txtNombreProducto,
                        Int32.Parse(txtCantidadProducto), txtDetalleCompra, true);

                    if (Resultado)
                    {
                        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                        mmsg.To.Add(txtCorreoElectronico);
                        asunto = "Orden de compra Tarimas LS";
                        mensaje =

                              "<h1 text-align: center;><b> Orden de compra Tarimas LS </b></h1>" +
                            "<br />" +
                            "<br /> Este es un correo automatizado del sistema de Tarimas LS, a continuación se detalla la siguiente orden de compra: " +
                            "<br />" +
                            "<br /> ********************************************************************************************** " +
                            "<h3 text-align: center;><b> Orden de compra: </b></h3>" +
                            "<br /> Nombre de producto: " + txtNombreProducto +
                            "<br /> Cantidad: " + txtCantidadProducto +
                            "<br /> Detalles: " + txtDetalleCompra +
                            "<br /> ********************************************************************************************** " +
                            "<br />" +
                            "<br /> Tarimas LS S.A. <a href='https://www.tarimasls.com/'> Tarimas LS S.A </a>";

                        mmsg.Subject = asunto;
                        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                        mmsg.Body = mensaje;
                        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                        mmsg.IsBodyHtml = true;
                        mmsg.From = new System.Net.Mail.MailAddress("sistemaASEcorreo@gmail.com"); //En "correo" se tiene que escribir el correo que va a usar el sistema para enviar correos

                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                        cliente.Credentials = new System.Net.NetworkCredential("sistemaASEcorreo@gmail.com", "sisASE-123"); //En "correo" se escribe de nuevo el correo que va a usar el sistema y en contraseña va la contraseña del correo
                                                                                                                            //OJO: cuidado ponen su correo y contraseña aqui y mandan una version del proyecto por accidente con eso
                        cliente.Port = 587;
                        cliente.EnableSsl = true;
                        cliente.Host = "smtp.gmail.com"; //esta dirección de hosteo va a cambiar dependiendo del proveedor de correo electronico que se use en el correo del sistema, en esta caso, esa es la dirección de hosteo de gmail

                        try
                        {
                            cliente.Send(mmsg);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                        TempData["alertaMensaje"] = "La orden de compra ha sido enviada por correo exitosamente.";
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
                clsCompra ObjCompra = new clsCompra();
                ViewBag.Lista = ObjCompra.ConsultaCorreoProveedor().ToList();
                TempData["errorMensaje"] = "Inserte correctamente el formato de los datos.";
                return View();
            }
        }

        // GET: Compra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Compra/Edit/5
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

        // GET: Cotizacion/Delete/5
        public ActionResult Deshabilitar(int id)
        {
            if (Session["ROLES"].Equals("Admin"))
            {
                clsCompra ObjCompra = new clsCompra();
                ObjCompra.DeshabilitarCompra(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }
    }
}
