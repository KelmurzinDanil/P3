using DB_993.Classes;

namespace design
{
    /// <summary>
    /// Класс используется для добавления новых элементов.
    /// </summary>
    public partial class AddList : Form
    {
        public Dictionary<int, string> Dict { get; set; } = new();
        public int IdRealty { get; set; }
        public AddList()
        {
            InitializeComponent();
            Design();
            FillComboBox();
        }
        public AddList(int idRealty)
        {
            InitializeComponent();
            Design();
            FillComboBox();
            IdRealty = idRealty;
        }
        private void CreateCollectionButton_Click(object sender, EventArgs e)
        {
            CreateNewList createNewList = new CreateNewList();
            createNewList.ShowDialog();
        }

        /// <summary>
        /// Метод настраивает внешний вид элементов управления на форме.
        /// </summary>
        public void Design()
        {
            Text1.Parent = Picture4;
            Text1.BackColor = Color.Transparent;
            ChooseButton.Parent = Picture4;
            ChooseButton.BackColor = Color.Transparent;
        }
        public void FillComboBox()
        {
            using (var context = new ApplicationContextBD())
            {
                var compilation = context.Compilations.ToList();
                foreach (var comp in compilation)
                {
                    CollectionsCombo.Items.Add(comp.Name!).ToString();
                    Dict.Add(comp.Id, comp.Name!);
                }
            }
        }

        private void AddCollectionBtn_Click(object sender, EventArgs e)
        {
            if (CollectionsCombo.Text == String.Empty)
            {
                using (var context = new ApplicationContextBD())
                {
                    var realty = new Realty
                    {
                        //CompilationId = 
                    };
                }
            }
        }
    }
}
