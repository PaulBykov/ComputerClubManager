using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using ComputerClub.View.modal;

namespace ComputerClub.Services
{
    public class Effector
    {
        private readonly Page _win;

        public Effector(Page page)
        {
            _win = page;
        }

        public static bool? TryApplyModalEffects(Window window) 
        {
            bool? dialogAnswer = false;
            try
            {
                Page parentWindow = ((Frame)(Application.Current.MainWindow.Content)).Content as Page;

                 Effector effector = new Effector(parentWindow);

                effector.ApplyBlurEffect(30);
                dialogAnswer = window.ShowDialog();

                if (dialogAnswer == true)
                {
                    window.Close();
                }

                effector.ClearEffect();
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error,e.ToString());
            }

            return dialogAnswer;
        }

        public void ApplyBlurEffect(uint radious = 30)
        {
            BlurEffect objBlur = new BlurEffect();
            objBlur.Radius = radious;

            _win.Effect = objBlur;
        }
        public void ClearEffect()
        {
            _win.Effect = null;
        }
    }
}
