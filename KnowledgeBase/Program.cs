using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace KnowledgeBase
{
    class Program
    {
        static XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Knowledge>));
        static int knowledgesCount;

        static public void Serialization()
        {
            string path = @"C:\Users\777\source\repos\KnowledgeBase\KnowledgeBase\bin\Debug\knowledges.xml";
            FileInfo fileInf = new FileInfo(path);
            fileInf.Delete();
            using (var file = new FileStream("knowledges.xml", FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file, Knowledge.Knowledges);
            }
        }

        static public void Deserialization()
        {
            using (var file = new FileStream("knowledges.xml", FileMode.OpenOrCreate))
            {
                Knowledge.Knowledges = xmlFormatter.Deserialize(file) as List<Knowledge>;
            }
        }

        static public void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Показать базу знаний");
            Console.WriteLine("2. Добавить знание");
            Console.WriteLine("3. Удалить знание");
            Console.WriteLine("4. Выход");

            int command = Convert.ToInt32(Console.ReadLine());
            switch (command)
            {
                case 1:
                    {
                        ShowKnowledges();
                        Menu();
                        break;
                    }
                case 2:
                    {
                        CreateKnowledge();
                        Menu();
                        break;
                    }
                case 3:
                    {
                        DeleteKnowledge();
                        Menu();
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }

        }

        static public void ShowKnowledges()
        {
            if (Knowledge.Knowledges.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("База знаний пустая");
                Console.ReadLine();
                return;
            }               
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine("|{0,12} |{1,30} |{2,50} |", "id", "Вопрос", "Ответ");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            for (int i = 0; i < Knowledge.Knowledges.Count; i++)
            {
                Console.WriteLine("|{0,12} |{1,30} |{2,50} |", Knowledge.Knowledges[i].Id, Knowledge.Knowledges[i].Question, Knowledge.Knowledges[i].Answer);
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
            }
            Console.ReadLine();
        }


        static public void CreateKnowledge()
        {
            if (Knowledge.Knowledges.Count == 0)
                knowledgesCount = 1;
            else
                knowledgesCount = Knowledge.Knowledges[Knowledge.Knowledges.Count - 1].Id + 1;

            Console.Clear();
            Console.WriteLine("Введите вопрос:\n");
            string question = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("\nВведите ответ:\n");
            string answer = Console.ReadLine();


            Knowledge knowledge = new Knowledge()
            {
                Id = knowledgesCount,
                Question = question,
                Answer = answer,        
            };

            Knowledge.Knowledges.Add(knowledge);

            Console.Clear();
            Console.WriteLine("Знание добавлено");
        }

        static public void DeleteKnowledge()
        {
            Console.Clear();
            Console.WriteLine("Введите id:\n");
            int id = Convert.ToInt32(Console.ReadLine());
            bool found = false;
            for (int i = 0; i < Knowledge.Knowledges.Count; i++)
            {
                if (Knowledge.Knowledges[i].Id == id)
                {
                    found = true;
                    Knowledge.Knowledges.RemoveAt(i);
                    break;
                }
            }
            Console.Clear();
            if (found)
                Console.WriteLine("Знание удалено");
            else
                Console.WriteLine("Знаний с таким id нет");
            Console.ReadLine();
        }



        static void Main(string[] args)
        {
            Deserialization();
            Menu();
            Serialization();
        }
    }
}
