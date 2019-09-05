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
    public class Response 
    {
        public int request { get; set; }
        public Dictionary<string, long> response { get; set; }
        public Response (int a, Dictionary<string, long> anser)
        {
            this.request = a;
            this.response = anser;
            
        }
    }
    public class JSONstring
    {
        public int type { get; set; }
        public string description { get; set; }
        public string task { get; set; }
    }
    public class JSONtype
    {
        public int type { get; set; }
    }
    public class JSONint
    {

        public int type { get; set; }
        public string description { get; set; }
        public List<long> task { get; set; }
        
    }
    class Request
    {
        static void request1()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync("http://api.cakeproject.ru/game/request1").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var ResponContent = response.Content;
                        string responseString = ResponContent.ReadAsStringAsync().Result;
                        Console.WriteLine("Ответ: {0}", responseString);

                    }
                }
            }
            catch
            {
                Console.WriteLine("Не возможно получить ответ с указаного сайта");
            }
            Console.ReadKey();
        }
        static async void request3()
        {
            
            JSONint somethingInt;
            JSONtype somethingType;
            JSONstring somethingString;
            for (int a = 0; a < 49; a++)
            {
                int x = 0;
                Response diction2;
                Dictionary<string, long> diction = new Dictionary<string, long>();
                diction.Add("request", a);
                //using (var client = new HttpClient())
                var client = new HttpClient();

                string json = JsonConvert.SerializeObject(diction);
                var respone = await client.PostAsync("http://api.cakeproject.ru/game/request2",
                   new StringContent(json, Encoding.UTF8, "application/json"));

                var ResponContent = respone.Content;
                string responseString = ResponContent.ReadAsStringAsync().Result;
                Console.WriteLine();
                Console.WriteLine("Ответ: {0}", responseString);

                somethingType = JsonConvert.DeserializeObject<JSONtype>(responseString);

                if (somethingType.type == 3)
                {
                    somethingString = JsonConvert.DeserializeObject<JSONstring>(responseString);
                    Console.WriteLine();
                    Console.WriteLine("Количество символов {0}", somethingString.task.ToString().Length);
                    diction.Add("response", somethingString.task.ToString().Length);
                }
                else
                {
                    somethingInt = JsonConvert.DeserializeObject<JSONint>(responseString);

                    if (somethingType.type == 1)
                    {

                        diction.Add("response", somethingInt.task[0] + somethingInt.task[1]);

                    }
                    else if (somethingType.type == 2)
                    {
                        foreach (int list in somethingInt.task)
                        {
                            x = list + x;
                        }
                        diction.Add("response", x);
                    }
                    else if (somethingType.type == 4)
                    {
                        var anser = new Dictionary<string, long>();
                        anser.Add("a", somethingInt.task[0]);
                        anser.Add("b", somethingInt.task[1]);
                        diction2 = new Response(a, anser);
                        json = JsonConvert.SerializeObject(diction2);
                    }
                }

                //using (var client1 = new HttpClient())
                if (somethingType.type != 4)
                {
                    json = JsonConvert.SerializeObject(diction);
                }
                var respone1 = await client.PostAsync("http://api.cakeproject.ru/game/request3",
                   new StringContent(json, Encoding.UTF8, "application/json"));
                if (respone1.IsSuccessStatusCode)
                {
                    var ResponContent1 = respone1.Content;
                    string responseString1 = ResponContent1.ReadAsStringAsync().Result;
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
