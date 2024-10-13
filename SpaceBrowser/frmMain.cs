using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceBrowser
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void TxtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//enter
                browser.Load(txtUrl.Text);
        }

        ChromiumWebBrowser browser;

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser(txtUrl.Text);
            browser.Dock = DockStyle.Fill;
            this.pContainer.Controls.Add(browser);//hopefully should be the browser page
            browser.AddressChanged += Chrome_AddressChanged;

        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                txtUrl.Text = e.Address;
            }));
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (browser.CanGoBack)
                browser.Back();
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            if (browser.CanGoForward)
                browser.Forward();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            browser.Refresh();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Shutdown cefsharp coz it cant do itself
            Cef.Shutdown();
        }
    }
}
