using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoya
{
    public partial class NewCourier : Form
    {
        public NewCourier()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text)) // проверка на пустоту
            {
                MessageBox.Show("Пустое поле!", "Ошибка");
                return;
            }
            else
            {
                using (ShoppingContext db = new ShoppingContext())
                {
                    foreach(var c in db.Couriers) //проверка на дубликат
                    {
                        if (textBox1.Text == c.Name)
                        {
                            MessageBox.Show("Данный курьер уже существует", "Ошибка");
                            return;
                        }
                    }
                    db.Couriers.Add(new Courier { Name = textBox1.Text }); // записываем в бд нового курьера
                    
                    db.SaveChanges();
                }
            }
            Close();
        }
    }
}
