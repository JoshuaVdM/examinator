using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAnalyticsApi
{
    class Demo
    {
        public ITextAnalyticsAPI client { get; set; }
        public List<String> text { get; set; }

        public Demo()
        {
            client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westcentralus;
            client.SubscriptionKey = "8e539c9707c04e659d9085673f970c6b";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            text = readTextFile(@"C:\Users\Joske\Documents\TextAnalytics.txt").Split(new char[] {'.', '?', '!' }).ToList();
            text.RemoveAll(str => String.IsNullOrWhiteSpace(str));
        }

        public string readTextFile(string path)
        {
            return File.ReadAllText(path);
        }

        public BatchInput getBatchInput()
        {
            BatchInput bi = new BatchInput();
            bi.Documents = new List<Input>();
            int i = 1;
            foreach (string s in text)
            {
                bi.Documents.Add(new Input(i.ToString(), s));
                i++;
            }
            return bi;
        }
        public MultiLanguageBatchInput getMultiLanguageBatchInput()
        {
            LanguageBatchResult langResult = getLanguage();

            MultiLanguageBatchInput bi = new MultiLanguageBatchInput();
            bi.Documents = new List<MultiLanguageInput>();
            int i = 0;
            foreach (string s in text)
            {
                bi.Documents.Add(new MultiLanguageInput(langResult.Documents[i].DetectedLanguages[0].Iso6391Name, i.ToString(), s));
                i++;
            }
            return bi;
        }

        public LanguageBatchResult getLanguage()
        {
            // Get language
            return client.DetectLanguage(getBatchInput());
        }
        public void printLanguage(LanguageBatchResult result)
        {
            // Print languages
            Console.WriteLine("===== LANGUAGE EXTRACTION ======");
            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0} , Language: {1}", document.Id, document.DetectedLanguages[0].Name);
            }
        }

        public KeyPhraseBatchResult getKeyPhrases()
        {
            // Get keyphrases           
            KeyPhraseBatchResult keyPhraseBatchResult = client.KeyPhrases(getMultiLanguageBatchInput());   
            return keyPhraseBatchResult;
        }

        public void printKeyPhrases(KeyPhraseBatchResult result)
        {
            // Print keyphrases
            Console.WriteLine("\n\n===== KEY-PHRASE EXTRACTION ======");
            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0} ", document.Id);

                Console.WriteLine("\t Key phrases:");

                foreach (string keyphrase in document.KeyPhrases)
                {
                    Console.WriteLine("\t\t" + keyphrase);
                }
            }
        }

        public SentimentBatchResult getSentiment()
        {
            // Extracting sentiment
            SentimentBatchResult result = client.Sentiment(getMultiLanguageBatchInput());
            return result;
        }

        public void printSentiment(SentimentBatchResult result)
        {
            // Printing sentiment results
            Console.WriteLine("\n\n===== SENTIMENT ANALYSIS ======");
            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0} , Sentiment Score: {1:0.00}", document.Id, document.Score);
            }
            Console.ReadKey();
        }
    }
}
