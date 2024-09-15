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
    /// PopupContent.xaml 的交互逻辑
    /// </summary>
    public partial class PopupContent : UserControl, IPopupAware
    {
        public PopupContent()
        {
            InitializeComponent();
        }

        public event Action<object>? RequestCloseEvent;

        public void Closed() { }

        public void Opened(PopupParameter? parameter) { }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RequestCloseEvent?.Invoke(this);
        }
    }
}
