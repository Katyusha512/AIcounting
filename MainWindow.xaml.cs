using AIcounting.Accounting;
using AIcounting.AI;
using AIcounting.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AIcounting
{
    public partial class MainWindow : Window
    {
        public string AccNameList;
        public string InvoiceEx1;
        public string InvoiceEx2;

        public AccountsChart Company2023Accounts;

        public MainWindow()
        {
            //NOTE: Pure strings, as if it was generated text, unmodified, from pdf or OCR or similar
            InvoiceEx1 = "Account Number\r\n\r\nPIN Number\r\n\r\nBill Date\r\n\r\n123123123\r\n\r\n11111\r\n\r\n10/24/2018\r\n\r\nCity of Blue Springs Utility Billing\r\n903 W. Main St.\r\nBlue Springs, MO 64015\r\n816-622-4444 Customer Service\r\n816-228-0220 24 hour, Pay-by-Phone Line\r\nwww.bluespringsgov.com\r\n\r\nBilling Service Period\r\n09/11/2018\r\n\r\nTO\r\n\r\nService Duration\r\n\r\n10/12/2018\r\n\r\n31\r\n\r\nService Address\r\n123 W ANYWHERE\r\n\r\nJOHN Q. PUBLIC\r\n123 W ANYWHERE\r\nBLUE SPRINGS MO 64014\r\nService\r\n\r\nPrior Reading Current Reading Consumption\r\n\r\nBalance Forward - PLEASE PAY IMMEDIATELY\r\nWATER RES\r\nSEWER-RES RES\r\nTAX RES\r\n\r\n41764\r\n\r\nFOR BILLING INQUIRIES PLEASE CALL 816-622-4444\r\nBETWEEN 8:00 A.M. AND 5:00 P.M M - F, OR\r\nEMAIL US AT water@bluespringsgov.com\r\n\r\n42739\r\n\r\n975\r\n\r\nService Amount\r\n$XXX.XX\r\n$649.10\r\n$554.20\r\n$315.05\r\n\r\nTOTAL AMOUNT DUE BY\r\n\r\n11/21/2018\r\n\r\n$299.15\r\n\r\nIF PAID AFTER\r\n\r\n11/21/2018\r\n\r\n$250.45\r\n\r\nCITY BULLETIN BOARD:\r\n\r\n13 Month Consumption History\r\n\r\n**WEBSITE/PAY-BY-PHONE PAYMENTS - ALLOW 24 TO 48\r\nBUSINESS HOURS TO POST TO YOUR UTILITY ACCOUNT**\r\n**CURRENT CHARGES ARE DUE ON OR BEFORE THE DUE\r\nDATE - SHOULD ANY \"BALANCE FORWARD\" BE PRESENT IT\r\nIS DUE IMMEDIATELY AND COULD BE SUBJECT TO\r\nADDITIONAL PENALIZATION OR DISCONNECTION**\r\n\r\nPrevious Balance Due Immediately\r\n\r\nJOHN Q. PUBLIC\r\n\r\n123 W. ANYWHERE\r\nRETURN THIS PORTION WITH YOUR PAYMENT. DO NOT SEND CASH.\r\nMake Check Payable To: Blue Springs Water Dept.\r\n\r\n• Pay your bill online at www.bluespringsgov.com\r\n• 24 Hour Pay-By-Phone Line (816) 228-0220\r\n• In person at Blue Springs City Hall, 903 W. Main Street\r\n• After Hours Drop Box, located in the City Hall parking lot.\r\n\r\nPayment must be RECEIVED, NOT POSTMARKED, by the due date to avoid a late payment charge.\r\n\r\nAccount Number\r\n123123123\r\n\r\nPIN Number\r\n11111\r\n\r\nBALANCE FORWARD\r\n\r\n$XXX.XX\r\n\r\nCURRENT AMOUNT DUE 11/21/2018\r\n\r\n$XXX.XX\r\n\r\nTOTAL AMOUNT DUE\r\n\r\n$XXX.XX\r\n\r\nIF RECEIVED AFTER 11/21/2018\r\nIf your address has changed, check here\r\nand complete other side\r\n\r\nCITY OF BLUE SPRINGS UTILITY BILLING\r\nPO BOX 219253\r\nKANSAS CITY, MO 64121-9253\r\n\r\n001231230012312300123\r\n\r\nAMOUNT PAID\r\n\r\n$XXX.XX\r\n\r\n\fCity of Blue Springs, Missouri\r\nAddress:\r\n\r\n903 W Main Street, Blue Springs, MO 64015\r\n\r\nOffice Hours:\r\n\r\n8:00 a. m. to 5:00 p. m. Weekdays. Closed most major Holidays.\r\n\r\nBilling Inquiries:\r\n\r\n(816) 622-4444\r\n\r\nUnderstanding Your Bill:\r\nYour bill includes charges for water and sanitary sewer services as applicable. The water portion of your bill has three separate charges: administrative fee, volume charge and sales tax.\r\nThe sanitary sewer portion of the bill is comprised of an administrative fee and volume charge. For more detailed information about the rates, please go to www.bluespringsgov.com.\r\nYour meter is read approximately every 30 days. Sometimes your meter cannot be read due to inclement weather, lack of access to the meter, a threatening pet, locked gates, etc. which\r\nmay result in an estimated billing.\r\nOccassionally, unusual bills represent a plumbing problem such as a leaking flapper in a toilet or other potential leaks within the customers system. Dye packets are available at the Utility\r\ncounter for proper toilet testing. A Customer Service Representative may have the account re-read or a meter check performed to assist you with these situations.\r\nWinter Sewer Average:\r\nSewer charges are based on your water consumption. Water consumption however may vary as customers irrigate their lawns, fill pools, wash cars, or do other outdoor activities. To\r\nestimate the amount of water that actually flows into the sanitary sewer system, The City of Blue Springs Utilities uses a Winter Sewer Average for Residental Customers only.\r\nYour winter sewer average is calculated using the water usages shown on your January, February, March, and April bills. The winter sewer average is based on your winter usage unless\r\nyour actual winter usage is less than your average and is applied on the May through December bills. In that case you will be billed for actual usage. New customers receive a default\r\naverage of 7,500 gallons.\r\nReading Your Residential Water Meter\r\nResidential meters in 5/8\", 3/4\", and 1\" sizes, consist of a single positive displacement measuring element that translates notations of the element into a visually readable odometer, much\r\nlike the odometer in an automobile.\r\nThe grand total cumulative value of water that has passed through the meter is the value shown on the odometer. Typically, this value is determined over a period of time (a billing\r\nperiod). To determine the consumption value over a period of time, an initial reading from the register must be obtained at the beginning of the time period. At the end of the time period, a\r\nsecond reading from the register must be obtained. Subtract the initial reading from the second reading to determine the consumption value from the register over the time period. Make\r\ncertain your values are consisten (tens, hundreds, thousands, etc.). Refer to the dial information below.\r\nExample: 5/8\" residential meter registering in US GALLONS\r\nContains six indexing wheels and one \"fixed\" zero, where each complete sweep hand revolution = 10 Gallons.\r\nExample reading:\r\nwheels\r\nfixed zeros\r\ndefined volume\r\nOdometer:\r\n164225\r\n0\r\n=\r\n1,642,250 gallons\r\n**Larger Commercial meters with more than 6 measuring elements will register in 100's\r\nPayment options:\r\n•\r\nMail your payment in the return envelope along with the bottom (remittance) portion of the bill.\r\n•\r\nPay your bill online at www.bluespringsgov.com. Blue Springs Utilities accepts MasterCard, VISA, Discover and E-check online. To access your account, you will need your\r\naccount and customer PIN number located on the top portion of your bill.\r\n•\r\n24 Hour Pay-by-Phone Line (816) 228-0220, accepting MasterCard, VISA, and Discover.\r\n•\r\nPay your bill through Bank Drafting. Payments are automatically drafted from your checking or savings account. To learn more about this program, visit\r\nwww.bluespringsgov.com or contact a Customer Service Representative at (816) 622-4444.\r\n•\r\n";
            InvoiceEx2 = "Page 1 of 2\r\nCashierʼs Office\r\nBHDPR\r\nBaldwin Wallace University\r\n275 Eastland Road\r\nBerea, OH 44017-2088\r\nForwarding Service Requested\r\n\r\nSTUDENT ID\r\n0481655\r\n0000000\r\n\r\nSTUDENT NAME\r\nGobor,STUDENT\r\nJessica\r\nNAME,\r\n\r\nTERM\r\n\r\nPAYMENT DUE DATE\r\n08/24/2018\r\n01/12/2018\r\n\r\nPAY THIS AMOUNT\r\n$3,184.00\r\n$6.24\r\n\r\nPAYMENT AMOUNT ENCLOSED\r\n\r\nPay online with e-check or credit card at bursar.bw.edu.\r\n\r\n/440172005755/\r\n\r\n1\r\n\r\nBALDWIN WALLACE UNIVERSITY\r\n275 EASTLAND RD\r\nBEREA OH 44017-2005\r\n\r\nSTUDENT\r\nNAME\r\nMS.\r\nJESSICA\r\nGOBOR\r\nSTREET\r\nADDRESS\r\nRUA CAETANO MUNHOZ DA ROCHA\r\nCITY, STATE 00000\r\n309\r\nCAMPO LARGO PARANA\r\n\r\n*0481655STAR00000624*\r\n*0481655STAR00000624*\r\nReturn above portion with your payment, or, pay online at bursar.bw.edu.\r\n\r\nStudent ID: 0000000\r\n0481655\r\nDATE\r\n********\r\n\r\nITEM DESCRIPTION\r\nBW\r\nFinance -Charge\r\nRegistration\r\n2018FA\r\n\r\n08/01/18\r\n\r\nMeal Plan\r\n\r\n08/01/18\r\n08/01/18\r\n\r\nBoardNew\r\n- Double\r\nRoom\r\nTotal\r\nCharges\r\nTotal New\r\nCharges\r\nEnding\r\nBalance\r\n\r\nPay This Amount: $3,184.00\r\n$6.24\r\n\r\nCHARGES\r\n\r\nBalance Forward *********\r\n\r\n12/12/17\r\n08/01/18\r\n08/01/18\r\n\r\nStudent Name: NAME,\r\nGobor, STUDENT\r\nJessica\r\n\r\nA\r\n\r\nDean’s Scholarship\r\n\r\nBW Heritage Award\r\nTotal Credits\r\n\r\nCREDITS\r\n\r\n0.00\r\n6.22\r\n\r\n0.02\r\n16,293.00\r\n\r\n0.02\r\n\r\n0.02\r\n2,759.00\r\n2,882.00\r\n\r\n21,934.00)\r\n6.24 *\r\n\r\n21,934.00\r\n\r\nB\r\n\r\nBALANCE\r\n\r\n12,000.00\r\n3,500.00\r\n\r\n15,500.00\r\n\r\n(15,500.00)\r\n6,434.00)\r\n\r\nBalance Before Anticipated Credits\r\nAnticipated Credits:\r\n2018FA\r\n2018FA\r\n\r\nFederal Direct Subsidized\r\nFederal Direct Unsubsidiz\r\n\r\nTotal Anticipated Credits\r\nBalance includes anticipated credits\r\n\r\n* If there is a balance due, a 5% A.P.R. finance charge (0.417% per month)\r\nwill be added until the amount decreases to zero or below as a result of\r\nadditional credit/payments received.\r\n\r\nC\r\n\r\n2,250.00\r\n1,000.00\r\n3,250.00\r\n\r\n(3,250.00)\r\n3,184.00*\r\n\r\n440-826-2217\r\nCashierʼs:\r\n440-826-3073\r\nFax:\r\n440-826-2108\r\nFinancial Aid:\r\n440-826-2114\r\nResidentʼs:\r\ncashier@bw.edu\r\nEmail:";
            InitializeComponent();
            ImportData();
        }

        public void ImportData()
        {
            Company2023Accounts = SimpleIO.ImportAccounts();

            foreach (var item in Company2023Accounts.Accounts)
            {
                if (item.AccountNo >= 4000 && item.AccountNo < 5500)
                    AccNameList += $"{item.AccountName}\n";
                //NOTE: Shortened. Token usage too big ~ 16k. 
            }            
        }

        void InitQuestion(in string InvoiceSource)
        {
            //ask about the date of the invoice
            ExampleTextBox_Invoice.Text = InvoiceSource;
            ExampleTextBox_BotResponse.Clear();
            AIClient.Init_GPT();
            AIClient.SetChatSysMessage("You are a helpful accountant of a software company, doing all the bookkeeping and helping with any accounting related task", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Here is an invoice, I want you to answer me several questions about it. Invoice: {InvoiceSource} ", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Report me the issue date in the format yyyy/mm/dd. No extra info or explanation, just the date.", DEPLOYMENT_OPTION.GPT);

            string[] OutputZ = AIClient.PullGPTResponse();
            DateTime IssueDate;
            if (!DateTime.TryParse(OutputZ[0], out IssueDate))
            {
                Console.WriteLine("no date provided or parsing error");
            }
            Booking_Date.Text = OutputZ[0];
            Booking_Date.DisplayDate = IssueDate;

            ExampleTextBox_BotResponse.Text += "Bot invoice date detected:   " + OutputZ[0] + "\n";
            ExampleTextBox_BotResponse.Text += "beep..boop.. setting booking date ... " + Booking_Date.DisplayDate + "\n\n";
            
            //add booking ID
            Booking_ID.Text = Company2023Accounts.RequestBookingID().ToString();

            //add invoice amount
            AIClient.Init_GPT();
            AIClient.SetChatSysMessage("You are a helpful accountant of a software company, doing all the bookkeeping and helping with any accounting related task", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Here is an invoice, I want you to answer me several questions about it. Invoice: {InvoiceSource} ", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"What amount do I have to pay. Your response will contain ONLY the amount, no extra characters.", DEPLOYMENT_OPTION.GPT);
            string[] OutputA = AIClient.PullGPTResponse();
            ExampleTextBox_BotResponse.Text += "Bot - detecting invoice total amount..: " + OutputA[0] + "\n";

            float Amount;
            var Style = System.Globalization.NumberStyles.Number |
                        System.Globalization.NumberStyles.AllowCurrencySymbol |
                        System.Globalization.NumberStyles.AllowParentheses;
            var Culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            float.TryParse(OutputA[0], Style, Culture, out Amount);

            Booking_AmountLocal.Text = Amount.ToString();
            ExampleTextBox_BotResponse.Text += "beep.. boop.. setting invoice amount ... " + Booking_AmountLocal.Text + "\n\n";

            //add short description to the accounting entry
            AIClient.Init_GPT();
            AIClient.SetChatSysMessage("You are a helpful accountant of a software company, doing all the bookkeeping and helping with any accounting related task", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Here is an invoice, I want you to answer me several questions about it. Invoice: {InvoiceSource} ", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Give me the Name of the issuer and a max 8 words summary for the reason of the invoice. No other extra information or explanation. Just name and summary", DEPLOYMENT_OPTION.GPT);
            string[] OutputB = AIClient.PullGPTResponse();
            ExampleTextBox_BotResponse.Text += "Bot response: " + OutputB[0] + "\n";
            Booking_Text.Text = OutputB[0];
            ExampleTextBox_BotResponse.Text += "beep.. boop.. setting booking text ... " + Booking_Text.Text + "\n";

            AIClient.Init_GPT();
            AIClient.SetChatSysMessage("You are a helpful accountant of a software company, doing all the bookkeeping and helping with any accounting related task", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Here is an invoice, I want you to answer me several questions about it. Invoice: {InvoiceSource} ", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"Here is the list of our accounts. Based on the reason of the invoice, name me THE ONE SINGLE account most suitable for accounting the invoice. I want the exact name of ONE account from the list." +
            $"-> list of accounts: {AccNameList}", DEPLOYMENT_OPTION.GPT);

            string[] OutputC = AIClient.PullGPTResponse();
            ExampleTextBox_BotResponse.Text += "Bot response: " + OutputC[0] + "\n";

            int AccountNumber = 1099; //clarification account in case the bot doesn't find a proper account;
            var acc = Company2023Accounts.Accounts.Find(x => x.AccountName == OutputC[0]);
            if (acc != null)
            {
                AccountNumber = acc.AccountNo;
            }

            Booking_AccDebit.Text = AccountNumber.ToString();
            ExampleTextBox_BotResponse.Text += "beep.. boop.. setting account number ... " + Booking_AccDebit.Text + "\n";

            Booking_AccCredit.Text = "2000"; //standard account for invoices. will be checked after payment when bank statement is processed electronically.

            //placeholder
            //ProcessTransaction();
            //MoveNext();

            //more things should be done here. - keep a database of all stakeholders and update accounting related  info on them.

        }

        private void File_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Book_Click(object sender, RoutedEventArgs e)
        {
            //placeholder

            //ValidateInput();
            //ProcessTransaction();
        }

        //proceed to the next field when pressing enter on a selected txtbx
        private void Booking_Currency_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }

        private void Booking_AmountLocal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }

        private void Btn_Example1Show_Click(object sender, RoutedEventArgs e)
        {
                InitQuestion(InvoiceEx1);
        }

        private void Btn_Example2Show_Click(object sender, RoutedEventArgs e)
        {
                InitQuestion(InvoiceEx2);
        }

        //Dream Catcher
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DreamAnalisys(250);
        }

         void DreamAnalisys(int MAX_LEN)
        {
            string ProcessingText = Input_Dream_Description.Text;
            if (ProcessingText.Length < 20)
            {
                ProcessingText = "I had a dream within a dream within additional dream. And the first one was lucid. This happened yesterday.\r\n\r\nI didn't sleep well last night. I've got a lot of things I'm worried about and fell into a restless, uneasy sleep.\r\n\r\nI've had dreams of falling to my death since I was a small child, but I had a powerful experience 20 years ago (remind me to tell you about that sometime), and because of that I've been able to take control of nightmares and keep them from happening. And through this process I've learned how to tell (sometimes!) that I'm dreaming. (Hint, look at your hands when a nightmare is starting)\r\n\r\nLast night I dreamt I was in an old rickety attic and as I tried to navigate down the stairs they started to crumple and I realized I was about to fall several stories down. This is what “woke\" me up to the fact that I was definitely dreaming! I realized I wasn't awake, forced myself not to fall, and then was so excited to know that I was, in fact in dreamstate yet aware, that I decided to try to fly. I went to the edge of the window in the attic and spiraled up into the air like a hawk using wind currents, but a bit sloppy and unsure of my capabilities. I got scared and decided to land in a tall fir tree in order to regroup before trying to really soar.\r\n\r\nBut then I woke up. Annoying! I was just getting comfortable and was so disappointed to not be able to really fly like I wanted.\r\n\r\nLater, I was talking to my friend Thomas and telling him about my dream, and how cool it was to know that I was dreaming and not just be beholden to the dream itself; to not be along for the ride so to speak, but have altered the outcome from my own awareness of being in a dream. And how amazing it was to go from falling to flying…!\r\n\r\nAnd then I woke up. Again. Wtf?\r\n\r\nThat was a moment of weirdness. Things were just so normal talking to Thomas, it took a minute to believe that both had been a dream.\r\n\r\nIt was now 3:33am and I was totally wide awake. I got up, a little bit frustrated because I have a really busy day ahead of me, but made some tea, let the cat out, and chuckled at how complicated and complex, and yet so simple the human mind/psyche is.\r\n\r\nAnd then, I actually, truly woke up. ALL of it had been a dream! It was really discombobulating because everything seemed so crystal clear, and crazy as it sounds, ordinary. It's been a lot of hours, I'm 98% sure that I'm back in waking reality, but who knows….";
                Input_Dream_Description.Text = "Not enough info provided. You're wasting the time of a professional. Let me tell you about my dream";
                Input_Dream_Description.Text += $"\n\n{ProcessingText}";
            }

            if (ProcessingText.Length > MAX_LEN) 
            {
                Input_Dream_Description.Text = $"Read my analisis below. You can email me the rest of the story .. I only process {MAX_LEN} characters worth of input.";
                ProcessingText = ProcessingText.Substring(0, MAX_LEN);
            }

            Input_Dream_Analysis.Clear();
            AIClient.Init_GPT();
            AIClient.SetChatSysMessage("You are a psychologist, specialized on the analysis of dreams", DEPLOYMENT_OPTION.GPT);
            AIClient.AddChatUsrMessage($"I describe you a dream as follows. Write a 50 words analysis, focus on the meaning of pictures and symbols described in my story, " +
                $"along other psychologycal point of views." +
            $". ->My dream: {ProcessingText} ", DEPLOYMENT_OPTION.GPT);

            string[] Output = AIClient.PullGPTResponse();
            Input_Dream_Analysis.Text = Output[0];

            Input_Dream_Analysis.Text += "\n\nThanks for doing business with me. You have a nice day now!";


            string AdjustedText = $"I had this dream last night. Draw it for me in fantasy digital art style: Dream->  :{ProcessingText}";
            AIClient.Init_DallE(AdjustedText, Azure.AI.OpenAI.ImageSize.Size256x256, 4);
            
            var Images = AIClient.PullDallEPic();

            if (Images == null) { return; }
            int c = 0;
            foreach (var Img in Images) 
            {
                var Location = Img.Url;
                if (Location == null) continue;

                BitmapImage Bitmap= new();
                Bitmap.BeginInit();
                Bitmap.UriSource = Location;
                Bitmap.DecodePixelWidth = 256;
                Bitmap.EndInit();

                if (c == 0) 
                {
                    DreamImg1.Source = Bitmap;
                }
                else if (c == 1)
                {
                    DreamImg2.Source = Bitmap;
                }
                else if (c == 2)
                {
                    DreamImg3.Source = Bitmap;
                }
                else if (c == 3)
                {
                    DreamImg4.Source = Bitmap;
                }
                c++;
            }
        }

        private void Input_Dream_Description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DreamAnalisys(250);
            }
        }
    }
}