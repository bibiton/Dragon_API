using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    public class SubjectResult
    {
        public SubjectResult() {
            SubjectList = new List<SubjectModel>();
        }
        public List<SubjectModel> SubjectList;
    }

    public class SubjectModel
    {
        public string template { get; set; }
        public QuestionModel QuestionData { get; set; }
        public List<OptionModel> OptionData { get; set; }
    }

    public class QuestionModel
    {
        public string QuestionTopicContent { get; set; }
        public string QuestionTopicPhoto { get; set; }
        public string QuestionTopicVoicePath { get; set; }
    }

    public class OptionModel
    {
        public string PhotoPath { get; set; }
        public string OptionContent { get; set; }
        public bool isCorrect { get; set; }
    }
}
