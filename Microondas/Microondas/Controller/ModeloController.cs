using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microondas.Model;

namespace Microondas.Controller
{
    class ModeloController
    {
        /// <summary>
        /// Valida potência baseado no Model preenchidom
        /// </summary>
        internal static MicroondasModel ValidarPotencia(MicroondasModel Context)
        {
            Int32.TryParse(Context.Potencia, out int Potencia);
            if (Potencia == 0 || Potencia > 10)
            {
                Context.Potencia = string.Empty;
                Context.Visor = "A potência deve ser maior que 0 e menor que 10!";
                return Context;
            }
            else
            {
                Context.Visor = "Potência Salva!";
                return Context;
            }
        }

        /// <summary>
        /// Valida tempo baseado no Model preenchido
        /// </summary>
        internal static MicroondasModel ValidarTempo(MicroondasModel Context)
        {
            Int32.TryParse(Context.Tempo, out int Tempo);
            if (Tempo == 0 || Tempo > 120)
            {
                Context.Tempo = string.Empty;
                Context.Visor = "O tempo não pode ser 0 ou acima de 120!";
                return Context;
            }
            else
            {
                Context.Visor = "Tempo Salvo!";
                return Context;
            }
        }

        /// <summary>
        /// Valida dados baseado no Model preenchido
        /// </summary>
        internal static MicroondasModel Validar(MicroondasModel Context)
        {
            if (string.IsNullOrEmpty(Context.Tempo))
            {
                Context.Tempo = "30";
            }
            if (string.IsNullOrEmpty(Context.Potencia))
            {
                Context.Potencia = "8";
            }
            Context.Visor = "AQUECENDO!!!";
            return Context;
        }

        internal static MicroondasModel ValidarPreConfiguradas(MicroondasModel Context)
        {
            switch (Context.PreConfigurada)
            {
                case (int)Enumerador.PreConfiguradas.Frango:
                    Context.Potencia = new PreConfiguradas().PotenciaFrango;
                    Context.Tempo = new PreConfiguradas().TempoFrango;
                    break;
                case (int)Enumerador.PreConfiguradas.Lasanha:
                    Context.Potencia = new PreConfiguradas().PotenciaLasanha;
                    Context.Tempo = new PreConfiguradas().TempoLasanha;
                    break;
                case (int)Enumerador.PreConfiguradas.Pizza:
                    Context.Potencia = new PreConfiguradas().PotenciaPizza;
                    Context.Tempo = new PreConfiguradas().TempoPizza;
                    break;
                case (int)Enumerador.PreConfiguradas.Arroz:
                    Context.Potencia = new PreConfiguradas().PotenciaArroz;
                    Context.Tempo = new PreConfiguradas().TempoArroz;
                    break;
                case (int)Enumerador.PreConfiguradas.Batata:
                    Context.Potencia = new PreConfiguradas().PotenciaBatata;
                    Context.Tempo = new PreConfiguradas().TempoBatata;
                    break;
                case (int)Enumerador.PreConfiguradas.Reaquecer:
                    Context.Potencia = new PreConfiguradas().PotenciaReaquecer;
                    Context.Tempo = new PreConfiguradas().TempoReaquecer;
                    break;
            }
            Context.Visor = "AQUECENDO!!!";
            return Context;
        }
    }
}
