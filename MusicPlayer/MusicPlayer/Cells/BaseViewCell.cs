using System;
using Xamarin.Forms;

namespace MusicPlayer.Cells
{
    public class BaseViewCell : ViewCell
    {
        public static readonly BindableProperty ParentBindingContextProperty =
            BindableProperty.Create(nameof(ParentBindingContext),
                typeof(object), typeof(BaseViewCell),
                (object)null, BindingMode.OneWay,
                (BindableProperty.ValidateValueDelegate)null,
                (BindableProperty.BindingPropertyChangedDelegate)null,
                (BindableProperty.BindingPropertyChangingDelegate)null,
                (BindableProperty.CoerceValueDelegate)null,
                (BindableProperty.CreateDefaultValueDelegate)null);

        public object ParentBindingContext
        {
            get
            {
                return this.GetValue(BaseViewCell.ParentBindingContextProperty);
            }
            set
            {
                if (object.Equals(this.ParentBindingContext, value))
                    return;
                this.SetValue(BaseViewCell.ParentBindingContextProperty, value);
                this.OnPropertyChanged(nameof(ParentBindingContext));
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            this.ParentBindingContext = this.Parent?.BindingContext;
        }
    }
}
