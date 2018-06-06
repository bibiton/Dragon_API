using Dragon_Library.API;
using Dragon_Library.Filter;
using Dragon_Library.Models.Entity;
using Dragon_Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dragon_API.Controller
{
    /// <summary>
    /// 題目
    /// </summary>
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class SubjectsController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<Subject> subject_db = new GenericRepository<Subject>();
        #endregion

        /// <summary>
        /// 取得所有題目
        /// </summary>
        /// <returns></returns>
        public ResultModel<object> Get() {
            ResultModel<object> result = new ResultModel<object>();

            try
            {
                List<Subject> subjects = subject_db.GetAll();
                SubjectResult data = new SubjectResult();
                foreach (var item in subjects)
                {
                    data.SubjectList.Add(new SubjectModel() {
                        template = item.Template,
                        QuestionData = new QuestionModel() {
                            QuestionTopicContent = item.Subject_Name,
                            QuestionTopicPhoto = item.SubjectFile.Any(f => f.FileType == 1) ? item.SubjectFile.Where(f => f.FileType == 1).FirstOrDefault().FileName : null,
                            QuestionTopicVoicePath = item.SubjectFile.Any(f => f.FileType == 2) ? item.SubjectFile.Where(f => f.FileType == 2).FirstOrDefault().FileName : null,
                        },
                        OptionData = (from o in item.Options
                                     select new OptionModel {
                                         OptionContent = o.Option_Name,
                                         PhotoPath = o.FileName,
                                         isCorrect = o.IsAnswer
                                     }).ToList()
                    });
                }

                result.result_status = true;
                result.result_content = data;
            }
            catch (Exception ex)
            {
                result.result_status = false;
                result.result_message = ex.ToString();
            }

            return result;
        }
    }
}
