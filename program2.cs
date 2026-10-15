using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace TelebeQeydiyyat
{
    public class Form2 : Form
    {
        private RichTextBox txtNote;
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblCharCount;

        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private FontDialog fontDialog;
        private ColorDialog colorDialog;

        private bool isModified = false;
        private string currentFilePath = string.Empty;

        public Form2()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Qeydlər Dəftəri - Tapşırıq 2";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Dialoqların yaradılması
            openFileDialog = new OpenFileDialog { Filter = "Mətn Faylları (*.txt)|*.txt|Bütün Fayllar (*.*)|*.*" };
            saveFileDialog = new SaveFileDialog { Filter = "Mətn Faylları (*.txt)|*.txt|Bütün Fayllar (*.*)|*.*" };
            fontDialog = new FontDialog();
            colorDialog = new ColorDialog();

            // MenuStrip
            menuStrip = new MenuStrip();

            // Fayl Menyusu
            ToolStripMenuItem menuFile = new ToolStripMenuItem("Fayl");
            ToolStripMenuItem itemNew = new ToolStripMenuItem("Yeni", null, ItemNew_Click);
            ToolStripMenuItem itemOpen = new ToolStripMenuItem("Aç", null, ItemOpen_Click);
            ToolStripMenuItem itemSave = new ToolStripMenuItem("Saxla", null, ItemSave_Click);
            ToolStripMenuItem itemExit = new ToolStripMenuItem("Çıxış", null, ItemExit_Click);
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { itemNew, itemOpen, itemSave, new ToolStripSeparator(), itemExit });

            // Format Menyusu
            ToolStripMenuItem menuFormat = new ToolStripMenuItem("Format");
            ToolStripMenuItem itemFont = new ToolStripMenuItem("Şrift", null, ItemFont_Click);
            ToolStripMenuItem itemColor = new ToolStripMenuItem("Rəng", null, ItemColor_Click);
            menuFormat.DropDownItems.AddRange(new ToolStripItem[] { itemFont, itemColor });

            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuFormat });

            // RichTextBox (Mətn sahəsi)
            txtNote = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11)
            };
            txtNote.TextChanged += TxtNote_TextChanged;

            // StatusStrip (Aşağı panel)
            statusStrip = new StatusStrip();
            lblCharCount = new ToolStripStatusLabel("Simvol sayı: 0");
            statusStrip.Items.Add(lblCharCount);

            // Form elementlərinin əlavə edilməsi
            this.Controls.Add(txtNote);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            this.FormClosing += Form2_FormClosing;
        }

        // Simvol sayının canlı yenilənməsi
        private void TxtNote_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            lblCharCount.Text = "Simvol sayı: " + txtNote.Text.Length;
        }

        // Saxlamaq sorğusu (YesNoCancel)
        private bool PromptSaveIfModified()
        {
            if (!isModified) return true;

            DialogResult result = MessageBox.Show(
                "Mətndə dəyişikliklər edilib. Saxlamaq istəyirsiniz?",
                "Xəbərdarlıq",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ItemSave_Click(null, null);
                return !isModified; // Əgər saxlanıldısa, davam et
            }
            else if (result == DialogResult.No)
            {
                return true; // Saxlamadan davam et
            }

            return false; // Cancel seçilərsə ləğv et
        }

        // Menyuların Eventləri
        private void ItemNew_Click(object sender, EventArgs e)
        {
            if (PromptSaveIfModified())
            {
                txtNote.Clear();
                currentFilePath = string.Empty;
                isModified = false;
                lblCharCount.Text = "Simvol sayı: 0";
            }
        }

        private void ItemOpen_Click(object sender, EventArgs e)
        {
            if (PromptSaveIfModified())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtNote.Text = File.ReadAllText(openFileDialog.FileName);
                    currentFilePath = openFileDialog.FileName;
                    isModified = false;
                    lblCharCount.Text = "Simvol sayı: " + txtNote.Text.Length;
                }
            }
        }

        private void ItemSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            File.WriteAllText(currentFilePath, txtNote.Text);
            isModified = false;
            MessageBox.Show("Fayl uğurla saxlanıldı!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ItemFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = txtNote.SelectionFont ?? txtNote.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (txtNote.SelectionLength > 0)
                    txtNote.SelectionFont = fontDialog.Font;
                else
                    txtNote.Font = fontDialog.Font;
            }
        }

        private void ItemColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = txtNote.SelectionColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (txtNote.SelectionLength > 0)
                    txtNote.SelectionColor = colorDialog.Color;
                else
                    txtNote.ForeColor = colorDialog.Color;
            }
        }

        private void ItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PromptSaveIfModified())
            {
                e.Cancel = true; // Bağlanmanı ləğv et
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
