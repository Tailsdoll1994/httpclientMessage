using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;

namespace httpclientMessage
{
    // Class отвечающий за сериализация\десерилизацию type(задач) отправляемый с ссылки (пример 50 строчка кода) 
    public class JSONtype
    {
        // Отвечает за номер задачи
        public int type { get; set; }
    }
    // Class отвечающий за сериализация\десерилизацию http-сообщений связаных с числами
    public class JSONint
    {
        // Отвечает за номер задачи
        public int type { get; set; }
        // Отвечает за условия в указаной задачи
        public string description { get; set; }
        // Отвечает за массив предоставленых чисел 
        public List<long> task { get; set; }

    }
    // Class отвечающий за сериализация\десерилизацию http-сообщений конкретной задачи (type = 3)
    public class JSONstring
    {
        // Отвечает за номер задачи
        public int type { get; set; }
        // Отвечает за условия в указаной задачи
        public string description { get; set; }
        // Отвечает за строку с символами предоставленой задачей
        public string task { get; set; }
    }
    // Class отвечающий за сериализация\десерилизацию http-сообщения, конкретной задачи (type = 4)
    public class Response 
    {
        // Отвечает за отправку запроса к заданию
        public int request { get; set; }
        // Отвечает за ответ в ввиде словаря
        public Dictionary<string, long> response { get; set; }
        // Конструктор в котором указвается, отправку запроса и ответ в ввиде словаря
        public Response (int a, Dictionary<string, long> anser)
        {
            this.request = a;
            this.response = anser;
            
        }
    }
    class Request
    {
        static void request1()
        {
            try
            {
                // Объявление локальной переменой httpclient
                using (var Http = new HttpClient())
                {
                    //1. Объявление переменой
                    //2. Http.GetAsync, отправка запрос GET на указаную ссылку (коду URI) 
                    //3. Result, получает итого значение данной ссылки или объекта
                    var respon = Http.GetAsync("http://api.cakeproject.ru/game/").Result;

                    // Усли respon успешно получает IsSuccessStatusCode (http-ответ) 
                    if (respon.IsSuccessStatusCode)
                    {
                        //1. Объявление переменой
                        //2. Content возвращает, задает или указывает содержимое http-сообщения
                        var ResponContent = respon.Content;

                        //1. Объявление переменой
                        //2. Сериализация содержимиого http-сообщения в строку 
                        //3. Result, получение итого значения http-сообщения
                        string responseString = ResponContent.ReadAsStringAsync().Result;

                        // Вывод сообщения в консоль
                        Console.WriteLine(responseString);

                    } 
                }
            }
            catch
            {
                Console.WriteLine("Не возможно получить ответ с указаного сайта");
            }
            Console.ReadKey();
        }
        //https://metanit.com/sharp/tutorial/13.3.php опредление что такое ансихроность 

        // async слуужит для упрощения написание асинхронного кода/используются для создания асинхронного метода
        static async void request3()
        {
            // Объявление классов в виде переменых, без указания значений
            JSONint somethingInt;
            JSONtype somethingType;
            JSONstring somethingString;
            // Цикл в котором указано кол-во отправки запросов 
            for (int a = 0; a < 49; a++)
            {
                //1. Объявление переменой
                //2. Служит для записи вычисление во втором задании (type = 2)
                int x = 0;

                // Объявление класса в виде переменой, без указания значения
                Response diction2;

                //1. Объявление словаря в виде переменой
                //2. Служит для записи и отправки запроса (request) и ответа (response)
                Dictionary<string, long> diction = new Dictionary<string, long>();

                //1. Отправка числого запроса
                //2. Так ссылка определяет кол-во решеных пример и на основе этого, отправляет новую задачу
                diction.Add("request", a);
                //using (var client = new HttpClient())

                // Объявление локальной переменой httpclient
                var client = new HttpClient();

                //1. Объявление переменой
                //2. Проводится сериализация словаря
                string json = JsonConvert.SerializeObject(diction);

                //1. Объявление переменой
                //2. await так же как и async создан для упрощения написание асинхронного кода/...
                //...Используются для создания асинхронного метода
                //3. Http.PostAsync, отправка запрос Post на указаную ссылку (коду URI) 
                //4. StringContent предоставляет содержимое Http на основе строки.
                //   Так же, в данном случаи создается  новый экземпляр, класса StringContent...
                //...В котором указываеться string (content), формат кодировки, а для описание последнего,...
                //...Нужно растолковать что такое MIME. MIME это - спецификация для кодирования информации и...
                //...Форматирования сообщений таким образом, чтобы их можно было пересылать по Интернету.

                //   https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_MIME-%D1%82%D0%B8%D0%BF%D0%BE%D0%B2
                //   Список MIME-типов
                var respone = await client.PostAsync("http://api.cakeproject.ru/game/request2",
                   new StringContent(json, Encoding.UTF8, "application/json"));

                //1. Объявление переменой
                //2. Content возвращает, задает или указывает содержимое http-сообщения
                var ResponContent = respone.Content;

                //1. Объявление переменой
                //2. Сериализация содержимиого http-сообщения в строку 
                //3. Result, получение итого значения http-сообщения
                string responseString = ResponContent.ReadAsStringAsync().Result;

                // Вывод сообщения в консоль
                Console.WriteLine();
                Console.WriteLine("Ответ: {0}", responseString);

                // Пороводем десерализацию http-сообщения с использованием JSONtype
                somethingType = JsonConvert.DeserializeObject<JSONtype>(responseString);

                // Если somethingType равен 3 задаче
                // Если нет, переход в else другому блоку if 
                if (somethingType.type == 3)
                {
                    // Пороводем десерализацию http-сообщения с использованием JSONstring
                    somethingString = JsonConvert.DeserializeObject<JSONstring>(responseString);
                    Console.WriteLine();

                    // Вывод символов в суммарном значении
                    Console.WriteLine("Количество символов {0}", somethingString.task.ToString().Length);
                    // Отправка ответа
                    diction.Add("response", somethingString.task.ToString().Length);

                }
                else 
                {
                    // Пороводем десерализацию http-сообщения с использованием JSONint
                    somethingInt = JsonConvert.DeserializeObject<JSONint>(responseString);

                    // Если somethingType равен 1 задаче
                    // Если somethingType равен 2 задаче
                    // Если somethingType равен 4 задаче
                    if (somethingType.type == 1)
                    {
                        //1. Складывается два числа
                        //2. Отправка суммарном значении\ответа
                        diction.Add("response", somethingInt.task[0] + somethingInt.task[1]);

                    }
                    else if (somethingType.type == 2)
                    {
                        // Из somethingInt.task берём массив предоставленых чисел и переносим локальной переменой list
                        foreach (int list in somethingInt.task)
                        {
                            // При помощи x складывается весь массив чисел list и все записывается в переменую x
                            x = list + x;
                        }

                        // Отправка суммарном значении\ответа
                        diction.Add("response", x);
                    }
                    else if (somethingType.type == 4)
                    {
                        //1. Объявление словаря в виде переменой
                        //2. Служит для записи и отправки запроса (request) и ответа (response)
                        var anser = new Dictionary<string, long>();

                        // Записывается ключ "a", "b" со значением somethingInt.task (массив предоставленых чисел)
                        anser.Add("a", somethingInt.task[0]);
                        anser.Add("b", somethingInt.task[1]);

                        //1. Объявление значение переменой 
                        //2. Вписываем в конструктор переменую указанаю в цикле for (request), и так же ответ в виде словаря (response)
                        diction2 = new Response(a, anser);
                        // Проводится сериализация словаря
                        json = JsonConvert.SerializeObject(diction2);
                    }
                }

                //using (var client1 = new HttpClient())
                // Если somethingType НЕ равен 4 задаче
                if (somethingType.type != 4)
                {
                    // Проводится сериализация словаря
                    json = JsonConvert.SerializeObject(diction);
                }

                //1. Объявление переменой
                //2. await так же как и async создан для упрощения написание асинхронного кода/...
                //...Используются для создания асинхронного метода
                //3. Http.PostAsync, отправка запрос Post на указаную ссылку (коду URI) 
                //4. StringContent предоставляет содержимое Http на основе строки.
                var respone1 = await client.PostAsync("http://api.cakeproject.ru/game/request3",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                // если respon успешно получает IsSuccessStatusCode (http-ответ) 
                if (respone1.IsSuccessStatusCode)
                {
                    //1. объявление переменой
                    //2. Content возвращает, задает или указывает содержимое http-сообщения
                    var ResponContent1 = respone1.Content;

                    //1. объявление переменой
                    //2. сериализация содержимиого http-сообщения в строку 
                    //3. Result, получение итого значения http-сообщения
                    string responseString1 = ResponContent1.ReadAsStringAsync().Result;

                    // Вывод сообщения в консоль
                    Console.WriteLine("Ответ: {0}", responseString1);

                }
            }
        }
        static void Main(string[] args)
        {
            request1();
            //request2();
            request3();
            Console.ReadKey();
        }
    }
}
/*static async void request2()
{
    JSON something;
    for (int a = 0; a < 20; a++)
    {
        Dictionary<string, int> diction = new Dictionary<string, int>();
        diction.Add("request", a);
        using (var client = new HttpClient())
        {

            string json = JsonConvert.SerializeObject(diction);
            var respone = await client.PostAsync("http://api.cakeproject.ru/game/request2",
               new StringContent(json, Encoding.UTF8, "application/json"));

            if (respone.IsSuccessStatusCode)
            {
                var ResponContent = respone.Content;
                string responseString = ResponContent.ReadAsStringAsync().Result;
                Console.WriteLine("Ответ: {0}", responseString);
                something = JsonConvert.DeserializeObject<JSON>(responseString);
                //Console.WriteLine("{0}, {1}", something.task[0], something.task[1]);
                if (something.type == 1)
                {
                    diction.Add("response", something.task[0] + something.task[1]);
                }
                else if (something.type == 2)
                {
                    switch (something.task.Count)
                    {
                        case 2:
                            diction.Add("response", something.task[0] + something.task[1]);
                            break;
                        case 3:
                            diction.Add("response", something.task[0] + something.task[1] + something.task[2]);
                            break;
                        case 4:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3]);
                            break;
                        case 5:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4]);
                            break;
                        case 6:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5]);
                            break;
                        case 7:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6]);
                            break;
                        case 8:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7]);
                            break;
                        case 9:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8]);
                            break;
                        case 10:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9]);
                            break;
                        case 11:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10]);
                            break;
                        case 12:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11]);
                            break;
                        case 13:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12]);
                            break;
                        case 14:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13]);
                            break;
                        case 15:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14]);
                            break;
                        case 16:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14] + something.task[15]);
                            break;
                        case 17:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14] + something.task[15] +
                                something.task[16]);
                            break;
                        case 18:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14] + something.task[15] +
                                something.task[16] + something.task[17]);
                            break;
                        case 19:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14] + something.task[15] +
                                something.task[16] + something.task[17] + something.task[18]);
                            break;
                        case 20:
                            diction.Add("response",
                                something.task[0] + something.task[1] + something.task[2] + something.task[3] +
                                something.task[4] + something.task[5] + something.task[6] + something.task[7] +
                                something.task[8] + something.task[9] + something.task[10] + something.task[11] +
                                something.task[12] + something.task[13] + something.task[14] + something.task[15] +
                                something.task[16] + something.task[17] + something.task[18] + something.task[19]);
                            break;
                    }
                }
            }
        }
        using (var client = new HttpClient())
        {
            string json = JsonConvert.SerializeObject(diction);
            var respone = await client.PostAsync("http://api.cakeproject.ru/game/request3",
               new StringContent(json, Encoding.UTF8, "application/json"));

            if (respone.IsSuccessStatusCode)
            {
                var ResponContent = respone.Content;
                string responseString = ResponContent.ReadAsStringAsync().Result;
                Console.WriteLine("Ответ: {0}", responseString);
                something = JsonConvert.DeserializeObject<JSON>(responseString);
                //Console.WriteLine("{0}, {1}", something.task[0], something.task[1]);
            }
        }
    }
}*/
