using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace CourtCheckInPrism.Views
{
    public partial class TabPage : Xamarin.Forms.TabbedPage
    {
        public TabPage()
        {
            InitializeComponent();
            //Placing tab bar at bottom
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.CurrentPageChanged += TabPage_CurrentPageChanged;
            
        }

        private void TabPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            if(TabIndex == 0)
            {
                this.Title = "Visit Schedule";
            }
            else if(TabIndex == 1)
            {
                this.Title = "Visit History";
            }
        }
    }
}