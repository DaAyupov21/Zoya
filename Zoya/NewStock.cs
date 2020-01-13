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
    public partial class NewStock : Form
    {
        public NewStock()
        {
            InitializeComponent();
        }
        //случайно созданны два метода, лучше их не трогать
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private static int StringToInt(string val) //функция которая будет переводить строковое значение в целочисленное
        {
            try //открываем блок обработки ошибок, нужен он для того чтобы в случае если мы напишем неправильно число случайно написав букву, например наша программа не сломалась
            {  //здесь блок кода, где может произойти данная ошибка 
                int NewVal = Int32.Parse(val); // с помощью встроенной функции мы преобразуем в целочисленное значение
                return NewVal; //и возращаем значение
            }
            catch (FormatException) // если что-то пошло не так в преобразовании то срабатывает блок кода ниже
            {
                MessageBox.Show("Неверный формат введенных данных", "Ошибка");
                return -1;
            }

        }
        private static double StringToDouble(string val) //функция которая будет переводить строковое значение в целочисленное
        {
            try //открываем блок обработки ошибок, нужен он для того чтобы в случае если мы напишем неправильно число случайно написав букву, например наша программа не сломалась
            {  //здесь блок кода, где может произойти данная ошибка 
                double NewVal = Convert.ToDouble(val); // с помощью встроенной функции мы преобразуем в целочисленное значение
                return NewVal; //и возращаем значение
            }
            catch (FormatException) // если что-то пошло не так в преобразовании то срабатывает блок кода ниже
            {
                MessageBox.Show("Неверный формат введенных данных", "Ошибка");
                return -1.0;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) ||
                String.IsNullOrEmpty(textBox2.Text) ||
                String.IsNullOrEmpty(textBox3.Text) ||
                String.IsNullOrEmpty(textBox4.Text) )
            {
                MessageBox.Show("Есть незаполненные поля", "Ошибка");
                return;
            }
            else if (
                     StringToInt(textBox2.Text) == -1 ||
                     StringToInt(textBox3.Text) == -1 ||
                     StringToDouble(textBox4.Text) == -1.0)
            {
                return;
            }
            using (ShoppingContext db = new ShoppingContext())
            {
                Stock stock = new Stock { Type = textBox1.Text, Count = StringToInt(textBox2.Text), Size = StringToInt(textBox3.Text), Price = StringToDouble(textBox4.Text) };
                db.Stocks.Add(stock);
                db.SaveChanges();
            }

        }
    }
}
