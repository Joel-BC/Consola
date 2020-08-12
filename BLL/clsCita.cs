using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class clsCita
    {
        public List<ConsultarCitaResult> ConsultarCita()
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultarCitaResult> data = dc.ConsultarCita().ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<ConsultaCitaResult> ConsultaCita(int Codigo)
        {
            try
            {
                DatosDataContext dc = new DatosDataContext();
                List<ConsultaCitaResult> data = dc.ConsultaCita(Codigo).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool AgregarCita(string Asunto, string Descripcion, DateTime Inicio, DateTime Fin, string ColorFonto, bool DiaCompleto)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = Convert.ToInt32(dc.AgregarCita(Asunto, Descripcion, Inicio, Fin, ColorFonto, DiaCompleto));

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
        public bool ActualizarCita(int IdCita, string Asunto, string Descripcion, DateTime Inicio, DateTime Fin, string ColorFonto, bool DiaCompleto)
        {
            try
            {
                int respuesta = 1;
                DatosDataContext dc = new DatosDataContext();
                respuesta = dc.ActualizarCita(IdCita, Asunto, Descripcion, Inicio, Fin, ColorFonto, DiaCompleto);
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


    }
}
