using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class Uslugi : Window
{
    
    public Uslugi()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM услуга;";//Запрос на отображение таблицы доктор
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    
    
    private List<Usluga> usluga;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=masterskaya;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        
        usluga = new List<Usluga>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        
        
        while (reader.Read() && reader.HasRows)
        {
            
            
            var currentData = new Usluga()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Наименование  = reader.GetString("Наименование"),
                Стоимость = reader.GetDouble("Стоимость"),
                Ожидаемое_время_выполнения = reader.GetString("Ожидаемое_время_выполнения"),
                Фото = LoadImage("avares://Servis/Assets/" + reader.GetString("Фото")),
                Скидка = reader.GetInt32("Скидка"),
                Итоговая_стоимость = reader.GetDouble("Стоимость")-(reader.GetDouble("Стоимость")*reader.GetInt32("Скидка")/100)
            };  
            
            usluga.Add(currentData);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = usluga;//Заполнение данными грида 
        
    }
    
    public void FillCmb()
    {
        usluga = new List<Usluga>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM услуга", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentData = new Usluga()
            {
                Код = reader.GetInt32("Код"),
                Наименование  = reader.GetString("Наименование"),
                Стоимость = reader.GetDouble("Стоимость"),
                Ожидаемое_время_выполнения = reader.GetString("Ожидаемое_время_выполнения"),
                Фото = LoadImage("avares://Servis/Assets/" + reader.GetString("Фото")),
                Скидка = reader.GetInt32("Скидка"),
                Итоговая_стоимость = reader.GetDouble("Стоимость")-(reader.GetDouble("Стоимость")*reader.GetInt32("Скидка")/100)
            };
            usluga.Add(currentData);
        }
        conn.Close();

        var discontComboBox = this.Find<ComboBox>("DiscontComboBox");
        discontComboBox.ItemsSource = new List<string>
        {
            "Все скидки",
            "От 0% до 5%",
            "От 5% до 15%",
            "От 15% до 30%",
            "От 30% до 70%",
            "От 70% до 100%"
        };
    }

    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }
    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var srv = usluga;
        srv = srv.Where(x => x.Наименование.Contains(Searchn.Text)).ToList();
        DataGrid.ItemsSource = srv;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки возврата на прошлую форму
    {
        Servis.Menu menu = new Servis.Menu();
        Close();
        menu.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки обновление таблицы и текстбоксов
    {
        string reset = "SELECT * FROM услуга;";
        ShowTable(reset);
    }
    

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Usluga currentData = DataGrid.SelectedItem as Usluga;
            if (currentData == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM услуга WHERE Код = " + currentData.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            usluga.Remove(currentData);
            ShowTable("SELECT * FROM услуга;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Usluga add = new Usluga();
        Servis.AddUpd_Uslugi addWindow = new Servis.AddUpd_Uslugi(add,usluga);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Usluga upd = DataGrid.SelectedItem as Usluga;
        if (upd == null)
        {
            return;
        }
        Servis.AddUpd_Uslugi editWindow = new Servis.AddUpd_Uslugi(upd,usluga);
        editWindow.Show();
        this.Close();
    }
    private void SortAscending(object sender, RoutedEventArgs e)
    {
        var sortedItems = DataGrid.ItemsSource.Cast<Usluga>().OrderBy(s => s.Стоимость).ToList();
        DataGrid.ItemsSource = sortedItems;
    }

    private void SortDescending(object sender, RoutedEventArgs e)
    {
        var sortedItems = DataGrid.ItemsSource.Cast<Usluga>().OrderByDescending(s => s.Стоимость).ToList();
        DataGrid.ItemsSource = sortedItems;
    }
    private void DiscountFilter(object? sender, SelectionChangedEventArgs e)
    {
        var discontComboBox = (ComboBox)sender;
        
        var selectedDiscount = discontComboBox.SelectedItem as string;

        int startDiscount = 0;
        int endDiscount = 0;

        if (selectedDiscount == "Все скидки")
        {
            DataGrid.ItemsSource = usluga;
        }
        else if (selectedDiscount == "От 0% до 5%")
        {
            startDiscount = 1;
            endDiscount = 5;
        }
        else if (selectedDiscount == "От 5% до 15%")
        {
            startDiscount = 5;
            endDiscount = 15;
        }
        else if (selectedDiscount == "От 15% до 30%")
        {
            startDiscount = 15;
            endDiscount = 30;
        }
        else if (selectedDiscount == "От 30% до 70%")
        {
            startDiscount = 30;
            endDiscount = 70;
        }
        else if (selectedDiscount == "От 70% до 100%")
        {
            startDiscount = 70;
            endDiscount = 100;
        }

        if (startDiscount != 0 && endDiscount != 0)
        {
            var filter = usluga
                .Where(x => x.Скидка >= startDiscount && x.Скидка < endDiscount)
                .ToList();

            DataGrid.ItemsSource = filter;
        }
    }
    
}