using ComputerClub.View.windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace ComputerClub.Services
{
    public class Effector
    {
        Page _win;

        public Effector(Page page)
        {
            _win = page;
        }

        public static void TryApplyModalEffects(Window window) 
        {
            try
            {
                Page parentWindow = ((Frame)(Application.Current.MainWindow.Content)).Content as Page;
                Effector effector = new Effector(parentWindow);

                effector.ApplyBlurEffect(30);

                if (window.ShowDialog() == true) 
                {
                    window.Close();
                }

                effector.ClearEffect();
            }
            catch(Exception e) 
            {

            }
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
