using System.Diagnostics;
using System.Windows.Controls;

namespace MapEditTool.Views
{
    public partial class SelectBlockPanel : UserControl
    {
        public SelectBlockPanel()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                Debug.WriteLine("SelectBlockPanel DataContext = " + DataContext);
            };
        }
    }
}
