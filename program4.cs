using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TelebeQeydiyyat
{
    public class Form4 : Form
    {
        private Label lblQuestion;
        private GroupBox grpOptions;
        private RadioButton rdbA;
        private RadioButton rdbB;
        private RadioButton rdbC;
        private RadioButton rdbD;
        private Button btnNext;
        private ProgressBar progressBar;
        private Label lblProgress;

        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public Form4()
        {
            InitializeQuestions();
            InitializeComponents();
            LoadQuestion();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
            {
                new Question("1. C# dilində tam ədəd tipi hansıdır?", new string[] { "string", "int", "double", "bool" }, 1),
                new Question("2. WinForms-da şərt seçimi üçün hansı kontrol istifadə olunur?", new string[] { "RadioButton", "TextBox", "Label", "ProgressBar" }, 0),
                new Question("3. Dövr operatoru hansıdır?", new string[] { "if", "switch", "for", "try" }, 2),
                new Question("4. Mətnləri birləşdirmək üçün hansı operator istifadə olunur?", new string[] { "+", "-", "*", "/" }, 0),
                new Question("5. OOP-nin əsas sütunlarından biri hansıdır?", new string[] { "Compilation", "Encapsulation", "Execution", "Debugging" }, 1)
            };
        }

        private void InitializeComponents()
        {
            this.Text = "Test Proqramı - Tapşırıq 4";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Sual Label
            lblQuestion = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(440, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            // GroupBox və RadioButton-lar
            grpOptions = new GroupBox
            {
                Text = "Cavab variantları",
                Location = new Point(20, 70),
                Size = new Size(440, 170)
            };

            rdbA = new RadioButton { Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10) };
            rdbB = new RadioButton { Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10) };
            rdbC = new RadioButton { Location = new Point(20, 90), AutoSize = true, Font = new Font("Segoe UI", 10) };
            rdbD = new RadioButton { Location = new Point(20, 120), AutoSize = true, Font = new Font("Segoe UI", 10) };

            grpOptions.Controls.Add(rdbA);
            grpOptions.Controls.Add(rdbB);
            grpOptions.Controls.Add(rdbC);
            grpOptions.Controls.Add(rdbD);

            // ProgressBar
            progressBar = new ProgressBar
            {
                Location = new Point(20, 260),
                Size = new Size(440, 23),
                Minimum = 0,
                Maximum = questions.Count
            };

            lblProgress = new Label
            {
                Location = new Point(20, 290),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };

            // Növbəti Düyməsi
            btnNext = new Button
            {
                Text = "Növbəti",
                Location = new Point(340, 315),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightSkyBlue
            };
            btnNext.Click += BtnNext_Click;

            // Form-a əlavə edilməsi
            this.Controls.Add(lblQuestion);
            this.Controls.Add(grpOptions);
            this.Controls.Add(progressBar);
            this.Controls.Add(lblProgress);
            this.Controls.Add(btnNext);
        }

        private void LoadQuestion()
        {
            ClearSelections();
            Question q = questions[currentQuestionIndex];
            lblQuestion.Text = q.Text;
            rdbA.Text = "A) " + q.Options[0];
            rdbB.Text = "B) " + q.Options[1];
            rdbC.Text = "C) " + q.Options[2];
            rdbD.Text = "D) " + q.Options[3];

            progressBar.Value = currentQuestionIndex;
            lblProgress.Text = $"Gedişat: {currentQuestionIndex} / {questions.Count} sual tamamlandı";

            if (currentQuestionIndex == questions.Count - 1)
            {
                btnNext.Text = "Bitir";
            }
            else
            {
                btnNext.Text = "Növbəti";
            }
        }

        private void ClearSelections()
        {
            rdbA.Checked = false;
            rdbB.Checked = false;
            rdbC.Checked = false;
            rdbD.Checked = false;
        }

        private int GetSelectedIndex()
        {
            if (rdbA.Checked) return 0;
            if (rdbB.Checked) return 1;
            if (rdbC.Checked) return 2;
            if (rdbD.Checked) return 3;
            return -1;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedIndex();

            if (selectedIndex == -1)
            {
                MessageBox.Show("Xahiş olunur bir cavab variantı seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedIndex == questions[currentQuestionIndex].CorrectAnswerIndex)
            {
                score += 20; // Hər sual 20 bal (Cəmi 100 bal)
            }

            currentQuestionIndex++;

            if (currentQuestionIndex < questions.Count)
            {
                LoadQuestion();
            }
            else
            {
                progressBar.Value = questions.Count;
                lblProgress.Text = $"Gedişat: {questions.Count} / {questions.Count} sual tamamlandı";

                DialogResult result = MessageBox.Show(
                    $"Test bitdi!\nSizin topladığınız bal: {score} / 100\n\nYenidən başlamaq istəyirsiniz?",
                    "Nəticə",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    RestartQuiz();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void RestartQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
            LoadQuestion();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form4());
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string text, string[] options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
