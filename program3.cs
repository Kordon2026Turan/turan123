using System;
using System.Drawing;
using System.Windows.Forms;

namespace TelebeQeydiyyat
{
    public class Form3 : Form
    {
        private TextBox txtA;
        private TextBox txtB;
        private Button btnAdd;
        private Button btnSubtract;
        private Button btnMultiply;
        private Button btnDivide;
        private Label lblResult;
        private Label lblNum1;
        private Label lblNum2;

        public Form3()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Kalkulyator - Tapşırıq 3";
            this.Size = new Size(380, 260);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Ədəd 1 Label və TextBox
            lblNum1 = new Label { Text = "Ədəd 1:", Location = new Point(20, 20), AutoSize = true };
            txtA = new TextBox { Location = new Point(100, 17), Width = 230 };

            // Ədəd 2 Label və TextBox
            lblNum2 = new Label { Text = "Ədəd 2:", Location = new Point(20, 60), AutoSize = true };
            txtB = new TextBox { Location = new Point(100, 57), Width = 230 };

            // Düymələr
            btnAdd = new Button { Text = "+", Location = new Point(100, 100), Width = 50, Height = 35, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnSubtract = new Button { Text = "−", Location = new Point(160, 100), Width = 50, Height = 35, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnMultiply = new Button { Text = "×", Location = new Point(220, 100), Width = 50, Height = 35, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnDivide = new Button { Text = "÷", Location = new Point(280, 100), Width = 50, Height = 35, Font = new Font("Segoe UI", 12, FontStyle.Bold) };

            // Event handler-lərin bağlanması
            btnAdd.Click += BtnAdd_Click;
            btnSubtract.Click += BtnSubtract_Click;
            btnMultiply.Click += BtnMultiply_Click;
            btnDivide.Click += BtnDivide_Click;

            // Nəticə Label-i
            lblResult = new Label
            {
                Text = "Nəticə: ",
                Location = new Point(20, 160),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            // Kontrolların Form-a əlavə edilməsi
            this.Controls.Add(lblNum1);
            this.Controls.Add(txtA);
            this.Controls.Add(lblNum2);
            this.Controls.Add(txtB);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSubtract);
            this.Controls.Add(btnMultiply);
            this.Controls.Add(btnDivide);
            this.Controls.Add(lblResult);
        }

        // int.TryParse ilə daxiletməni yoxlayan metod
        private bool ValidateInputs(out int num1, out int num2)
        {
            bool isAValid = int.TryParse(txtA.Text, out num1);
            bool isBValid = int.TryParse(txtB.Text, out num2);

            if (!isAValid || !isBValid)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Xəbərdarlıq: Xahiş olunur düzgün tam ədəd daxil edin!";
                return false;
            }

            lblResult.ForeColor = Color.DarkBlue;
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs(out int a, out int b))
            {
                lblResult.Text = "Nəticə: " + (a + b);
            }
        }

        private void BtnSubtract_Click(object sender, EventArgs e)
        {
            if (ValidateInputs(out int a, out int b))
            {
                lblResult.Text = "Nəticə: " + (a - b);
            }
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            if (ValidateInputs(out int a, out int b))
            {
                lblResult.Text = "Nəticə: " + (a * b);
            }
        }

        private void BtnDivide_Click(object sender, EventArgs e)
        {
            if (ValidateInputs(out int a, out int b))
            {
                if (b == 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Sıfıra bölmək olmaz!";
                }
                else
                {
                    lblResult.Text = "Nəticə: " + ((double)a / b);
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());
        }
    }
}
