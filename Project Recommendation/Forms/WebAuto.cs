using DB_993.Classes;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_993.Forms
{
    public partial class WebAuto : Form
    {
        public WebAuto()
        {
            InitializeComponent();
        }
        const int AppID = 51920258; //ID нашего приложения

        private string access_token = null!; //ключ сессии
        private string user_id = null!; //ID авторизованного пользователя
        public bool Authorize()
        {
            access_token = null!;
            user_id = null!;
            //создаем форму авторизации
            WebAuto authform = new WebAuto();
            //получаем объект компонента WebView2
            var webView2 = (WebView2)authform.webView21;
            //Подписываемся на событие NavigationCompleted в функции Authorize_proceed
            webView2.NavigationStarting += new EventHandler<CoreWebView2NavigationStartingEventArgs>(Authorize_proceed);
            //переходим на страницу авторизации с нужными нам данными.
            webView2.Source = new Uri("http://api.vkontakte.ru/oauth/authorize?client_id=" + AppID.ToString() +
            "&scope=photos&redirect_uri=" +
            "http://api.vkontakte.ru/blank.html&display=popup&response_type=token");
            //показываем диалог авторизации
            authform.ShowDialog();

            //проверяем были ли мы авторизованы и возвращаем значение
            if (access_token == null || user_id == null) return false;
            else return true;
        }
        private void Authorize_proceed(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            //Разбираем ссылку на кусочки
            string[] parts = e.Uri.Split('#');
            //получаем объект браузера
            WebView2 browser = (WebView2)sender;
            //получаем объект формы авторизации
            WebAuto authform = (WebAuto)browser.Parent!;

            //проверяем, что нас перенаправили на нужный адрес при успешной авторизации
            if (parts[0] == "http://api.vkontakte.ru/blank.html")
            {
                //если ошибка, закрываем форму
                if (parts[1].Substring(0, 5) == "error") authform.Close();
                //если авторизация успешна
                else if (parts[1].Substring(0, 12) == "access_token")
                {
                    //разбираем ответ
                    parts = parts[1].Split('&');

                    //записываем данные
                    access_token = parts[0].Split('=')[1];
                    user_id = parts[2].Split('=')[1];
                    //закрываем форму авторизации
                    authform.Close();
                }
            }
            else
            {
                //неизвестный ответ при неудачном входе и нажатии кнопки "отмена"
                parts = e.Uri.Split('?');
                if (parts[0] == "http://api.vkontakte.ru/oauth/grant_access")
                    //снова переходим к авторизации
                    browser.Source = new Uri("http://api.vkontakte.ru/oauth/authorize?client_id=" + AppID.ToString() +
                    "&scope=photos&redirect_uri=" +
                    "http://api.vkontakte.ru/blank.html&display=popup&response_type=token");
            }
        }
        public string[] GetMyProfile()
        {
            string profiles = "https://api.vkontakte.ru/method/getProfiles?uid=" + user_id +
            "&access_token=" + access_token;

            System.Net.WebRequest reqGET = System.Net.WebRequest.Create(profiles);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string json = sr.ReadToEnd();

            JObject o = JObject.Parse(json);
            JArray response = (JArray)o["response"]!;

            string[] profile = new string[2];
            profile[0] = (string)response[0]["first_name"]!;
            profile[1] = (string)response[0]["last_name"]!;

            return profile;
        }
    }
}
