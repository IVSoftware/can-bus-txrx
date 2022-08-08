﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static can_bus_timer.McuController;

namespace can_bus_timer
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void onPacketResponded(object sender, EventArgs e)
        {
            string text;
            var packet = (Packet)sender;
            if (packet.MockResponse is uint[] bytes)
            {
                text = string.Join(" ", bytes.Select(_ => _.ToString("X4")));
            }
            else
            {
                text = $"{packet.MockResponse}";
            }
            BeginInvoke((MethodInvoker)delegate
            {
                switch (packet.RequestID)
                {
                    case RequestID.REQ1:
                        richTextBox.SelectionColor = Color.Navy;
                        break;
                    case RequestID.REQ2:
                        richTextBox.SelectionColor = Color.DarkGreen;
                        break;
                    case RequestID.REQ3:
                        richTextBox.SelectionColor = Color.Maroon;
                        break;
                    default: throw new InvalidOperationException();
                }
                richTextBox.AppendText($"{DateTime.Now}: {text}{Environment.NewLine}");
                richTextBox.ScrollToCaret();
            });
        }

        McuController _busController = new McuController();

        private async void checkBoxRun_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRun.Checked)
            {
                await Task.Run(() => loop());
                labelIndicator.BackColor = Color.LightGray;
            }
        }
        private async Task loop()
        {
            do
            {
                // Nondeterministic. But you MUST wait for a bus read to complete
                // or the messages will stack up and cause a system fail eventually.
                await readCanBusSingle();
                // Space the bus reads by 100 ms or whatever. Here it's slow for demo.
#if PRODUCTION
                await Task.Delay(TimeSpan.FromMilliseconds(100));
#else
                await Task.Delay(TimeSpan.FromSeconds(1));
#endif

            }
            while (checkBoxRun.Checked);
        }
        /// <summary>
        /// Send all the requests and either wait for each one to respond or not.
        /// </summary>
        private async Task readCanBusSingle()
        {
            string text;
            if (_busController.TryConnect())
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    labelIndicator.BackColor = Color.LightGreen;
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold);
                    richTextBox.AppendText($"{Environment.NewLine}{DateTime.Now}: REQUEST{Environment.NewLine}");
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Regular);
                });
                foreach (RequestID requestID in Enum.GetValues(typeof(RequestID)))
                {
                    var req = new Packet { RequestID = requestID };
                    if (checkBoxSynchronous.Checked)
                    {
                        await _busController.SendReq(unitIndex: 1, packet: req);
                    }
                    else
                    {
                        // Receive a disposable task
                        _ = _busController.SendReq(unitIndex: 1, packet: req);
                    }
                }
            }
            else
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    labelIndicator.BackColor = Color.LightSalmon;
                    richTextBox.SelectionColor = Color.Red;
                    richTextBox.AppendText($"{Environment.NewLine}Connection lost{Environment.NewLine}");
                    richTextBox.ScrollToCaret();
                });
            }
        }
    }
    public enum RequestID { REQ1, REQ2, REQ3 }

    public class McuController
    {
        public McuController() => Packet.Responded += onPacketResponded;

        private void onPacketResponded(object sender, EventArgs e)
        {
            RXQueue.Enqueue((Packet)sender);
        }

        public Queue<Packet> RXQueue { get; } = new Queue<Packet>();
        internal bool TryConnect()
        {
            switch (Rando.Next(10))
            {
                case 0:
                    // Fails every now and then
                    return false;
                default:
                    return true;
            }
        }

        internal async Task SendReq(int unitIndex, Packet packet)
        {
            // TXQueue.Enqueue(packet);
            await packet.SetMockResponse(TimeSpan.FromMilliseconds(Rando.Next(10, 51)));
        }

        public static Random Rando { get; } = new Random();
    }
    public class Packet
    {
        public RequestID RequestID { get; set; }
        public int UnitID { get; set; }
        public int CommandLength { get; set; }
        public byte[] CommandBytes { get; set; }
        public int ResponseLength { get; set; }
        public byte[] ResponseBytes { get; set; }

        public static event EventHandler Responded;

        // MOCK
        public object MockResponse { get; set; }
        public async Task SetMockResponse(TimeSpan mockDelay)
        {
            await Task.Delay(mockDelay);
            switch (RequestID)
            {
                case RequestID.REQ1:
                    // String
                    MockResponse =
                        $"Unit {UnitID}";
                    break;
                case RequestID.REQ2:
                    // ByteArray
                    MockResponse =
                        Enumerable.Range(0, 8)
                        .Select(_ => (uint)Rando.Next(45000, 55000))
                        .ToArray();
                    break;
                case RequestID.REQ3:
                    MockResponse =
                        mockDelay;
                    break;
                default: throw new InvalidOperationException();
            }
            Responded?.Invoke(this, EventArgs.Empty);
        }
    }
}
