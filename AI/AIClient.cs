using Azure;
using Azure.AI.OpenAI;

public enum DEPLOYMENT_OPTION
{
    GPT, EMBED, DALL_E
};


namespace AIcounting.AI
{
    public static class AIClient
    {
        public const string GPT35 = "gpt-35-turbo";
        public const string EMBED = "text-embedding-ada-002";
        public const int MAX_TOLKIENS = 0x0800;

        //the client
        public static OpenAIClient? Client;

        //the options for gpt, embed, and dall-e
        public static ChatCompletionsOptions? GPT_Options;
        public static EmbeddingsOptions? Embed_Options;
        public static ImageGenerationOptions? DallE_Options;

        public static readonly Uri Proxy = new("https://aoai.hacktogether.net/v1/api");
        //hardcoded .. TODO: use env variable or user input.
        public static readonly AzureKeyCredential JRRTolkien = new("84e64307-0d6d-4c5a-a2db-ab278f6f599c/Katyusha512");


        public static void Init_GPT()
        {
            Client ??= new OpenAIClient(Proxy, JRRTolkien);
            GPT_Options = new ChatCompletionsOptions
            {
                MaxTokens = MAX_TOLKIENS,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                DeploymentName = GPT35
            };
        }

        public static void Init_Embedding(in List<string> _Input)
        {
            Client ??= new OpenAIClient(Proxy, JRRTolkien);
            EmbeddingsOptions embeddingsOptions = new()
            {
                DeploymentName = EMBED,
                Input = _Input,
            };
        }

        public static void Init_DallE(string _Input, in ImageSize _Size, int _ImageCount)
        {
            Client ??= new OpenAIClient(Proxy, JRRTolkien);
            DallE_Options = new ImageGenerationOptions
            {
                ImageCount = _ImageCount,
                Prompt = _Input,
                Size = _Size,
                User = "Ron Jambo"
            };
        }

        public static void SetChatSysMessage(string Msg, DEPLOYMENT_OPTION O)
        {
            //since I want only one sysmsg, we clear it before adding one
            GPT_Options?.Messages.Clear();
            switch (O)
            {
                case DEPLOYMENT_OPTION.GPT:
                    GPT_Options?.Messages.Add(new ChatMessage(ChatRole.System, Msg));
                    break;
                case DEPLOYMENT_OPTION.EMBED:
                case DEPLOYMENT_OPTION.DALL_E:
                    break;
                default:
                    break;
            }
        }

        public static void AddChatUsrMessage(string Msg, DEPLOYMENT_OPTION O)
        {
            switch (O)
            {
                case DEPLOYMENT_OPTION.GPT:
                    GPT_Options?.Messages.Add(new ChatMessage(ChatRole.User, Msg));
                    break;
                case DEPLOYMENT_OPTION.EMBED:
                case DEPLOYMENT_OPTION.DALL_E:
                    break;
                default:
                    break;
            }
        }

        public static void AddChatAssistMessage(string Msg, DEPLOYMENT_OPTION O)
        {
            switch (O)
            {
                case DEPLOYMENT_OPTION.GPT:
                    GPT_Options?.Messages.Add(new ChatMessage(ChatRole.Assistant, Msg));
                    break;
                case DEPLOYMENT_OPTION.EMBED:
                case DEPLOYMENT_OPTION.DALL_E:
                    break;
                default:
                    break;
            }
        }

        //NOTE: functions would be cool too. test out later if enough time?

        public static string[] PullGPTResponse()
        {
            if (Client != null && GPT_Options != null)
            {
                try
                {
                    var Response = Client.GetChatCompletions(GPT_Options);
                    string[] Result = new string[Response.Value.Choices.Count];

                    int Ptr = 0;
                    foreach (var Choice in Response.Value.Choices)
                    {
                        Result[Ptr] = Choice.Message.Content;
                        ++Ptr;
                    }
                    return (Result);
                }
                catch (Exception)
                {
                    return ((string[])null);
                }
            }
            return ((string[])null);
        }

        public static ReadOnlyMemory<float> PullEmbedResponse()
        {
            Response<Embeddings> R = Client?.GetEmbeddings(Embed_Options);

            if (R != null)
            {
                EmbeddingItem Item = R.Value.Data[0];
                ReadOnlyMemory<float> Embedding = Item.Embedding;
                return (Embedding);
            }
            return ((ReadOnlyMemory<float>)null);
        }

        public static ImageLocation[] PullDallEPic()
        {
            try
            {
                Response<ImageGenerations> GenImg = Client?
                    .GetImageGenerations(DallE_Options);

                if (GenImg != null)
                {
                    return (GenImg.Value.Data.ToArray());
                }
                return ((ImageLocation[])null);
            }
            catch (Exception e)
            {
                return ((ImageLocation[])null);
            }
        }
    }
}
