using DB_993.Classes;
namespace design
{
    /// <summary>
    /// Класс используется для создания нового элемента.
    /// </summary>
    public partial class CreateNewList : Form
    {
        public CreateNewList()
        {
            InitializeComponent();
            Design();
        }

        /// <summary>
        /// Метод настраивает внешний вид элементов управления на форме.
        /// </summary>
        public void Design()
        {
            Text1.Parent = Picture5;
            Text1.BackColor = Color.Transparent;
            Text2.Parent = Picture5;
            Text2.BackColor = Color.Transparent;
        }

        private void CreateCollectionButton_Click(object sender, EventArgs e)
        {
            using (var context = new ApplicationContextBD())
            {
                var newCompilation = new Compilation
                {
                    Name = CollectionNameText.Text
                };
                context.Compilations.Add(newCompilation);
                context.SaveChanges();
            }

        }
    }
}
