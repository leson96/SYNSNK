using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace APPSYNCSNK
{
    public partial class SYNC_SNK : Form
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Timers.Timer timer = new System.Timers.Timer();

        public SYNC_SNK()
        {

            InitializeComponent();
            //Thread.Sleep(60 * 1 * 1000);
            //this.Visible = false;





            //t.Interval = 1000 * 60 * 15; // specify interval time as you want
            //t.Tick += new EventHandler(Form1_Load);


            ////t.A = true;
            //t.Enabled = true;
            //t.Start();
        }

        private void SYNC_SNK_Load(object sender, EventArgs e)
        {
            //Visible = false; // Hide form window.
            //ShowInTaskbar = false; // Remove from taskbar.
            //Opacity = 0;

            //base.OnLoad(e);


            //Visible = false; // Hide form window.
            //ShowInTaskbar = false; // Remove from taskbar.
            //Opacity = 0;

            //base.OnLoad(e);


            txtminutes.Text = "15";
            btnon.PerformClick();
        }

        private void btnon_Click(object sender, EventArgs e)
        {
            int a, n;

            if (String.IsNullOrEmpty(txtminutes.Text))
            {
                txtminutes.Text = "Vui Lòng Nhập Số";
                return;
            }
            else if (!(int.TryParse(txtminutes.Text, out n)))
            {

                txtminutes.Text = "Vui Long Nhap So";
                return;

            }
            else if (Convert.ToInt32(txtminutes.Text) < 15)
            {
                txtminutes.Text = "Vui Long Nhap Lon Hon 15";
                return;
            }

            a = Convert.ToInt32(txtminutes.Text);


            timer.Interval = 1000 * 60 * a;

            timer.AutoReset = true;

            //timer.Elapsed += SYNC_SNK_Load;
            timer.Elapsed += btnon_Click;
            //this.Visible = false;
            ////this.ShowInTaskbar = false;
            //var db = new snkdbEntities();
            //var db2 = new Expert_SNKEntities();
            //var db3 = new snk_interfaceEntities();
            timer.Start();


            syncDATA syncDATA = new syncDATA();
            Thread.Sleep(1000);
            //syncDATA.get_from_ERP_PMSDT2();

            syncDATA.get_from_ERP_Buyer_Info();
            Thread.Sleep(1000);
            syncDATA.get_from_ERP_Supplier_Info();

            syncDATA.get_from_ERP_PO_INFO();

            ////syncDATA.get_from_ERP_PO_DETAIL();

            //Thread.Sleep(1000);
            //syncDATA.get_from_ERP_BOM4();

            //Thread.Sleep(1000);
            //syncDATA.get_from_ERP_BOM_DT4();

            //Thread.Sleep(1000);
            //syncDATA.get_from_ERP_PMS_INFO();


            Thread.Sleep(1000);
            syncDATA.get_from_ERP_Product();

            Thread.Sleep(1000);
            syncDATA.insert_SNK_Material2();
        }

        private void btnoff_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
