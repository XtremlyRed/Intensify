using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intensify.Wpf.Demo.Dialog
{
    /// <summary>
    /// DialogView.xaml 的交互逻辑
    /// </summary>
    public partial class DialogView : UserControl
    {
        DialogService dialogService = new DialogService();

        public DialogView()
        {
            InitializeComponent();

            DialogService.SetDialogWindiw(new Window());
        }

        private void popup_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextBlock() { Text = ":124343" };

            dialogService.ShowDialog(text);
        }

        private void popup_show_Click(object sender, RoutedEventArgs e) { }
    }
}
