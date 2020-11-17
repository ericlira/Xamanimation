using Xamarin.Forms;

namespace Xamanimation
{
    public static class Transition
    {
        public readonly static BindableProperty CascadeTransitionProperty;

        static Transition()
        {
            CascadeTransitionProperty = BindableProperty.CreateAttached("CascadeTransition", typeof(bool), typeof(BindableObject), true, BindingMode.OneWay, null, null, null, null, null);
        }

        public static bool GetCascadeTransition(BindableObject bindable)
        {
            object value = bindable.GetValue(CascadeTransitionProperty);
            if (value == null)
            {
                value = true;
            }
            return (bool)value;
        }

        public static void SetCascadeTransition(BindableObject bindable, bool value)
        {
            bindable.SetValue(CascadeTransitionProperty, value);
        }
    }
}