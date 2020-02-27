using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SSPWorld.Utilities.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += TextChanged_Handler;
        }

        private void TextChanged_Handler(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                return;

            int _;
            if (!int.TryParse(e.NewTextValue, out _))
                ((Entry)sender).Text = e.OldTextValue;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= TextChanged_Handler;
        }
    }
}
