using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using udWpf.Classes;

namespace udWpf
{
    /// <summary>
    /// Interaction logic for contactDetailWindow.xaml
    /// </summary>
    public partial class contactDetailWindow : Window
    {
        Contact contact;
        public contactDetailWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;
            Initialize();
        }

        private void Initialize() 
        {
            detailName.Text = contact.Name;
            detailEmail.Text = contact.Email;
            detailPhone.Text = contact.Phone;
        }

        private void detailUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = detailName.Text;
            contact.Email = detailEmail.Text;
            contact.Phone = detailPhone.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) 
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }
        }

        private void detailDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            { 
                connection.CreateTable<Contact>();
                connection.Delete(contact);
            }
        }
    }
}
