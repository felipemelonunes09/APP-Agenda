using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public enum estado 
    {
        incluir,
        alterar
    }

    public partial class frmAgendaContatos : Form
    {

        private estado acao;

        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            HabilitarBotoesSalvarCancelar(false);
            HabilitarBotoesIncluirAlterarExcluir(true);

            // DEFINIR OS ELEMENTOS DO LISTBOX

            CarregarListBox();
        }

        private void HabilitarBotoesSalvarCancelar(bool validador) 
        {
            btnSalvar.Enabled = validador;
            btnCancelar.Enabled = validador;

            HabilitarCampos(validador);
        }

        private void HabilitarBotoesIncluirAlterarExcluir(bool validador) 
        {
            btnIncluir.Enabled = validador;
            btnAlterar.Enabled = validador;
            btnExcluir.Enabled = validador;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            LimparCampos();

            HabilitarBotoesIncluirAlterarExcluir(false);
            HabilitarBotoesSalvarCancelar(true);

            acao = estado.incluir;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            HabilitarBotoesIncluirAlterarExcluir(false);
            HabilitarBotoesSalvarCancelar(true);

            acao = estado.alterar;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja exluir este contato? ", "Excluir", MessageBoxButtons.YesNo) == DialogResult.Yes)  // PERGUNTA AO USUARIO SE REALMENTEE DESEJA EXCLUIR
            {
                int indice = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indice);

                List<Contato> contatos = ObterLista();
                ManipuladorArquivos.EscreverArquivo(contatos);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {


            Contato contato= new Contato
            {
                Nome = txbNome.Text.ToString(),
                Email = txbEmail.Text.ToString(),
                Telefone = txbTelefone.Text.ToString()

            };

            List<Contato> contatos = ObterLista();

            switch (acao) 
            {
                case estado.incluir:

                    contatos.Add(contato);
                    break;

                case estado.alterar:

                    int indice = lbxContatos.SelectedIndex;

                    contatos.RemoveAt(indice);
                    contatos.Insert(indice, contato);

                    break;
            }


            ManipuladorArquivos.EscreverArquivo(contatos);

            CarregarListBox();

            HabilitarBotoesIncluirAlterarExcluir(true);
            HabilitarBotoesSalvarCancelar(false);

            LimparCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarBotoesIncluirAlterarExcluir(true);
            HabilitarBotoesSalvarCancelar(false);

            LimparCampos();
        }

        private void CarregarListBox() 
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquivos.LerArquivo().ToArray());

            lbxContatos.SelectedIndex = 0;
        }

        private void LimparCampos() 
        {
            txbEmail.Clear();
            txbNome.Clear();
            txbTelefone.Clear();
        }

        private void HabilitarCampos(bool validador) 
        {
            txbEmail.Enabled = validador;
            txbNome.Enabled = validador;
            txbTelefone.Enabled = validador;
        }

        private void frmAgendaContatos_Load(object sender, EventArgs e)
        {

        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato) lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbTelefone.Text = contato.Telefone;

        }

        private List<Contato> ObterLista() 
        {
            List<Contato> contatos = new List<Contato>();

            foreach(Contato ctt in lbxContatos.Items) 
            {
                contatos.Add(ctt);
            }

            return contatos;
        }
    }
}
