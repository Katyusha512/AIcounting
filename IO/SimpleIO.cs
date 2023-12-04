using AIcounting.Accounting;
using Azure.AI.OpenAI;
using System.IO;
using System.Text;

namespace AIcounting.IO
{

    public static class SimpleIO
    {
        public static readonly string GPTLogPath = (Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData) + $"/GPTChatLog/");

        public static readonly string AccountingDBPath = (Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData) + $"/AccountingDB/");

        public const char CSV_DELIMITER = '¢';
        public const string FILE_NAME_ACCOUNTS = "Accounts.aicc";
        public const string FILE_NAME_COMPANYINFO = "Companies.aicc";


        public static AccountsChart ImportAccounts()
        {
            AccountsChart Result = new AccountsChart();

            string CurrentPath = Environment.CurrentDirectory + $"/../../../Kontenplan.csv";
            StreamReader Reader = (StreamReader)null;

            try
            {
                Reader = new StreamReader(CurrentPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (Result);
            }

            while (true)
            {
                string LineOfCoke;
                try
                {
                    LineOfCoke = Reader.ReadLine();
                    if (string.IsNullOrEmpty(LineOfCoke)) { break; }

                    string[] Chunks = LineOfCoke.Split(CSV_DELIMITER);

                    ACC_CLASS AccClass = ACC_CLASS.ILLEGAL;
                    //TODO: we calculate the account class from account no % 1000 and % 100
                    //but since we don't need that for the demo we leave it at "illegal"
                    string AccName = "";
                    int AccNo = -1;
                    for (int i = 0; i < Chunks.Length; i++)
                    {
                        int Buffer;
                        if (i == 0)
                        {
                            if (!Int32.TryParse(Chunks[i], out Buffer))
                            {
                                Console.WriteLine("Parsing error");
                                break;
                            }
                            AccNo = Buffer;
                        }
                        if (i == 1) { AccName = Chunks[i]; }
                    }
                    Account Konto = new(AccClass, AccName, AccNo);
                    Result.Accounts.Add(Konto);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Reader.Close();
                    return (Result);
                }
            }
            Reader.Close();
            return (Result);
        }


        public  static int AppendChatHistory(string Topic, List<ChatMessage> Conversation)
        {
            string FilePath = GPTLogPath + Topic + ".aicc";
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }

            StreamWriter Writer = new StreamWriter(FilePath, true, Encoding.UTF8);
            foreach (var Message in Conversation)
            {
                try
                {
                    Writer.WriteLine(Message.Content);
                }
                catch (Exception)
                {

                    Writer.Close();
                    return (-1);
                }
            }
            Writer.Close();
            return (0);
        }

        public static List<ChatMessage> ReadChatHistory(string Topic)
        {
            List<ChatMessage> ChatMessages = new List<ChatMessage>();
            string FilePath = GPTLogPath + Topic + ".aicc";
            if (!File.Exists(FilePath))
            {
                return ((List<ChatMessage>)null);
            }

            StreamReader Reader = new StreamReader(FilePath, Encoding.UTF8);
            string Line = string.Empty;
            try
            {
                while (!string.IsNullOrEmpty(Line = Reader.ReadLine()))
                {
                    //TODO:  Wir brauchen hier noch info betreffend der Rolle,
                    //-> Dies müssen wir im writer vorher entsprechend speichern. Machen wir 2 linien? oder ein prefix?

                    //ChatMessages.Add(new(ChatMessage(Line));
                }

            }
            catch (Exception)
            {
                Reader.Close();
                return ((List<ChatMessage>)null);
            }


            Reader.Close();
            return (ChatMessages);
        }


    }
}
