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
    public partial class Shop : Form
    {
        public Shop() //код создан автоматически
        {
            InitializeComponent();
        }

        

        private void Shop_Load(object sender, EventArgs e) //при загрузке окна будет загружать в combobox каталог вещей и курьеров, чтобы их можно было выбрать
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                List<string> Catalog = new List<string>(); //создадим пустой список каталога в который загрузим название и количество в строку
                foreach(var c in db.Stocks) // проходимся по всем записям склада
                {
                    Catalog.Add($"{c.Type}  {c.Count}шт."); //добавляем их в список название и количество в виде одной строки
                }
                comboBox1.Items.AddRange(Catalog.ToArray()); //передаем значение, преобразовывая список в массив
                List<string> Delivers = new List<string>(); //проделываем ту же операцию с курьерами
                foreach (var c in db.Couriers)
                {
                    Delivers.Add(c.Name);
                }
                comboBox2.Items.AddRange(Delivers.ToArray());
            }
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
        private static string makeAnOrder(string NameOrder, int Count, Courier courier, Customer customer) // функция в которую будем создавать заказы и изменять вместе с этим значения в базе данных
        {
            using (ShoppingContext db = new ShoppingContext())
            {
                Stock stock = null; //создаем пустую локальную переменную 
                foreach (var c in db.Stocks) //ищем по названию нужную запись в бд
                {
                    if(NameOrder.IndexOf(c.Type) >= 0)
                    {
                        stock = c; //записываем найденную запись в локальную переменную
                        break;
                    }

                }
                if (stock == null)
                {
                    MessageBox.Show("Не найдена запись", "Ошибка");
                    return ""; //прерываем программу
                }
                stock.Count -= Count;// вычитаем значение количества
                if (stock.Count < 0)
                {
                    MessageBox.Show("На складен нет столько товаров, сколько вы заказали", "Ошибка");
                    return "";
                }
                else if (stock.Count == 0)
                {
                    db.Stocks.Remove(stock); //удаляем запись т.к. товаров больше не осталось
                    db.SaveChanges(); //сохраняем изменения в бд
                }
                else
                {
                    db.Entry(stock).State = System.Data.Entity.EntityState.Modified; //если товара на складе остается, то просто меняем 
                    db.SaveChanges();
                }
                
               
                string Order = $"Заказ успешно совершен!\n" +
                    $"Детали заказа:\n" +
                    $"{stock.Type} Количество: {Count} Цена: {stock.Price * Count}\n" +
                    $"Сервис доставки: {courier.Name}\n" +
                    $"Имя заказчика: {customer.Name} {customer.SecondName}";
                return Order;
            }
        }

        private void button1_Click(object sender, EventArgs e) //функция которая обрабатывает кнопку "Заказать"
        {
            int CountStock = StringToInt(textBox5.Text);
            if(CountStock == -1)
            {
                return; //если мы неправильно ввели количество, то функция прерывается
            }
            if(String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || 
                String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox4.Text) ||
                String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Одно из полей пустое", "Ошибка");
                return; //проверяем все поля формы на пустоту, если хоть один пустой, то идет прирывание
            }
            Customer customer = new Customer { Name = textBox1.Text, 
                SecondName = textBox2.Text, 
                Email = textBox3.Text, 
                PhoneNumber = textBox4.Text }; //создаем класс заказчика и считываем данные с полей
            Courier courier = null; //создадим пустого курьера, в который запишем курьера, которого выбрал заказчик
            using (ShoppingContext db = new ShoppingContext())
            {
                db.Customers.Add(customer); //записывем заказчика в базу данных
                db.SaveChanges();//сохраняем изменения
                
                foreach (var c in db.Couriers)
                {
                    if (comboBox2.Text == c.Name)
                    {
                        courier = c; //находим нужного курьера и записываем его
                        break; //прерываем цикл
                    }
                    
                }
                List<string> Catalog = new List<string>(); //создадим пустой список каталога в который загрузим название и количество в строку
                foreach (var c in db.Stocks) // проходимся по всем записям склада
                {
                    Catalog.Add($"{c.Type}  {c.Count}шт."); //добавляем их в список название и количество в виде одной строки
                }
                comboBox1.Items.Clear(); //очищаем выпадающий список
                comboBox1.Items.AddRange(Catalog.ToArray()); //и записываем новые
            }
            label8.Text = makeAnOrder(comboBox1.Text, CountStock, courier, customer); //делаем запись о заказе на форме

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewCourier newCourier = new NewCourier();
            newCourier.Show();
            List<string> Delivers = new List<string>(); //проделываем ту же операцию с курьерами
           using(ShoppingContext db = new ShoppingContext())
            {
                foreach (var c in db.Couriers)
                {
                    Delivers.Add(c.Name);
                }
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(Delivers.ToArray());
                return;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewStock newStock = new NewStock();
            newStock.Show();
            using (ShoppingContext db = new ShoppingContext())
            {
                List<string> Catalog = new List<string>(); //создадим пустой список каталога в который загрузим название и количество в строку
                foreach (var c in db.Stocks) // проходимся по всем записям склада
                {
                    Catalog.Add($"{c.Type}  {c.Count}шт."); //добавляем их в список название и количество в виде одной строки
                }
                comboBox1.Items.Clear(); //очищаем выпадающий список
                comboBox1.Items.AddRange(Catalog.ToArray()); //и записываем новые
            }
        }
    }
}
