using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class AddUpd_Zakazs : Window
{
    

    private List<Zakaz> zakazs;
    private Zakaz CurrentData;
    public AddUpd_Zakazs(Zakaz currentData, List<Zakaz>zakazes)
    {
        InitializeComponent();
        CurrentData = currentData;
        this.DataContext = CurrentData;
        zakazs = zakazes;
    }
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=masterskaya;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var save = zakazs.FirstOrDefault(x => x.Код == CurrentData.Код);
        if (save == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO заказ VALUES (" + Convert.ToInt32(Код.Text) + ", " +
                             Convert.ToInt32(Клиент.Text) + "," + Convert.ToInt32(Услуга.Text) + ", '" + Дата.Text +
                             "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
            {
                try
                {
                    conn = new MySqlConnection(connStr);
                    conn.Open();
                    string upd = "UPDATE заказ SET Клиент = '" + Convert.ToInt32(Клиент.Text) + "',Услуга = "+ Convert.ToInt32(Услуга.Text) + ", Дата = '" + Дата.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                    MySqlCommand cmd = new MySqlCommand(upd, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception exception)
                {
                    Console.Write("Error" + exception);
                }
            }
        }
    



    private void GoBack(object? sender, RoutedEventArgs e)
    {

        var form = new Servis.Zakazs();
        Hide();
        form.Show();
    }
}