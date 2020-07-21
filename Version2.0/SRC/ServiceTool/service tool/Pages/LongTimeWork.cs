using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service_tool.Pages
{
    public delegate void ValueChangedEventHandler(object sender, ValueEventArgs e);
    public class LongTimeWork
    {
        public event ValueChangedEventHandler ValueChanged;

        // 触发事件的方法
        protected void OnValueChanged(ValueEventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        public void LongTimeMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                // 进行工作
                System.Threading.Thread.Sleep(1000);

                // 触发事件
                ValueEventArgs e = new ValueEventArgs() { Value = i + 1 };
                this.OnValueChanged(e);
            }
        }
    }

    public class ValueEventArgs
    : EventArgs
    {
        public int Value { set; get; }
    }
}
