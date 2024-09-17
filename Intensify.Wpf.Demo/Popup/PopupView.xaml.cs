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

namespace Intensify.Wpf.Demo.Popup
{
    /// <summary>
    /// PopupView.xaml 的交互逻辑
    /// </summary>
    public partial class PopupView : UserControl
    {
        PopupService popupService = new PopupService();

        public PopupView()
        {
            InitializeComponent();
        }

        private async void popup_show_Click(object sender, RoutedEventArgs e)
        {
            await popupService.ShowAsync("测试");
        }

        private async void popup_Click(object sender, RoutedEventArgs e)
        {
            var visual = new PopupContent();

            await popupService.PopupAsync<object>(visual);
        }

        private async void popup_confirm_Click(object sender, RoutedEventArgs e)
        {
            await popupService.ConfirmAsync("测试", null, new[] { "确定", "取消" });
        }
    }
}
