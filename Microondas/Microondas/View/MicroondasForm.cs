using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microondas.Controller;
using Microondas.Model;

namespace Microondas
{
    public partial class MicroondasForm : Form
    {
        MicroondasModel Context = new MicroondasModel();
        private bool isPotencia { get; set; }
        private bool isTempo { get; set; }
        private bool isAguardando { get; set; }
        private bool isPause { get; set; }
        private int segundos { get; set; }
        private Timer timer;

        /// <summary>
        /// Inicializar Microondas
        /// </summary>
        public MicroondasForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            Resetar();
        }

        /// <summary>
        /// Evento de Tick de 1 segundo
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isAguardando)
            {
                Titulo_MemoEdit.Text = "DATA / HORA";
                Visor_MemoEdit.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                if (Convert.ToInt32(Context.Tempo) >= segundos && !isPause)
                {
                    int calcTime = segundos * Convert.ToInt32(Context.Potencia) - Context.Visor.Length;
                    for (int i = 0; i < calcTime; i++)
                        Context.Visor += ".";
                    Visor_MemoEdit.Text = Context.Visor;
                    segundos++;
                }
                else if (isPause)
                    Context.Visor = "PAUSE !!!";
                else
                {
                    Visor_MemoEdit.Text = "AQUECIMENTO FINALIZADO !!!";
                    isAguardando = true;
                    DentroMicroondas_Panel.BackColor = Color.FromArgb(224, 224, 224);
                    segundos = 0;
                }
            }
        }

        /// <summary>
        /// Caputa cliques no teclado do microondas
        /// </summary>
        private void Entrada_SimpleButton_Click(object sender, EventArgs e)
        {
            if (!isPotencia && !isTempo)
                Context.Visor = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            else
            {
                timer.Stop();
                SimpleButton button = (SimpleButton)sender;
                switch (button.Text.ToUpper())
                {
                    case "1":
                        Context.Visor += button.Text;
                        break;
                    case "2":
                        Context.Visor += button.Text;
                        break;
                    case "3":
                        Context.Visor += button.Text;
                        break;
                    case "4":
                        Context.Visor += button.Text;
                        break;
                    case "5":
                        Context.Visor += button.Text;
                        break;
                    case "6":
                        Context.Visor += button.Text;
                        break;
                    case "7":
                        Context.Visor += button.Text;
                        break;
                    case "8":
                        Context.Visor += button.Text;
                        break;
                    case "9":
                        Context.Visor += button.Text;
                        break;
                    case "0":
                        Context.Visor += button.Text;
                        break;
                }
            }
            Visor_MemoEdit.Text = Context.Visor;
        }

        /// <summary>
        /// Click no botão desligar
        /// </summary>
        private void Desligar_SimpleButton_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Carregar dados na tela
        /// </summary>
        private void FormPrincipal_Shown(object sender, EventArgs e) => Visor_MemoEdit.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss");

        /// <summary>
        /// Click no botão ligar
        /// </summary>
        private void Ligar_SimpleButton_Click(object sender, EventArgs e)
        {
            isPause = false;
            if (Context.PreConfigurada == 0)
            {
                if (isPotencia)
                {
                    Context.Potencia = Context.Visor;
                    Context = ModeloController.ValidarPotencia(Context);
                    Visor_MemoEdit.Text = Context.Visor;
                    Resetar();
                }
                else if (isTempo || int.TryParse(Context.Visor, out int tmpTempo))
                {
                    Context.Tempo = Context.Visor;
                    Context = ModeloController.ValidarTempo(Context);
                    Visor_MemoEdit.Text = Context.Visor;
                    Resetar();
                }
                else
                {
                    Context = ModeloController.Validar(Context);
                    Aquecendo();
                }
            }
            else
            {
                Context = ModeloController.ValidarPreConfiguradas(Context);
                Aquecendo();
            }
        }

        /// <summary>
        /// Metodo usado quando está aquecendo o microondas
        /// </summary>
        private void Aquecendo()
        {
            DentroMicroondas_Panel.BackColor = Color.FromArgb(255, 255, 128);
            Titulo_MemoEdit.Text = Context.Visor;
            Context.Visor = string.Empty;
            isAguardando = false;
            timer.Start();
        }
        /// <summary>
        /// Click no botão potência
        /// </summary>
        private void Potencia_SimpleButton_Click(object sender, EventArgs e)
        {
            Titulo_MemoEdit.Text = "SELECIONE A POTÊNCIA:";
            timer.Stop();
            isPotencia = true;
            Context.Visor = Visor_MemoEdit.Text = string.Empty;
        }

        /// <summary>
        /// Limpar dados do microondas
        /// </summary>
        /// <param name="isLimparVisor">Limpar dados</param>
        public void Resetar(bool isLimparVisor = false)
        {
            DentroMicroondas_Panel.BackColor = Color.FromArgb(224, 224, 224);
            isPotencia = false;
            isTempo = false;
            isPause = false;
            isAguardando = !isLimparVisor;
            segundos = 0;
            timer.Start();
        }

        /// <summary>
        /// Click no botão tempo
        /// </summary>
        private void Tempo_SimpleButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            isTempo = true;
            Titulo_MemoEdit.Text = "SELECIONE O TEMPO:";
            Context.Visor = Visor_MemoEdit.Text = string.Empty;
        }

        /// <summary>
        /// Click no botão cancelar
        /// </summary>
        private void Cancelar_SimpleButton_Click(object sender, EventArgs e) => Resetar();

        /// <summary>
        /// Evento para chamar as teclas de atalho
        /// </summary>
        private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad1:
                case Keys.D1:
                    Um_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    Dois_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    Tres_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    Quatro_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    Cinco_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    Seis_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    Sete_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    Oito_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    Nove_SimpleButton.PerformClick();
                    break;
                case Keys.NumPad0:
                case Keys.D0:
                    Zero_SimpleButton.PerformClick();
                    break;
                case Keys.Enter:
                    Ligar_SimpleButton.PerformClick();
                    break;
                case Keys.Delete:
                    Cancelar_SimpleButton.PerformClick();
                    break;
                case Keys.Escape:
                    Desligar_SimpleButton.PerformClick();
                    break;
            }
        }

        private void PreProgramadasSimpleButton_Click(object sender, EventArgs e) => AtivarDesativarPreConfiguradas(true);

        private void FecharPreProgramadasSimpleButton_Click(object sender, EventArgs e) => AtivarDesativarPreConfiguradas(false);

        private void AtivarDesativarPreConfiguradas(bool AtivarDesativar)
        {
            PreProgramadasSimpleButton.Size = new Size(321, 46);
            FecharPreProgramadasSimpleButton.Visible = AtivarDesativar;
            FrangoSimpleButton.Visible = AtivarDesativar;
            LasanhaSimpleButton.Visible = AtivarDesativar;
            ArrozSimpleButton.Visible = AtivarDesativar;
            BatataSimpleButton.Visible = AtivarDesativar;
            PizzaSimpleButton.Visible = AtivarDesativar;
            ReaquecerSimpleButton.Visible = AtivarDesativar;
            if (!AtivarDesativar)
            {
                Visor_MemoEdit.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                PreProgramadasSimpleButton.Size = new Size(380, 46);
            }
        }

        private void ExibeTempoPotenciaSimpleButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            PreConfiguradas tmpPreConfiguradas = new PreConfiguradas();
            SimpleButton button = (SimpleButton)sender;
            switch (button.Name)
            {
                case "FrangoSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Frango;
                    Visor_MemoEdit.Text = $"Frango {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoFrango} segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaFrango}";

                    break;
                case "LasanhaSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Lasanha;
                    Visor_MemoEdit.Text = $"Lasanha {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoLasanha} segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaLasanha}";
                    break;
                case "PizzaSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Pizza;
                    Visor_MemoEdit.Text = $"Pizza {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoPizza} segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaPizza}";
                    break;
                case "ArrozSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Arroz;
                    Visor_MemoEdit.Text = $"Arroz {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoArroz} segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaArroz}";
                    break;
                case "BatataSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Batata;
                    Visor_MemoEdit.Text = $"Batata {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoBatata} segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaBatata}";
                    break;
                case "ReaquecerSimpleButton":
                    Context.PreConfigurada = (int)Enumerador.PreConfiguradas.Reaquecer;
                    Visor_MemoEdit.Text = $"Reaquecer {Environment.NewLine}Tempo: {tmpPreConfiguradas.TempoReaquecer}segundos {Environment.NewLine}Potência: {tmpPreConfiguradas.PotenciaReaquecer}";
                    break;
                default:
                    Visor_MemoEdit.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    break;
            }
        }

        private void PausarSimpleButton_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void Pause()
        {
            isPause = true;
            Titulo_MemoEdit.Text = "PAUSADO!!!";
            timer.Stop();
        }
    }
}
