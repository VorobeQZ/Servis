using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class Uslugic : Window
{
    public Uslugic()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM услуга;";//Запрос на отображение таблицы доктор
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
    }

    private List<Zakaz> zakazes;
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

    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }
    private void Searchz(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для поиска по фамилии и имени
    {
        string poisk = "SELECT * FROM услуга  WHERE Наименование LIKE '%" + Searchn.Text + "%'";
        ShowTable(poisk);//Отображение запроса
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки возврата на прошлую форму
    {
        Servis.Autorization autorization = new Servis.Autorization();
        Hide();
        autorization.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки обновление таблицы и текстбоксов
    {
        Servis.Zayvki addWindow = new Servis.Zayvki();
        addWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var srv = usluga;
        srv = srv.Where(x => x.Наименование.Contains(Searchn.Text)).ToList();
        DataGrid.ItemsSource = srv;
    }
}