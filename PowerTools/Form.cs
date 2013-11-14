using PowerTools.Business.Manager;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PowerTools
{
    public partial class PowerTools : Form
    {
        #region Fields

        AudioManager audioManager;

        #endregion Fields

        #region Constructors

        public PowerTools()
        {
            InitializeComponent();

            this.audioManager = AudioManager.GetInstance();
        }

        #endregion Constructors

        #region Methods

        #region EventHandlers

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void audioOutputMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.InvokeUIOperation(
                sender: sender,
                action: () => this.UpdateAudioOutputDevices());
        }

        private void audioDeviceMenuItem_Click(object sender, EventArgs e)
        {
            this.InvokeUIOperation(
                sender: sender,
                action: () => this.SetCurrentAudioDevice((sender as ToolStripMenuItem).Name));
        }

        #endregion EventHandlers

        private void InvokeUIOperation(object sender, Action action)
        {
            Control control = sender as Control;

            if (control != null)
            {
                control.UseWaitCursor = true;
                action();
                control.UseWaitCursor = false;
            }
            else
            {
                Application.UseWaitCursor = true;
                action();
                Application.UseWaitCursor = false;
            }
        }

        private void UpdateAudioOutputDevices()
        {
            this.menuItemAudioOutput.DropDownItems.Clear();

            var audioDevices = this.audioManager.GetOutputDevices();

            this.menuItemAudioOutput.DropDownItems.AddRange(
                audioDevices.Select(x => new ToolStripMenuItem(
                    name: x.Id,
                    text: x.Caption,
                    onClick: new EventHandler(audioDeviceMenuItem_Click), 
                    image: null)).ToArray());
        }

        private void SetCurrentAudioDevice(string deviceId)
        {
            this.audioManager.SetCurrentAudioDevice(deviceId);
        }

        #endregion Methods
    }
}
