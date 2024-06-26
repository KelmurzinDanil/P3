﻿using DB_993.Classes;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace design
{
    /// <summary>
    /// Класс используется для отображения и взаимодействия с коллекцией карточек.
    /// </summary>
    public partial class CollectionCard : Form
    {
        //cQ8-jbJ-Tin-9tA
        //SS9rxQxQp63Yhi1jgXvx
        public string Email {  get; set; }
        public int IdComp { get; set; }
        public CollectionCard(int idComp, string email)
        {
            InitializeComponent();
            Design();
            IdComp = idComp;
            Email = email;
            LoadData();
            
        }

        /// <summary>
        /// Метод настраивает внешний вид элементов управления на форме.
        /// </summary>
        public void Design()
        {
            ProfileButton.Parent = Picture7;
            ProfileButton.BackColor = Color.Transparent;
            ProfileButton.FlatAppearance.BorderSize = 0;
            ProfileButton.FlatStyle = FlatStyle.Flat;
            FavButton.Parent = Picture7;
            FavButton.BackColor = Color.Transparent;
            FavButton.FlatAppearance.BorderSize = 0;
            FavButton.FlatStyle = FlatStyle.Flat;
            BlackListButton.Parent = Picture7;
            BlackListButton.BackColor = Color.Transparent;
            BlackListButton.FlatAppearance.BorderSize = 0;
            BlackListButton.FlatStyle = FlatStyle.Flat;
            MyListsButton.Parent = Picture7;
            MyListsButton.BackColor = Color.Transparent;
            MyListsButton.FlatAppearance.BorderSize = 0;
            MyListsButton.FlatStyle = FlatStyle.Flat;
        }
        public void LoadData()
        {
            using (var context = new ApplicationContextBD())
            {
                var imageList = new ImageList();
                imageList.ImageSize = new Size(100, 100);
                var listRealty = context.Compilations.Where(w => w.Id == IdComp).Select(u => u.Realtys).FirstOrDefault();
                if (listRealty != null)
                {
                    for (int i = 0; i < listRealty!.Count; i++)
                    {
                        imageList.Images.Add(new Bitmap(listRealty[i].PhotoRealty!));
                    }
                    ListRealtyComp.SmallImageList = imageList;

                    for (int i = 0; i < listRealty.Count; i++)
                    {
                        var listViewItem = new ListViewItem(new string[] { string.Empty, listRealty[i].Price.ToString()!,
                        listRealty[i].Address!, listRealty[i].NameRealty!});
                        listViewItem.ImageIndex = i;
                        ListRealtyComp.Items.Add(listViewItem);
                    }
                }

            }
        }

        private void GoInEmailBtn_Click(object sender, EventArgs e)
        {
            using (var context = new ApplicationContextBD()) 
            {
                try
                {
                    MailAddress from = new MailAddress("testikovich77@mail.ru", "ООО ДДД");
                    MailAddress to = new MailAddress(Email);
                    MailMessage m = new MailMessage(from, to);
                    var realty = context.Compilations.Where(w => w.Id == IdComp).Select(u => u.Realtys).FirstOrDefault();
                    string textComp = String.Empty;
                    string messegeText = String.Empty;
                    string imagePath = String.Empty;
                    for (int i = 0; i < realty?.Count; i++)
                    {
                        m.Attachments.Add(new Attachment(realty[i].PhotoRealty!));
                        textComp = $"<p>Название - {realty[i].NameRealty}," +
                            $"<br/> Цена - {realty[i].Price}," +
                            $"<br/> Адрес - {realty[i].Address}," +
                            $"<br/> Площадь - {realty[i].Square}," +
                            $"<br/> Этажи - {realty[i].Floor}," +
                            $"<br/> Комнаты - {realty[i].Rooms}," +
                            $"<br/> Город - {realty[i].City}," +
                            $"<br/> Тип - {realty[i].Type}," +
                            $"<br/> Для чего - {realty[i].ForWhat}</p>";
                        messegeText = string.Concat(messegeText, textComp);
                    }
                    m.Subject = "Подборка";
                    m.Body = $"<h2>{messegeText}</h2>";
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("testikovich77@mail.ru", "SS9rxQxQp63Yhi1jgXvx");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }           
            }
            
        }
    }
}
