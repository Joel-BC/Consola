using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class clsSancion
    {
        public List<ConsultarSancionResult> ConsultarSancion()
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultarSancionResult> data = dc.ConsultarSancion().ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ConsultaSancionResult> ConsultaSancion(int idSanciones)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultaSancionResult> data = dc.ConsultaSancion(idSanciones).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AgregarSancion(int idEmpleado, int idDepartamento, DateTime fechaSancion, string codigo, string nombre, string departamento, string detalle, bool estadoSancion)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = Convert.ToInt32(dc.AgregarSancion(idEmpleado, idDepartamento, fechaSancion, codigo, nombre, departamento, detalle, estadoSancion));

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

        public bool ActualizarSancion(int idSanciones, int idEmpleado, int idDepartamento, DateTime fechaSancion, string codigo, string nombre, string departamento, string detalle, bool estadoSancion)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = dc.ActualizarSancion(idSanciones, idEmpleado, idDepartamento, fechaSancion, codigo, nombre, departamento, detalle, estadoSancion);
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

        public bool DeshabilitarSancion(int IdSancion)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                dc.DeshabilitarSancion(IdSancion);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
