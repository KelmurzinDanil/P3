﻿using DB_993.Classes;

namespace design
{
    public partial class MyCollections : Form
    {
        public Dictionary<int, string> Dict { get; set; } = new Dictionary<int, string>();
        public MyCollections()
        {
            InitializeComponent();
            Design();
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

                var listComp = context.Compilations.ToList();
                for (int i = 0; i < listComp.Count; i++)
                {
                    var listViewComp = new ListViewItem(new string[] { listComp[i]!.Name!.ToString()! });
                    Dict.Add(listComp[i].Id, listComp[i].Name!);
                    CompList.Items.Add(listViewComp);
                }
            }
        }
        private void CompList_ItemActivate(object sender, EventArgs e)
        {
            var cCard = new CollectionCard();
            cCard.Show();
        }
    }
}
