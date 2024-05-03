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


        public void ApplyBlurEffect(uint radious = 15)
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
