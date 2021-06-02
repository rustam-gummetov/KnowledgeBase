using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase
{
    [Serializable]
    public class Knowledge
    {
        public static List<Knowledge> Knowledges = new List<Knowledge>();

        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
