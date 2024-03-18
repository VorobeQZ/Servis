using System;
using Avalonia.Media.Imaging;

namespace Servis;

public class Usluga
{
   public int Код { get; set; }
   public string Наименование { get; set; }
   public double Стоимость { get; set; }
   public string Ожидаемое_время_выполнения { get; set; }
   public Bitmap Фото { get; set; }
   public int Скидка { get; set; }
   public double Итоговая_стоимость { get; set; }
}
public class Sotrudnik
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public string Телефон { get; set; }
    public int Пол { get; set; }
    public int Возраст { get; set; }
}
public class Sostoynie
{
    public int Код { get; set; }
    public string Состояние { get; set; }
}
public class Remont
{
    public int Код { get; set; }
    public int Заказ { get; set; }
    public int Сотрудник { get; set; }
    public int Состояние { get; set; }
}
public class Pol
{
    public int Код { get; set; }
    public string Пол { get; set; }
}
public class Avto
{
    public int Код { get; set; }
    public string Марка { get; set; }
    public string Модель { get; set; }
    public string Год { get; set; }

}
public class Client
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public int Пол { get; set; }
    public string Телефон { get; set; }
    public int Машина { get; set; }
}
public class Zakaz
{
    public int Код { get; set; }
    public int Клиент { get; set; }
    public int Услуга { get; set; }
    public DateTime Дата { get; set; }

}