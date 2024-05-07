using DB_993.Classes;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace design
{
    /// <summary>
    /// Класс используется для отображения и взаимодействия с коллекцией карточек.
    /// </summary>
    public partial class CollectionCard : Form
    {
        //cQ8-jbJ-Tin-9tA
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
                //// отправитель - устанавливаем адрес и отображаемое в письме имя
                //MailAddress from = new MailAddress("danil.kelmurzin@mail.ru", "ООО ДДД");
                //// кому отправляем
                //MailAddress to = new MailAddress(Email);
                //// создаем объект сообщения
                //MailMessage m = new MailMessage(from, to);
                //// тема письма
                //m.Subject = "Подборка";
                //// текст письма
                //m.Body = "<h2>----</h2>";
                //// письмо представляет код html
                //m.IsBodyHtml = true;
                //// адрес smtp-сервера и порт, с которого будем отправлять письмо
                //SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
                //// логин и пароль
                //smtp.Credentials = new NetworkCredential(Email, "password");
                //smtp.EnableSsl = true;
                //smtp.Send(m);
                try
                {

                    SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru", 465);
                    mySmtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential basicAuthenticationInfo = new
                       System.Net.NetworkCredential("testikovich77@mail.ru", "cQ8-jbJ-Tin-9tA");
                    mySmtpClient.Credentials = basicAuthenticationInfo;
                    MailAddress from = new MailAddress(Email, "TestFromName");
                    MailAddress to = new MailAddress(Email, "TestToName");
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                    // add ReplyTo
                    MailAddress replyTo = new MailAddress(Email);
                    myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    myMail.Subject = "Test message";
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    mySmtpClient.Send(myMail);
                }

                catch (SmtpException ex)
                {
                    throw new ApplicationException
                      ("SmtpException has occured: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }
    }
}
