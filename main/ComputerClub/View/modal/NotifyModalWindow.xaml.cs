using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ComputerClub.Services;

namespace ComputerClub.View.modal
{
    public partial class NotifyModalWindow : Window
    {
        public enum NotifyKind { Error, Success }

        private NotifyKind _notifyKind;


        public NotifyModalWindow(string msg, NotifyKind notifyKind)
        {
            _notifyKind = notifyKind;
            Message = msg;
            InitializeComponent();
        }
        
        
        public ImageSource Img
        {
            get
            {
                string fileName = "";

                if (_notifyKind == NotifyKind.Error)
                    fileName = "error.png";
                
                if(_notifyKind == NotifyKind.Success)
                    fileName = "success.png";

                return new BitmapImage(new Uri($"/assets/icons/{fileName}", UriKind.RelativeOrAbsolute));
            }
        }

        public string Message { get; private set; }

        public static void Show(NotifyKind notifyKind = NotifyKind.Success, string message = "Успех!")
        {
            var window = new NotifyModalWindow(message, notifyKind);

            Effector.TryApplyModalEffects(window);
        }
    }
}
