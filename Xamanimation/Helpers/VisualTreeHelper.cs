namespace Xamanimation.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xamarin.Forms;

    public static class VisualTreeHelper
    {
        public static T GetParent<T>(this Element element) where T : Element
        {
            if (element is T)
            {
                return element as T;
            }
            if (element.Parent != null)
            {
                return element.Parent.GetParent<T>();
            }
            return default(T);
        }

        public static IEnumerable<T> GetChildren<T>(this Element element) where T : Element
        {
            var properties = element.GetType().GetRuntimeProperties();
            var contentProperty = properties.FirstOrDefault(w => w.Name == "Content");
            if (contentProperty == null || !Transition.GetCascadeTransition(element))
            {
                var childrenProperty = properties.FirstOrDefault(w => w.Name == "Children");
                if (childrenProperty != null && Transition.GetCascadeTransition(element))
                {
                    IEnumerable children = childrenProperty.GetValue(element) as IEnumerable;
                    foreach (var child in children)
                    {
                        if (child is Element childVisualElement)
                        {
                            if (childVisualElement is T)
                            {
                                yield return childVisualElement as T;
                            }

                            foreach (var childVisual in childVisualElement.GetChildren<T>())
                            {
                                yield return childVisual;
                            }
                        }
                    }
                }
            }
            else
            {
                if (contentProperty.GetValue(element) is Element contentElement)
                {
                    if (contentElement is T)
                    {
                        yield return contentElement as T;
                    }
                    foreach (var t in contentElement.GetChildren<T>())
                    {
                        yield return t;
                    }
                }
            }
        }
    }
}