using Reposytory;
using Translator;
using System.Windows.Forms;
namespace Service
{
    public class Main
    {
        public DataBase dataBase;

        public Translator.Translator translate;

        public Main(DataBase _database = null, Translator.Translator _translator = null)
        {
            if (_database != null)
            {
                dataBase = _database;
            }
            else
            {
                dataBase = new DataBase();
            }
            if (_translator != null)
            {
                translate = _translator; 
            }
            else
            {
                translate = new Translator.Translator();
            }
            Set_Translator();
        }
        public List<Car> Custom_sort(List<Car> lst, string item)
        {
            List<Car> data = lst;
            int a;
            if(int.TryParse(lst[0][item], out a))
            {
                data.Sort((a, b) => Convert.ToUInt32(a[item]).CompareTo(Convert.ToUInt32(b[item])));
            }
            else
            {
                data.Sort((a, b) => a[item].CompareTo(b[item]));
            }
            return data;
        }
        public void Set_Translator()
        {
            translate.Add("is_babyseat", "Детское кресло");
            translate.Add("is_gps", "GPS");
            translate.Add("colour", "Цвет");
            translate.Add("True", "Да");
            translate.Add("False", "Нет");
            translate.Add("body_type", "Тип Кузова");
            translate.Add("Automatic", "Автомат");
            translate.Add("Manual", "Механическая");
            translate.Add("brand_name", "Бренд");
            translate.Add("liters_per_100", "Расход");
            translate.Add("price", "Цена");
            translate.Add("transmission_type", "Коробка");
        }
        public bool Is_string(string str)
        {
            int a;
            bool t;
            return !int.TryParse(str, out a) && !bool.TryParse(str.ToLower(), out t);
        }
        public string Get_Condition(Dictionary<string,List<string>> search)
        {
            if(search.Keys.Count == 0)
            {
                return "TRUE";
            }
            string condition_string = "";
            foreach (var column in search.Keys)
            {
                string condition_item = "";
                if (Is_string(search[column][0]))
                {
                    foreach (var condition in search[column])
                    {
                        condition_item += column + " = '" + condition + "' OR ";
                    }
                }
                else
                {
                    foreach (var condition in search[column])
                    {
                        condition_item += column + " = " + condition + " OR ";
                    }
                }
                condition_string += string.Concat("(", condition_item.AsSpan(0, condition_item.Length - 4), ") AND ");
            }
            condition_string = condition_string.Substring(0, condition_string.Length - 5);
            return condition_string;
        }
        public List<Car> Search_Cars(Dictionary<string, List<string>> _search, string _sort)
        {
            Dictionary<string, List<string>> search = translate.GetWord(_search);
            string sort = translate.GetWord(_sort);
            string condition_string = Get_Condition(search);
                        
            string query = "SELECT * FROM " +
                "\"Car\" JOIN \"Model\" ON \"Car\".model_id = \"Model\".id WHERE " + condition_string;

            List<Car> cars = new List<Car>();
            var table = dataBase.GetTable(query);

            foreach (var car in table)
            {
                cars.Add(new Car(translate.GetTranslate(car)));
            }
            if (sort != "")
            {
                cars = Custom_sort(cars, sort);
            }
            return (cars);
        }
        public Dictionary<string, List<string>> GetCategorySearch()
        {
            Dictionary<string, List<string>> category = new Dictionary<string, List<string>>();
            //получаем строки таблицы не содержащие "id и image"
            string query = "SELECT column_name FROM information_schema.columns WHERE" +
                " table_name = 'Car' AND column_name NOT LIKE '%id%' AND column_name NOT LIKE '%image%' " +
                "UNION ALL SELECT column_name FROM information_schema.columns WHERE table_name = 'Model'" +
                "  AND column_name NOT LIKE '%id%';";
            List<string> columns = new List<string>();
            foreach (Dictionary<string, string> row in dataBase.GetTable(query))
            {
                columns.Add(row["column_name"]);
            }
            //формируем список уникальных значений для каждой строки в виде словаря
            foreach(string column in columns)
            {
                string query1 = "SELECT DISTINCT " + column + 
                    " FROM \"Car\" INNER JOIN \"Model\" ON \"Car\".model_id = \"Model\".id";
                List<string> values = new List<string>();
                foreach(var value in dataBase.GetTable(query1))
                {
                    values.Add(translate.GetTranslate(value[column]));
                }
                category[translate.GetTranslate(column)] = values;
            }
            return category;
        }
        public List<string> GetCategorySort()
        {
            List<string> sorts = new List<string>();
            string query = "SELECT column_name FROM information_schema.columns WHERE " +
                "table_name = 'Car' AND data_type = 'integer' AND column_name NOT LIKE '%id%' " +
                "UNION ALL SELECT column_name FROM information_schema.columns WHERE " +
                "table_name = 'Model' AND data_type = 'integer' AND column_name NOT LIKE '%id%'";
            foreach (var column in dataBase.GetTable(query))
            {
                sorts.Add(translate.GetTranslate(column["column_name"]));
            }
            return sorts;
        }
       
    }
}
