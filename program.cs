using System;
using System.Windows.Forms;

namespace TelebeQeydiyyat
{
    public class Form1 : Form
    {
        private TextBox txtAd;
        private NumericUpDown numYas;
        private ComboBox cmbKurs;
        private RadioButton rdbKisi;
        private RadioButton rdbQadin;
        private GroupBox grpCins;
        private ListBox lstTelebeler;
        private Button btnElaveEt;
        private Button btnSil;
        private System.Windows.Forms.Label lblSecilmis;
        private System.Windows.Forms.Label lblAd;
        private System.Windows.Forms.Label lblYas;
        private System.Windows.Forms.Label lblKurs;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtAd = new TextBox();
            this.numYas = new NumericUpDown();
            this.cmbKurs = new ComboBox();
            this.rdbKisi = new RadioButton();
            this.rdbQadin = new RadioButton();
            this.grpCins = new GroupBox();
            this.lstTelebeler = new ListBox();
            this.btnElaveEt = new Button();
            this.btnSil = new Button();
            this.lblSecilmis = new System.Windows.Forms.Label();
            this.lblAd = new System.Windows.Forms.Label();
            this.lblYas = new System.Windows.Forms.Label();
            this.lblKurs = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numYas)).BeginInit();
            this.grpCins.SuspendLayout();
            this.SuspendLayout();

            this.lblAd.AutoSize = true;
            this.lblAd.Location = new System.Drawing.Point(20, 20);
            this.lblAd.Text = "Ad:";

            this.txtAd.Location = new System.Drawing.Point(80, 17);
            this.txtAd.Size = new System.Drawing.Size(150, 20);

            this.lblYas.AutoSize = true;
            this.lblYas.Location = new System.Drawing.Point(20, 50);
            this.lblYas.Text = "Yaş:";

            this.numYas.Location = new System.Drawing.Point(80, 48);
            this.numYas.Minimum = 16;
            this.numYas.Maximum = 100;
            this.numYas.Value = 18;
            this.numYas.Size = new System.Drawing.Size(150, 20);

            this.lblKurs.AutoSize = true;
            this.lblKurs.Location = new System.Drawing.Point(20, 80);
            this.lblKurs.Text = "Kurs:";

            this.cmbKurs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbKurs.Items.AddRange(new object[] { "1-ci kurs", "2-ci kurs", "3-cü kurs", "4-cü kurs" });
            this.cmbKurs.Location = new System.Drawing.Point(80, 77);
            this.cmbKurs.Size = new System.Drawing.Size(150, 21);
            this.cmbKurs.SelectedIndex = 0;

            this.grpCins.Controls.Add(this.rdbKisi);
            this.grpCins.Controls.Add(this.rdbQadin);
            this.grpCins.Location = new System.Drawing.Point(23, 110);
            this.grpCins.Size = new System.Drawing.Size(207, 60);
            this.grpCins.Text = "Cins";

            this.rdbKisi.AutoSize = true;
            this.rdbKisi.Checked = true;
            this.rdbKisi.Location = new System.Drawing.Point(15, 25);
            this.rdbKisi.Text = "Kişi";

            this.rdbQadin.AutoSize = true;
            this.rdbQadin.Location = new System.Drawing.Point(100, 25);
            this.rdbQadin.Text = "Qadın";

            this.btnElaveEt.Location = new System.Drawing.Point(23, 180);
            this.btnElaveEt.Size = new System.Drawing.Size(95, 30);
            this.btnElaveEt.Text = "Əlavə et";
            this.btnElaveEt.Click += new EventHandler(this.btnElaveEt_Click);

            this.btnSil.Location = new System.Drawing.Point(135, 180);
            this.btnSil.Size = new System.Drawing.Size(95, 30);
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new EventHandler(this.btnSil_Click);

            this.lstTelebeler.Location = new System.Drawing.Point(250, 17);
            this.lstTelebeler.Size = new System.Drawing.Size(320, 199);
            this.lstTelebeler.SelectedIndexChanged += new EventHandler(this.lstTelebeler_SelectedIndexChanged);

            this.lblSecilmis.AutoSize = true;
            this.lblSecilmis.Location = new System.Drawing.Point(20, 230);
            this.lblSecilmis.Text = "Seçilmiş tələbə: Məlumat yoxdur";

            this.ClientSize = new System.Drawing.Size(590, 260);
            this.Controls.Add(this.lblSecilmis);
            this.Controls.Add(this.lstTelebeler);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnElaveEt);
            this.Controls.Add(this.grpCins);
            this.Controls.Add(this.cmbKurs);
            this.Controls.Add(this.lblKurs);
            this.Controls.Add(this.numYas);
            this.Controls.Add(this.lblYas);
            this.Controls.Add(this.txtAd);
            this.Controls.Add(this.lblAd);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Tələbə Qeydiyyat Sistemi";

            ((System.ComponentModel.ISupportInitialize)(this.numYas)).EndInit();
            this.grpCins.ResumeLayout(false);
            this.grpCins.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnElaveEt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad daxil edin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cins = rdbKisi.Checked ? "Kişi" : "Qadın";
            string telebe = $"{txtAd.Text} | {numYas.Value} yaş | {cmbKurs.SelectedItem} | {cins}";

            lstTelebeler.Items.Add(telebe);
            txtAd.Clear();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstTelebeler.SelectedIndex == -1) return;

            if (MessageBox.Show("Silmək istəyirsiniz?", "Təsdiq", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lstTelebeler.Items.RemoveAt(lstTelebeler.SelectedIndex);
                lblSecilmis.Text = "Seçilmiş tələbə: Məlumat yoxdur";
            }
        }

        private void lstTelebeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTelebeler.SelectedItem != null)
            {
                lblSecilmis.Text = "Seçilmiş tələbə: " + lstTelebeler.SelectedItem.ToString();
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }









}
