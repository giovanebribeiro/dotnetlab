using System.Globalization;
using System;
using System.Windows.Forms;

namespace MyWinFormsControl
{
    [ProvideToolboxControl("General", false)]
    public partial class Counter : UserControl
    {
        int currentValue;
        string displayText;
        public EventHandler Incremented;

        public Counter()
        {
            InitializeComponent();
        }

        public int Value
        {
            get{ return currentValue; }
        }

        public string Message
        {
            get { return displayText; }
            set { displayText = value; }
        }

        public bool ShowReset
        {
            get { return btnReset.Visible; }
            set
            {
                btnReset.Visible = value;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format(CultureInfo.CurrentUICulture, "We are inside {0}.Button1_Click()", this.ToString()));
        }

        private void Counter_Load(object sender, EventArgs e)
        {
            currentValue = 0;
            label1.Text = Message + Value;
        }

        public void Increment()
        {
            currentValue++;
            label1.Text = displayText + Value;
            Incremented(this, EventArgs.Empty);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            currentValue = 0;
            label1.Text = displayText + Value;
        }
    }
}
