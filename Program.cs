using PigLatinTranslator.Util;
using System;

namespace PigLatinTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateCharacterDel validateCharacterDel = TranslationUtil.ValidateCharacterDelProp;
            TranslateSentenceDel translateSentenceDel = TranslationUtil.TranslateSentenceDelProp;
            Console.WriteLine("Hello World!");
        }

        
    }
}