using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsStock
    {
        public List<ConsultarStockResult> ConsultarStock()
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultarStockResult> data = dc.ConsultarStock().ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ConsultaStockResult> ConsultaStock(int Codigo)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultaStockResult> data = dc.ConsultaStock(Codigo).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public int ConsultaIdBodega(string codigoBodega)
        //{
        //    try
        //    {
        //        DatosDataContext dc = new DatosDataContext();
        //        List<ConsultaIdBodegaResult> data = dc.ConsultaIdBodega(codigoBodega).ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public List<ConsultarIdProveedorResult> ConsultarIdProveedor()
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultarIdProveedorResult> data = dc.ConsultarIdProveedor().ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ConsultaIdBodega(string codigoBodega)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                int ID = dc.ConsultaIdBodega(codigoBodega);
                return ID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ConsultarNombreBodegaResult> ConsultarNombreBodega()
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultarNombreBodegaResult> data = dc.ConsultarNombreBodega().ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool AgregarStock(int idBodega, string nombreProducto, int unidad, string codigoBodega, int idProveedor, bool estadoStock)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = Convert.ToInt32(dc.AgregarStock(idBodega, nombreProducto, unidad, codigoBodega, idProveedor, estadoStock));

                if (respuesta == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ActualizarStock(int idStock, int idBodega, string nombreProducto, int unidad, string codigoBodega
            , int idProveedor, bool estadoStock)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = dc.ActualizarStock(idStock, idBodega, nombreProducto, unidad, codigoBodega, idProveedor, estadoStock);
                if (respuesta == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeshabilitarStock(int IdStock)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                dc.DeshabilitarStock(IdStock);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
