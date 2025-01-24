using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System.Web;
using System.Text.Json;

namespace Controller
{
    public partial class MainForm : Form
    {
        Main service = new Main();
        public void SetCategorySearch()
        {
            var category = service.GetCategorySearch();
            foreach (var key in category.Keys)
            {
                treeView_Category.Nodes.Add(new TreeNode(key));
                foreach (var value in category[key])
                {
                    treeView_Category.Nodes[treeView_Category.Nodes.Count - 1].Nodes.Add(new TreeNode(value));
                }
            }
            treeView_Category.CheckBoxes = true;
        }
        private void SetCategorySort()
        {
            var sorts = service.GetCategorySort();
            foreach (var sort in sorts)
            {
                listBox_Sort.Items.Add(sort);
            }
        }
        public MainForm()
        {
            InitializeComponent();
            SetCategorySearch();
            SetCategorySort();
        }
        private async void Show_Cars()
        {

            Dictionary<string, List<string>> search = new Dictionary<string, List<string>>();
            foreach (TreeNode category in treeView_Category.Nodes)
            {
                if (category.Checked)
                {
                    search[category.Text] = new List<string>();
                    foreach (TreeNode condition in category.Nodes)
                    {
                        if (condition.Checked)
                        {
                            search[category.Text].Add(condition.Text);
                        }
                    }
                }
            }
            string sort;
            if (listBox_Sort.SelectedIndex != -1)
            {
                sort = listBox_Sort.Items[listBox_Sort.SelectedIndex].ToString();
            }
            else
            {
                sort = "";
            }

            var cars = service.Search_Cars(search, sort);

            int size_x = 200;
            int size_y = 300;
            int x = cars.Count * (size_x + 6);
            int panel_size_x = Math.Min(this.ClientSize.Width - 20, x);

            flowLayoutPanel.AutoScroll = true; // Отключаем прокрутку
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown; // Устанавливаем направление потока
            //flowLayoutPanel.BackColor = Color.Transparent;
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.Visible = false;
            flowLayoutPanel.Location = new Point(10, this.ClientSize.Height - size_y - 24);
            flowLayoutPanel.Size = new Size(panel_size_x, size_y + 30);

            // Создаем карточки товаров
            int pos = 0;
            foreach (var car in cars)
            {
                Panel productPanel = new Panel
                {
                    Size = new Size(size_x, size_y),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                };

                // Добавляем изображение
                Image image;
                using (var client = new HttpClient())
                {
                    var url = "https://localhost:7223/ImageLoader/FromURL/" + HttpUtility.UrlEncode(car["image"]);
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var stream = JsonSerializer.Deserialize<string>(json);
                    byte[] imageBytes = Convert.FromBase64String(stream);
                    using (MemoryStream ms1 = new MemoryStream(imageBytes))
                    {
                       image = Image.FromStream(ms1);
                    }
                }
                PictureBox pictureBox = new PictureBox
                {
                    Image = image, // Загрузите изображение
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(180, 100),
                    Location = new Point(10, 10)
                };
                pictureBox.BringToFront();
                productPanel.Controls.Add(pictureBox);

                // Добавляем название продукта
                Label nameLabel = new Label
                {
                    Text = car["Бренд"],
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 120),
                    AutoSize = true
                };
                productPanel.Controls.Add(nameLabel);

                // Добавляем описание продукта
                string my_text = "";
                List<string> uses_columns = new List<string>
                             {
                            "Бренд",
                            "Тип Кузова",
                            "Цвет",
                            "Коробка",
                            "Расход",
                            "GPS"
                        };
                foreach (var column in uses_columns)
                {
                    my_text += column + "| " + car[column] + "\n";
                }
                Label descriptionLabel = new Label
                {
                    Text = my_text,
                    Location = new Point(10, 150),
                    AutoSize = true
                };
                productPanel.Controls.Add(descriptionLabel);

                // Добавляем цену продукта
                Label priceLabel = new Label
                {
                    Text = car["Цена"] + " ₽",
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Green,
                    Location = new Point(10, 155 + 20 * uses_columns.Count),
                    AutoSize = true
                };
                productPanel.Controls.Add(priceLabel);

                productPanel.Location = new Point(10 + (size_x + 10) * pos, size_y);
                productPanel.BackColor = SystemColors.InactiveCaption;
                flowLayoutPanel.Controls.Add(productPanel);
            }
            flowLayoutPanel.Visible = true;
        }
        private void button_show_checkedListBox_Category_Click(object sender, EventArgs e)
        {
            treeView_Category.BringToFront();
            treeView_Category.Visible = !treeView_Category.Visible;
        }
        private void button_Search_Click(object sender, EventArgs e)
        {
            Show_Cars();
            flowLayoutPanel.Visible = true;
        }
        private bool isMouse = true;
        public void treeView_Category_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (isMouse)
            {
                if (e.Node.Nodes.Count != 0)
                {
                    if (e.Node.Checked)
                    {
                        isMouse = false;
                        foreach (TreeNode node in e.Node.Nodes)
                        {
                            node.Checked = false;
                        }
                        isMouse = true;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (!e.Node.Checked)
                    {
                        isMouse = false;
                        e.Node.Parent.Checked = true;
                        isMouse = true;
                    }
                }
            }


        }

        private void treeView_Category_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        public void treeView_Category_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.Checked && isMouse && e.Node.Nodes.Count == 0)
            {
                bool f = false;
                foreach (TreeNode node in e.Node.Parent.Nodes)
                {
                    f = f || node.Checked;
                }
                isMouse = false;
                e.Node.Parent.Checked = f;
                isMouse = true;
            }
        }
        private void button_Sort_Click(object sender, EventArgs e)
        {
            listBox_Sort.Visible = !listBox_Sort.Visible;
        }

        private void button_ShowAdminLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminLoginForm().ShowDialog();
            this.Show();
        }
    }
}
