using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    [Serializable]
    public class InvalidMenuChoice : Exception
    {
        public InvalidMenuChoice()
        { }

        public InvalidMenuChoice(string message)
            : base(message)
        { }

        public InvalidMenuChoice(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}