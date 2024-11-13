using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace depositoBD
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            listaClientes();
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnBorrar.Enabled = false;

        }
        //funcion que carga la lista de clientes
        private void listaClientes()
        {
            string query = "select * from clientes";

            //variable de tipo objetos de memoria genericos
            DataTable datos = new DataTable();
            datos = claseConexion.leerDatos(query);

            //control de tipo grilla 
            dgvClientes.DataSource = datos;
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            txtId.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text= dgvClientes.CurrentRow.Cells[1].Value.ToString();
            txtCiudad.Text= dgvClientes.CurrentRow.Cells[2].Value.ToString();
            cboEstado.Text= dgvClientes.CurrentRow.Cells[3].Value.ToString();

            string estado =cboEstado.Text;

            if (estado =="True")
            {
                cboEstado.Text = "1";
            }
            else
            {
                if (estado == "False") { cboEstado.Text = "0"; }
            }

        }
        private void guardarClientes()
        {
            //variables 
            string nombre = txtNombre.Text;
            string ciudad = txtCiudad.Text;
            int estado = int.Parse(cboEstado.Text);

            string query = "insert into clientes(nombre, ciudad, estado) values ('"+nombre+"','"+ciudad+"',"+estado+")";
            claseConexion.ejecutarSQL(query);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text=="" || txtCiudad.Text=="")
            {
                MessageBox.Show("este campo es vacio");
                return;
            }
            //si no hay campos vacios guardo los datos
            guardarClientes();
            listaClientes();
            txtNombre.Text = "";
            txtCiudad.Text = "";
            cboEstado.Text= "1";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text==""|| txtNombre.Text == "" || txtCiudad.Text == "")
            {
                MessageBox.Show("este campo es vacio");
                return;
            }
            else
            {
                if (MessageBox.Show("MODIFICAR", "Se modificara cliente " + txtNombre.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    editarClientes();
                    listaClientes();
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtCiudad.Text = "";
                    cboEstado.Text = "1";

                }

            }
            
        }
        private void editarClientes()
        {
            //variables 
            int id = int.Parse(txtId.Text);
            string nombre = txtNombre.Text;
            string ciudad = txtCiudad.Text;
            int estado = int.Parse(cboEstado.Text);

            string query = "update clientes set nombre = '"+nombre+"', ciudad ='"+ciudad+"', estado = "+estado+" where id_cliente = "+id+"";
            claseConexion.ejecutarSQL(query);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNombre.Text == "" || txtCiudad.Text == "")
            {
                MessageBox.Show("este campo es vacio");
                return;
            }
            else
            {
                if (MessageBox.Show("!!!ELIMINAR!!!", "Se ELIMINARA cliente " + txtNombre.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    listaClientes();
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtCiudad.Text = "";
                    cboEstado.Text = "1";
                }
            }
        }
        private void eliminarCliente()
        {
            int id = int.Parse(txtId.Text);
            string query = "delete clientes where id_cliente = "+id+"";
            claseConexion.ejecutarSQL(query);
        }

        private void pbxGrabar_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnBorrar.Enabled = false;
            txtId.Text = "";
            txtNombre.Text = "";
            txtCiudad.Text = "";
            cboEstado.Text = "1";
        }

        private void pbxEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnBorrar.Enabled = true;
            btnAgregar.Enabled=false;
        }
    }
}
