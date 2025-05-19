using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceLibTest
{
    internal class Decoder
    {
        public static Dictionary<String, String> decoder = new Dictionary<string, string>
        {
            { "кот", "AorticValveMaxVelocity" },
            { "собака", "tbAorticValveMaxGradient" },
            { "сова", "tbAorticValveAvarageVelocity" }
        };
    }
}
