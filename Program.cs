using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosk;
using NAudio.Wave;

namespace VoiceLibTest
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Recognizer recognizer;

            recognizer = new Recognizer("C:\\Users\\proro\\Downloads\\vosk-model-mod-ru-0.22 (1)\\vosk-model-mod-ru-0.22");
            recognizer.RecognitionResultReceived += Recognizer_RecognitionResultReceived;

            void Recognizer_RecognitionResultReceived(object sender, string result)
            {
                Console.WriteLine("Распознанный текст: " + result);
            }

            Console.ReadLine();
        }

        
    }
}
