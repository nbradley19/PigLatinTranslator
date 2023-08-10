using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PigLatinTranslator.Util
{
    public delegate bool ValidateCharacterDel(char character);
    public delegate string TranslateSentenceDel(string sentence);
    static class TranslationUtil
    {
        private static ValidateCharacterDel _validateCharacterDel;
        private static TranslateSentenceDel _translateSentenceDel;
        /**
         * This function determines if character is a valid letter based on its ASCII value, table linked below:
         * https://www.c-sharpcorner.com/article/get-string-ascii-value/
         */
        private static bool ValidateCharacter(char character)
        {
            int character_ascii = (int)character;
            if (character_ascii < 65 || (character_ascii > 90 && character_ascii < 97) || (character_ascii > 122 && character_ascii < 128) || (character_ascii > 154 && character_ascii < 160) || (character_ascii > 165 && character_ascii < 224))
            {
                return false;
            }
            return true;
        }
        public static ValidateCharacterDel ValidateCharacterDelProp
        {
            get
            {
                if( _validateCharacterDel == null)
                {
                    _validateCharacterDel = ValidateCharacter;
                }
                return _validateCharacterDel;
            }
        }
        public static TranslateSentenceDel TranslateSentenceDelProp
        {
            get
            {
                if(_translateSentenceDel == null)
                {
                    _translateSentenceDel = TranslateToPigLatin;
                }
                return _translateSentenceDel;
            }
        }
        private string 
        private static string TranslateToPigLatin(string sentence)
        {
            string[] words = sentence.Split(' ');
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            string translatedSentence = "";

            foreach (string word in words)
            {
                if (word.Length < 3)
                    continue;

                char firstLetter = '_', secondLetter = '_', lastCharacter = (word.Length >=1) ? word[word.Length - 1]: '_';
                string translatedWord = "";
                for(int i = 0; i < word.Length; i++)
                {
                    char character = word[i];
                    if (firstLetter == '_' && char.IsLetter(character))
                    {
                         firstLetter = character;
                    }
                    else if(secondLetter == '_' && char.IsLetter(character))
                    {
                        secondLetter = character;
                    } 
                }
                if (!vowels.Contains(firstLetter))
                {
                    if (vowels.Contains(secondLetter))
                        translatedWord = TranslationUtil.ValidateCharacterDelProp(lastCharacter) ?
                            word.Substring(1, word.Length - 3) + firstLetter + "ay" + lastCharacter : 
                            word.Substring(1) + firstLetter + "ay";
                }
                else if (!vowels.Contains(secondLetter))
                {
                    translatedWord = TranslationUtil.ValidateCharacterDelProp(lastCharacter) ?
                        word.Substring(2) + word.Substring(0, 2) + "ay" :
                        word.Substring(2, word.Length - 2) + word.Substring(0, 2) + "ay" + lastCharacter;
                }
                else
                {
                    translatedWord = TranslationUtil.ValidateCharacterDelProp(lastCharacter) ?
                        word + "way" :
                        word.Substring(0, word.Length - 1) + "way" + lastCharacter;
                }

                translatedSentence += (translatedSentence == "") ? translatedWord : " " + translatedWord;
            }

            return translatedSentence;
        }
    }
}
