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

namespace Intensify.Wpf.Demo.Notify
{
    /// <summary>
    /// NotifyView.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyView : UserControl
    {
        NotificationService notificationService = new NotificationService();

        public NotifyView()
        {
            InitializeComponent();
        }

        private async void notification_Click(object sender, RoutedEventArgs e)
        {
            await notificationService.NotifyAsync("Hello World", TimeSpan.FromSeconds(3));
        }
    }
}
