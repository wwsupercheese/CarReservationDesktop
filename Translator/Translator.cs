using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class Translator
    {
        public Dictionary<string, string> translate_base;

        public Dictionary<string, string> uses_translate;
        public Translator()
        {
            translate_base = new Dictionary<string, string>();
            uses_translate = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            translate_base[key] = value;
            uses_translate[value] = key;
        }
        public virtual Dictionary<string, string> GetTranslate(Dictionary<string, string> my_words)
        {
            Dictionary<string, string> translates = new Dictionary<string, string>();
            foreach (string key in my_words.Keys)
            {
                translates[GetTranslate(key)] = GetTranslate(my_words[key]);
            }
            return translates;
        }
        public virtual string GetTranslate(string my_word)
        {
            if (translate_base.TryGetValue(my_word, out string translatedWord))
            {
                return translatedWord;
            }
            return my_word; // Возврат оригинального слова, если перевод не найден
        }
        public virtual Dictionary<string, List<string>> GetWord(Dictionary<string, List<string>> my_translate)
        {
            Dictionary<string, List<string>> words = new Dictionary<string, List<string>>();
            foreach (string key in my_translate.Keys)
            {
                List<string> lst_word = new List<string>();
                foreach (var word in my_translate[key])
                {
                    lst_word.Add(GetWord(word));
                }
                words[GetWord(key)] = lst_word;
            }
            return words;
        }
        public virtual Dictionary<string, string> GetWord(Dictionary<string, string> my_translate)
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            foreach (string key in my_translate.Keys)
            {
                words[GetTranslate(key)] = GetTranslate(my_translate[key]);
            }
            return words;
        }
        public virtual string GetWord(string my_translate)
        {
            if (uses_translate.TryGetValue(my_translate, out string originalWord))
            {
                return originalWord;
            }
            return my_translate; // Возврат оригинального перевода, если слово не найдено
        }
    }

}
