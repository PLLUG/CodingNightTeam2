using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace BotTest1
{
    public partial class Main : Form
    {
        string clientMsgLog = "";
        string botMsgLog = "";
        TelegramBotClient tbc;
        ApiAi apiai;

        public Main()
        {
            InitializeComponent();
            tbc = new TelegramBotClient("391813246:AAEDCf0OVWN0fZJjq-PIRZbM_6Q_7_7330s");
            tbTimer.Start();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var config = new AIConfiguration("90861bda9ce9466791b4b49321460248", SupportedLanguage.English);
            apiai = new ApiAi(config);

            tbc.OnMessage += Tbc_OnMessage;
            tbc.StartReceiving();
        }

        private void Tbc_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //if (e.Message != null)
            //{
            //    messageBox.Text += e.Message.Text + "\n";
            //}
            
            clientMsgLog = e.Message.Chat.FirstName + ": " + e.Message.Text + "\r\n";

            var response = apiai.TextRequest(e.Message.Text);
            tbc.SendTextMessageAsync(e.Message.Chat.Id, response.Result.Fulfillment.Speech);
            botMsgLog = "Bot: " + response.Result.Fulfillment.Speech + "\r\n";
            //throw new NotImplementedException();
        }

        private void tbTimer_Tick(object sender, EventArgs e)
        {
            if (clientMsgLog.Length > 0)
            {
                messageBox.Text += clientMsgLog;
                clientMsgLog = "";
            }

            if (botMsgLog.Length > 0)
            {
                messageBox.Text += botMsgLog;
                botMsgLog = "";
            }
        }
    }
}
