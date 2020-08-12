using BLL;
using Consola.Helpers;
using Consola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            try
            {
                List<Stock> listaStock = new List<Stock>();
                clsStock stock = new clsStock();
                var data = stock.ConsultarStock().ToList();

                foreach (var item in data)
                {
                    Stock modelo = new Stock();
                    modelo.idStock = item.idStock;
                    modelo.codigo = item.idBodega;
                    modelo.nombreProducto = item.nombreProducto;
                    modelo.unidad = item.unidad;
                    modelo.codigoBodega = item.codigoBodega;
                    modelo.idProveedor = item.idProveedor;
                    modelo.estadoStock = item.estadoStock;

                    listaStock.Add(modelo);
                }
                return View(listaStock);
            }
            catch
            {
                string descMsg = "Error consultando la informacion del CLiente.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Stock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proveedores/Create
        public ActionResult Crear()
        {
            clsStock ObjStock = new clsStock();
            ViewBag.Lista = ObjStock.ConsultarNombreBodega().ToList();
            ViewBag.Lista2 = ObjStock.ConsultarIdProveedor().ToList();
            return View();
        }

        // GET: Stock/Create
        [HttpPost]
        public ActionResult Crear(Stock stocks, string txtCodigoBodega, int listIdProveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsStock objstock = new clsStock();

                    int IdBodega = objstock.ConsultaIdBodega(txtCodigoBodega);

                    bool Resultado = objstock.AgregarStock(IdBodega, stocks.nombreProducto, stocks.unidad, txtCodigoBodega, listIdProveedor, true);

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
                clsStock ObjStock = new clsStock();
                ViewBag.Lista = ObjStock.ConsultarNombreBodega().ToList();
                ViewBag.Lista2 = ObjStock.ConsultarIdProveedor().ToList();
                TempData["errorMensaje"] = "Inserte correctamente el formato de los datos.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            try
            {
                clsStock ObjStock = new clsStock();

                ViewBag.Lista = ObjStock.ConsultarNombreBodega().ToList();
                ViewBag.Lista2 = ObjStock.ConsultarIdProveedor().ToList();

                var dato = ObjStock.ConsultaStock(id);

                Stock modelo = new Stock();
                modelo.idStock = dato[0].idStock;
                modelo.codigo = dato[0].idBodega;
                modelo.nombreProducto = dato[0].nombreProducto;
                modelo.unidad = dato[0].unidad;
                modelo.codigoBodega = dato[0].codigoBodega;
                modelo.idProveedor = dato[0].idProveedor;
                modelo.estadoStock = dato[0].estadoStock;
                return View(modelo);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // POST: Activos/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, Stock stock, string txtCodigoBodega, int listIdProveedor)
        {
            try
            {
                clsStock ObjStock = new clsStock();
                ViewBag.Lista = ObjStock.ConsultarNombreBodega().ToList();
                ViewBag.Lista2 = ObjStock.ConsultarIdProveedor().ToList();

                int IdBodega = ObjStock.ConsultaIdBodega(txtCodigoBodega);

                bool Resultado = ObjStock.ActualizarStock(stock.idStock, IdBodega, stock.nombreProducto,
                    stock.unidad, txtCodigoBodega, listIdProveedor, true);
                return View();
            }
            catch
            {
                clsStock ObjStock = new clsStock();
                ViewBag.Lista = ObjStock.ConsultarNombreBodega().ToList();
                ViewBag.Lista2 = ObjStock.ConsultarIdProveedor().ToList();
                TempData["errorMensaje"] = "Inserte correctamente el formato de los datos.";
                return View();
            }
        }

        // GET: Proveedor/Deshabilitar/5
        public ActionResult Deshabilitar(int id)
        {
            if (Session["ROLES"].Equals("Admin"))
            {
                clsStock ObjStock = new clsStock();
                ObjStock.DeshabilitarStock(id);
                return RedirectToAction("Index");
            }
            TempData["alertaMensaje"] = "El usuario con el que ha ingresado no tiene permiso para realizar esta operación.";
            return RedirectToAction("Index");
        }
    }
}
