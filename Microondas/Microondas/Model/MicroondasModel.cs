using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microondas.Model
{
    /// <summary>
    /// Modelo das funções do microondas
    /// </summary>
    class MicroondasModel
    {
        public string Potencia { get; set; }
        public string Tempo { get; set; }
        public string Visor { get; set; }
        public int PreConfigurada { get; set; }

    }

    /// <summary>
    /// Enumerador aquecimentos pré-configuradas
    /// </summary>
    class Enumerador
    {
        public enum PreConfiguradas
        {
            Lasanha = 1,
            Pizza = 2,
            Arroz = 3,
            Batata = 4,
            Reaquecer = 5,
            Frango = 6
        }
    }

    /// <summary>
    /// Valores padrões de aquecimentos
    /// </summary>
    class PreConfiguradas
    {
        public string TempoFrango
        {
            get
            {
                return "80";
            }
        }
        public string PotenciaFrango
        {
            get
            {
                return "8";
            }
        }
        public string TempoLasanha
        {
            get
            {
                return "120";
            }
        }
        public string PotenciaLasanha
        {
            get
            {
                return "10";
            }
        }
        public string TempoPizza
        {
            get
            {
                return "110";
            }
        }
        public string PotenciaPizza
        {
            get
            {
                return "9";
            }
        }
        public string TempoArroz
        {
            get
            {
                return "85";
            }
        }
        public string PotenciaArroz
        {
            get
            {
                return "7";
            }
        }
        public string TempoBatata
        {
            get
            {
                return "90";
            }
        }
        public string PotenciaBatata
        {
            get
            {
                return "5";
            }
        }
        public string TempoReaquecer
        {
            get
            {
                return "30";
            }
        }
        public string PotenciaReaquecer
        {
            get
            {
                return "3";
            }
        }
    }
}
