using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using udWpf.Classes;

namespace udWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // kayıt işlemi

            // Create contact data 
            Contact _contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = mailTextBox.Text,
                Phone = phoneTextBox.Text
            };


            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // using içinde kullandığımız nesne IDisposable sınıfından miras alması gerklidir.
            { // using kullanım amacı Tanımlanan bir nesnenin dispose edilmesini garantilemek
                connection.CreateTable<Contact>();
                // Add data to db
                connection.Insert(_contact);
            }

            ReadDatabase();
            MessageBox.Show("Bilgi", "Kayıt eklendi");

        }


        public void ReadDatabase()
        {


            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().OrderBy(c => c.Phone).ToList();
            }
            //contactListView.Items.Clear();
            //if(contacts != null) 
            //{
            //    foreach (var c in contacts)
            //    {
            //        contactListView.Items.Add(c);
            //    }
            //}
            contactListView.ItemsSource = contacts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchBox.Text.ToLower())).ToList();

            // sql komutu olarak aşağıdaki gibi girebiliriz !
            //var filteredListBySql = (from _list in contacts
            //                        where _list.Name.ToLower().Contains(searchBox.Text.ToLower())
            //                        select _list).ToList();


            contactListView.ItemsSource = filteredList;
        }

        private void contactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Contact contact = sender as Contact;
            Contact contact = (Contact)contactListView.SelectedItem;

            if (contact != null)
            {
                contactDetailWindow detailWindow = new contactDetailWindow(contact);
                detailWindow.ShowDialog();
            }
        }
    }
}
