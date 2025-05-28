using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechRecognision;

namespace VoiceLibTest
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Recognizer recognizer;

            recognizer = new Recognizer(@"models\vosk-model-mod-ru-0.22", true,true);


            recognizer.RecognitionResultReceived += Recognizer_RecognitionResultReceived;
            recognizer.startRecording();

            void Recognizer_RecognitionResultReceived(object sender, string result)
            {
                Console.WriteLine("Распознанный текст: " + result);
                
            }

            Console.ReadLine();
        }

        
    }
}
