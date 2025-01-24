using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service
{
    public class Car
    {
        private Dictionary<string, string> items = new Dictionary<string, string>();
        public List<string> columns;
        public string this[string key]
        {
            get
            {
                // Возвращаем значение по ключу или "N", если ключ не найден
                return items.TryGetValue(key, out string value) ? value : "NaN";
            }
            set
            {
                items[key] = value;
            }
        }

        public Dictionary<string, string> Get()
        {
            return items;
        }
        public Car(Dictionary<string, string> items)
        {
            this.items = items;
            this.columns = items.Keys.ToList();
        }

        public Car(List<string> my_columns, List<string> my_rows)
        {
            for(int i = 0; i < my_rows.Count; i++)
            {
                items[my_columns[i]] = my_rows[i];
            }
            columns = my_columns;
        }
    }
}
