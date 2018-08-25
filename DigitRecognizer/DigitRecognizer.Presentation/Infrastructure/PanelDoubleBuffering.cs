using System;
using System.Reflection;
using System.Windows.Forms;

namespace DigitRecognizer.Presentation.Infrastructure
{
    public static class PanelDoubleBuffering
    {
        private const string DoubleBufferedName = "DoubleBuffered";

        public static void Enable(object target)
        {
            if (target == null)
            {
                throw new NullReferenceException();
            }

            if (!(target is Panel))
            {
                throw new ArgumentException($"Can not enable double buffering on object of type {target.GetType()}");
            }

            typeof(Panel).InvokeMember(DoubleBufferedName,
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, 
                target, 
                new object[] { true });
        }
    }
}
