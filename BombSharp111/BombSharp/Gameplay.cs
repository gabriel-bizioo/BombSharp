namespace BombSharp
{
    public partial class Gameplay : Form
    {
        public Gameplay()
        {
            InitializeComponent();
        }

        public void LoadGame(int lvl)
        {
            string map = string.Empty;
            switch (lvl)
            {
                default:
                    MessageBox.Show($"Can't find {lvl} lvl");
                    return;
                case 1:
                    map = Properties.Resources.lvl1;
                    break;
            }
        }
    }
}